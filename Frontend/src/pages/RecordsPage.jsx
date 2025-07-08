import React, { useEffect, useState } from 'react';
import { getPersonalRecords } from '../services/api';
import { useAuth } from '../context/AuthContext';
import '../styles/pages/records.css';

export default function RecordsPage() {
    const { user } = useAuth();
    const userId = user?.userId;
    const [records, setRecords] = useState([]);
    const [loading, setLoading] = useState(true)

    useEffect(() => {
            if (!userId) return;
        (async () => {
            try {
                const { data } = await getPersonalRecords(userId);
                data.sort((a, b) => new Date(b.achievedAt) - new Date(a.achievedAt));
                setRecords(data);
            } catch (err) {
                console.error(err);
            } finally {
                setLoading(false);
            }
        })();
    }, [userId]);

    if (loading) return <p className="center">Loading…</p>;
    if (!records.length) return <p className="center">No personal records yet.</p>;

    return (
        <section className="records-page">
            <h1>Personal Bests</h1>

            <ul className="records-list">
                {records.map(r => (
                    <li key={r.id} className="record-card">
                        <header>
                            <strong>{r.exerciseName}</strong>
                            <small>{new Date(r.achievedAt).toLocaleDateString()}</small>
                        </header>
                        <p>{r.maxWeight} kg × {r.reps} rep{r.reps !== 1 ? 's' : ''}</p>
                    </li>
                ))}
            </ul>
        </section>
    );
}
