import React, { useState } from 'react';
import { useAuth } from '../context/AuthContext';
import { useNavigate } from 'react-router-dom';
import '../styles/pages/AuthForm.css';

export default function Register() {
  const { register } = useAuth();
  const navigate      = useNavigate();

  const [form, setForm] = useState({
    email: '',
    userName: '',
    password: '',
    confirmPassword: '',
  });
  const [error, setError] = useState(null);

  const handleChange = e =>
    setForm({ ...form, [e.target.name]: e.target.value });

  async function handleSubmit(e) {
    e.preventDefault();

    if (form.password !== form.confirmPassword) {
      setError('Passwords do not match');
      return;
    }

    const payload = {
      email: form.email,
      userName: form.userName,
      password: form.password,
    };

    try {
      await register(payload);   // ➜ { email, userName, password }
      navigate('/');             // или '/plans'
    } catch (err) {
      setError(err.message);
    }
  }

  return (
    <div className="form-container">
      <h2>Create Account</h2>
      {error && <p className="form-error">{error}</p>}
      <form onSubmit={handleSubmit}>
        <input
          type="email"
          name="email"
          placeholder="Email"
          value={form.email}
          onChange={handleChange}
          required
        />

        <input
          type="text"
          name="userName"
          placeholder="Username"
          value={form.userName}
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

        <input
          type="password"
          name="confirmPassword"
          placeholder="Confirm Password"
          value={form.confirmPassword}
          onChange={handleChange}
          required
        />

        <button className="btn-primary">Register</button>
      </form>
    </div>
  );
}
