Farm Attendance System
📌 Overview
The Farm Attendance System is a Windows Forms (C# .NET 8) application that helps manage and track daily attendance for farmers. It provides CRUD functionality for farmer registration, attendance marking, and report generation.

✅ Features
Farmer Registration

Capture details: Name, ID Number, County, Gender, and Active/Inactive status.

Attendance Management

Mark daily attendance for each farmer.

View total attendance by date.

Farmer Management

Edit or delete farmer details.

Search for farmers using ID or Name.

Reports

Attendance summaries by date, gender, or county.

🛠️ Technical Skills Used
1. Programming & Frameworks
C# (Windows Forms / .NET 8)

ADO.NET (for database connectivity)

SQL Server (Stored procedures for CRUD operations)

2. Tools & IDEs
Visual Studio 2022 (development)

SQL Server Management Studio (SSMS) (database management)

Git & GitHub (version control)

3. Database Management
SQL Server database: FarmDbContext / Farmers table

Stored procedures:

proc_getAllFarmers

proc_getFarmer

proc_AddEditFarmer

📂 Deliverables
Functional Windows Application

Executable: Farm_Attendance_System.exe (in bin\Debug\net8.0-windows)

Database Scripts

Table creation script.

Stored procedure scripts.

Source Code

Full Visual Studio project (.sln, .cs files).

Documentation

User guide (how to add, edit, delete farmers & mark attendance).

⚙️ Installation & Setup
1. Clone the Repository
bash
Copy
Edit
git clone https://github.com/joannekenya/Farm_Attendance_System.git
cd Farm_Attendance_System
2. Configure Database
Create a SQL Server database, e.g., FarmDbContext.

Run the provided SQL scripts to create tables and stored procedures.

3. Update Connection String
In Link.cs or your data access class, set your SQL Server credentials:

csharp
Copy
Edit
conn.ConnectionString = "Data Source=YOUR_SERVER;Initial Catalog=FarmDbContext;User ID=YOUR_USER;Password=YOUR_PASSWORD";
4. Run the Application
Open the solution in Visual Studio 2022.

Press F5 or click Start.

📸 Screenshots
(You can add screenshots of the app UI here for better presentation)
<img width="1337" height="723" alt="farmersattendance" src="https://github.com/user-attachments/assets/61108e43-ff8d-4b66-a37f-8713a7f96c70" />


🤝 Contributing
Contributions are welcome!

Fork the repo.

Create a new branch for your feature.

Submit a pull request.

📞 Contact
phone number: +254718022368
Author: Joan Chemutai
Email: joanchemutai249@gmail.com

