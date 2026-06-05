from django import forms
from .models import Book

class BookForm(forms.ModelForm):
    class Meta:
        model = Book
        fields = ['title', 'author', 'price']
        labels = {
            'title': 'Tiêu đề',
            'author': 'Tác giả',
            'price': 'Giá',
        }
        widgets = {
            'title': forms.TextInput(attrs={'placeholder': 'Nhập tiêu đề sách'}),
            'author': forms.TextInput(attrs={'placeholder': 'Nhập tên tác giả'}),
            'price': forms.NumberInput(attrs={'placeholder': 'Nhập giá sách', 'step': '0.01'}),
        }
