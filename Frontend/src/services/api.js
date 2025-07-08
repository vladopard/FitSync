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

// ----- Exercises -----
export const getExercisesPaged = (params) =>
  api.get('/exercises/paged', { params });

export const getAllPlans = () => api.get('/exerciseplans');
export const getPlansByUser = (userId) =>
  api.get(`/exerciseplans/user/${userId}`);
export const deletePlan = (planId) => axios.delete(`/api/exerciseplans/${planId}`);
export const copyPlan = (planId, userId) =>
  api.post(`/exerciseplans/${planId}/copy/${userId}`);
export const updatePlan = (planId, payload) =>
  api.put(`/exerciseplans/${planId}`, payload);

export const updatePlanItemOrders = (items) =>
  api.put('/exerciseplanitems/reorder', items);
export const deletePlanItem = itemId => api.delete(`/exerciseplanitems/${itemId}`);

// ----- Workouts -----
export const getWorkoutsByUser = (userId) =>
  api.get(`/workouts/user/${userId}`);

export const getWorkout = (id) => api.get(`/workouts/${id}`);
export const createWorkout = (userId, payload) =>
  api.post(`/workouts/user/${userId}`, payload);
export const updateWorkout = (id, payload) =>
  api.put(`/workouts/${id}`, payload);
export const deleteWorkout = (id) => api.delete(`/workouts/${id}`);

// ----- Workout Exercises -----
export const addWorkoutExercise = (workoutId, payload) =>
  api.post(`/workouts/${workoutId}/exercises`, payload);
export const updateWorkoutExercise = (workoutId, id, payload) =>
  api.put(`/workouts/${workoutId}/exercises/${id}`, payload);
export const deleteWorkoutExercise = (workoutId, id) =>
  api.delete(`/workouts/${workoutId}/exercises/${id}`);

export const getPersonalRecords = (userId) =>
  api.get(`/personalrecords/user/${userId}`);

export const createPersonalRecord = (userId, payload) =>
  api.post(`/personalrecords/user/${userId}`, payload);

export const updatePersonalRecord = (id, payload) =>
  api.put(`/personalrecords/${id}`, payload);