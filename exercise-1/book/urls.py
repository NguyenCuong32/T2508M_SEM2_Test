from django.urls import path

from . import views

urlpatterns = [
    path("add/", views.add_book, name="add_book"),
    path("list/", views.list_books, name="list_books"),
    path("expensive/", views.add_book, name="expensive_books"),
    path("count/", views.add_book, name="book_count"),
]
