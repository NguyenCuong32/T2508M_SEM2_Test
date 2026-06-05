from django.test import TestCase, Client
from django.urls import reverse
from django.contrib.auth.models import User
from books.models import Book

class BookstoreTestCase(TestCase):
    def setUp(self):
        # Create a test user
        self.user = User.objects.create_user(username='testuser', password='testpassword123')
        # Create some test books
        self.book1 = Book.objects.create(title='Book A', author='Author A', price=50.0)
        self.book2 = Book.objects.create(title='Book B', author='Author B', price=150.0)
        self.client = Client()

    def test_book_model(self):
        self.assertEqual(str(self.book1), 'Book A')
        self.assertEqual(self.book1.price, 50.0)

    def test_book_list_view(self):
        response = self.client.get(reverse('book_list'))
        self.assertEqual(response.status_code, 200)
        self.assertContains(response, 'Book A')
        self.assertContains(response, 'Book B')
        self.assertContains(response, 'Total Books:')
        self.assertContains(response, 'Premium Books (Price > $100)')
        self.assertContains(response, 'Book B')

    def test_add_book_view(self):
        # GET request should return the form
        response = self.client.get(reverse('add_book'))
        self.assertEqual(response.status_code, 200)
        
        # POST request should add a book
        response = self.client.post(reverse('add_book'), {
            'title': 'Book C',
            'author': 'Author C',
            'price': 120.0
        })
        self.assertEqual(response.status_code, 302) # Redirects
        self.assertEqual(Book.objects.count(), 3)
        self.assertTrue(Book.objects.filter(title='Book C').exists())

    def test_dashboard_restricted_to_anonymous(self):
        # Anonymous access to dashboard should redirect to login
        response = self.client.get(reverse('dashboard'))
        self.assertEqual(response.status_code, 302)
        self.assertRedirects(response, '/login/?next=/dashboard/')

    def test_dashboard_accessible_to_logged_in(self):
        # Log in the user
        self.client.login(username='testuser', password='testpassword123')
        response = self.client.get(reverse('dashboard'))
        self.assertEqual(response.status_code, 200)
        # Dashboard should display welcome message with username
        self.assertContains(response, 'Welcome, testuser!')

    def test_login_flow(self):
        # GET login
        response = self.client.get(reverse('login'))
        self.assertEqual(response.status_code, 200)
        
        # POST login with valid credentials
        response = self.client.post(reverse('login'), {
            'username': 'testuser',
            'password': 'testpassword123'
        })
        self.assertRedirects(response, reverse('dashboard'))

        # Logout before testing invalid login
        self.client.logout()

        # POST login with invalid credentials
        response = self.client.post(reverse('login'), {
            'username': 'testuser',
            'password': 'wrongpassword'
        })
        self.assertEqual(response.status_code, 200)
        self.assertContains(response, 'Invalid username or password')

    def test_logout_flow(self):
        # Log in first
        self.client.login(username='testuser', password='testpassword123')
        
        # Logout
        response = self.client.get(reverse('logout'))
        self.assertRedirects(response, reverse('login'))
        
        # Accessing dashboard after logout should redirect to login
        response = self.client.get(reverse('dashboard'))
        self.assertEqual(response.status_code, 302)
