from django.shortcuts import render, redirect
from django.contrib.auth import authenticate, login, logout
from django.contrib.auth.forms import AuthenticationForm
from django.contrib.auth.decorators import login_required
from django.contrib.auth.models import User
from django.contrib import messages
from .forms import UserRegisterForm, ForgotPasswordUsernameForm, ForgotPasswordResetForm
from .models import UserProfile
from books.models import Book
from django.db.models import Avg, Max, Min, Count
from decimal import Decimal

def login_view(request):
    if request.user.is_authenticated:
        return redirect('accounts:dashboard')
        
    if request.method == 'POST':
        form = AuthenticationForm(request, data=request.POST)
        if form.is_valid():
            username = form.cleaned_data.get('username')
            password = form.cleaned_data.get('password')
            user = authenticate(username=username, password=password)
            if user is not None:
                login(request, user)
                messages.success(request, f"Welcome back, {username}!")
                return redirect('accounts:dashboard')
            else:
                messages.error(request, "Invalid username or password.")
        else:
            messages.error(request, "Invalid username or password.")
    else:
        form = AuthenticationForm()
        
    return render(request, 'accounts/login.html', {'form': form})

def register_view(request):
    if request.user.is_authenticated:
        return redirect('accounts:dashboard')

    if request.method == 'POST':
        form = UserRegisterForm(request.POST)
        if form.is_valid():
            username         = form.cleaned_data.get('username')
            password         = form.cleaned_data.get('password')
            security_question = form.cleaned_data.get('security_question')
            security_answer   = form.cleaned_data.get('security_answer')

            # Tạo user không cần email
            user = User.objects.create_user(username=username, password=password)

            # Tạo Profile lưu câu hỏi bảo mật
            UserProfile.objects.create(
                user=user,
                security_question=security_question,
                security_answer=security_answer.strip().lower()
            )
            
            messages.success(request, f"Account '{username}' created successfully! Please sign in below.")
            return redirect('accounts:login')
        else:
            messages.error(request, "Please correct the errors below to register.")
    else:
        form = UserRegisterForm()

    return render(request, 'accounts/register.html', {'form': form})

def forgot_password_username(request):
    if request.user.is_authenticated:
        return redirect('accounts:dashboard')

    if request.method == 'POST':
        form = ForgotPasswordUsernameForm(request.POST)
        if form.is_valid():
            username = form.cleaned_data.get('username')
            user = User.objects.get(username=username)
            if not hasattr(user, 'profile'):
                messages.error(request, "This account does not have a security question configured. Please contact the administrator.")
                return render(request, 'accounts/forgot_password_username.html', {'form': form})
            
            request.session['reset_username'] = username
            return redirect('accounts:forgot_password_verify')
        else:
            messages.error(request, "Please enter a valid username.")
    else:
        form = ForgotPasswordUsernameForm()

    return render(request, 'accounts/forgot_password_username.html', {'form': form})

def forgot_password_verify(request):
    if request.user.is_authenticated:
        return redirect('accounts:dashboard')

    username = request.session.get('reset_username')
    if not username:
        messages.error(request, "Please enter your username first.")
        return redirect('accounts:forgot_password')

    try:
        user = User.objects.get(username=username)
        profile = user.profile
    except (User.DoesNotExist, UserProfile.DoesNotExist):
        messages.error(request, "Invalid account or no security profile configured.")
        return redirect('accounts:forgot_password')

    question_display = profile.get_security_question_display()

    if request.method == 'POST':
        form = ForgotPasswordResetForm(request.POST)
        if form.is_valid():
            security_answer = form.cleaned_data.get('security_answer')
            new_password = form.cleaned_data.get('new_password')
            
            if profile.security_answer == security_answer.strip().lower():
                user.set_password(new_password)
                user.save()
                
                # Xóa dữ liệu session sau khi đặt lại mật khẩu thành công
                if 'reset_username' in request.session:
                    del request.session['reset_username']
                    
                messages.success(request, "Password reset successfully! Please sign in with your new password.")
                return redirect('accounts:login')
            else:
                messages.error(request, "Security answer is incorrect. Please try again.")
                form.add_error('security_answer', "Security answer is incorrect.")
        else:
            messages.error(request, "Please correct the errors below.")
    else:
        form = ForgotPasswordResetForm()

    context = {
        'form': form,
        'question_display': question_display,
        'username': username
    }
    return render(request, 'accounts/forgot_password_verify.html', context)

def logout_view(request):
    logout(request)
    messages.info(request, "You have been logged out.")
    return redirect('accounts:login')

@login_required
def dashboard_view(request):
    # Lấy thống kê sách để hiển thị trên dashboard
    books = Book.objects.all()
    total_books   = books.count()
    above_100_usd = books.filter(price_usd__gt=100).count()
    below_100k_vnd = books.filter(price_vnd__lt=100000).count()
    avg_price_usd = books.aggregate(avg=Avg('price_usd'))['avg'] or Decimal('0')
    max_price_book = books.order_by('-price_usd').first()
    cheapest_book  = books.order_by('price_usd').first()
    latest_books   = books.order_by('-created_at')[:3]
    total_authors  = books.values('author').distinct().count()

    context = {
        'total_books':    total_books,
        'above_100_usd':  above_100_usd,
        'below_100k_vnd': below_100k_vnd,
        'avg_price_usd':  avg_price_usd,
        'max_price_book': max_price_book,
        'cheapest_book':  cheapest_book,
        'latest_books':   latest_books,
        'total_authors':  total_authors,
    }
    return render(request, 'accounts/dashboard.html', context)
