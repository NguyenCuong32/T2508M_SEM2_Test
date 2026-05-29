def run_exercise_1():
    print("--- EXERCISE 1: STUDENT SCORE MANAGEMENT ---")
    
    while True:
        try:
            num_students = int(input("Enter the number of students: "))
            if num_students > 0:
                break
            print("Please enter a number greater than 0.")
        except ValueError:
            print("Invalid input. Please enter an integer.")

    students_list = []

    for i in range(num_students):
        print(f"\nInput information for student {i+1}:")
        student_id = input("Enter Student ID: ").strip()
        full_name = input("Enter Full Name: ").strip()
        
        while True:
            try:
                score = float(input("Enter Python Score (0-10): "))
                if 0 <= score <= 10:
                    break
                print("Score must be between 0 and 10.")
            except ValueError:
                print("Invalid input. Please enter a number for score.")

        student_dict = {
            "id": student_id,
            "name": full_name,
            "score": score
        }
        students_list.append(student_dict)

    print("\n--- RESULTS FOR EXERCISE 1 ---")
    
    print("\nAll Students:")
    for s in students_list:
        print(f"ID: {s['id']} | Name: {s['name']} | Python Score: {s['score']}")

    highest_score = students_list[0]['score']
    total_score = 0
    passed_students = []

    for s in students_list:
        total_score += s['score']
        if s['score'] > highest_score:
            highest_score = s['score']
        if s['score'] >= 5:
            passed_students.append(s)

    print("\nStudent(s) with the highest score:")
    for s in students_list:
        if s['score'] == highest_score:
            print(f"ID: {s['id']} | Name: {s['name']} | Score: {s['score']}")

    average_score = total_score / num_students
    print(f"\nThe average score: {average_score:.2f}")

    print("\nStudents who passed (score >= 5):")
    if passed_students:
        for s in passed_students:
            print(f"ID: {s['id']} | Name: {s['name']} | Score: {s['score']}")
    else:
        print("No students passed.")


import pandas as pd

def run_exercise_2():
    print("\n\n--- EXERCISE 2: PRODUCT MANAGEMENT WITH PANDAS ---")
    
    data = {
        "id": [101, 102, 103, 104, 105],
        "name": ["Laptop", "Mouse", "Keyboard", "Monitor", "Headphones"],
        "price": [1200, 25, 80, 150, 45],
        "quantity": [5, 20, 15, 8, 30]
    }
    
    df = pd.DataFrame(data)
    print("Initial DataFrame created successfully:")
    print(df)
    
    csv_filename = "products.csv"
    df.to_csv(csv_filename, index=False)
    print(f"\nData saved successfully to '{csv_filename}'.")

    print(f"\nReading data back from '{csv_filename}':")
    loaded_df = pd.read_csv(csv_filename)
    
    print("\nAll products from CSV:")
    print(loaded_df)
    
    print("\nProducts with price > 100:")
    filtered_df = loaded_df[loaded_df['price'] > 100]
    print(filtered_df)
    
    total_value = (loaded_df['price'] * loaded_df['quantity']).sum()
    print(f"\nCalculate the total inventory value: ${total_value:,}")

    loaded_df['total'] = loaded_df['price'] * loaded_df['quantity']
    print("\nDataFrame after adding 'total' column:")
    print(loaded_df)


if __name__ == "__main__":
    run_exercise_1()
    print("\n" + "="*50 + "\n")
    run_exercise_2()