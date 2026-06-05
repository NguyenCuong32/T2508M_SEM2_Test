from django.test import TestCase, Client
from django.urls import reverse
from django.contrib.auth.models import User

class AccountsAuthenticationTest(TestCase):
    def setUp(self):
        self.client = Client()
        self.username = "testuser"
        self.password = "Secr3tP@ss!"
        self.user = User.objects.create_user(
            username=self.username,
            password=self.password
        )

    def test_login_view_get(self):
        response = self.client.get(reverse('login'))
        self.assertEqual(response.status_code, 200)
        self.assertTemplateUsed(response, 'accounts/login.html')

    def test_login_view_post_valid(self):
        post_data = {
            'username': self.username,
            'password': self.password
        }
        response = self.client.post(reverse('login'), post_data)
        self.assertRedirects(response, reverse('dashboard'))

    def test_login_view_post_invalid(self):
        post_data = {
            'username': self.username,
            'password': 'wrongpassword'
        }
        response = self.client.post(reverse('login'), post_data)
        self.assertEqual(response.status_code, 200)
        # Should display error message
        self.assertContains(response, "Invalid username or password.")

    def test_logout_view(self):
        # First log the user in
        self.client.login(username=self.username, password=self.password)
        # Call logout
        response = self.client.get(reverse('logout'))
        # Should redirect back to the login page
        self.assertRedirects(response, reverse('login'))

    def test_dashboard_unauthenticated_redirect(self):
        # Try accessing dashboard when logged out
        response = self.client.get(reverse('dashboard'))
        # Should redirect to the login page with Next parameter
        self.assertRedirects(response, f"{reverse('login')}?next={reverse('dashboard')}")

    def test_dashboard_authenticated_success(self):
        # Log the user in
        self.client.login(username=self.username, password=self.password)
        response = self.client.get(reverse('dashboard'))
        self.assertEqual(response.status_code, 200)
        self.assertTemplateUsed(response, 'accounts/dashboard.html')
        self.assertContains(response, f"Welcome, <span class=\"gradient-text\" id=\"welcome-username\">{self.username}</span>")

    def test_registration_view_post(self):
        post_data = {
            'username': 'newuser123',
            'password': 'SomeStrongPassword!123',
            'password_confirm': 'SomeStrongPassword!123'
        }
        # In Django's UserCreationForm, fields are typically username, password1, password2
        # Let's make sure our POST handles exactly what UserCreationForm expects:
        # UserCreationForm expects 'username', 'password1', and 'password2'
        post_data = {
            'username': 'newuser123',
            'password1': 'SomeStrongPassword!123',
            'password2': 'SomeStrongPassword!123'
        }
        response = self.client.post(reverse('register'), post_data)
        self.assertRedirects(response, reverse('dashboard'))
        self.assertTrue(User.objects.filter(username='newuser123').exists())
