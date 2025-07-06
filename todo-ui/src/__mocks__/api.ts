import { vi } from "vitest";

let mockTasks = [
  {
    id: 1,
    title: "Mocked Task",
    description: "mocked",
    done: false,
  },
];

export const getTasks = vi.fn(() =>
  Promise.resolve({ data: [...mockTasks] })
);

export const createTask = vi.fn((data: { title: string; description: string }) => {
  const newTask = {
    id: mockTasks.length + 1,
    done: false,
    ...data,
  };
  mockTasks.push(newTask);
  return Promise.resolve({ success: true });
});

export const markTaskDone = vi.fn((id: number) => {
  mockTasks = mockTasks.map((task) =>
    task.id === id ? { ...task, done: true } : task
  );
  return Promise.resolve({ success: true });
});

export const __resetMockTasks = () => {
  mockTasks = [
    {
      id: 1,
      title: "Mocked Task",
      description: "mocked",
      done: false,
    },
  ];
};
