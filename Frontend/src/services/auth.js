import api from "./api";

// export async function login(credentials) {
//     const { data } = await api.post('/auth/login', credentials);
//     return data;
// }

// export async function register(form) {
//     const { data } = await api.post('/auth/register', form);
//     return data;
// }

export function login(cred) {
  return api.post('/auth/login', cred);
}

export function register(data) {
  return api.post('/auth/register', data);
}
