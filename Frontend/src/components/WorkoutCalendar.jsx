import React from 'react';
import '../styles/components/WorkoutCalendar.css';

export default function WorkoutCalendar({ workouts }) {
  const today = new Date();
  const year = today.getFullYear();
  const month = today.getMonth();

  const startDate = new Date(year, month, 1);
  const daysInMonth = new Date(year, month + 1, 0).getDate();
  const startDay = startDate.getDay(); // 0=Sun

  const workoutDays = new Set(
    workouts
      .map((w) => {
        const d = new Date(w.date);
        return d.getFullYear() === year && d.getMonth() === month
          ? d.getDate()
          : null;
      })
      .filter(Boolean)
  );

  const cells = [];
  for (let i = 0; i < startDay; i++) cells.push(null);
  for (let d = 1; d <= daysInMonth; d++) cells.push(d);

  // pad to full weeks
  while (cells.length % 7 !== 0) cells.push(null);

  return (
    <div className="calendar">
      <h3 className="calendar-month">
        {today.toLocaleDateString(undefined, { month: 'long', year: 'numeric' })}
      </h3>
      <div className="calendar-grid">
        {['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'].map((name) => (
          <div key={name} className="day-name">
            {name}
          </div>
        ))}
        {cells.map((day, idx) => (
          <div
            key={idx}
            className={`calendar-day${
              day && workoutDays.has(day) ? ' has-workout' : ''
            }`}
          >
            {day || ''}
          </div>
        ))}
      </div>
    </div>
  );
}