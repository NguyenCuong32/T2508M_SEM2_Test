# Exercise 1: Manage Student Scores using List and Dictionary

def manage_students():
    students = []
    
    # Input number of students
    num_students = int(input("Enter the number of students: "))
    
    # Input student information
    for i in range(num_students):
        print(f"\nEnter information for student {i + 1}:")
        student_id = input("Student ID: ")
        full_name = input("Full name: ")
        score = float(input("Python score: "))
        
        student = {
            'id': student_id,
            'name': full_name,
            'score': score
        }
        students.append(student)
    
    # Display all students
    print("\n" + "="*60)
    print("ALL STUDENTS:")
    print("="*60)
    print(f"{'ID':<15} {'Name':<25} {'Score':<10}")
    print("-"*60)
    for student in students:
        print(f"{student['id']:<15} {student['name']:<25} {student['score']:<10}")
    
    # Find student with highest score
    print("\n" + "="*60)
    highest_student = max(students, key=lambda x: x['score'])
    print("STUDENT WITH HIGHEST SCORE:")
    print("="*60)
    print(f"ID: {highest_student['id']}")
    print(f"Name: {highest_student['name']}")
    print(f"Score: {highest_student['score']}")
    
    # Calculate average score
    print("\n" + "="*60)
    average_score = sum(student['score'] for student in students) / len(students)
    print(f"AVERAGE SCORE: {average_score:.2f}")
    print("="*60)
    
    # Display students who passed (score >= 5)
    print("\n" + "="*60)
    passed_students = [student for student in students if student['score'] >= 5]
    print("STUDENTS WHO PASSED (Score >= 5):")
    print("="*60)
    print(f"{'ID':<15} {'Name':<25} {'Score':<10}")
    print("-"*60)
    for student in passed_students:
        print(f"{student['id']:<15} {student['name']:<25} {student['score']:<10}")
    print(f"Total passed: {len(passed_students)}/{len(students)}")

if __name__ == "__main__":
    manage_students()
