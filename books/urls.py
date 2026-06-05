from django.urls import path
from . import views

urlpatterns = [
    path('', views.book_list, name='book_list'),
    path('expensive/', views.expensive_books, name='expensive_books'),
]
