import React from 'react';
import { Link } from 'react-router-dom';
import '../styles/home.css';

export default function Home() {
  return (
    <div className="home-container">
      <header className="home-header">
        <h1 className="title">FitSync</h1>
        <p className="subtitle">
          Track your workouts. Stay consistent. Crush goals.
        </p>
      </header>

      <section className="features">
        <div className="feature-card">
          <h2>Create Plans</h2>
          <p>Design custom workout plans tailored to your goals.</p>
        </div>
        <div className="feature-card">
          <h2>Log Workouts</h2>
          <p>Track sets, reps, and weights to monitor progress.</p>
        </div>
        <div className="feature-card">
          <h2>Review Records</h2>
          <p>See personal bests and how far you’ve come.</p>
        </div>
        <div className="feature-card">
          <h2>View Plans</h2>
          <p>Browse and manage your existing exercise plans.</p>
          <Link to="/plans" className="feature-link">
            Go to Plans →
          </Link>
        </div>
      </section>
    </div>
  );
}
