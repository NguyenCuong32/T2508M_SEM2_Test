from django.shortcuts import render, redirect
from django.contrib import messages
from .models import Book
from .forms import BookForm

def book_list(request):
    if request.method == 'POST':
        form = BookForm(request.POST)
        if form.is_valid():
            form.save()
            messages.success(request, 'Thêm sách mới thành công!')
            return redirect('book_list')
    else:
        form = BookForm()

    all_books = Book.objects.all()
    expensive_books = Book.objects.filter(price__gt=100)
    total_books = Book.objects.count()

    # Get active filter from GET parameter (default is 'all')
    active_filter = request.GET.get('filter', 'all')
    if active_filter == 'expensive':
        display_books = expensive_books
    else:
        display_books = all_books

    context = {
        'form': form,
        'books': display_books,
        'total_books': total_books,
        'expensive_count': expensive_books.count(),
        'active_filter': active_filter,
    }
    return render(request, 'books/book_list.html', context)
