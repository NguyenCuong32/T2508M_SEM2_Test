students = []

n = int(input("Enter number of students: "))

for i in range(n):
    print(f"\nStudent {i + 1}")

    student_id = int(input("Enter student ID: "))
    full_name = str(input("Enter full name: "))
    python_score = float(input("Enter Python score: "))

    student = {"id": student_id, "name": full_name, "score": python_score}

    students.append(student)

# Display
print("\n=== MENU ===")
print("1. Display all students")
print("2. Display highest score student")
print("3. Display average score")
print("4. Display passed students")

choice = int(input("Enter choice: "))

match choice:
    case 1:
        print("\n--- All Students ---")
        for student in students:
            print(f"\nID: {student['id']}")
            print(f"Name: {student['name']}")
            print(f"Score: {student['score']}")

    case 2:
        highest_student = max(students, key=lambda x: x["score"])

        print("\n--- Highest Score Student ---")
        print(f"\nID: {highest_student['id']}")
        print(f"Name: {highest_student['name']}")
        print(f"Score: {highest_student['score']}")

    case 3:
        average_score = sum(s["score"] for s in students) / n
        print("\n--- Average Score ---")
        print(f"{average_score:.2f}")

    case 4:
        print("\n--- Passed Students (score >= 5) ---")

        for student in students:
            if student["score"] >= 5:
                print(f"\nID: {student['id']}")
                print(f"Name: {student['name']}")
                print(f"Score: {student['score']}")

    case _:
        print("Invalid choice!")
