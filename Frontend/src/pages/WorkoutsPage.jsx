import React, { useEffect, useState } from 'react';
import { useAuth } from '../context/AuthContext';
import {
  getWorkoutsByUser,
  getPlansByUser,
  createWorkout,
  addWorkoutExercise,
  updateWorkoutExercise,
  deleteWorkout,
  getPersonalRecords,
  createPersonalRecord,
  updatePersonalRecord
} from '../services/api';
import api from '../services/api';
import '../styles/pages/workouts.css';

export default function WorkoutsPage() {
  const { user } = useAuth();
  const userId = user?.userId;

  const [workouts, setWorkouts] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  const [plans, setPlans] = useState([]);
  const [showCreate, setShowCreate] = useState(false);
  const [createForm, setCreateForm] = useState({ exercisePlanId: '' });

  const [addForId, setAddForId] = useState(null);
  const [exercises, setExercises] = useState([]);
  const [exerciseForm, setExerciseForm] = useState({
    exerciseId: '',
    sets: 3,
    reps: 8,
    weight: 0,
    restSeconds: 60,
    orderInWorkout: 1,
    notes: '',
  });

  useEffect(() => {
    if (!userId) return;
    (async () => {
      try {
        const { data } = await getWorkoutsByUser(userId);
        setWorkouts(data);
      } catch {
        setError('Failed to load workouts');
      } finally {
        setLoading(false);
      }
    })();
  }, [userId]);

  const openCreate = async () => {
    setCreateForm({ exercisePlanId: '' });
    setShowCreate(true);
    try {
      if (!userId) {
        setPlans([]);
        return;
      }
      const { data } = await getPlansByUser(userId);
      setPlans(data);
    } catch {
      setPlans([]);
    }
  };

  const handleCreate = async (e) => {
    e.preventDefault();
    try {
      const payload = {};
      if (createForm.exercisePlanId)
        payload.exercisePlanId = Number(createForm.exercisePlanId);
      const { data } = await createWorkout(userId, payload);
      setWorkouts((ws) => [...ws, data]);
      setShowCreate(false);
    } catch {
      setError('Failed to create workout');
    }
  };

  const openAddExercise = async (workoutId) => {
    setAddForId(workoutId);
    setExerciseForm({
      exerciseId: '',
      sets: 3,
      reps: 8,
      weight: 0,
      restSeconds: 60,
      orderInWorkout:
        workouts.find((w) => w.id === workoutId)?.exercises.length + 1 || 1,
      notes: '',
    });
    if (exercises.length === 0) {
      try {
        const { data } = await api.get('/exercises');
        setExercises(data);
      } catch {
        setExercises([]);
      }
    }
  };

  const handleAddExercise = async (e) => {
    e.preventDefault();
    try {
      const payload = {
        exerciseId: Number(exerciseForm.exerciseId),
        sets: Number(exerciseForm.sets),
        reps: Number(exerciseForm.reps),
        weight: Number(exerciseForm.weight),
        restSeconds: Number(exerciseForm.restSeconds),
        orderInWorkout: Number(exerciseForm.orderInWorkout),
        notes: exerciseForm.notes,
      };
      const { data } = await addWorkoutExercise(addForId, payload);
      setWorkouts((ws) =>
        ws.map((w) =>
          w.id === addForId ? { ...w, exercises: [...w.exercises, data] } : w
        )
      );
      setAddForId(null);
    } catch {
      setError('Failed to add exercise');
    }
  };

  const handleChangeCreate = (e) =>
    setCreateForm({ ...createForm, [e.target.name]: e.target.value });
  const handleChangeExercise = (e) =>
    setExerciseForm({ ...exerciseForm, [e.target.name]: e.target.value });

  const handleDeleteWorkout = async (id) => {
    try {
      await deleteWorkout(id);
      setWorkouts((ws) => ws.filter((w) => w.id !== id));
    } catch {
      setError('Failed to delete workout');
    }
  };

const handleWeightInput = (workoutId, exId, value) => {
    setWorkouts((ws) =>
      ws.map((w) =>
        w.id === workoutId
          ? {
              ...w,
              exercises: w.exercises.map((ex) =>
                ex.id === exId ? { ...ex, weight: value } : ex
              ),
            }
          : w
      )
    );
  };

  const handleSaveWeight = async (workoutId, ex) => {
    try {
      const payload = {
        exerciseId: ex.exerciseId,
        sets: ex.sets,
        reps: ex.reps,
        weight: Number(ex.weight),
        restSeconds: ex.restSeconds,
        orderInWorkout: ex.orderInWorkout,
        notes: ex.notes,
      };
      await updateWorkoutExercise(workoutId, ex.id, payload);
      const { data: records } = await getPersonalRecords(userId);
      const existing = records.find((r) => r.exerciseId === ex.exerciseId);
      const weight = Number(ex.weight);
      const recordPayload = {
        exerciseId: ex.exerciseId,
        maxWeight: weight,
        reps: ex.reps,
        achievedAt: new Date().toISOString(),
      };

      if (!existing) {
        await createPersonalRecord(userId, recordPayload);
      } else if (weight > existing.maxWeight) {
        await updatePersonalRecord(existing.id, recordPayload);
      }
    } catch {
      setError('Failed to update weight');
    }
  };

  if (loading) return <p>Loading…</p>;
  if (error) return <p className="error">{error}</p>;

  return (
    <section className="workouts-page">
      <header className="page-header">
        <h1>Workouts</h1>
        <button className="btn-create" onClick={openCreate}>
          + Create Workout
        </button>
      </header>

      {workouts.length ? (
        workouts.map((w) => (
          <article key={w.id} className="workout-card">
            <header>
              <h3>{new Date(w.date).toLocaleDateString()}</h3>
              {w.planName && (
                <span className="plan-name">Plan: {w.planName}</span>
              )}
            </header>

            {w.exercises.length > 0 && (
              <table>
                <thead>
                  <tr>
                    <th>#</th>
                    <th>Exercise</th>
                    <th>Sets</th>
                    <th>Reps</th>
                    <th>Weight</th>
                    <th>Rest</th>
                    <th>Notes</th>
                    <th></th>
                  </tr>
                </thead>
                <tbody>
                  {w.exercises.map((ex) => (
                    <tr key={ex.id}>
                      <td>{ex.orderInWorkout}</td>
                      <td>{ex.exerciseName}</td>
                      <td>{ex.sets}</td>
                      <td>{ex.reps}</td>
                      <td>
                        <input
                          type="number"
                          value={ex.weight}
                          step="0.1"
                          onChange={(e) =>
                            handleWeightInput(w.id, ex.id, e.target.value)
                          }
                        />
                      </td>
                      <td>{ex.restSeconds}</td>
                      <td>{ex.notes}</td>
                      <td>
                        <button
                          className="btn-save"
                          onClick={() => handleSaveWeight(w.id, ex)}
                        >
                          Save
                        </button>
                      </td>
                    </tr>
                  ))}
                </tbody>
              </table>
            )}

            <div className="card-actions">
              <button
                className="btn-add-ex"
                onClick={() => openAddExercise(w.id)}
              >
                + Add Exercise
              </button>
              <button
                className="btn-delete"
                onClick={() => handleDeleteWorkout(w.id)}
              >
                🗑 Delete
              </button>
            </div>
          </article>
        ))
      ) : (
        <p>No workouts yet.</p>
      )}

      {showCreate && (
        <div className="overlay">
          <form className="workout-form" onSubmit={handleCreate}>
            <h2>New Workout</h2>
            <label>
              Exercise Plan (optional)
              <select
                name="exercisePlanId"
                value={createForm.exercisePlanId}
                onChange={handleChangeCreate}
              >
                <option value="">-- None --</option>
                {plans.map((p) => (
                  <option key={p.id} value={p.id}>
                    {p.name}
                  </option>
                ))}
              </select>
            </label>
            <div className="form-actions">
              <button type="button" onClick={() => setShowCreate(false)}>
                Cancel
              </button>
              <button type="submit">Create</button>
            </div>
          </form>
        </div>
      )}

      {addForId && (
        <div className="overlay">
          <form className="workout-form" onSubmit={handleAddExercise}>
            <h2>Add Exercise</h2>
            <label>
              Exercise
              <select
                name="exerciseId"
                value={exerciseForm.exerciseId}
                onChange={handleChangeExercise}
                required
              >
                <option value="" disabled>
                  Select…
                </option>
                {exercises.map((e) => (
                  <option key={e.id} value={e.id}>
                    {e.name}
                  </option>
                ))}
              </select>
            </label>
            <label>
              Sets
              <input
                type="number"
                name="sets"
                min="1"
                value={exerciseForm.sets}
                onChange={handleChangeExercise}
                required
              />
            </label>
            <label>
              Reps
              <input
                type="number"
                name="reps"
                min="1"
                value={exerciseForm.reps}
                onChange={handleChangeExercise}
                required
              />
            </label>
            <label>
              Weight
              <input
                type="number"
                name="weight"
                min="0"
                step="0.1"
                value={exerciseForm.weight}
                onChange={handleChangeExercise}
              />
            </label>
            <label>
              Rest Seconds
              <input
                type="number"
                name="restSeconds"
                min="0"
                value={exerciseForm.restSeconds}
                onChange={handleChangeExercise}
              />
            </label>
            <label>
              Order
              <input
                type="number"
                name="orderInWorkout"
                min="1"
                value={exerciseForm.orderInWorkout}
                onChange={handleChangeExercise}
                required
              />
            </label>
            <label>
              Notes
              <textarea
                name="notes"
                rows="2"
                value={exerciseForm.notes}
                onChange={handleChangeExercise}
              />
            </label>
            <div className="form-actions">
              <button type="button" onClick={() => setAddForId(null)}>
                Cancel
              </button>
              <button type="submit">Add</button>
            </div>
          </form>
        </div>
      )}
    </section>
  );
}
