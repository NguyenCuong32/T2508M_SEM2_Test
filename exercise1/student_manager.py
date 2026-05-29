def input_number_of_students():
    while True:
        try:
            num_students = int(input("Enter the number of students: "))
            if num_students < 0:
                print("Number of students cannot be negative. Please try again.")
                continue
            return num_students
        except ValueError:
            print("Invalid input. Please enter an integer.")

def input_students(num_students):
    students = []
    for i in range(num_students):
        print(f"\nEntering information for student {i+1}:")
        student_id = input("Student ID: ")
        full_name = input("Full name: ")
        
        while True:
            try:
                score = float(input("Python score: "))
                if score < 0 or score > 10:
                    print("Score must be between 0 and 10. Please try again.")
                    continue
                break
            except ValueError:
                print("Invalid input. Please enter a number.")
                
        student = {
            "id": student_id,
            "name": full_name,
            "score": score
        }
        students.append(student)
    return students

def display_all_students(students):
    print("\n" + "="*30)
    print("--- All Students ---")
    if not students:
        print("No students to display.")
        return
        
    for s in students:
        print(f"ID: {s['id']}, Name: {s['name']}, Score: {s['score']}")

def display_highest_score(students):
    if not students:
        return
    highest_score_student = max(students, key=lambda x: x['score'])
    print("\n--- Student with highest score ---")
    print(f"ID: {highest_score_student['id']}, Name: {highest_score_student['name']}, Score: {highest_score_student['score']}")

def display_average_score(students):
    if not students:
        return
    total_score = sum(s['score'] for s in students)
    average_score = total_score / len(students)
    print(f"\n--- Average score ---")
    print(f"Average: {average_score:.2f}")

def display_passed_students(students):
    if not students:
        return
    print("\n--- Students who passed (score >= 5) ---")
    passed_students = [s for s in students if s['score'] >= 5]
    if passed_students:
        for s in passed_students:
            print(f"ID: {s['id']}, Name: {s['name']}, Score: {s['score']}")
    else:
        print("No students passed.")
    print("="*30 + "\n")
