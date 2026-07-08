import { TaskStatus, type TaskResponse } from "../../types/task";

interface Props {
  task: TaskResponse | null;
  isOpen: boolean;
  onClose: () => void;
}

function getStatusText(status: TaskStatus) {
  switch (status) {
    case TaskStatus.Pending:
      return "Pending";
    case TaskStatus.InProgress:
      return "In Progress";
    case TaskStatus.Completed:
      return "Completed";
    default:
      return "Unknown";
  }
}

function formatDate(date: string) {
  return new Date(date).toLocaleString("en-GB", {
    day: "2-digit",
    month: "short",
    year: "numeric",
    hour: "2-digit",
    minute: "2-digit",
  });
}

export default function TaskDetails({ task, isOpen, onClose }: Props) {
  if (!isOpen || !task) return null;

  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center bg-black/40 p-4">
      <div className="w-full max-w-lg rounded-xl bg-white p-6 shadow-xl">
        <div className="mb-6 flex items-center justify-between">
          <h2 className="text-2xl font-bold">Task Details</h2>

          <button
            onClick={onClose}
            className="text-2xl text-gray-500 hover:text-black"
          >
            ×
          </button>
        </div>

        <div className="space-y-4">
          <div>
            <p className="text-sm text-gray-500">Title</p>
            <p className="font-semibold">{task.title}</p>
          </div>

          <div>
            <p className="text-sm text-gray-500">Description</p>
            <p>{task.description || "-"}</p>
          </div>

          <div>
            <p className="text-sm text-gray-500">Status</p>
            <p>{getStatusText(task.status)}</p>
          </div>

          <div>
            <p className="text-sm text-gray-500">Due Date</p>
            <p>{formatDate(task.dueDate)}</p>
          </div>

          <div>
            <p className="text-sm text-gray-500">Owner</p>
            <p>{task.ownerName ?? "-"}</p>
          </div>

          <div>
            <p className="text-sm text-gray-500">Created</p>
            <p>{formatDate(task.createdAt)}</p>
          </div>
        </div>

        <div className="mt-8 flex justify-end">
          <button
            onClick={onClose}
            className="rounded-lg bg-blue-600 px-5 py-2 text-white hover:bg-blue-700"
          >
            Close
          </button>
        </div>
      </div>
    </div>
  );
}
