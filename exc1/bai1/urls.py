from django.contrib import admin
from django.urls import path, include

urlpatterns = [
    path('admin/', admin.site.urls),
    path('', include('accounts.urls')),
    path('books/', include('bai1.books.urls')), # Trỏ mọi đường dẫn /books/ về app books
]