export function getAuthToken(){
    const stored = localStorage.getItem('auth');
    return stored ? JSON.parse(stored).token : null;
}

export function logout(){
    localStorage.removeItem('auth');
    window.location.href = '/login';
}