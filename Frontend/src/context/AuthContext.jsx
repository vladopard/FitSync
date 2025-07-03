import React, { createContext, useState, useContext, useEffect } from 'react';
import { login as apiLogin, register as apiRegister } from '../services/auth';

const AuthContext = createContext();

export function AuthProvider({ children }) {
  const [user, setUser] = useState(null);

  useEffect(() => {
    const stored = JSON.parse(localStorage.getItem('user'));
    if (stored) setUser(stored);
  }, []);

  async function login(creds) {
    const { data } = await apiLogin(creds);  // data = AuthResultDTO

    if (!data.isSuccess) throw new Error('Login failed');

    const auth = {
      token: data.token,
      userName: data.userName,   
      expiresAt: data.expiresAt,
      roles: data.roles,
    };

    setUser(auth);
    localStorage.setItem('user', JSON.stringify(auth));
  }

  async function register(payload) {
    const { data } = await apiRegister(payload);

    // исто очекујемо AuthResultDTO назад
    if (!data.isSuccess) throw new Error('Register failed');

    const auth = {
      token: data.token,
      userName: data.userName,
      expiresAt: data.expiresAt,
      roles: data.roles,
    };

    setUser(auth);
    localStorage.setItem('user', JSON.stringify(auth));
  }


  function logout() {
    setUser(null);
    localStorage.removeItem('user');
  }

  return (
    <AuthContext.Provider value={{ user, login, register, logout }}>
      {children}
    </AuthContext.Provider>
  );
}

export const useAuth = () => useContext(AuthContext);
