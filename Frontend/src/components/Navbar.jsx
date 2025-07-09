// src/components/Navbar.jsx
import React, { useState } from 'react';
import { NavLink } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';

export default function Navbar() {
  const { user, logout } = useAuth();
  const [menuOpen, setMenuOpen] = useState(false);

  const toggleMenu = () => setMenuOpen(o => !o);
  const closeMenu = () => setMenuOpen(false);

  const capitalize = s =>
    s ? s.charAt(0).toUpperCase() + s.slice(1) : '';

  return (
    <nav className="navbar">
      <div className="navbar-container">
        {/* Logo */}
        <NavLink to="/" className="navbar-logo" onClick={closeMenu}>
          FitSync
        </NavLink>
        <button
          className="navbar-toggle"
          onClick={toggleMenu}
          aria-label="Toggle navigation"
          aria-expanded={menuOpen}
        >
          &#9776;
        </button>
        
        {/* Main menu + auth/user controls */}
        <ul className={`navbar-menu ${menuOpen ? 'open' : ''}`}>
          <li>
            <NavLink to="/" end onClick={closeMenu}>
              Home
            </NavLink>
          </li>
          <li>
            <NavLink to="/plans" onClick={closeMenu}>Plans</NavLink>          </li>
          <li>
            <NavLink to="/records" onClick={closeMenu}>Records</NavLink>          </li>
          <li>
            <NavLink to="/workouts" onClick={closeMenu}>Workouts</NavLink>          </li>

          {/* push the user area to the right */}
          {user ? (
            <li className="navbar-user">
              <span>
                Welcome {capitalize(user.userName ?? user.email)}
              </span>
              <button onClick={() => { logout(); closeMenu(); }} className="link-btn">                Logout
              </button>
            </li>
          ) : (
            <>
              <li><NavLink to="/login" onClick={closeMenu}>Login</NavLink></li>
              <li><NavLink to="/register" onClick={closeMenu}>Register</NavLink></li>
            </>
          )}
        </ul>
      </div>
    </nav>
  );
}
