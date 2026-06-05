from django.db import models

class Book(models.Model):
    title_en = models.CharField(max_length=255)
    title_vi = models.CharField(max_length=255)
    author = models.CharField(max_length=255)
    price_usd = models.DecimalField(max_digits=10, decimal_places=2)
    price_vnd = models.DecimalField(max_digits=12, decimal_places=0)
    cover_image = models.ImageField(upload_to='covers/', blank=True, null=True)
    info_link = models.URLField(max_length=500, blank=True, null=True)
    created_at = models.DateField(verbose_name="Publication Date")

    def __str__(self):
        return f"{self.title_en} / {self.title_vi} by {self.author}"

    @property
    def title(self):
        # Đảm bảo tương thích ngược với yêu cầu đề bài gốc
        return self.title_en

    @property
    def price(self):
        # Đảm bảo tương thích ngược với yêu cầu đề bài gốc
        return self.price_usd
