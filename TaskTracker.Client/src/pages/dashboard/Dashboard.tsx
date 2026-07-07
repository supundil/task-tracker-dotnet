import Navbar from "../../components/layout/Navbar";

export default function Dashboard() {
  return (
    <>
      <Navbar />

      <main className="mx-auto max-w-7xl p-8">
        <div className="rounded-xl bg-white p-8 shadow">
          <h2 className="text-3xl font-bold">
            Dashboard
          </h2>

          <p className="mt-3 text-gray-500">
            Authentication successful.

            <br />

            Task Management module will be added next.
          </p>
        </div>
      </main>
    </>
  );
}