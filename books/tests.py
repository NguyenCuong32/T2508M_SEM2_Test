from decimal import Decimal
from django.test import TestCase
from django.urls import reverse
from django.contrib.auth.models import User
from .models import Book
from .forms import BookForm

class BookModelTest(TestCase):
    def test_book_creation(self):
        book = Book.objects.create(
            title="Clean Code",
            author="Robert C. Martin",
            price=120.00
        )
        self.assertEqual(book.title, "Clean Code")
        self.assertEqual(book.author, "Robert C. Martin")
        self.assertEqual(book.price, 120.00)
        self.assertIsNotNone(book.created_at)
        self.assertEqual(str(book), "Clean Code")

class BookFormTest(TestCase):
    def test_valid_book_form(self):
        form_data = {
            'title': 'The Pragmatic Programmer',
            'author': 'Andy Hunt',
            'price': 99.99
        }
        form = BookForm(data=form_data)
        self.assertTrue(form.is_valid())

    def test_invalid_book_form_missing_fields(self):
        form_data = {
            'title': '',
            'author': 'Andy Hunt',
            'price': 99.99
        }
        form = BookForm(data=form_data)
        self.assertFalse(form.is_valid())
        self.assertIn('title', form.errors)

class BookViewsTest(TestCase):
    def setUp(self):
        self.book_cheap = Book.objects.create(
            title="Intro to Python",
            author="Guido van Rossum",
            price=45.00
        )
        self.book_expensive = Book.objects.create(
            title="Advanced Django Design Patterns",
            author="Two Scoops Press",
            price=150.00
        )
        # Create users
        self.admin_password = "adminpassword123"
        self.admin_user = User.objects.create_user(
            username="adminuser",
            password=self.admin_password,
            is_staff=True
        )
        self.regular_password = "userpassword123"
        self.regular_user = User.objects.create_user(
            username="regularuser",
            password=self.regular_password,
            is_staff=False
        )

    def test_book_list_view_get(self):
        response = self.client.get(reverse('book_list'))
        self.assertEqual(response.status_code, 200)
        self.assertTemplateUsed(response, 'books/book_list.html')
        
        # Check context variables
        self.assertEqual(response.context['total_books'], 2)
        
        # Check all books list contains both
        all_books = list(response.context['all_books'])
        self.assertIn(self.book_cheap, all_books)
        self.assertIn(self.book_expensive, all_books)
        
        # Check expensive books contains only the one > 100
        expensive_books = list(response.context['expensive_books'])
        self.assertNotIn(self.book_cheap, expensive_books)
        self.assertIn(self.book_expensive, expensive_books)
        
        # Check form in context
        self.assertIsInstance(response.context['form'], BookForm)

    def test_anonymous_user_cannot_add_book(self):
        form_data = {
            'title': 'Domain-Driven Design',
            'author': 'Eric Evans',
            'price': 115.50
        }
        response = self.client.post(reverse('book_list'), data=form_data)
        self.assertEqual(response.status_code, 403)
        self.assertEqual(Book.objects.count(), 2)

    def test_regular_user_cannot_add_book(self):
        self.client.login(username="regularuser", password=self.regular_password)
        form_data = {
            'title': 'Domain-Driven Design',
            'author': 'Eric Evans',
            'price': 115.50
        }
        response = self.client.post(reverse('book_list'), data=form_data)
        self.assertEqual(response.status_code, 403)
        self.assertEqual(Book.objects.count(), 2)

    def test_admin_user_can_add_book_success(self):
        self.client.login(username="adminuser", password=self.admin_password)
        form_data = {
            'title': 'Domain-Driven Design',
            'author': 'Eric Evans',
            'price': 115.50
        }
        response = self.client.post(reverse('book_list'), data=form_data)
        self.assertRedirects(response, reverse('book_list'))
        self.assertEqual(Book.objects.count(), 3)
        self.assertTrue(Book.objects.filter(title='Domain-Driven Design').exists())

    def test_admin_user_add_book_invalid(self):
        self.client.login(username="adminuser", password=self.admin_password)
        form_data = {
            'title': '',
            'author': 'Eric Evans',
            'price': -5.00
        }
        response = self.client.post(reverse('book_list'), data=form_data)
        self.assertEqual(response.status_code, 200)
        self.assertTemplateUsed(response, 'books/book_list.html')
        self.assertEqual(Book.objects.count(), 2)

    def test_anonymous_user_cannot_edit_book(self):
        edit_url = reverse('book_edit', args=[self.book_cheap.pk])
        response = self.client.get(edit_url)
        self.assertRedirects(response, f"/accounts/login/?next={edit_url}")

    def test_regular_user_cannot_edit_book(self):
        self.client.login(username="regularuser", password=self.regular_password)
        response = self.client.get(reverse('book_edit', args=[self.book_cheap.pk]))
        self.assertEqual(response.status_code, 403)
        
        response = self.client.post(reverse('book_edit', args=[self.book_cheap.pk]), data={
            'title': 'New Title',
            'author': 'New Author',
            'price': 50.00
        })
        self.assertEqual(response.status_code, 403)
        self.book_cheap.refresh_from_db()
        self.assertEqual(self.book_cheap.title, "Intro to Python")

    def test_admin_user_can_edit_book(self):
        self.client.login(username="adminuser", password=self.admin_password)
        response = self.client.get(reverse('book_edit', args=[self.book_cheap.pk]))
        self.assertEqual(response.status_code, 200)
        self.assertTemplateUsed(response, 'books/book_edit.html')
        
        response = self.client.post(reverse('book_edit', args=[self.book_cheap.pk]), data={
            'title': 'Intro to Python (Second Edition)',
            'author': 'Guido van Rossum',
            'price': 49.99
        })
        self.assertRedirects(response, reverse('book_list'))
        self.book_cheap.refresh_from_db()
        self.assertEqual(self.book_cheap.title, "Intro to Python (Second Edition)")
        self.assertEqual(self.book_cheap.price, Decimal('49.99'))

    def test_anonymous_user_cannot_delete_book(self):
        delete_url = reverse('book_delete', args=[self.book_cheap.pk])
        response = self.client.post(delete_url)
        self.assertRedirects(response, f"/accounts/login/?next={delete_url}")
        self.assertEqual(Book.objects.count(), 2)

    def test_regular_user_cannot_delete_book(self):
        self.client.login(username="regularuser", password=self.regular_password)
        response = self.client.post(reverse('book_delete', args=[self.book_cheap.pk]))
        self.assertEqual(response.status_code, 403)
        self.assertEqual(Book.objects.count(), 2)

    def test_admin_user_can_delete_book(self):
        self.client.login(username="adminuser", password=self.admin_password)
        response = self.client.post(reverse('book_delete', args=[self.book_cheap.pk]))
        self.assertRedirects(response, reverse('book_list'))
        self.assertEqual(Book.objects.count(), 1)
        self.assertFalse(Book.objects.filter(pk=self.book_cheap.pk).exists())


