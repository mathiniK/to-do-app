# ToDo App 

A fullstack ToDo application built with:
- Frontend: React + TypeScript + CSS + Vite (in `todo-ui`)
- Backend: ASP.NET Core Web API (.NET 9) (in `TodoApi`)
- Tests: xUnit-based unit/integration tests (in `TodoApi.Tests`)
- Containerization: Docker

# Prerequisites
- [.NET 9 SDK]
- [Node.js (v18+)](https://nodejs.org/)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)

# Run Tests
dotnet test TodoApi.Tests

# Ensure Docker Desktop is running
docker-compose up --build

# Manual Run (Without Docker)
cd TodoApi
dotnet run

cd todo-ui
npm install
npm run dev

Author: Mathini Kamalanaathan
