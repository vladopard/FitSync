// src/components/Navbar.jsx
import React from 'react';
import { NavLink } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';

export default function Navbar() {
  const { user, logout } = useAuth();

  const capitalize = s =>
    s ? s.charAt(0).toUpperCase() + s.slice(1) : '';

  return (
    <nav className="navbar">
      <div className="navbar-container">
        {/* Logo */}
        <NavLink to="/" className="navbar-logo">
          FitSync
        </NavLink>

        {/* Main menu + auth/user controls */}
        <ul className="navbar-menu">
          <li>
            <NavLink to="/" end>
              Home
            </NavLink>
          </li>
          {/* future links:
            <li><NavLink to="/plans">Plans</NavLink></li>
            <li><NavLink to="/workouts">Workouts</NavLink></li>
          */}

          {/* push the user area to the right */}
          {user ? (
            <li className="navbar-user">
              <span>
                Welcome {capitalize(user.userName ?? user.email)}
              </span>
              <button onClick={logout} className="link-btn">
                Logout
              </button>
            </li>
          ) : (
            <>
              <li><NavLink to="/login">Login</NavLink></li>
              <li><NavLink to="/register">Register</NavLink></li>
            </>
          )}
        </ul>
      </div>
    </nav>
  );
}
