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

**Clone the Repository**
  
# Ensure Docker Desktop is running
docker-compose up --build
This will:
Build and run the backend on http://localhost:5044
Run the frontend on http://localhost:5100
Set up PostgreSQL for the backend

# Run Tests
**Backend** (Inside the root directory)
docker build -f Dockerfile.test -t todoapi-tests .
docker run --rm todoapi-tests

**Frontend** (Inside the todo-ui)
docker build -f Dockerfile.test -t todo-ui-test .
docker run --rm todo-ui-test

# Manual Run (Without Docker)
cd TodoApi
dotnet run

cd todo-ui
npm install
npm run dev

**Author: Mathini Kamalanaathan**
