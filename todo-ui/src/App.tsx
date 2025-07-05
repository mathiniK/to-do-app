import { useState } from 'react';
import TaskForm from './components/TaskForm';
import TaskList from './components/TaskList';

export default function App() {
  const [refresh, setRefresh] = useState(false);

  return (
    <div className="container">
      <div className="navbar">
        <span>TODO App</span>
      </div>
      <div className="main">
        <div className="card left-panel">
          <div className="card-header">
            Create New Task
            <div className="card-subtext">Add your tasks with title and description</div>
          </div>
          <TaskForm onCreated={() => setRefresh(!refresh)} />
        </div>
        <div className="card right-panel">
          <div className="card-header">
            Your Tasks
            <div className="card-subtext">Manage your tasks efficiently</div>
          </div>
          <TaskList key={refresh.toString()} />
        </div>
      </div>
    </div>
  );
}
