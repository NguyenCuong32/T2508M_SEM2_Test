from django.shortcuts import render, redirect
from .models import Book
from .forms import BookForm
from django.contrib.auth.decorators import login_required
@login_required
def book_dashboard(request):
    # Thêm sách mới
    if request.method == 'POST':
        form = BookForm(request.POST)
        if form.is_valid():
            form.save()
            return redirect('book_dashboard')
    else:
        form = BookForm()
         #  Lấy tất cả sách
    all_books = Book.objects.all().order_by('-created_at')

    #  Lọc sách có giá > 100
    expensive_books = Book.objects.filter(price__gt=100)

    # Tính tổng số lượng sách
    total_books = Book.objects.count()
    context = {
        'form': form,
        'all_books': all_books,
        'expensive_books': expensive_books,
        'total_books': total_books,
    }
    return render(request, 'books/dashboard.html', context)