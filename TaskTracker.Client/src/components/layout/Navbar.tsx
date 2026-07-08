import { useNavigate } from "react-router-dom";
import { useAuth } from "../../context/AuthContext";

export default function Navbar() {
  const navigate = useNavigate();

  const { logout } = useAuth();

  function handleLogout() {
    const confirmed = window.confirm("Are you sure you want to log out?");

    if (!confirmed) return;

    logout();

    navigate("/");
  }

  return (
    <nav className="flex items-center justify-between border-b bg-white px-8 py-4 shadow-sm">
      <h1 className="text-xl font-bold text-blue-600">Task Tracker</h1>

      <button
        onClick={handleLogout}
        className="rounded bg-red-500 px-4 py-2 text-white hover:bg-red-600"
      >
        Logout
      </button>
    </nav>
  );
}
