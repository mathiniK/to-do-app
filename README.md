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
- React + TypeScript + Vite

**Clone the Repository**
git clone https://github.com/mathiniK/to-do-app.git
  
# Ensure Docker Desktop is running
docker-compose up --build<br />  

This will:<br />
Build and run the backend on http://localhost:5044<br />  
Run the frontend on http://localhost:5100<br />  
Set up PostgreSQL for the backend

# Run Tests
**Backend** (Inside the root directory)
docker build -f Dockerfile.test -t todoapi-tests . <br />
docker run --rm todoapi-tests

**Frontend** (Inside the todo-ui)
docker build -f Dockerfile.test -t todo-ui-test .<br /> 
docker run --rm todo-ui-test

# Manual Run (Without Docker)
cd TodoApi<br />
dotnet run

cd todo-ui<br />
npm install<br />
npm run dev

**Author: Mathini Kamalanaathan**
