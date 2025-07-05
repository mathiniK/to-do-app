import { useState } from "react";
import { markTaskDone } from "../api";
import type { Task } from "../types";

export default function TaskCard({ task, onDone }: { task: Task; onDone: () => void }) {
  const [checked, setChecked] = useState(false);

  const handleComplete = async () => {
    setChecked(true); // triggers animation
    await new Promise((res) => setTimeout(res, 350)); // wait 300ms
    await markTaskDone(task.id);
    onDone();
  };

  return (
    <div className="task-card">
      <label className="checkbox-wrapper">
        <input
          type="checkbox"
          onChange={handleComplete}
          checked={checked}
          className="custom-checkbox"
        />
        <span className="checkmark"></span>
      </label>

      <div className="task-info">
        <h3 className="task-title">{task.title}</h3>
        <p className="task-desc">{task.description}</p>
      </div>
    </div>
  );
}
