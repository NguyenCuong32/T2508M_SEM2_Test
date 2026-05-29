students = []

n = int(input("Enter number of students: "))

for i in range(n):
    print(f"\nStudent {i + 1}")

    student = {
        "id": input("Student ID: "),
        "name": input("Full Name: "),
        "score": float(input("Python Score: "))
    }

    students.append(student)

print("\n===== ALL STUDENTS =====")

for s in students:
    print(f"ID: {s['id']} | Name: {s['name']} | Score: {s['score']}")

highest_student = max(students, key=lambda x: x["score"])

print("\n===== HIGHEST SCORE =====")
print(
    f"ID: {highest_student['id']} | "
    f"Name: {highest_student['name']} | "
    f"Score: {highest_student['score']}"
)

average_score = sum(s["score"] for s in students) / len(students)

print("\n===== AVERAGE SCORE =====")
print(f"Average Score: {average_score:.2f}")

print("\n===== PASSED STUDENTS =====")

passed_students = [s for s in students if s["score"] >= 5]

for s in passed_students:
    print(f"ID: {s['id']} | Name: {s['name']} | Score: {s['score']}")