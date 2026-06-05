from django import forms
from .models import Book

class BookForm(forms.ModelForm):
    class Meta:
        model = Book
        fields = ['title', 'author', 'price']
        widgets = {
            'title': forms.TextInput(attrs={'placeholder': 'Enter book title...'}),
            'author': forms.TextInput(attrs={'placeholder': 'Enter author name...'}),
            'price': forms.NumberInput(attrs={'placeholder': '0.00', 'step': '0.01'}),
        }
