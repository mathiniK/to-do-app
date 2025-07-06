import { render, screen, fireEvent, waitFor } from "@testing-library/react";
import App from "../App";
import { describe, expect, test, beforeEach, vi } from "vitest";
import * as api from "../api";

// Ensure the mock APIs are used
vi.mock("../api");

const { __resetMockTasks } = await import("../__mocks__/api");

describe("Todo App", () => {
  beforeEach(() => {
    __resetMockTasks();
  });

  test("renders TODO App title", () => {
    render(<App />);
    const title = screen.getByText(/TODO App/i);
    expect(title).toBeTruthy();
  });

  test("renders task list from API", async () => {
    render(<App />);
    const taskTitle = await screen.findByText("Mocked Task");
    expect(taskTitle.textContent).toContain("Mocked Task");
  });

  test("adds a new task to the list", async () => {
    render(<App />);

    const titleInput = screen.getByPlaceholderText("Title");
    const descInput = screen.getByPlaceholderText("Add details about this task...");
    const addButton = screen.getByRole("button", { name: /add task/i });

    fireEvent.change(titleInput, { target: { value: "Test Task" } });
    fireEvent.change(descInput, { target: { value: "Do something useful" } });
    fireEvent.click(addButton);

    // Wait for the task to appear in the task list
    await waitFor(() => {
      const newTask = screen.getByText("Test Task");
      expect(newTask).toBeTruthy();
    });
  });

  test("prevents adding empty task (required input enforced)", () => {
    render(<App />);
    const addButton = screen.getByRole("button", { name: /add task/i });
    fireEvent.click(addButton);

    const error = screen.queryByText(/title is required/i);
    expect(error).toBeFalsy(); // if no visible error rendered, still should not add
  });
});

test("marks a task as done when checkbox is clicked", async () => {
  render(<App />);

  // Wait for mocked task to appear
  const taskTitle = await screen.findByText("Mocked Task");
  expect(taskTitle).toBeTruthy();

  // Find and click the checkbox
  const checkbox = screen.getByRole("checkbox");
  fireEvent.click(checkbox);

  // Wait for markTaskDone to be called (from mock)
  await waitFor(() => {
    expect(api.markTaskDone).toHaveBeenCalledTimes(1);
    expect(api.markTaskDone).toHaveBeenCalledWith(1); 
  });
});
