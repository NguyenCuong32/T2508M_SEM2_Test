from django import forms
from .models import Book

class BookForm(forms.ModelForm):
    class Meta:
        model = Book
        fields = ['title_en', 'title_vi', 'author', 'price_usd', 'price_vnd', 'cover_image', 'created_at']
        widgets = {
            'title_en': forms.TextInput(attrs={'class': 'form-input', 'placeholder': 'Enter English title'}),
            'title_vi': forms.TextInput(attrs={'class': 'form-input', 'placeholder': 'Nhập tên sách tiếng Việt'}),
            'author': forms.TextInput(attrs={'class': 'form-input', 'placeholder': 'Enter author name'}),
            'price_usd': forms.NumberInput(attrs={'class': 'form-input', 'placeholder': 'Enter price in USD (e.g. 12.99)', 'step': '0.01'}),
            'price_vnd': forms.NumberInput(attrs={'class': 'form-input', 'placeholder': 'Nhập giá bằng VNĐ (ví dụ: 325000)', 'step': '1'}),
            'cover_image': forms.FileInput(attrs={'class': 'form-input', 'accept': 'image/*'}),
            'created_at': forms.DateInput(attrs={'class': 'form-input', 'type': 'date'}),
        }
        labels = {
            'title_en': 'English Title',
            'title_vi': 'Tên sách (Tiếng Việt)',
            'author': 'Author / Tác giả',
            'price_usd': 'Price in USD',
            'price_vnd': 'Giá VNĐ',
            'cover_image': 'Cover Image / Ảnh bìa',
            'created_at': 'Publication Date / Ngày xuất bản',
        }
