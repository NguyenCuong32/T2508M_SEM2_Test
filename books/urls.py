from django.urls import path
from . import views

urlpatterns = [
    path('', views.book_list, name='book_list'),
    path('<int:pk>/edit/', views.edit_book, name='book_edit'),
    path('<int:pk>/delete/', views.delete_book, name='book_delete'),
]

