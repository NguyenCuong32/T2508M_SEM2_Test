from django import forms
from django.contrib.auth.models import User
from django.core.exceptions import ValidationError
from .models import SECURITY_QUESTIONS

class UserRegisterForm(forms.Form):
    username = forms.CharField(
        max_length=150,
        label="Username",
        widget=forms.TextInput(attrs={'class': 'form-input', 'placeholder': 'Create a username'})
    )
    password = forms.CharField(
        label="Password",
        widget=forms.PasswordInput(attrs={'class': 'form-input', 'placeholder': 'Enter a secure password'})
    )
    confirm_password = forms.CharField(
        label="Confirm Password",
        widget=forms.PasswordInput(attrs={'class': 'form-input', 'placeholder': 'Repeat your password'})
    )
    security_question = forms.ChoiceField(
        choices=SECURITY_QUESTIONS,
        label="Security Question",
        widget=forms.Select(attrs={'class': 'form-input'})
    )
    security_answer = forms.CharField(
        max_length=255,
        label="Security Answer",
        widget=forms.TextInput(attrs={'class': 'form-input', 'placeholder': 'Enter your answer (case-insensitive)'})
    )

    def clean_username(self):
        username = self.cleaned_data.get('username')
        if User.objects.filter(username=username).exists():
            raise ValidationError("This username is already taken. Please choose another one.")
        return username

    def clean(self):
        cleaned_data = super().clean()
        password = cleaned_data.get('password')
        confirm_password = cleaned_data.get('confirm_password')

        if password and confirm_password and password != confirm_password:
            self.add_error('confirm_password', "Passwords do not match.")

        if password:
            if len(password) < 8:
                self.add_error('password', "Password must be at least 8 characters long.")
            
            # Kiểm tra mật khẩu phải có ít nhất 1 chữ số
            if not any(char.isdigit() for char in password):
                self.add_error('password', "Password must contain at least one number (0-9).")

        return cleaned_data


class ForgotPasswordUsernameForm(forms.Form):
    username = forms.CharField(
        max_length=150,
        label="Username",
        widget=forms.TextInput(attrs={'class': 'form-input', 'placeholder': 'Enter your username'})
    )

    def clean_username(self):
        username = self.cleaned_data.get('username')
        if not User.objects.filter(username=username).exists():
            raise ValidationError("Account does not exist on our system.")
        return username


class ForgotPasswordResetForm(forms.Form):
    security_answer = forms.CharField(
        max_length=255,
        label="Security Answer",
        widget=forms.TextInput(attrs={'class': 'form-input', 'placeholder': 'Enter your security answer'})
    )
    new_password = forms.CharField(
        label="New Password",
        widget=forms.PasswordInput(attrs={'class': 'form-input', 'placeholder': 'Enter new password'})
    )
    confirm_new_password = forms.CharField(
        label="Confirm New Password",
        widget=forms.PasswordInput(attrs={'class': 'form-input', 'placeholder': 'Confirm new password'})
    )

    def clean(self):
        cleaned_data = super().clean()
        new_password = cleaned_data.get('new_password')
        confirm_new_password = cleaned_data.get('confirm_new_password')

        if new_password and confirm_new_password and new_password != confirm_new_password:
            self.add_error('confirm_new_password', "Passwords do not match.")

        if new_password:
            if len(new_password) < 8:
                self.add_error('new_password', "Password must be at least 8 characters long.")
            if not any(char.isdigit() for char in new_password):
                self.add_error('new_password', "Password must contain at least one number (0-9).")

        return cleaned_data
