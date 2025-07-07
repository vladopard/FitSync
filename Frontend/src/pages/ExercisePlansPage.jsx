// src/pages/ExercisePlansPage.jsx
import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { getAllPlans, deletePlan } from '../services/api';
import { useAuth } from '../context/AuthContext';
import '../styles/pages/exercisePlans.css';

const PREMADE_USER_ID = 'd6f53391-3e40-4e9d-9c1d-7c1b91faadfb';

export default function ExercisePlansPage() {
  const { user } = useAuth();
  const currentUserId = user?.userId;

  const [plans,   setPlans]  = useState([]);
  const [loading, setLoad]   = useState(true);
  const [error,   setError]  = useState(null);
  const [openId,  setOpenId] = useState(null);
  const navigate = useNavigate();

  useEffect(() => {
    (async () => {
      try {
        const { data } = await getAllPlans();
        setPlans(data);
      } catch {
        setError('Failed to load plans');
      } finally {
        setLoad(false);
      }
    })();
  }, []);

  const handleDelete = async (planId) => {
    if (!window.confirm('Заиста обрисати овај план?')) return;
    try {
      await deletePlan(planId);
      setPlans(plans.filter(p => p.id !== planId));
    } catch {
      alert('Грешка при брисању плана.');
    }
  };

  if (loading) return <p>Loading…</p>;
  if (error)   return <p className="error">{error}</p>;

  const premadePlans = plans.filter(p => p.userId === PREMADE_USER_ID);
  const myPlans      = plans.filter(p => p.userId === currentUserId);

  const renderPlan = (plan, isMine = false) => (
    <article key={plan.id} className="plan-card">
      <header onClick={() => setOpenId(openId === plan.id ? null : plan.id)}>
        <h3>{plan.name}</h3>
        <span>{plan.items.length} exercise{plan.items.length !== 1 && 's'}</span>
      </header>

      {openId === plan.id && (
        <div className="plan-body">
          {plan.description && <p className="plan-desc">{plan.description}</p>}

          {isMine && (
            <div className="plan-actions">
              <button
                className="btn-add-items"
                onClick={() => navigate(`/plans/${plan.id}/items`)}
              >
                + Add Exercises
              </button>
              <button
                className="btn-delete-plan"
                onClick={() => handleDelete(plan.id)}
              >
                Delete Plan
              </button>
            </div>
          )}

          <table>
            <thead>
              <tr>
                <th>#</th><th>Exercise</th><th>Sets</th><th>Reps</th><th>Note</th>
              </tr>
            </thead>
            <tbody>
              {plan.items.map(i => (
                <tr key={i.id}>
                  <td>{i.order}</td>
                  <td>{i.exerciseName}</td>
                  <td>{i.sets}</td>
                  <td>{i.reps}</td>
                  <td>{i.note}</td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      )}
    </article>
  );

  return (
    <section className="plans-page">
      <h1>Exercise Plans</h1>

      {/* ----- Premade ----- */}
      <section className="plans-section">
        <h2>Already Made</h2>
        {premadePlans.length
          ? premadePlans.map(plan => renderPlan(plan, false))
          : <p>No premade plans available.</p>}
      </section>

      {/* ----- My Plans ----- */}
      <section className="plans-section">
        <div className="section-header">
          <h2>My Plans</h2>
          <button
            className="btn-create-plan"
            onClick={() => navigate('/plans/new')}
          >
            + Create New Plan
          </button>
        </div>

        {myPlans.length
          ? myPlans.map(plan => renderPlan(plan, true))
          : <p>You don’t have any plans yet.</p>}
      </section>
    </section>
  );
}
