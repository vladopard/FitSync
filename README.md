# FitSync

FitSync is a workout and personal records tracker built with ASP.NET Core 8 and React.

## Repository Structure

    FitSync/
    ├── Backend
    └── Frontend

## Tech Stack

- **Backend:** ASP.NET Core 8, Entity Framework Core, PostgreSQL  
- **Frontend:** React, Vite, React Router, Axios  
- **Authentication:** ASP.NET Identity + JWT  

## Features

- Create, update, delete exercise plans  
- Log workouts with date, exercises, sets, reps, weight, rest  
- Automatic tracking of personal bests  
- JWT‑based authentication with Admin and User roles  

## Requirements

- .NET 8 SDK  
- Node.js  
- PostgreSQL  

## Configuration

1. Copy `Backend/appsettings.json.example` → `Backend/appsettings.json`  
2. Update connection string in `Backend/appsettings.json`:

    ```json
    {
      "ConnectionStrings": {
        "DefaultConnection": "Host=localhost;Port=5432;Database=FitSyncDb;Username=postgres;Password=your_password"
      }
    }
    ```

## Running Locally

### Backend

    cd Backend
    dotnet restore
    dotnet run

- API listens on `https://localhost:7202`  
- Applies migrations and seeds demo data on startup  

### Frontend

    cd Frontend
    npm install
    npm run dev

- Dev server runs on `http://localhost:5173`  
- Proxies `/api` to backend (configured in `vite.config.js`)  

## Seeded Accounts

| Role  | Username | Email               | Password  |
|-------|----------|---------------------|-----------|
| Admin | admin    | admin@fitsync.local | Admin123! |
| User  | milan    | milan@fitsync.local | Milan123! |

## Scripts

**Backend**  
- `dotnet run` — start API  

**Frontend**  
- `npm run dev` — start dev server  
- `npm run build` — build for production  

## License

This project is licensed under the MIT License. See `LICENSE.md` for details.  
