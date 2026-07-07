import { type TaskResponse, TaskStatus} from "../../types/task";
import { TaskStatus as Status } from "../../types/task";
interface Props {
  task: TaskResponse;
  onEdit: (task: TaskResponse) => void;
  onDelete: (id: string) => void;
}

function getStatusText(status: TaskStatus) {
  switch (status) {
    case Status.Pending:
      return "Pending";

    case Status.InProgress:
      return "In Progress";

    case Status.Completed:
      return "Completed";

    default:
      return "Unknown";
  }
}

export default function TaskCard({
  task,
  onEdit,
  onDelete,
}: Props) {
  return (
    <div className="rounded-xl border bg-white p-5 shadow-sm">
      <div className="flex items-start justify-between">
        <div>
          <h3 className="text-lg font-bold">
            {task.title}
          </h3>

          <p className="mt-2 text-gray-500">
            {task.description}
          </p>
        </div>

        <span className="rounded bg-blue-100 px-3 py-1 text-sm">
          {getStatusText(task.status)}
        </span>
      </div>

      <div className="mt-4 flex justify-end gap-3">
        <button
          onClick={() => onEdit(task)}
          className="rounded bg-yellow-500 px-4 py-2 text-white"
        >
          Edit
        </button>

        <button
          onClick={() => onDelete(task.id)}
          className="rounded bg-red-500 px-4 py-2 text-white"
        >
          Delete
        </button>
      </div>
    </div>
  );
}