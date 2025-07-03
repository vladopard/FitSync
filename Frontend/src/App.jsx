// src/App.jsx
import React from 'react';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import Layout   from './components/Layout';
import Home     from './pages/Home';
import Login    from './pages/Login';
import Register from './pages/Register';

export default function App() {
  return (
    <BrowserRouter>
      <Routes>
        {/* Svi putevi idu kroz Layout (Navbar + Outlet) */}
        <Route path="/" element={<Layout />}>
          {/* index == "/" */}
          <Route index element={<Home />} />

          {/* javne stranice */}
          <Route path="login"    element={<Login />} />
          <Route path="register" element={<Register />} />

          {/* ovde kasnije možeš dodati plans, workouts... */}
        </Route>
      </Routes>
    </BrowserRouter>
  );
}
