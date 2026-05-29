import student_manager

def main():
    num_students = student_manager.input_number_of_students()
    
    students = student_manager.input_students(num_students)
    
    if not students:
        print("\nNo students were entered.")
        return
        
    student_manager.display_all_students(students)
    student_manager.display_highest_score(students)
    student_manager.display_average_score(students)
    student_manager.display_passed_students(students)

if __name__ == "__main__":
    main()
