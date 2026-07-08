import { useEffect, useState } from "react";

import Navbar from "../../components/layout/Navbar";
import TaskList from "../../components/task/TaskList";
import TaskForm from "../../components/task/TaskForm";
import { toast } from "react-toastify";
import useSignalR from "../../hooks/useSignalR";
import TaskDetails from "../../components/task/TaskDetails";

import { taskService } from "../../services/taskService";

import {
  TaskStatus,
  type CreateTaskRequest,
  type TaskResponse,
  type UpdateTaskRequest,
} from "../../types/task";

export default function Dashboard() {
  const [tasks, setTasks] = useState<TaskResponse[]>([]);

  const [loading, setLoading] = useState(true);

  const [showForm, setShowForm] = useState(false);

  const [search, setSearch] = useState("");

  const [pageNumber, setPageNumber] = useState(1);

  const PAGE_SIZE = 10;
  const [selectedStatus, setSelectedStatus] = useState<number | undefined>();

  const [editingTask, setEditingTask] = useState<TaskResponse | null>(null);

  const [selectedTask, setSelectedTask] = useState<TaskResponse | null>(null);

  const [showDetails, setShowDetails] = useState(false);

  async function loadTasks() {
    try {
      setLoading(true);

      //const data = await taskService.getTasks();

      const data = await taskService.getTasks(
        pageNumber,
        PAGE_SIZE,
        search,
        selectedStatus,
      );

      setTasks(data);
    } catch (error) {
      console.error(error);

      //alert("Failed to load tasks.");
      toast.error("Failed to load tasks.");
    } finally {
      setLoading(false);
    }
  }

  useSignalR(loadTasks);

  useEffect(() => {
    loadTasks();
  }, [pageNumber, search, selectedStatus]);

  useEffect(() => {
    setPageNumber(1);
  }, [search]);

  useEffect(() => {
    setPageNumber(1);
  }, [selectedStatus]);

  async function handleCreate(request: CreateTaskRequest) {
    try {
      setLoading(true);

      await taskService.createTask(request);

      toast.success("Task created successfully.");
      setShowForm(false);
      await loadTasks();
    } catch (error) {
      console.error(error);

      toast.error("Failed to create task.");
    } finally {
      setLoading(false);
    }
  }

  async function handleUpdate(request: UpdateTaskRequest) {
    if (!editingTask) return;

    try {
      setLoading(true);

      await taskService.updateTask(editingTask.id, request);

      toast.success("Task updated successfully.");

      setEditingTask(null);
      setShowForm(false);
      await loadTasks();
    } catch (error) {
      console.error(error);

      toast.error("Failed to update task.");
    } finally {
      setLoading(false);
    }
  }

  async function handleDelete(id: string) {
    const confirmed = window.confirm("Delete this task?");

    if (!confirmed) return;

    try {
      setLoading(true);

      await taskService.deleteTask(id);

      toast.success("Task deleted successfully.");

      await loadTasks();
    } catch (error) {
      console.error(error);

      toast.error("Failed to delete task.");
    } finally {
      setLoading(false);
    }
  }

  function handleEdit(task: TaskResponse) {
    setEditingTask(task);

    setShowForm(true);
  }

  function handleView(task: TaskResponse) {
    setSelectedTask(task);
    setShowDetails(true);
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
          <h1 className="text-3xl font-bold">My Tasks</h1>

          <button
            onClick={handleCreateClick}
            className="rounded-lg bg-blue-600 px-5 py-3 text-white hover:bg-blue-700"
          >
            + New Task
          </button>
        </div>
        <div className="mb-6 flex flex-col gap-4 md:flex-row">
          <input
            type="text"
            placeholder="Search tasks..."
            value={search}
            onChange={(e) => setSearch(e.target.value)}
            className="flex-1 rounded-lg border border-gray-300 p-3"
          />

          <select
            value={selectedStatus ?? ""}
            onChange={(e) =>
              setSelectedStatus(
                e.target.value ? Number(e.target.value) : undefined,
              )
            }
            className="rounded-lg border border-gray-300 p-3"
          >
            <option value="">All Statuses</option>

            <option value={TaskStatus.Pending}>Pending</option>

            <option value={TaskStatus.InProgress}>In Progress</option>

            <option value={TaskStatus.Completed}>Completed</option>
          </select>
        </div>

        {loading ? (
          <div className="rounded-xl bg-white p-12 text-center shadow">
            <div className="mx-auto h-10 w-10 animate-spin rounded-full border-4 border-blue-600 border-t-transparent"></div>

            <p className="mt-4 text-gray-500">Loading tasks...</p>
          </div>
        ) : (
          <TaskList
            tasks={tasks}
            onView={handleView}
            onEdit={handleEdit}
            onDelete={handleDelete}
          />
        )}
        <div className="mt-6 flex justify-center gap-3">
          <button
            disabled={pageNumber === 1}
            onClick={() => setPageNumber((p) => p - 1)}
            className="rounded-lg border px-4 py-2 disabled:opacity-50"
          >
            Previous
          </button>

          <span className="flex items-center font-medium">
            Page {pageNumber}
          </span>

          <button
            onClick={() => setPageNumber((p) => p + 1)}
            className="rounded-lg border px-4 py-2"
          >
            Next
          </button>
        </div>

        <TaskForm
          isOpen={showForm}
          onClose={() => {
            setShowForm(false);

            setEditingTask(null);
          }}
          editingTask={editingTask}
          onSubmit={editingTask ? handleUpdate : handleCreate}
        />
        <TaskDetails
          task={selectedTask}
          isOpen={showDetails}
          onClose={() => {
            setShowDetails(false);
            setSelectedTask(null);
          }}
        />
      </main>
    </>
  );
}
