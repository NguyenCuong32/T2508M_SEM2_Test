from django.urls import path
from . import views

urlpatterns = [
    path("", views.book_list, name="book_list"),
    path("add/", views.add_book, name="add_book"),
    path("login/", views.login_view, name="login"),
    path("logout/", views.logout_view, name="logout"),
    path("dashboard/", views.dashboard, name="dashboard"),
]