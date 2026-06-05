from django.urls import path
from . import views

urlpatterns = [
    path('', views.book_list, name='book_list'),
    path('add/', views.add_book, name='add_book'),
    path('expensive/', views.expensive_books, name='expensive_books'),
    path('total/', views.total_books, name='total_books'),
]
