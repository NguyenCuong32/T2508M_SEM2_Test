from django.shortcuts import render, redirect
from .models import Book


def book_list(request):
    # 1. Tính năng: Thêm sách mới
    if request.method == "POST":
        title = request.POST.get('title')
        author = request.POST.get('author')
        price = request.POST.get('price')

        # Kiểm tra điều kiện dữ liệu không rỗng trước khi lưu
        if title and author and price:
            Book.objects.create(title=title, author=author, price=price)

        # Sau khi thêm thành công, tải lại trang để tránh trùng lặp dữ liệu khi F5
        return redirect('book_list')

    # 2. Tính năng: Hiển thị toàn bộ sách (sắp xếp theo thời gian mới nhất)
    all_books = Book.objects.all().order_by('-created_at')

    # 3. Tính năng: Hiển thị sách có giá lớn hơn 100 (__gt đại diện cho "greater than")
    expensive_books = Book.objects.filter(price__gt=100)

    # 4. Tính năng: Đếm tổng số lượng sách trong cơ sở dữ liệu
    total_books = Book.objects.count()

    # Đóng gói dữ liệu vào context để gửi lên Template HTML
    context = {
        'all_books': all_books,
        'expensive_books': expensive_books,
        'total_books': total_books,
    }
    return render(request, 'books/book_list.html', context)