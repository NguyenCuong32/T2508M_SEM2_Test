from django.urls import path
from . import views

urlpatterns = [
    path('', views.book_dashboard, name='book_dashboard'),
]