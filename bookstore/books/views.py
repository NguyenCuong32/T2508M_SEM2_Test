from django.shortcuts import render, redirect
from .models import Book
from .forms import BookForm

def book_list(request):
    books = Book.objects.all()
    total_books = books.count()
    expensive_books = Book.objects.filter(price__gt=100)
    
    context = {
        'books': books,
        'total_books': total_books,
        'expensive_books': expensive_books
    }
    return render(request, 'books/book_list.html', context)

def add_book(request):
    if request.method == 'POST':
        form = BookForm(request.POST)
        if form.is_valid():
            form.save()
            return redirect('book_list')
    else:
        form = BookForm()
    
    return render(request, 'books/add_book.html', {'form': form})
