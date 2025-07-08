import React, { useEffect, useState } from 'react';
import { useParams, Link } from 'react-router-dom';
import {
  DragDropContext,
  Droppable,
  Draggable
} from 'react-beautiful-dnd';
import api, { updatePlanItemOrders, deletePlanItem, updatePlan, getExercisesPaged } from '../services/api';
import '../styles/pages/addPlanItems.css';

export default function AddPlanItemsPage() {
  const { planId } = useParams();

  const [planName, setPlanName] = useState('');
  const [planDescription, setPlanDescription] = useState('');
  const [planItems, setPlanItems] = useState([]);
  const [exercises, setExercises] = useState([]);
  const [searchResults, setSearchResults] = useState([]);
  const [searchMeta, setSearchMeta] = useState({ totalCount: 0, pageSize: 6, currentPage: 1 });
  const [searchTerm, setSearchTerm] = useState('');
  const [searchPage, setSearchPage] = useState(1);
  const [loadingPlan, setLoadingPlan] = useState(true);
  const [loadingEx, setLoadingEx] = useState(true);
  const [error, setError] = useState(null);
  const [showFormFor, setShowFormFor] = useState(null);
  const [editingName, setEditingName] = useState(false);
  const [newPlanName, setNewPlanName] = useState('');
  const [formData, setFormData] = useState({
    order: 1,
    sets: 3,
    reps: 8,
    note: ''
  });

  // 1) Load plan details
  useEffect(() => {
    (async () => {
      try {
        const { data } = await api.get(`/exerciseplans/${planId}`);
        setPlanName(data.name);
        setPlanDescription(data.description || '');
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

  // 3) Search exercises with pagination
  useEffect(() => {
    (async () => {
      try {
        const res = await getExercisesPaged({
          pageNumber: searchPage,
          searchTerm: searchTerm || undefined
        });
        const meta = res.headers['x-pagination']
          ? JSON.parse(res.headers['x-pagination'])
          : { totalCount: res.data.length, pageSize: 6, currentPage: searchPage };
        setSearchResults(res.data);
        setSearchMeta(meta);
      } catch {
        setError('Failed to load exercises');
      }
    })();
  }, [searchPage, searchTerm]);

  const openForm = exerciseId => {
    setError(null);
    setShowFormFor(exerciseId);
    setFormData({ order: planItems.length + 1, sets: 3, reps: 8, note: '' });
  };

  const openRename = () => {
    setError(null);
    setNewPlanName(planName);
    setEditingName(true);
  };

  const handleChange = e => {
    const { name, value } = e.target;
    setFormData(fd => ({ ...fd, [name]: value }));
  };

  const handleSearchChange = e => {
    setSearchTerm(e.target.value);
    setSearchPage(1);
  };

  const totalPages = Math.ceil(searchMeta.totalCount / searchMeta.pageSize) || 1;

  const prevPage = () => setSearchPage(p => Math.max(p - 1, 1));
  const nextPage = () => setSearchPage(p => Math.min(p + 1, totalPages));

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

  const handleDeleteItem = async itemId => {
    setError(null);
    try {
      await deletePlanItem(itemId);
      setPlanItems(ps => ps.filter(i => i.id !== itemId));
    } catch {
      setError('Failed to delete item');
    }
  };

  const handleRenameSubmit = async e => {
    e.preventDefault();
    try {
      await updatePlan(planId, { name: newPlanName, description: planDescription });
      setPlanName(newPlanName);
      setEditingName(false);
    } catch {
      setError('Failed to update plan name');
    }
  };

  const handleSubmit = async e => {
    e.preventDefault();
    try {
      await api.post('/exerciseplanitems', {
        exercisePlanId: Number(planId),
        exerciseId: showFormFor,
        order: Number(formData.order),
        sets: Number(formData.sets),
        reps: Number(formData.reps),
        note: formData.note
      });
      const { data } = await api.get(`/exerciseplans/${planId}`);
      setPlanItems(data.items);
      setShowFormFor(null);
    } catch (err) {
      if (err.response?.status === 409) {
        const detail = err.response.data?.detail;
        setError(detail === 'ExerciseAlreadyExists'
          ? 'That exercise is already in the plan.'
          : 'That order number is already used in the plan.');
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
        <h1>“{planName}”</h1>
        <div className="header-actions">
          <button className="btn-rename" onClick={openRename}>Rename</button>
          <Link to="/plans" className="btn-back">← Back to Plans</Link>
        </div>
      </header>

      {error && <p className="error">{error}</p>}

      <section className="current-items">
        <h2>Current Plan Items (Drag and drop to change order)</h2>
        {planItems.length ? (
          <DragDropContext onDragEnd={handleDragEnd}>
            <table className="items-table">
              <thead>
                <tr>
                  <th>#</th>
                  <th>Exercise</th>
                  <th>Sets</th>
                  <th>Reps</th>
                  <th>Note</th>
                  <th></th>
                </tr>
              </thead>
              <Droppable droppableId="planItems">
                {(provided) => (
                  <tbody
                    ref={provided.innerRef}
                    {...provided.droppableProps}
                  >
                    {planItems.map((item, idx) => (
                      <Draggable key={item.id} draggableId={`${item.id}`} index={idx}>
                        {(prov) => (
                          <tr
                            ref={prov.innerRef}
                            {...prov.draggableProps}
                            {...prov.dragHandleProps}
                          >
                            <td>{item.order}</td>
                            <td>{item.exerciseName}</td>
                            <td>{item.sets}</td>
                            <td>{item.reps}</td>
                            <td>{item.note}</td>
                            <td>
                              <button
                                className="btn-delete-item"
                                onClick={() => handleDeleteItem(item.id)}
                              >
                                🗑
                              </button>
                            </td>
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
          <p>No items in this plan.</p>
        )}
      </section>

      <section className="add-item-form">
        <h2>Add Exercise</h2>
        <select
          value={showFormFor || ''}
          onChange={e => openForm(Number(e.target.value))}
        >
          <option value="">Select an exercise…</option>
          {exercises.map(ex => (
            <option key={ex.id} value={ex.id}>{ex.name}</option>
          ))}
        </select>

        <div className="search-section">
          <input
            type="text"
            placeholder="Search exercises…"
            value={searchTerm}
            onChange={handleSearchChange}
          />
          <table className="ex-table">
            <thead>
              <tr>
                <th>Name</th>
                <th>Muscle</th>
                <th></th>
              </tr>
            </thead>
            <tbody>
              {searchResults.map(ex => (
                <tr key={ex.id}>
                  <td>{ex.name}</td>
                  <td>{ex.muscleGroup}</td>
                  <td>
                    <button className="btn-add" onClick={() => openForm(ex.id)}>
                      +
                    </button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
          <div className="pagination">
            <button onClick={prevPage} disabled={searchPage === 1}>Prev</button>
            <span>Page {searchMeta.currentPage} of {totalPages}</span>
            <button onClick={nextPage} disabled={searchPage === totalPages}>Next</button>
          </div>
        </div>

        {showFormFor && (
          <form onSubmit={handleSubmit} className="item-form">
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
        )}
      </section>

      {editingName && (
        <div className="overlay">
          <form className="item-form" onSubmit={handleRenameSubmit}>
            <h2>Rename Plan</h2>
            <label>
              Name
              <input
                type="text"
                value={newPlanName}
                onChange={e => setNewPlanName(e.target.value)}
                required
              />
            </label>
            <div className="form-actions">
              <button type="button" onClick={() => setEditingName(false)}>
                Cancel
              </button>
              <button type="submit">Save</button>
            </div>
          </form>
        </div>
      )}
    </section>
  );
}