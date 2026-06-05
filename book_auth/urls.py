# book_auth/urls.py
from django.urls import path
from . import views

urlpatterns = [
    # Book URLs (Bài 1)
    path('books/', views.book_list, name='book_list'),
    path('books/add/', views.add_book, name='add_book'),
    
    # Auth URLs (Bài 2)
    path('login/', views.login_view, name='login'),
    path('logout/', views.logout_view, name='logout'),
    path('dashboard/', views.dashboard_view, name='dashboard'),
]