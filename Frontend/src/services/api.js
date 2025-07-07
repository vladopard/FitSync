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

export const reorderPlanItems = (items) =>
  api.put('/exerciseplanitems/reorder', items);
