from django.urls import path
from . import views

urlpatterns = [
    path('', views.index_view, name='home'),
    path('books/', views.books_view, name='books'),
    path('books/edit/<int:book_id>/', views.edit_book_view, name='edit_book'),
    path('books/delete/<int:book_id>/', views.delete_book_view, name='delete_book'),
    path('login/', views.login_view, name='login'),
    path('logout/', views.logout_view, name='logout'),
    path('dashboard/', views.dashboard_view, name='dashboard'),
]
