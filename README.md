# ☕ CafeBackend API 

A modern backend API for a Café app, built with **ASP.NET Core & PostgreSQL**. Supports **user authentication, coffee orders, reservations, and a barista dashboard**.

## ⚡ Tech Stack
- Backend: **ASP.NET Core 9.0**
- Database: **PostgreSQL**
- Authentication: **JWT Tokens**
- API Documentation: **OpenAPI / Scalar**
- Error Handling: **Custom Middleware**

## 🔹 Features
- User authentication
- Order placement
- Table reservation
- Order status management (Barista)

## 🎯 Configuration
1. Clone the repo:
```
git clone https://github.com/AlexAndreiBuzas/Cafe-Backend.git
```
2. Open with JetBrains Rider
3. Set up environment variables
```
export CONNECTION_STRING="Host=localhost;Database=cafedb;Username=cafeuser;Password=yourpassword"
```
4. Run migrations and start the server
```
dotnet ef database update
```   

## 📌 Endpoints Overview
 🔐 **Authentication (/api/auth)**
	•	**POST /api/auth/register** → Register a new user (Client or Barista)
	•	**POST /api/auth/login** → Authenticate and receive a JWT token
 🛠 **Barista Dashboard (/api/dashboard)**
	•	**GET /api/dashboard/order**s → View all active orders (Barista only)
	•	**GET /api/dashboard/reservations** → View pending reservations (Barista only)
	•	**GET /api/dashboard/summary** → Get total orders & reservations overview
 ☕ **Coffee Menu (/api/coffee-options)**
	•	**GET /api/coffee-options/types** → Get predefined coffee types
	•	**GET /api/coffee-options/customizations** → Get available customization options
 🛒 **Orders (/api/orders)**
	•	**POST /api/orders** → Place a coffee order
	•	**GET /api/orders/my-orders** → View your orders
	•	**GET /api/orders/all** → View all orders (Barista only)
	•	**PATCH /api/orders/{orderId}** → Update order status (e.g., “Ready”)
 📅 **Table Reservations (/api/reservations)**
	•	**POST /api/reservations** → Create a reservation
	•	**GET /api/reservations/my** → View your reservations
	•	**GET /api/reservations/all** → View all reservations (Barista only)
	•	**PATCH /api/reservations/{reservationId}** → Update reservation status
	•	**GET /api/reservations/status/{status}** → Filter reservations by status
	•	**GET /api/reservations/upcoming** → View upcoming reservations

## 🚀 Future Improvements
 - 🔄 [ ] Implement JWT Refresh Tokens (Automatic session renewal)
 - 🧪 [ ] Add unit & integration tests (Ensure stability)
