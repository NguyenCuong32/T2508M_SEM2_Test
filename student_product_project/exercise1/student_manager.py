def input_students():
    students = []

    n = int(input("Enter number of students: "))

    for i in range(n):
        print(f"\nStudent {i + 1}")

        student_id = input("Student ID: ")
        full_name = input("Full Name: ")
        score = float(input("Python Score: "))

        student = {
            "id": student_id,
            "name": full_name,
            "score": score
        }

        students.append(student)

    return students


def display_students(students):
    print("\n=== ALL STUDENTS ===")

    for student in students:
        print(
            f"ID: {student['id']}, "
            f"Name: {student['name']}, "
            f"Score: {student['score']}"
        )


def get_top_student(students):
    return max(students, key=lambda student: student["score"])


def calculate_average(students):
    total_score = sum(student["score"] for student in students)
    return total_score / len(students)


def get_passed_students(students):
    return [
        student
        for student in students
        if student["score"] >= 5
    ]