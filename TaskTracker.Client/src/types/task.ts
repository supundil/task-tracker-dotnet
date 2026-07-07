export const TaskStatus = {
  Pending: 1 as const,
  InProgress: 2 as const,
  Completed: 3 as const,
};

export type TaskStatus = (typeof TaskStatus)[keyof typeof TaskStatus];

export interface TaskResponse {
  id: string;
  title: string;
  description: string;
  status: TaskStatus;
  dueDate: string;
  ownerId: string;
  ownerName?: string;
  createdAt: string;
}

export interface CreateTaskRequest {
  title: string;
  description: string;
  status: TaskStatus;
  dueDate: string;
}

export interface UpdateTaskRequest {
  title: string;
  description: string;
  status: TaskStatus;
  dueDate: string;
}