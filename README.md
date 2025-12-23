# 📧 TempMailX

TempMailX is a lightweight ASP.NET Core MVC application that generates temporary email addresses, stores them securely, auto-expires them using a background service, and provides application logs using Serilog.

---

## 🚀 Features
- Temporary Email Generation
- Email Expiry Background Service
- SQL Server (LocalDB) Integration
- MVC Architecture
- Serilog File Logging
- Admin Log Viewer UI
- Bootstrap 5 Responsive UI

---

## 🛠 Tech Stack
- ASP.NET Core MVC (.NET 8)
- SQL Server LocalDB
- Serilog
- Bootstrap 5

---

## 📂 Project Structure
TempMailX/
│── Controllers/
│── Models/
│── Data/
│── Services/
│── Background/
│── Views/
│── Logs/
│── Program.cs

Browser
   |
   v
ASP.NET Core MVC
(Controllers)
   |
   v
Services (EmailGenerator, ExpiryService)
   |
   v
DAL (TempEmailDAL)
   |
   v
SQL Server (LocalDB)
   |
   v
Serilog Logs (File)



---

## ▶ How to Run
1. Clone the repo
2. Open in Visual Studio 2022
3. Run the project
4. Navigate to  
   `/Mail/Create` – Generate temp email  
   `/Mail/Inbox` – View emails  
   `/Logs/Index` – View logs

---

## 📌 Author
**Divyansh Singh**
