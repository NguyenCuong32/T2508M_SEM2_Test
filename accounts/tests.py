from django.test import TestCase
from django.urls import reverse
from django.contrib.auth.models import User

class AccountsAuthenticationTest(TestCase):
    def setUp(self):
        self.username = "testuser"
        self.password = "secure_pass_123"
        self.user = User.objects.create_user(
            username=self.username,
            password=self.password
        )

    def test_login_view_get(self):
        response = self.client.get(reverse('login'))
        self.assertEqual(response.status_code, 200)
        self.assertTemplateUsed(response, 'accounts/login.html')

    def test_login_view_post_success(self):
        response = self.client.post(reverse('login'), data={
            'username': self.username,
            'password': self.password
        })
        self.assertRedirects(response, reverse('dashboard'))
        # Check if the user is authenticated in the session
        self.assertIn('_auth_user_id', self.client.session)

    def test_login_view_post_invalid(self):
        response = self.client.post(reverse('login'), data={
            'username': self.username,
            'password': 'wrongpassword'
        })
        self.assertEqual(response.status_code, 200)
        self.assertTemplateUsed(response, 'accounts/login.html')
        self.assertEqual(response.context['error'], 'Invalid username or password.')
        self.assertNotIn('_auth_user_id', self.client.session)

    def test_register_view_get(self):
        response = self.client.get(reverse('register'))
        self.assertEqual(response.status_code, 200)
        self.assertTemplateUsed(response, 'accounts/register.html')

    def test_register_view_post_success(self):
        # We need data that passes UserCreationForm rules
        response = self.client.post(reverse('register'), data={
            'username': 'newuser123',
            # UserCreationForm checks password strength in Django, 
            # let's use a long password to pass validators
            'password1': 'strong_new_pass_123', 
            'password2': 'strong_new_pass_123'
        })
        
        # It should redirect to login upon success
        self.assertRedirects(response, reverse('login'))
        self.assertTrue(User.objects.filter(username='newuser123').exists())

    def test_dashboard_view_anonymous(self):
        # Accessing dashboard when not logged in should redirect to login
        response = self.client.get(reverse('dashboard'))
        # It redirects to settings.LOGIN_URL which is '/accounts/login/?next=/accounts/dashboard/'
        expected_redirect = f"{reverse('login')}?next={reverse('dashboard')}"
        self.assertRedirects(response, expected_redirect)

    def test_dashboard_view_authenticated(self):
        # Log in first
        self.client.login(username=self.username, password=self.password)
        response = self.client.get(reverse('dashboard'))
        self.assertEqual(response.status_code, 200)
        self.assertTemplateUsed(response, 'accounts/dashboard.html')
        # Check context
        self.assertEqual(response.context['user'].username, self.username)

    def test_logout_view(self):
        self.client.login(username=self.username, password=self.password)
        response = self.client.get(reverse('logout'))
        # Should redirect to login
        self.assertRedirects(response, reverse('login'))
        # Check session is cleared of auth user id
        self.assertNotIn('_auth_user_id', self.client.session)

