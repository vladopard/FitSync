import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';

export default defineConfig({
  plugins: [react()],
  server: {
    proxy: {
      '/api': {
        target: 'https://localhost:7202',  // твој backend
        changeOrigin: true,
        secure: false,                      // ако је self‑signed SSL
      },
    },
  },
});