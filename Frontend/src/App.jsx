import React from 'react';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import Layout             from './components/Layout';
import Home               from './pages/Home';
import Login              from './pages/Login';
import Register           from './pages/Register';
import ExercisePlansPage  from './pages/ExercisePlansPage';
import CreatePlanPage     from './pages/CreatePlanPage';   // <‑‑ ново
import AddPlanItemsPage from './pages/AddPlanItemsPage';
import WorkoutsPage      from './pages/WorkoutsPage';

export default function App() {
  return (
    <BrowserRouter>
      <Routes>
        {/* Сви путеви иду кроз Layout (Navbar + Outlet) */}
        <Route path="/" element={<Layout />}>
          <Route index element={<Home />} />

          {/* јавне стране */}
          <Route path="login"    element={<Login />} />
          <Route path="register" element={<Register />} />

          {/* Exercise plans */}
          <Route path="plans" element={<ExercisePlansPage />} />
          <Route path="plans/new" element={<CreatePlanPage />} />
          <Route path='plans/:planId/items' element={<AddPlanItemsPage />} />

          {/* Workouts */}
          <Route path="workouts" element={<WorkoutsPage />} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
}
