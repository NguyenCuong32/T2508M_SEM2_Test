import pandas as pd
import os

def exercise_1_student_management():
    print("\n" + "="*30)
    print("EXERCISE 1: STUDENT SCORE MANAGEMENT")
    print("="*30)
    students = []
    
    try:
        num_students = int(input("Enter the number of students: "))
    except ValueError:
        print("Invalid input. Please enter a number.")
        return

    for i in range(num_students):
        print(f"\n--- Student {i+1} ---")
        student_id = input("Student ID: ")
        full_name = input("Full name: ")
        try:
            python_score = float(input("Python score: "))
        except ValueError:
            print("Invalid score. Defaulting to 0.")
            python_score = 0.0
            
        student = {
            "id": student_id,
            "name": full_name,
            "score": python_score
        }
        students.append(student)

    if not students:
        print("\nNo students to display.")
        return

    print("\nALL STUDENTS")
    print("-" * 20)
    for s in students:
        print(f"ID: {s['id']} | Name: {s['name']} | Score: {s['score']}")

    highest_student = max(students, key=lambda x: x['score'])
    print(f"\nHIGHEST SCORE STUDENT: {highest_student['name']} ({highest_student['score']})")

    average_score = sum(s['score'] for s in students) / len(students)
    print(f"AVERAGE SCORE: {average_score:.2f}")

    print("\nPASSED STUDENTS (Score >= 5):")
    passed_students = [s for s in students if s['score'] >= 5]
    if passed_students:
        for s in passed_students:
            print(f"- {s['name']} (ID: {s['id']}, Score: {s['score']})")
    else:
        print("No students passed.")


def exercise_2_product_pandas():
    print("\n" + "="*30)
    print("EXERCISE 2: PRODUCT MANAGEMENT WITH PANDAS")
    print("="*30)
    
    # Check if pandas is installed
    try:
        import pandas as pd
    except ImportError:
        print("Error: pandas library is not installed. Please install it using 'pip install pandas'.")
        return

    # 1. Create a DataFrame
    data = {
        'id': [101, 102, 103, 104, 105],
        'name': ['Laptop', 'Mouse', 'Keyboard', 'Monitor', 'Headset'],
        'price': [1200, 25, 45, 150, 80],
        'quantity': [5, 50, 30, 10, 20]
    }
    df = pd.DataFrame(data)
    
    # 2. Save data to products.csv
    csv_file = 'products.csv'
    df.to_csv(csv_file, index=False)
    print(f"Data successfully saved to {csv_file}")
    
    # 3. Read the CSV file
    loaded_df = pd.read_csv(csv_file)
    print("\n--- ALL PRODUCTS ---")
    print(loaded_df)
    
    # 4. Display products with price > 100
    print("\n--- PRODUCTS WITH PRICE > 100 ---")
    print(loaded_df[loaded_df['price'] > 100])
    
    # 5. Calculate total inventory value
    total_value = (loaded_df['price'] * loaded_df['quantity']).sum()
    print(f"\nTOTAL INVENTORY VALUE: {total_value}")
    
    # 6. Add new column 'total'
    print("\nAdding 'total' column...")
    loaded_df['total'] = loaded_df['price'] * loaded_df['quantity']
    print("--- FINAL DATA WITH TOTAL COLUMN ---")
    print(loaded_df)


def main():
    while True:
        print("\n" + "#"*40)
        print("      PYTHON EXERCISES MAIN MENU      ")
        print("#"*40)
        print("1. Run Exercise 1: Student Management")
        print("2. Run Exercise 2: Product Management (Pandas)")
        print("3. Exit")
        
        choice = input("\nSelect an option (1-3): ")
        
        if choice == '1':
            exercise_1_student_management()
        elif choice == '2':
            exercise_2_product_pandas()
        elif choice == '3':
            print("Exiting... Goodbye!")
            break
        else:
            print("Invalid choice. Please try again.")

if __name__ == "__main__":
    main()
