import React, { useState } from 'react';
import { useAuth } from '../context/AuthContext';
import { useNavigate } from 'react-router-dom';
import '../styles/pages/AuthForm.css';

export default function Login() {
  const { login } = useAuth();
  const navigate  = useNavigate();

  const [form, setForm] = useState({
    identifier: '',
    password: '',
  });
  const [error, setError] = useState(null);

  const handleChange = e =>
    setForm({ ...form, [e.target.name]: e.target.value });

  async function handleSubmit(e) {
    e.preventDefault();
    try {
      await login(form);      // ➜ { identifier, password }
      navigate('/');          // или '/plans'
    } catch (err) {
      setError(err.message);
    }
  }

  return (
    <div className="form-container">
      <h2>Login</h2>
      {error && <p className="form-error">{error}</p>}
      <form onSubmit={handleSubmit}>
        <input
          type="text"
          name="identifier"
          placeholder="Email or Username"
          value={form.identifier}
          onChange={handleChange}
          required
        />

        <input
          type="password"
          name="password"
          placeholder="Password"
          value={form.password}
          onChange={handleChange}
          required
        />

        <button className="btn-primary">Sign in</button>
      </form>
    </div>
  );
}
