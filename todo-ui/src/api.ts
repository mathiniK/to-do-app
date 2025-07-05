import axios from "axios";

const API = axios.create({
  baseURL: "http://localhost:5044/api", 
});

export const getTasks = () => API.get("/tasks");
export const createTask = (data: { title: string; description: string }) =>
  API.post("/tasks", data);
export const markTaskDone = (id: number) => API.patch(`/tasks/${id}/done`);
