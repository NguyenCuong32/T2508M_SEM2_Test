from django.shortcuts import render, redirect
from .models import Book
from .forms import BookForm
from django.contrib.auth.decorators import login_required
from django.contrib.auth import login as auth_login, logout as auth_logout
from django.contrib.auth.forms import AuthenticationForm

def login_view(request):
    if request.user.is_authenticated:
        return redirect('dashboard')
    if request.method == 'POST':
        form = AuthenticationForm(request, data=request.POST)
        if form.is_valid():
            user = form.get_user()
            auth_login(request, user)
            return redirect('dashboard')
    else:
        form = AuthenticationForm()
    return render(request, 'books/login.html', {'form': form})

def logout_view(request):
    auth_logout(request)
    return redirect('login')

@login_required
def dashboard(request):
    return render(request, 'books/dashboard.html')

def book_list(request):
    books = Book.objects.all()
    expensive_books = Book.objects.filter(price__gt=100)
    total_books = Book.objects.count()

    return render(request, 'books/book_list.html', {
        'books': books,
        'expensive_books': expensive_books,
        'total_books': total_books
    })

def add_book(request):
    if request.method == 'POST':
        form = BookForm(request.POST)

        if form.is_valid():
            form.save()
            return redirect('/')

    else:
        form = BookForm()

    return render(request, 'books/add_book.html', {
        'form': form
    })