import { useEffect } from "react";

import { getSignalRConnection } from "../services/signalr";
import { storage } from "../utils/storage";

export default function useSignalR(refreshTasks: () => void) {
  useEffect(() => {
    let connection: any;

    async function connect() {
      const token = storage.getToken();

      if (!token) return;

      connection = await getSignalRConnection(token);

      connection.on("TaskCreated", () => {
        refreshTasks();
      });

      connection.on("TaskUpdated", () => {
        refreshTasks();
      });

      connection.on("TaskDeleted", () => {
        refreshTasks();
      });
    }

    connect();

    return () => {
      if (!connection) return;

      connection.off("TaskCreated");

      connection.off("TaskUpdated");

      connection.off("TaskDeleted");
    };
  }, [refreshTasks]);
}
