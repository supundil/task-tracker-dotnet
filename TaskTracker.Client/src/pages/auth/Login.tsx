import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";

import Button from "../../components/common/Button";
import Input from "../../components/common/Input";

import { authService } from "../../services/authService";
import { useAuth } from "../../context/AuthContext";
import { toast } from "react-toastify";

export default function Login() {
  const navigate = useNavigate();

  const { login } = useAuth();

  const [email, setEmail] = useState("");

  const [password, setPassword] = useState("");

  const [loading, setLoading] = useState(false);

  async function handleSubmit(e: React.FormEvent) {
    e.preventDefault();

    try {
      setLoading(true);

      const response = await authService.login({
        email,
        password,
      });

      login(response.accessToken);

      navigate("/dashboard");
    } catch (error) {
      console.error(error);

      toast.error("Invalid credentials.");
    } finally {
      setLoading(false);
    }
  }

  return (
    <div className="flex min-h-screen items-center justify-center bg-slate-100 p-4">
      <div className="w-full max-w-md rounded-xl bg-white p-8 shadow-lg">
        <h1 className="mb-6 text-center text-3xl font-bold">Task Tracker</h1>

        <form onSubmit={handleSubmit} className="space-y-4">
          <Input
            label="Email"
            type="email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
          />

          <Input
            label="Password"
            type="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />

          <Button type="submit" disabled={loading}>
            {loading ? "Signing In..." : "Login"}
          </Button>
        </form>

        <p className="mt-5 text-center text-sm">
          Don't have an account?
          <Link className="ml-1 text-blue-600" to="/register">
            Register
          </Link>
        </p>
      </div>
    </div>
  );
}
