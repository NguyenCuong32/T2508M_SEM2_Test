from django.db import models


# Create your models here.
class Book(models.Model):
    objects = models.Manager()

    title = models.CharField(max_length=255)
    author = models.CharField(max_length=255)
    price = models.DecimalField(max_digits=10, decimal_places=2)
    create_at = models.DateTimeField(auto_now_add=True)
