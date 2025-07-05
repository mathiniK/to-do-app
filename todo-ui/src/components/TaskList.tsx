import { useEffect, useState } from "react";
import { getTasks } from "../api";
import TaskCard from "./TaskCard";
import type { Task } from "../types";
import { FiClipboard } from "react-icons/fi";

export default function TaskList() {
  const [tasks, setTasks] = useState<Task[]>([]);

  const fetchTasks = async () => {
    const res = await getTasks();
    setTasks(res.data);
  };

  useEffect(() => {
    fetchTasks();
  }, []);

  return (
    <div>
      {tasks.length === 0 ? (
        <div className="empty-state">
          <div className="empty-icon">
            <FiClipboard size={48} color="#2C4880" />
          </div>
          <p className="empty-title">No tasks yet</p>
        </div>
      ) : (
        tasks.map((task) => (
          <TaskCard key={task.id} task={task} onDone={fetchTasks} />
        ))
      )}
    </div>
  );
}
