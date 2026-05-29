def main():
    students = []
    
    # 1. Input the number of students
    try:
        num_students = int(input("Enter the number of students: "))
    except ValueError:
        print("Invalid input. Please enter a number.")
        return

    # 2. Input student information
    for i in range(num_students):
        print(f"\n--- Student {i+1} ---")
        student_id = input("Student ID: ")
        full_name = input("Full name: ")
        try:
            python_score = float(input("Python score: "))
        except ValueError:
            print("Invalid score. Defaulting to 0.")
            python_score = 0.0
            
        # 3. Store all students in a list (as dictionaries)
        student = {
            "id": student_id,
            "name": full_name,
            "score": python_score
        }
        students.append(student)

    # 4. Display results
    if not students:
        print("\nNo students to display.")
        return

    print("\n" + "="*30)
    print("ALL STUDENTS")
    print("="*30)
    for s in students:
        print(f"ID: {s['id']} | Name: {s['name']} | Score: {s['score']}")

    # The student with the highest score
    highest_student = max(students, key=lambda x: x['score'])
    print("\n" + "="*30)
    print("HIGHEST SCORE STUDENT")
    print("="*30)
    print(f"ID: {highest_student['id']} | Name: {highest_student['name']} | Score: {highest_student['score']}")

    # The average score
    average_score = sum(s['score'] for s in students) / len(students)
    print("\n" + "="*30)
    print(f"AVERAGE SCORE: {average_score:.2f}")

    # Students who passed (score >= 5)
    passed_students = [s for s in students if s['score'] >= 5]
    print("\n" + "="*30)
    print("PASSED STUDENTS (Score >= 5)")
    print("="*30)
    if passed_students:
        for s in passed_students:
            print(f"ID: {s['id']} | Name: {s['name']} | Score: {s['score']}")
    else:
        print("No students passed.")

if __name__ == "__main__":
    main()
