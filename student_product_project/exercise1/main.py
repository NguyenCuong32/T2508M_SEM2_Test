from student_manager import (
    input_students,
    display_students,
    get_top_student,
    calculate_average,
    get_passed_students
)


def main():
    students = input_students()

    display_students(students)

    top_student = get_top_student(students)

    print("\n=== TOP STUDENT ===")
    print(
        f"ID: {top_student['id']}, "
        f"Name: {top_student['name']}, "
        f"Score: {top_student['score']}"
    )

    average_score = calculate_average(students)

    print("\n=== AVERAGE SCORE ===")
    print(f"{average_score:.2f}")

    passed_students = get_passed_students(students)

    print("\n=== PASSED STUDENTS (Score >= 5) ===")

    for student in passed_students:
        print(
            f"ID: {student['id']}, "
            f"Name: {student['name']}, "
            f"Score: {student['score']}"
        )


if __name__ == "__main__":
    main()