import { createContext, useContext, useEffect, useState } from "react";
import { storage } from "../utils/storage";

interface AuthContextType {
  isAuthenticated: boolean;
  token: string | null;
  login: (token: string) => void;
  logout: () => void;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export function AuthProvider({
  children,
}: {
  children: React.ReactNode;
}) {
const [token, setToken] = useState<string | null>(
  storage.getToken()
);
const [isAuthenticated, setIsAuthenticated] =
  useState(storage.isAuthenticated());

  useEffect(() => {
    setIsAuthenticated(storage.isAuthenticated());
  }, []);

  const login = (jwt: string) => {
  storage.setToken(jwt);
  setToken(jwt);
  setIsAuthenticated(true);
  };

  const logout = () => {
  storage.removeToken();
  setToken(null);
  setIsAuthenticated(false);
  };

  return (
    <AuthContext.Provider
      value={{
        isAuthenticated,
        token,
        login,
        logout,
      }}
    >
      {children}
    </AuthContext.Provider>
  );
}

export function useAuth() {
  const context = useContext(AuthContext);

  if (!context)
    throw new Error("useAuth must be used inside AuthProvider");

  return context;
}