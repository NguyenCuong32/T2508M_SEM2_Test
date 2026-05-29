number_students = int(input("Nhập số lượng sinh viên: "))
students = []
for i in range(number_students):
 print(f"Nhập thông tin cho sinh viên thứ {i+1}")
 students_id = input("Mã số sinh viên ")
 full_name = input("Họ và tên ")
 python_score = float(input("Điểm Python "))

 students_info = {
  "id": students_id,
  "name": full_name,
  "score": python_score
 }
 students.append(students_info)
 print("\n" + "="*40 + "\n")
print("\nDanh sách tất cả sinh viên")
for student in students:
   print(f"ID: {student['id']} | Tên: {student['name']} | Điểm: {student['score']}")
   highest_student = max(students, key=lambda x: x['score'])
print(f"\n Sinh viên có điểm cao nhất ")
print(f"Tên: {highest_student['name']} - Điểm: {highest_student['score']}")


total_score = sum(student['score'] for student in students)
average_score = total_score / number_students
print(f"\n Điểm trung bình của lớp ")
print(f"Điểm trung bình: {average_score:.2f}")


print(f"\n Danh sách sinh viên qua môn (Score >= 5) ")
passed_students = [student for student in students if student['score'] >= 5]

if passed_students:
    for student in passed_students:
        print(f"Tên: {student['name']} - Điểm: {student['score']}")
else:
    print("Không có sinh viên nào qua môn.")