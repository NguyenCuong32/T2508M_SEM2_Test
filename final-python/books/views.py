from django.shortcuts import render, redirect
from .models import Book
from .forms import BookForm

def add_book(request):
    if request.method == "POST":
        form = BookForm(request.POST)
        if form.is_valid():
            form.save()
            return redirect('book_list')
    else:
        form = BookForm()
    return render(request, 'books/add_book.html', {'form': form})

def book_list(request):
    books = Book.objects.all()
    return render(request, 'books/book_list.html', {'books': books, 'title': 'All Books'})

def expensive_books(request):
    books = Book.objects.filter(price__gt=100)
    return render(request, 'books/book_list.html', {'books': books, 'title': 'Expensive Books (>100)'})

def total_books(request):
    count = Book.objects.count()
    return render(request, 'books/total_books.html', {'count': count})
