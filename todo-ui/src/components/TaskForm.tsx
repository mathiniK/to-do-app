import { useState } from "react";
import { createTask } from "../api";
import { FiPlus } from "react-icons/fi";

export default function TaskForm({ onCreated }: { onCreated: () => void }) {
  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    await createTask({ title, description });
    setTitle("");
    setDescription("");
    onCreated();
  };

  return (
    <div className="card left-panel">
      <form onSubmit={handleSubmit}>
        <h2>Add Task</h2>
        <label>
          Title
          <input
            value={title}
            onChange={(e) => setTitle(e.target.value)}
            placeholder="Title"
            required
          />
        </label>
        <label>
          Description
          <textarea
            value={description}
            onChange={(e) => setDescription(e.target.value)}
            placeholder="Add details about this task..."
            rows={4}
          />
        </label>
        <button className="add-task-button" type="submit">
          <FiPlus style={{ marginRight: "0.5rem" }} />
          Add Task
        </button>{" "}
      </form>
    </div>
  );
}
