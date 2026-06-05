from django.shortcuts import redirect, render

from .models import Book

# Create your views here.


# Add a new book
def add_book(request):
    if request.method == "POST":
        title = request.POST.get("title")
        author = request.POST.get("author")
        price = request.POST.get("price")

        Book.objects.create(
            title=title,
            author=author,
            price=price,
        )
        return redirect("list_books")

    return render(request, "add_book.html")


# Display all books
def list_books(request):
    books = Book.objects.all()
    return render(request, "list_books.html", {"books": books})


# Display books with price > 100
def expensive_books(request):
    books = Book.objects.filter(price__gt=100)
    return render(request, "expensive_books.html", {"books": books})


# Display the total number of books
def book_count(request):
    count = Book.objects.count()
    return render(request, "book_count.html", {"count": count})
