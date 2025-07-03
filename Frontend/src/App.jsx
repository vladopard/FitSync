// src/App.jsx
import React from 'react';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import Layout from './components/Layout';
import Home from './pages/Home';
// import Plans from './pages/Plans';
// import Workouts from './pages/Workouts';

export default function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route element={<Layout />}>
          <Route path="/" element={<Home />} />
          {/*
          Future routes:
          <Route path="/plans" element={<Plans />} />
          <Route path="/workouts" element={<Workouts />} />
        */}
        </Route>
      </Routes>
    </BrowserRouter>
  );
}
