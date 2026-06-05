from django.shortcuts import render, redirect
from django.contrib import messages
from .models import Book
from .forms import BookForm

def book_list(request):
    if request.method == 'POST':
        if not request.user.is_authenticated:
            messages.error(request, "You must be logged in to add a new book.")
            return redirect('accounts:login')
            
        form = BookForm(request.POST, request.FILES)
        if form.is_valid():
            form.save()
            messages.success(request, "Book added successfully!")
            return redirect('books:book_list')
        else:
            messages.error(request, "Failed to add book. Please check the inputs.")
    else:
        form = BookForm()

    # Xử lý tham số sắp xếp từ query string
    sort_param = request.GET.get('sort', 'default')
    if sort_param == 'alphabetical':
        books = Book.objects.all().order_by('title_en')
    elif sort_param == 'price_asc':
        books = Book.objects.all().order_by('price_usd')
    elif sort_param == 'price_desc':
        books = Book.objects.all().order_by('-price_usd')
    else:
        books = Book.objects.all().order_by('-created_at')

    total_books = Book.objects.count()
    above_100_count = Book.objects.filter(price_usd__gt=100).count()
    above_100k_vnd_count = Book.objects.filter(price_vnd__gt=100000).count()

    context = {
        'form': form,
        'books': books,
        'total_books': total_books,
        'above_100_count': above_100_count,
        'above_100k_vnd_count': above_100k_vnd_count,
        'sort_param': sort_param,
    }
    return render(request, 'books/index.html', context)
