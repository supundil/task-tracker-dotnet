import { useEffect, useState } from "react";
import type { CreateTaskRequest } from "../../types/task";
import { TaskStatus, type TaskResponse } from "../../types/task";
import Button from "../common/Button";
import Input from "../common/Input";
import { toast } from "react-toastify";

interface Props {
  isOpen: boolean;
  onClose: () => void;
  onSubmit: (task: CreateTaskRequest) => Promise<void>;
  editingTask?: TaskResponse | null;
}

export default function TaskForm({
  isOpen,
  onClose,
  onSubmit,
  editingTask,
}: Props) {
  const [form, setForm] = useState<CreateTaskRequest>({
    title: "",
    description: "",
    status: TaskStatus.Pending,
    dueDate: "",
  });

  const [loading, setLoading] = useState(false);

  useEffect(() => {
    if (editingTask) {
      setForm({
        title: editingTask.title,
        description: editingTask.description,
        status: editingTask.status,
        dueDate: editingTask.dueDate.substring(0, 10),
      });
    } else {
      setForm({
        title: "",
        description: "",
        status: TaskStatus.Pending,
        dueDate: "",
      });
    }
  }, [editingTask]);

  function handleChange(
    e: React.ChangeEvent<
      HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement
    >,
  ) {
    const { name, value } = e.target;

    setForm((prev) => ({
      ...prev,
      [name]: name === "status" ? Number(value) : value,
    }));
  }

  async function handleSubmit(e: React.FormEvent) {
    e.preventDefault();

    if (!form.title.trim()) {
      //alert("Title is required.");
      toast.error("Title is required.");
      return;
    }

    try {
      setLoading(true);

      await onSubmit(form);

      onClose();
    } catch (error) {
      console.error(error);
    } finally {
      setLoading(false);
    }
  }

  if (!isOpen) return null;

  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center bg-black/40 p-4">
      <div className="w-full max-w-lg rounded-xl bg-white p-6 shadow-xl">
        <h2 className="mb-6 text-2xl font-bold">
          {editingTask ? "Edit Task" : "Create Task"}
        </h2>

        <form onSubmit={handleSubmit} className="space-y-4">
          <Input
            label="Title"
            name="title"
            value={form.title}
            onChange={handleChange}
          />

          <div>
            <label className="mb-2 block text-sm font-medium">
              Description
            </label>

            <textarea
              name="description"
              value={form.description}
              onChange={handleChange}
              rows={4}
              className="w-full rounded-lg border border-gray-300 p-3 outline-none focus:border-blue-500"
            />
          </div>

          <div>
            <label className="mb-2 block text-sm font-medium">Status</label>

            <select
              name="status"
              value={form.status}
              onChange={handleChange}
              className="w-full rounded-lg border border-gray-300 p-3"
            >
              <option value={TaskStatus.Pending}>Pending</option>

              <option value={TaskStatus.InProgress}>In Progress</option>

              <option value={TaskStatus.Completed}>Completed</option>
            </select>
          </div>

          <Input
            label="Due Date"
            type="date"
            name="dueDate"
            value={form.dueDate}
            onChange={handleChange}
          />

          <div className="flex justify-end gap-3 pt-2">
            <button
              type="button"
              onClick={onClose}
              className="rounded-lg border px-5 py-2"
            >
              Cancel
            </button>

            <Button type="submit" disabled={loading}>
              {loading
                ? "Saving..."
                : editingTask
                  ? "Update Task"
                  : "Create Task"}
            </Button>
          </div>
        </form>
      </div>
    </div>
  );
}
