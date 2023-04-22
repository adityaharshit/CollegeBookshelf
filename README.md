# CollegeBookshelf
This is a library management system built using VB.net and MySQL database. It allows the admin to manage student and book details, issue and return books, and generate reports.

Prerequisites
Visual Studio with Data storage and processing already installed.

Features
Student Details Management: In this module, the admin can register students, view the list of students, and update or modify student details.

Book Details Management: In this module, the admin can add new book data, view the list of available books, and update or modify Book Details.

Issued Books Record: Here the admin can see the list of currently issued books and the issued books history.

Issue Books: Here the admin can issue the books for a particular student. Inside this module is a reserve module where a student can reserve a currently issued book.

Return Books: In this module, an issued book can be returned back with automatic fine calculation.

Change Admin Details: The admin can change its login credentials.

Report: This can be viewed by the admin to see the total number of books, total registered students, currently issued books, books issued today, books returned today, and total reserved books.

Problem Statement
Students face issues when they go to issue a book and the book is already issued by someone else. This project provides them with an opportunity to reserve a book, which is currently issued by someone else, for 10 days. So when the owner returns the book, they can go and collect it, anyone else won't be allowed to issue this book.

How to Run the Project
Clone or download the project from the GitHub repository.
Open the project in Visual Studio.
Replace the connection string in the project with your own MySQL database connection string.
Restore the database backup file included in the project.
Build and run the project.

Author
Harshit Aditya

License
This project is licensed under the MIT License.
