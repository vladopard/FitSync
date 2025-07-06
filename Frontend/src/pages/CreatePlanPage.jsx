import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';
import api from '../services/api';              // Axios instance
import '../styles/pages/createPlan.css';

export default function CreatePlanPage() {
    const { user } = useAuth();                  // { id, … }
    const navigate = useNavigate();

    const [name, setName] = useState('');
    const [description, setDescription] = useState('');
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);

    const handleSubmit = async e => {
        e.preventDefault();
        if (!name.trim()) {
            setError('Name is required');
            return;
        }

        setLoading(true);
        setError(null);

        try {
            console.log('USER IN CONTEXT', user);          //  ⬅︎ додај у CreatePlanPage
            console.log('PAYLOAD', { name, description, userId: user?.userId });
            await api.post('/exerciseplans', {
                name,
                description,
                userId: user.userId
            });
            navigate('/plans');
        } catch {
            setError('Failed to create plan');
        } finally {
            setLoading(false);
        }
    };

    return (
        <section className="create-plan-page">
            <h1>Create New Plan</h1>

            <form className="create-plan-form" onSubmit={handleSubmit}>
                {error && <p className="error">{error}</p>}

                <label>
                    Name
                    <input
                        type="text"
                        value={name}
                        onChange={e => setName(e.target.value)}
                        maxLength={50}
                        required
                    />
                </label>

                <label>
                    Description (optional)
                    <textarea
                        value={description}
                        onChange={e => setDescription(e.target.value)}
                        maxLength={200}
                        rows={3}
                    />
                </label>

                <button type="submit" disabled={loading}>
                    {loading ? 'Saving…' : 'Save Plan'}
                </button>
            </form>
        </section>
    );
}
