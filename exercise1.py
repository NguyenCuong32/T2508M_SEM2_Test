"""Exercise 1: Manage student Python scores using list and dictionary."""


def input_positive_integer(prompt):
    """Ask until the user enters a positive integer."""
    while True:
        value = input(prompt).strip()
        try:
            number = int(value)
            if number > 0:
                return number
            print("Please enter a number greater than 0.")
        except ValueError:
            print("Invalid input. Please enter an integer.")


def input_required_text(prompt):
    """Ask until the user enters non-empty text."""
    while True:
        value = input(prompt).strip()
        if value:
            return value
        print("This field cannot be empty.")


def input_score(prompt):
    """Ask until the user enters a valid score from 0 to 10."""
    while True:
        value = input(prompt).strip()
        try:
            score = float(value)
            if 0 <= score <= 10:
                return score
            print("Score must be between 0 and 10.")
        except ValueError:
            print("Invalid input. Please enter a numeric score.")


def input_students():
    """Input all student information and store it in a list of dictionaries."""
    number_of_students = input_positive_integer("Enter number of students: ")
    students = []
    used_ids = set()

    for index in range(number_of_students):
        print(f"\nStudent {index + 1}")

        while True:
            student_id = input_required_text("Student ID: ")
            if student_id not in used_ids:
                used_ids.add(student_id)
                break
            print("This student ID already exists. Please enter another ID.")

        full_name = input_required_text("Full name: ")
        python_score = input_score("Python score: ")

        student = {
            "id": student_id,
            "full_name": full_name,
            "python_score": python_score,
        }
        students.append(student)

    return students


def display_students(title, students):
    """Display students in a readable table."""
    print(f"\n{title}")
    print("-" * 56)
    print(f"{'ID':<12}{'Full name':<30}{'Python score':>12}")
    print("-" * 56)

    if not students:
        print("No students found.")
    else:
        for student in students:
            print(
                f"{student['id']:<12}"
                f"{student['full_name']:<30}"
                f"{student['python_score']:>12.2f}"
            )

    print("-" * 56)


def get_highest_score_students(students):
    """Return all students with the highest Python score."""
    highest_score = max(student["python_score"] for student in students)
    return [
        student
        for student in students
        if student["python_score"] == highest_score
    ]


def get_average_score(students):
    """Calculate the average Python score."""
    total_score = sum(student["python_score"] for student in students)
    return total_score / len(students)


def get_passed_students(students):
    """Return students who passed with score >= 5."""
    return [
        student
        for student in students
        if student["python_score"] >= 5
    ]


def main():
    students = input_students()

    display_students("All students", students)

    highest_score_students = get_highest_score_students(students)
    display_students("Student(s) with the highest score", highest_score_students)

    average_score = get_average_score(students)
    print(f"\nAverage score: {average_score:.2f}")

    passed_students = get_passed_students(students)
    display_students("Students who passed (score >= 5)", passed_students)


if __name__ == "__main__":
    main()
