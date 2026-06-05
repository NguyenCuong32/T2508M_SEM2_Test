from django.db import models

class Book(models.Model):
    title = models.CharField(max_length=200, verbose_name="Tiêu đề")
    author = models.CharField(max_length=150, verbose_name="Tác giả")
    price = models.DecimalField(max_digits=10, decimal_places=2, verbose_name="Giá tiền")
    created_at = models.DateTimeField(auto_now_add=True, verbose_name="Ngày tạo")

    def __str__(self):
        return self.title