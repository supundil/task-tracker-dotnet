import api from "../api/axios";
import type{
  CreateTaskRequest,
  TaskResponse,
  UpdateTaskRequest,
} from "../types/task";

export const taskService = {
  async getTasks(
    pageNumber = 1,
    pageSize = 10,
    search?: string,
    status?: number
) {

    const response = await api.get("/tasks", {
        params: {
            pageNumber,
            pageSize,
            search,
            status,
        },
    });

    return response.data;
},

  async getTask(id: string) {
    const response = await api.get<TaskResponse>(`/tasks/${id}`);
    return response.data;
  },

  async createTask(data: CreateTaskRequest) {
    const response = await api.post<TaskResponse>("/tasks", data);
    return response.data;
  },

  async updateTask(
    id: string,
    data: UpdateTaskRequest
  ) {
    await api.put(`/tasks/${id}`, data);
  },

  async deleteTask(id: string) {
    await api.delete(`/tasks/${id}`);
  },
};