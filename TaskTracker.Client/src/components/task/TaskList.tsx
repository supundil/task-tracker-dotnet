import TaskCard from "./TaskCard";
import type{ TaskResponse } from "../../types/task";

interface Props {
  tasks: TaskResponse[];
  onEdit: (task: TaskResponse) => void;
  onDelete: (id: string) => void;
}

export default function TaskList({
  tasks,
  onEdit,
  onDelete,
}: Props) {
  if (tasks.length === 0) {
    return (
      <div className="rounded-xl bg-white p-10 text-center shadow">
        <h2 className="text-xl font-semibold">
          No Tasks Found
        </h2>

        <p className="mt-2 text-gray-500">
          Create your first task.
        </p>
      </div>
    );
  }

  return (
    <div className="grid gap-5">
      {tasks.map((task) => (
        <TaskCard
          key={task.id}
          task={task}
          onEdit={onEdit}
          onDelete={onDelete}
        />
      ))}
    </div>
  );
}