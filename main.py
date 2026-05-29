students = []

# Nhập số lượng sinh viên
n = int(input("Enter number of students: "))

# Nhập thông tin
for i in range(n):
    print(f"\nStudent {i+1}")

    student_id = input("Enter ID: ")
    full_name = input("Enter full name: ")
    python_score = float(input("Enter Python score: "))

    student = {
        "id": student_id,
        "name": full_name,
        "score": python_score
    }

    students.append(student)

# Hiển thị tất cả sinh viên
print("\n=== ALL STUDENTS ===")

for s in students:
    print(s)

# Sinh viên điểm cao nhất
top_student = max(students, key=lambda x: x["score"])

print("\n=== TOP STUDENT ===")
print(top_student)

# Điểm trung bình
total = sum(s["score"] for s in students)
average = total / n

print("\nAverage score:", average)

# Sinh viên đậu
print("\n=== PASSED STUDENTS ===")

for s in students:
    if s["score"] >= 5:
        print(s)