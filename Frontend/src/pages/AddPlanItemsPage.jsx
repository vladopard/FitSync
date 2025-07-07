// src/pages/AddPlanItemsPage.jsx
import React, { useEffect, useState } from 'react';
import { useParams, Link } from 'react-router-dom';
import {
  DragDropContext,
  Droppable,
  Draggable
} from 'react-beautiful-dnd';
import api, { updatePlanItemOrders } from '../services/api';
import '../styles/pages/addPlanItems.css';

export default function AddPlanItemsPage() {
  const { planId } = useParams();

  const [planName,    setPlanName]    = useState('');
  const [planItems,   setPlanItems]   = useState([]);
  const [exercises,   setExercises]   = useState([]);
  const [loadingPlan, setLoadingPlan] = useState(true);
  const [loadingEx,   setLoadingEx]   = useState(true);
  const [error,       setError]       = useState(null);
  const [showFormFor, setShowFormFor] = useState(null);
  const [formData,    setFormData]    = useState({
    order: 1,
    sets:  3,
    reps:  8,
    note:  ''
  });

  // 1) Load plan details (name + existing items)
  useEffect(() => {
    (async () => {
      try {
        const { data } = await api.get(`/exerciseplans/${planId}`);
        setPlanName(data.name);
        setPlanItems(data.items);
      } catch {
        setError('Failed to load plan details');
      } finally {
        setLoadingPlan(false);
      }
    })();
  }, [planId]);

  // 2) Load all exercises
  useEffect(() => {
    (async () => {
      try {
        const { data } = await api.get('/exercises');
        setExercises(data);
      } catch {
        setError('Failed to load exercises');
      } finally {
        setLoadingEx(false);
      }
    })();
  }, []);

  const openForm = exerciseId => {
    setError(null);
    setShowFormFor(exerciseId);
    setFormData({ order: planItems.length + 1, sets: 3, reps: 8, note: '' });
  };

  const handleChange = e => {
    const { name, value } = e.target;
    setFormData(fd => ({ ...fd, [name]: value }));
  };

  const handleDragEnd = async result => {
    if (!result.destination) return;

    const items = Array.from(planItems);
    const [moved] = items.splice(result.source.index, 1);
    items.splice(result.destination.index, 0, moved);

    const reordered = items.map((it, idx) => ({ ...it, order: idx + 1 }));

    setPlanItems(reordered);

    try {
      await updatePlanItemOrders(reordered.map(({ id, order }) => ({ id, order })));
    } catch {
      setError('Failed to update item order');
    }
  };

  const handleSubmit = async e => {
  e.preventDefault();
  try {
    await api.post('/exerciseplanitems', {
      exercisePlanId: Number(planId),
      exerciseId:     showFormFor,
      order:          Number(formData.order),
      sets:           Number(formData.sets),
      reps:           Number(formData.reps),
      note:           formData.note
    });
    const { data } = await api.get(`/exerciseplans/${planId}`);
    setPlanItems(data.items);
    setShowFormFor(null);
  } catch (err) {
    if (err.response?.status === 409) {
      const detail = err.response.data?.detail;
      switch (detail) {
        case 'ExerciseAlreadyExists':
          setError('That exercise is already in the plan.');
          break;
        case 'OrderAlreadyExists':
          setError('That order number is already present in the plan.');
          break;
        case 'DuplicatePlanName':
        default:
          // Middleware is (mis)using this code for order conflicts, so treat it as order error:
          setError('That order number is already used in the plan.');
          break;
      }
      setShowFormFor(null);
    } else {
      setError('Failed to add item');
    }
  }
};

  if (loadingPlan || loadingEx) return <p>Loading…</p>;

  return (
    <section className="add-items-page">
      <header>
        <h1>Add Exercises to “{planName}”</h1>
        <Link to="/plans" className="btn-back">← Back to Plans</Link>
      </header>

      {/* inline error message */}
      {error && <p className="error">{error}</p>}

      {/* Existing plan items */}
      <section className="current-items">
        <h2>Current Plan Items</h2>
        {planItems.length ? (
          <DragDropContext onDragEnd={handleDragEnd}>
            <table className="items-table">
              <thead>
                <tr>
                  <th>#</th><th>Exercise</th><th>Sets</th><th>Reps</th><th>Note</th>
                </tr>
              </thead>
              <Droppable droppableId="planItems">
                {(provided) => (
                  <tbody ref={provided.innerRef} {...provided.droppableProps}>
                    {planItems.map((i, index) => (
                      <Draggable key={i.id} draggableId={String(i.id)} index={index}>
                        {(prov, snapshot) => (
                          <tr
                            ref={prov.innerRef}
                            {...prov.draggableProps}
                            className={snapshot.isDragging ? 'dragging' : ''}
                          >
                            <td className="handle-cell" {...prov.dragHandleProps}>
                              <span className="drag-handle">☰</span> {i.order}
                            </td>
                            <td>{i.exerciseName}</td>
                            <td>{i.sets}</td>
                            <td>{i.reps}</td>
                            <td>{i.note}</td>
                          </tr>
                        )}
                      </Draggable>
                    ))}
                    {provided.placeholder}
                  </tbody>
                )}
              </Droppable>
            </table>
          </DragDropContext>
        ) : (
          <p>No items yet. Click + below to add.</p>
        )}
      </section>

      {/* Available exercises */}
      <section className="available-exercises">
        <h2>Available Exercises</h2>
        <table className="ex-table">
          <thead>
            <tr>
              <th>Name</th><th>Muscle</th><th>Type</th><th>Description</th><th></th>
            </tr>
          </thead>
          <tbody>
            {exercises.map(ex => (
              <tr key={ex.id}>
                <td>{ex.name}</td>
                <td>{ex.muscleGroup}</td>
                <td>{ex.type}</td>
                <td>{ex.description}</td>
                <td>
                  <button
                    className="btn-add"
                    onClick={() => openForm(ex.id)}
                  >+</button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </section>

      {/* Modal for adding an item */}
      {showFormFor && (
        <div className="overlay">
          <form className="item-form" onSubmit={handleSubmit}>
            <h2>
              Configure “{exercises.find(e => e.id === showFormFor)?.name}”
            </h2>
            <label>
              Order
              <input
                type="number"
                name="order"
                min="1"
                value={formData.order}
                onChange={handleChange}
                required
              />
            </label>
            <label>
              Sets
              <input
                type="number"
                name="sets"
                min="1"
                value={formData.sets}
                onChange={handleChange}
                required
              />
            </label>
            <label>
              Reps
              <input
                type="number"
                name="reps"
                min="1"
                value={formData.reps}
                onChange={handleChange}
                required
              />
            </label>
            <label>
              Note (optional)
              <textarea
                name="note"
                rows="2"
                value={formData.note}
                onChange={handleChange}
              />
            </label>
            <div className="form-actions">
              <button type="button" onClick={() => setShowFormFor(null)}>
                Cancel
              </button>
              <button type="submit">Add to Plan</button>
            </div>
          </form>
        </div>
      )}
    </section>
  );
}
