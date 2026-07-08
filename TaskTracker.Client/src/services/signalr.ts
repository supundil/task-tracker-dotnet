import * as signalR from "@microsoft/signalr";

let connection: signalR.HubConnection | null = null;

export async function getSignalRConnection(token: string) {
  if (connection) {
    return connection;
  }

  connection = new signalR.HubConnectionBuilder()
    // .withUrl("http://localhost:5104/hubs/tasks", {
    //   accessTokenFactory: () => token,
    // })
    .withUrl(import.meta.env.VITE_SIGNALR_URL, {
      accessTokenFactory: () => token,
    })
    .configureLogging(signalR.LogLevel.Warning)
    .withAutomaticReconnect()
    .build();

  await connection.start();

  console.log("✅ SignalR Connected");

  return connection;
}
