# EaseUp – Mental Health & Wellbeing Backend API

A scalable RESTful backend built with **ASP.NET Core 9** and structured using **Clean Architecture** principles. EaseUp is designed to support students, supervisors, doctors, and administrators through a comprehensive mental health and wellbeing platform featuring AI-driven assessments, real-time communication, gamification, and progress tracking.

---

# 🏛️ System Architecture

The solution follows a layered **Clean Architecture** approach and is divided into four main projects:

```text
Software_project_V2/
├── API/                  → Presentation Layer (Controllers, SignalR Hubs)
├── Application/          → Business Logic (CQRS, MediatR, DTOs, Interfaces)
├── Domain/               → Core Entities and Business Models
└── Infrastructure/       → EF Core, Repositories, Services, Migrations
```

---

# 🚀 Technologies Used

| Category                | Technology                           |
| ----------------------- | ------------------------------------ |
| Framework               | ASP.NET Core 9                       |
| Database                | SQL Server                           |
| ORM                     | Entity Framework Core                |
| Authentication          | ASP.NET Identity + JWT               |
| Architecture            | Clean Architecture + CQRS            |
| Mediator Pattern        | MediatR                              |
| Real-time Communication | SignalR                              |
| OAuth Providers         | Google OAuth 2.0, LinkedIn OAuth 2.0 |
| API Documentation       | Swagger / OpenAPI                    |
| Testing                 | xUnit                                |

---

# ✨ Core Features

## 🔐 Authentication & Authorization

* JWT-based authentication
* User registration and login
* Google & LinkedIn OAuth integration
* Role-based authorization:

  * Student
  * Admin
  * Supervisor
  * Doctor
* Email and password management

---

## 👨‍🎓 Student Functionality

* Daily mood journaling
* Goal creation and progress tracking
* Checklist item management
* Exercise browsing and completion
* AI-powered mental health survey integration
* Dashboard analytics with mental health insights and weekly sentiment tracking

---

## 🛠️ Admin Functionality

* Student monitoring and management
* Admin dashboard and overview
* Student notes and observations
* Add and manage administrators

---

## 🏆 Gamification System

* Achievement unlocking
* Points and reward tracking
* User progress monitoring

---

## 💬 Real-Time Communication

* Real-time messaging using SignalR
* Instant notifications
* Notification management and retrieval

---

## 📊 Assessments & Surveys

* Survey creation and response handling
* Assessment management
* AI survey result storage and analysis

---

## 👤 Profile Management

* Student and supervisor profile updates
* Profile image uploading
* Personal information management

---

# 📡 API Overview

| Controller             | Route               | Responsibility                 |
| ---------------------- | ------------------- | ------------------------------ |
| AuthController         | `/api/auth`         | Authentication & OAuth         |
| StudentsController     | `/api/students`     | Mood tracking & dashboard      |
| GoalsController        | `/api/goals`        | Goals and checklist management |
| ExercisesController    | `/api/exercises`    | Exercise operations            |
| GamificationController | `/api/gamification` | Achievements & points          |
| NotificationController | `/api/notification` | Notifications                  |
| ChatController         | `/api/chat`         | Chat messaging                 |
| AssessmentsController  | `/api/assessments`  | Assessments                    |
| SurveyController       | `/api/survey`       | Survey responses               |
| AdminsController       | `/api/admins`       | Admin operations               |
| ProfileController      | `/api/profile`      | User profiles                  |
| HomeController         | `/api/home`         | Home dashboard data            |

---

# ⚡ SignalR Hubs

| Hub             | Endpoint              |
| --------------- | --------------------- |
| NotificationHub | `/hubs/notifications` |
| ChatHub         | `/hubs/chat`          |

---

# ⚙️ Getting Started

## Prerequisites

Before running the project, make sure you have:

* .NET 9 SDK
* SQL Server (LocalDB or full SQL Server instance)
* Visual Studio 2022+ or VS Code

---

## Installation & Setup

### 1. Clone the Repository

```bash
git clone https://github.com/your-username/your-repo.git
cd your-repo
```

---

### 2. Configure the Database

Update the connection string inside:

```text
API/appsettings.json
```

```json
"ConnectionStrings": {
  "default": "Data Source=YOUR_SERVER;Initial Catalog=SOFWARE_PROJECT;Integrated Security=True;Trust Server Certificate=True"
}
```

---

### 3. Configure JWT Authentication

```json
"Jwt": {
  "Key": "YOUR_SECRET_KEY",
  "Issuer": "YourIssuer",
  "Audience": "YourAudience"
}
```

---

### 4. Configure OAuth Providers

Add your Google and LinkedIn OAuth credentials inside `appsettings.json`.

---

### 5. Apply Database Migrations

```bash
cd API
dotnet ef database update
```

---

### 6. Run the Application

```bash
dotnet run --project API
```

The API will run locally at:

```text
https://localhost:7057
```

(or the port configured in `launchSettings.json`).

---

# 🧪 Running Unit Tests

```bash
dotnet test
```

The test suite includes handlers such as:

* `CompleteExerciseHandlerTests`
* `CreateNotificationHandlerTests`

---

# 📁 Internal Project Structure

## Application Layer

```text
Application/
├── Features/        → CQRS Commands & Queries
├── DTO/             → Data Transfer Objects
├── Interfaces/      → Contracts & Abstractions
└── Common/          → Shared Configurations & Utilities
```

---

## Domain Layer

```text
Domain/
└── Models/          → Core Business Entities
```

Examples:

* Student
* Goal
* Exercise
* MentalHealth
* Survey
* Notification

---

## Infrastructure Layer

```text
Infrastructure/
├── Data/            → DbContext & EF Core Configuration
├── Identity/        → ASP.NET Identity Models
├── Implementation/  → Services & Repository Implementations
├── SignalR/         → Real-time Hubs
└── Migrations/      → Database Migrations
```

---

# 🔒 Security Recommendations

Before deploying to production:

* Store JWT secrets securely using environment variables or a secrets manager
* Remove hardcoded OAuth credentials from configuration files
* Enforce HTTPS
* Configure a secure CORS policy
* Validate and sanitize incoming requests

---

# 🤝 Contributing

Contributions are welcome.
For major changes, please open an issue first to discuss the proposed improvements.

---

# 📄 License

This project is intended for academic and educational purposes.
