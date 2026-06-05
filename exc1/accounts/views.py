from django.shortcuts import render, redirect
from django.contrib.auth import authenticate, login, logout
from django.contrib.auth.decorators import login_required

# 0. View Login (Dùng hệ thống auth của Django)
def login_view(request):
    if request.user.is_authenticated:
        return redirect("dashboard")

    context = {}
    if request.method == "POST":
        username = request.POST.get("username", "").strip()
        password = request.POST.get("password", "")

        user = authenticate(request, username=username, password=password)
        if user is not None:
            login(request, user)
            return redirect("dashboard")

        context["error"] = "Invalid username or password."

    return render(request, "accounts/login.html", context)

# 1. View Dashboard (Được bảo vệ - Chỉ cho phép user đã đăng nhập)
@login_required
def dashboard_view(request):
    # Trả về giao diện dashboard, thông tin user đã có sẵn trong đối tượng request
    return render(request, 'accounts/dashboard.html')

# 2. Xử lý chức năng Đăng xuất
def logout_view(request):
    logout(request) # Xóa session đăng nhập của user
    return redirect('login') # Đẩy user về lại trang login