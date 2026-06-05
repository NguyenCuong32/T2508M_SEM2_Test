from django import forms
from .models import Book

class BookForm(forms.ModelForm):
    class Meta:
        model = Book
        fields = ['title', 'author', 'price']
        widgets = {
            'title': forms.TextInput(attrs={'class': 'form-control', 'placeholder': 'Nhập tiêu đề sách...'}),
            'author': forms.TextInput(attrs={'class': 'form-control', 'placeholder': 'Nhập tên tác giả...'}),
            'price': forms.NumberInput(attrs={'class': 'form-control', 'placeholder': 'Nhập giá tiền...', 'step': '0.01'}),
        }
