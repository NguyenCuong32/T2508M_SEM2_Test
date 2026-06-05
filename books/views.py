from django.shortcuts import render, redirect
from django.contrib import messages
from .models import Book
from .forms import BookForm

def book_list(request):
    if request.method == 'POST':
        form = BookForm(request.POST)
        if form.is_valid():
            form.save()
            messages.success(request, 'Book added successfully!')
            return redirect('book_list')
        else:
            messages.error(request, 'Error adding book. Please check the details.')
    else:
        form = BookForm()
    
    books = Book.objects.all().order_by('-created_at')
    total_books = books.count()
    
    context = {
        'books': books,
        'total_books': total_books,
        'form': form,
    }
    return render(request, 'books/book_list.html', context)

def expensive_books(request):
    books = Book.objects.filter(price__gt=100).order_by('-created_at')
    total_expensive = books.count()
    
    context = {
        'books': books,
        'total_expensive': total_expensive,
    }
    return render(request, 'books/expensive_books.html', context)
