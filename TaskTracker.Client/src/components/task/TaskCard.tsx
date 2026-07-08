import { type TaskResponse, TaskStatus } from "../../types/task";
//import { TaskStatus as Status } from "../../types/task";
interface Props {
  task: TaskResponse;
  onView: (task: TaskResponse) => void;
  onEdit: (task: TaskResponse) => void;
  onDelete: (id: string) => void;
}

// function getStatusText(status: TaskStatus) {
//   switch (status) {
//     case Status.Pending:
//       return "Pending";

//     case Status.InProgress:
//       return "In Progress";

//     case Status.Completed:
//       return "Completed";

//     default:
//       return "Unknown";
//   }
// }

function formatDate(date: string) {
  return new Date(date).toLocaleDateString("en-GB", {
    day: "2-digit",
    month: "short",
    year: "numeric",
  });
}

function getStatusBadge(status: TaskStatus) {
  switch (status) {
    case TaskStatus.Pending:
      return (
        <span className="rounded-full bg-yellow-100 px-3 py-1 text-sm font-medium text-yellow-800">
          Pending
        </span>
      );

    case TaskStatus.InProgress:
      return (
        <span className="rounded-full bg-blue-100 px-3 py-1 text-sm font-medium text-blue-800">
          In Progress
        </span>
      );

    case TaskStatus.Completed:
      return (
        <span className="rounded-full bg-green-100 px-3 py-1 text-sm font-medium text-green-800">
          Completed
        </span>
      );

    default:
      return (
        <span className="rounded-full bg-gray-100 px-3 py-1 text-sm">
          Unknown
        </span>
      );
  }
}

export default function TaskCard({ task, onView, onEdit, onDelete }: Props) {
  return (
    <div
      onClick={() => onView(task)}
      className="cursor-pointer rounded-xl border bg-white p-5 shadow-sm transition hover:shadow-md"
    >
      <div className="flex items-start justify-between">
        <div>
          <h3 className="text-lg font-bold">{task.title}</h3>

          <p className="mt-2 text-gray-500">{task.description}</p>

          <p className="mt-3 text-sm text-gray-500">
            Due: {formatDate(task.dueDate)}
          </p>
        </div>

        {/* <span className="rounded bg-blue-100 px-3 py-1 text-sm">
          {getStatusBadge(task.status)}
        </span> */}
        {getStatusBadge(task.status)}
      </div>

      <div className="mt-4 flex justify-end gap-3">
        <button
          onClick={(e) => {
            e.stopPropagation();
            onEdit(task);
          }}
          className="rounded bg-yellow-500 px-4 py-2 text-white"
        >
          Edit
        </button>

        <button
          onClick={(e) => {
            e.stopPropagation();
            onDelete(task.id);
          }}
          className="rounded bg-red-500 px-4 py-2 text-white"
        >
          Delete
        </button>
      </div>
    </div>
  );
}
