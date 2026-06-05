from django.shortcuts import render, redirect
from .models import Book
from django.contrib.auth import authenticate, login, logout
from django.contrib.auth.decorators import login_required


def add_book(request):
    if request.method == "POST":

        title = request.POST.get("title")
        author = request.POST.get("author")
        price = request.POST.get("price")

        Book.objects.create(
            title=title,
            author=author,
            price=price
        )

        return redirect("book_list")

    return render(request, "add_book.html")


def book_list(request):

    tab = request.GET.get("tab", "ex1")

    books = Book.objects.all()
    expensive_books = Book.objects.filter(price__gt=100)
    total_books = Book.objects.count()

    context = {
        "books": books,
        "expensive_books": expensive_books,
        "total_books": total_books,
        "tab": tab
    }

    return render(request, "book_list.html", context)

def login_view(request):

    if request.method == "POST":

        username = request.POST.get("username")
        password = request.POST.get("password")

        user = authenticate(
            request,
            username=username,
            password=password
        )

        if user:
            login(request, user)
            return redirect("dashboard")

        return render(request, "login.html", {
            "error": "Invalid username or password"
        })

    return render(request, "login.html")


def logout_view(request):
    logout(request)
    return redirect("login")


@login_required(login_url='login')
def dashboard(request):

    tab = request.GET.get("tab", "ex2")

    return render(request, "dashboard.html", {
        "tab": tab
    })