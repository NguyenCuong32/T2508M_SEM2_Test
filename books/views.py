from django.shortcuts import render, redirect, get_object_or_404
from django.http import HttpResponseForbidden
from django.contrib.auth.decorators import login_required
from .models import Book
from .forms import BookForm

def book_list(request):
    if request.method == 'POST':
        if not (request.user.is_authenticated and request.user.is_staff):
            return HttpResponseForbidden("You do not have permission to add books.")
        form = BookForm(request.POST)
        if form.is_valid():
            form.save()
            return redirect('book_list')
    else:
        form = BookForm()

    all_books = Book.objects.all()
    expensive_books = Book.objects.filter(price__gt=100)
    total_books = Book.objects.count()

    return render(request, 'books/book_list.html', {
        'form': form,
        'all_books': all_books,
        'expensive_books': expensive_books,
        'total_books': total_books,
    })

@login_required
def edit_book(request, pk):
    if not request.user.is_staff:
        return HttpResponseForbidden("You do not have permission to edit books.")
    book = get_object_or_404(Book, pk=pk)
    if request.method == 'POST':
        form = BookForm(request.POST, instance=book)
        if form.is_valid():
            form.save()
            return redirect('book_list')
    else:
        form = BookForm(instance=book)
    return render(request, 'books/book_edit.html', {
        'form': form,
        'book': book
    })

@login_required
def delete_book(request, pk):
    if not request.user.is_staff:
        return HttpResponseForbidden("You do not have permission to delete books.")
    if request.method == 'POST':
        book = get_object_or_404(Book, pk=pk)
        book.delete()
    return redirect('book_list')

