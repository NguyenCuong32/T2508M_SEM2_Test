# book_auth/views.py
from django.shortcuts import render, redirect
from django.contrib.auth import login, logout, authenticate
from django.contrib.auth.forms import AuthenticationForm
from django.contrib.auth.decorators import login_required
from .models import Book

# ==================== BÀI 1: QUẢN LÝ BOOK ====================

def book_list(request):
    all_books = Book.objects.all()
    expensive_books = Book.objects.filter(price__gt=100) # Lọc sách giá > 100
    total_books = all_books.count() # Đếm tổng số sách
    
    context = {
        'all_books': all_books,
        'expensive_books': expensive_books,
        'total_books': total_books
    }
    return render(request, 'books/book_list.html', context)

def add_book(request):
    if request.method == 'POST':
        title = request.POST.get('title')
        author = request.POST.get('author')
        price = request.POST.get('price')
        
        if title and author and price:
            Book.objects.create(title=title, author=author, price=price)
            return redirect('book_list')
            
    return render(request, 'books/add_book.html')


# ==================== BÀI 2: ĐĂNG NHẬP / ĐĂNG XUẤT ====================

def login_view(request):
    if request.user.is_authenticated:
        return redirect('dashboard')
        
    error_message = None
    if request.method == 'POST':
        form = AuthenticationForm(data=request.POST)
        if form.is_valid():
            user = form.get_user()
            login(request, user)
            return redirect('dashboard')
        else:
            error_message = "Tên đăng nhập hoặc mật khẩu không đúng!"
    else:
        form = AuthenticationForm()
        
    return render(request, 'auth/login.html', {'form': form, 'error_message': error_message})

def logout_view(request):
    logout(request)
    return redirect('login')

@login_required
def dashboard_view(request):
    # Trang dashboard hiển thị thông báo chào mừng user sau khi login đúng yêu cầu đề bài
    return render(request, 'auth/dashboard.html')