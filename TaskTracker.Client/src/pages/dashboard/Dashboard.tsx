import { useEffect, useState } from "react";

import Navbar from "../../components/layout/Navbar";
import TaskList from "../../components/task/TaskList";
import TaskForm from "../../components/task/TaskForm";

import { taskService } from "../../services/taskService";

import type{
  CreateTaskRequest,
  TaskResponse,
  UpdateTaskRequest,
} from "../../types/task";

export default function Dashboard() {
  const [tasks, setTasks] = useState<TaskResponse[]>([]);

  const [loading, setLoading] = useState(true);

  const [showForm, setShowForm] = useState(false);

  const [editingTask, setEditingTask] =
    useState<TaskResponse | null>(null);

  async function loadTasks() {
    try {
      setLoading(true);

      const data = await taskService.getTasks();

      setTasks(data);
    } catch (error) {
      console.error(error);

      alert("Failed to load tasks.");
    } finally {
      setLoading(false);
    }
  }

  useEffect(() => {
    loadTasks();
  }, []);

  async function handleCreate(
    request: CreateTaskRequest
  ) {
    await taskService.createTask(request);

    await loadTasks();
  }

  async function handleUpdate(
    request: UpdateTaskRequest
  ) {
    if (!editingTask) return;

    await taskService.updateTask(
      editingTask.id,
      request
    );

    setEditingTask(null);

    await loadTasks();
  }

  async function handleDelete(id: string) {
    const confirmed = window.confirm(
      "Delete this task?"
    );

    if (!confirmed) return;

    await taskService.deleteTask(id);

    await loadTasks();
  }

  function handleEdit(task: TaskResponse) {
    setEditingTask(task);

    setShowForm(true);
  }

  function handleCreateClick() {
    setEditingTask(null);

    setShowForm(true);
  }

  return (
    <>
      <Navbar />

      <main className="mx-auto max-w-6xl p-6">
        <div className="mb-6 flex items-center justify-between">
          <h1 className="text-3xl font-bold">
            My Tasks
          </h1>

          <button
            onClick={handleCreateClick}
            className="rounded-lg bg-blue-600 px-5 py-3 text-white hover:bg-blue-700"
          >
            + New Task
          </button>
        </div>

        {loading ? (
          <div className="rounded-xl bg-white p-10 text-center shadow">
            Loading...
          </div>
        ) : (
          <TaskList
            tasks={tasks}
            onEdit={handleEdit}
            onDelete={handleDelete}
          />
        )}

        <TaskForm
          isOpen={showForm}
          onClose={() => {
            setShowForm(false);

            setEditingTask(null);
          }}
          editingTask={editingTask}
          onSubmit={
            editingTask
              ? handleUpdate
              : handleCreate
          }
        />
      </main>
    </>
  );
}