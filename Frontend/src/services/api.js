import axios from "axios";
import { getAuthToken, logout } from "./auth-helpers";

const api = axios.create({
    baseURL: 'https://localhost:7202/api',
    timeout: 10_000,
});

// 🔒 Attach JWT to every request
// api.interceptors.request.use((config) => {
//   const token = getAuthToken();
//   if (token) config.headers.Authorization = `Bearer ${token}`;
//   return config;
// });

// 🔄 Global response handler
// api.interceptors.response.use(
//   (res) => res,
//   (err) => {
//     if (err.response?.status === 401) {
//       logout();               // clear storage + redirect to /login
//     }
//     return Promise.reject(err);
//   }
// );

export default api;

export const getAllPlans = () => api.get('/exerciseplans');
export const deletePlan = (planId) => axios.delete(`/api/exerciseplans/${planId}`);

export const updatePlanItemOrders = (items) =>
    api.put('/exerciseplanitems/reorder', items);
export const deletePlanItem = itemId => api.delete(`/exerciseplanitems/${itemId}`);

export const getWorkouts        = () => api.get('/workouts');
export const createWorkout      = payload => api.post('/workouts', payload);   // { date, exercisePlanId? }
export const getWorkout         = id => api.get(`/workouts/${id}`);
export const updateWorkout      = (id, payload) => api.put(`/workouts/${id}`, payload);
export const deleteWorkout      = id => api.delete(`/workouts/${id}`);

export const addWorkoutExercise    = payload => api.post('/workoutexercises', payload);
export const updateWorkoutExercise = (id, payload) => api.put(`/workoutexercises/${id}`, payload);
export const deleteWorkoutExercise = id => api.delete(`/workoutexercises/${id}`);
export const reorderWorkoutItems   = list => api.put('/workoutexercises/reorder', list);