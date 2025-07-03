import React from 'react';
import { NavLink } from 'react-router-dom';

export default function Navbar() {
  return (
    <nav className="navbar">
      <div className="navbar-container">
        <NavLink to="/" className="navbar-logo">
          FitSync
        </NavLink>
        <ul className="navbar-menu">
          <li><NavLink to="/" end>Home</NavLink></li>
          <li><NavLink to="/plans">Plans</NavLink></li>
          <li><NavLink to="/workouts">Workouts</NavLink></li>
          <li><NavLink to="/profile">Profile</NavLink></li>
        </ul>
      </div>
    </nav>
  );
}
