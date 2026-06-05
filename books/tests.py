from django.test import TestCase, Client
from django.urls import reverse
from .models import Book

class BookModelTest(TestCase):
    def test_book_creation(self):
        book = Book.objects.create(
            title="Clean Code",
            author="Robert C. Martin",
            price=35.50
        )
        self.assertEqual(book.title, "Clean Code")
        self.assertEqual(book.author, "Robert C. Martin")
        self.assertEqual(book.price, 35.50)
        self.assertEqual(str(book), "Clean Code")

class BookViewsTest(TestCase):
    def setUp(self):
        self.client = Client()
        self.book1 = Book.objects.create(
            title="Cheap Book",
            author="Author A",
            price=20.00
        )
        self.book2 = Book.objects.create(
            title="Expensive Book",
            author="Author B",
            price=150.00
        )

    def test_book_list_view_get(self):
        response = self.client.get(reverse('book_list'))
        self.assertEqual(response.status_code, 200)
        self.assertContains(response, "Cheap Book")
        self.assertContains(response, "Expensive Book")
        self.assertContains(response, "Total Books")
        self.assertEqual(response.context['total_books'], 2)

    def test_book_list_view_post_valid(self):
        post_data = {
            'title': 'New Test Book',
            'author': 'Tester',
            'price': 45.00
        }
        response = self.client.post(reverse('book_list'), post_data)
        # Should redirect back to the book list page to prevent resubmission
        self.assertRedirects(response, reverse('book_list'))
        self.assertEqual(Book.objects.count(), 3)
        self.assertTrue(Book.objects.filter(title='New Test Book').exists())

    def test_expensive_books_view(self):
        response = self.client.get(reverse('expensive_books'))
        self.assertEqual(response.status_code, 200)
        self.assertContains(response, "Expensive Book")
        self.assertNotContains(response, "Cheap Book")
        self.assertEqual(response.context['total_expensive'], 1)
        # Check title
        self.assertContains(response, "Premium Catalog")
