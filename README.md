# LearnForge

> Build. Learn. Improve.

LearnForge is a personal project practice platform and coding playground where I implement concepts, tools, and architectures that I learn during my software engineering journey.

This project is designed to help me strengthen real-world development skills by building practical features using modern technologies and industry-standard practices.

---

## 🚀 About The Project

LearnForge is a developer-focused platform created for:

- Practicing software engineering concepts
- Implementing real-world backend architectures
- Exploring frontend development patterns
- Experimenting with authentication, caching, containerization, and APIs
- Improving problem-solving and coding skills
- Building production-style applications for learning purposes

The goal of this project is simple:

> Learn by building real systems.

---

## 🛠️ Tech Stack

### Backend
- ASP.NET Core Web API
- JWT Authentication
- Redis
- SQL Server

### Frontend
- Angular

### DevOps & Tools
- Docker
- Git & GitHub

---

## ✨ Features

Current and planned features include:

- User Authentication & Authorization
- JWT-based Secure Login System
- Code Practice Modules
- RESTful API Architecture
- Redis Caching
- Dockerized Environment
- Modular Project Structure
- Developer Playground for Experimentation
- Scalable Backend Design

---

## 📁 Project Structure

```bash
LearnForge/
│
├── backend/          # ASP.NET Core Web API
├── frontend/         # Angular Application
├── docker/           # Docker configurations
├── database/         # Database scripts and migrations
└── docs/             # Documentation
```

---

## ⚙️ Installation Guide

### Prerequisites

Make sure you have installed:

- [.NET SDK](https://dotnet.microsoft.com/)
- [Node.js](https://nodejs.org/)
- [Angular CLI](https://angular.io/cli)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/)
- [Docker](https://www.docker.com/)
- [Redis](https://redis.io/)

---

## 🔧 Backend Setup

```bash
cd backend
dotnet restore
dotnet build
dotnet run
```

Backend will run on:

```bash
https://localhost:5001
```

---

## 🎨 Frontend Setup

```bash
cd frontend
npm install
ng serve
```

Frontend will run on:

```bash
http://localhost:4200
```

---

## 🐳 Docker Setup

Run the application using Docker:

```bash
docker-compose up --build
```

---

## 🔐 Environment Variables

Create an `appsettings.Development.json` file inside the backend project.

Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "YOUR_SQL_SERVER_CONNECTION"
  },
  "Jwt": {
    "Key": "YOUR_SECRET_KEY",
    "Issuer": "LearnForge",
    "Audience": "LearnForgeUsers"
  }
}
```

---

## 🎯 Learning Goals

This project is helping me practice:

- Clean Architecture
- REST API Design
- Authentication & Authorization
- Frontend-Backend Integration
- Caching Strategies
- Docker & Containerization
- Scalable Application Design
- Real-world Development Workflow

---

## 🧠 Why LearnForge?

Most tutorials teach concepts in isolation.

LearnForge focuses on applying those concepts in a practical and connected way by building complete systems and experimenting with real engineering challenges.

---

## 🤝 Contribution

This is currently a personal solo learning project.

However, suggestions, ideas, and feedback are always welcome.

---

## 📌 Status

🚧 Under Active Development

---

## 👨‍💻 Author

### Dabananda Mitra

Software Engineer passionate about building scalable applications and continuously improving development skills through hands-on projects.
