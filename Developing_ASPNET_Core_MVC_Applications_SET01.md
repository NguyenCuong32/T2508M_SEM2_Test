# Developing ASP.NET Core MVC Applications - SET01
**FPT Aptech - Unleash your potential**  
**Developing ASP.NET Core MVC Applications - Exam Paper**  
**Duration:** 60 minutes | **Marks:** 15

---

## Question:

### Database Diagram & Structure

#### 1. Customers
| Column Name | Data Type | Description |
| :--- | :--- | :--- |
| **CustomerID** (PK) | int | |
| FullName | nvarchar(255) | Họ tên |
| PhoneNumber | nvarchar(15) | Số điện thoại |
| RegistrationDate | datetime | Ngày đăng ký |

#### 2. ComicBooks
| Column Name | Data Type | Description |
| :--- | :--- | :--- |
| **ComicBookID** (PK) | int | |
| Title | nvarchar(255) | Tên sách |
| Author | nvarchar(255) | Tác giả |
| PricePerDay | decimal(10, 2) | Giá thuê 1 ngày |

#### 3. Rentals
| Column Name | Data Type | Description |
| :--- | :--- | :--- |
| **RentalID** (PK) | int | |
| CustomerID (FK) | int | |
| RentalDate | datetime | Ngày thuê |
| ReturnDate | datetime | Ngày trả |
| Status | nvarchar(50) | Trạng thái: Đang thuê, Có thể thuê |

#### 4. RentalDetails
| Column Name | Data Type | Description |
| :--- | :--- | :--- |
| **RentalDetailID** (PK) | int | |
| RentalID (FK) | int | |
| ComicBookID (FK) | int | |
| Quantity | int | Số lượng |
| PricePerDay | decimal(10, 2) | |

---

The comic system's books have business to rent books. Everyone can rent some books and pay a fee for them. Using ASP.Net core to develop the website with the database diagram above.

* **Website:** comicsys.com  
* **Database Information:**
  * **Database name:** ComicSystem
  * **Tables:** Customers, ComicBooks, Rentals, RentalDetails

---

### Requirement:

1. Write a page to create, read, update, delete comic books (insert into `ComicBooks` table).
2. The first time a customer uses it must be registered with some information such as: Fullname, Phone number, Register date (insert into `Customers` table).
3. When a customer rents a book, the book is added to the rental page with some information such as: Rental date, Return date, Quantity, Price per day (Insert into `Rentals` and `RentalDetails` tables).
4. Create a report of all book rents between start date to end date same table below:

| No | Book name | Rental date | Return date | Customer name | Quantity |
| :--- | :--- | :--- | :--- | :--- | :--- |
| 1 | Conan | 01/10/2024 | 10/10/2024 | Nguyen Hung | 1 |
| 2 | Doraemon | 01/10/2024 | 10/20/2024 | Nguyen Hung | 3 |

---

### Marking Schema:

| Question | Mark |
| :--- | :--- |
| **Question 1:** CRUD for comic book | 3 |
| **Question 2:** Customer register | 3 |
| **Question 3:** Rental book page | 5 |
| **Question 4:** Report all book | 3 |
| **Bonus:** good UI/UX, coding convention | 1 |
| **Total** | **15** |

---

### Note:
1. Students can make it with Code first or Database first. If you use Database First method please commit all scripts to create database, store procedure.
2. Students can make it with MVC model, Web API, any Front End such as reactjs, angular.
3. Database: SQL Server or MySQL.
