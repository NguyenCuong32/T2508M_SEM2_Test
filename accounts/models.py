from django.db import models
from django.contrib.auth.models import User

SECURITY_QUESTIONS = [
    ('pet', 'What is the name of your first pet?'),
    ('school', 'What was the name of your elementary school?'),
    ('city', 'In what city were you born?'),
    ('mother', "What is your mother's maiden name?"),
]

class UserProfile(models.Model):
    user = models.OneToOneField(User, on_delete=models.CASCADE, related_name='profile')
    security_question = models.CharField(max_length=50, choices=SECURITY_QUESTIONS)
    security_answer = models.CharField(max_length=255)

    def __str__(self):
        return f"Profile for {self.user.username}"
