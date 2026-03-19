# EventEase 🎉

EventEase is a web-based event booking and management platform built with **ASP.NET Core MVC** and **Entity Framework Core**, deployed on **Azure Web App** with an **Azure SQL Database** backend.  
It provides role-based dashboards for **Admins** and **Customers**, enabling venue management, event scheduling, and booking workflows.

---

## 🚀 Features
- **Role-based access**:
  - **Admin**: Manage venues, events, and bookings.
  - **Customer**: Browse events, book venues, and view personal bookings.
- **Venue Management**: Add, edit, and delete venues with details like name, location, capacity, and image.
- **Event Management**: Schedule events linked to venues, with status tracking (`Pending`, `Approved`, `Denied`).
- **Booking System**: Customers can book events at venues, storing contact details and booking dates.
- **Responsive UI**: Built with Bootstrap for clean, mobile-friendly layouts.

---

## 🗂 Database Schema
The database consists of five main tables:

- **Roles**: Defines user roles (`Admin`, `Customer`).
- **Users**: Stores user accounts with role assignments.
- **Venues**: Venue details (name, location, capacity, image).
- **Events**: Event details linked to venues.
- **Bookings**: Central table linking users, events, and venues.

### Relationships
- Roles → Users: One-to-Many  
- Users → Bookings: One-to-Many  
- Venues → Events: One-to-Many  
- Events → Bookings: One-to-Many  
- Venues → Bookings: One-to-Many  

---

## ⚙️ Constraints
- **Validation**: Required fields (`VenueName`, `Location`, `Capacity`) enforced via Data Annotations.
- **Capacity**: Must be greater than 0.
- **Unique fields**: `Email` and `Username` in Users are unique.
- **Foreign Keys**: Enforce relational integrity between tables.
- **Role-based routing**: Admin and Customer dashboards are separated.

---

## 🛠️ ACCESS LINK :
- GITHUB: https://github.com/AaliyahAllie/CLDV7111_ST10212542_AALIYAHALLIE_POE.git (SOURCE CODE)
- WEB APP: cldv-eventease-hzbgardxcwgpg4dy.canadacentral-01.azurewebsites.net
- YouTube Links:
- Event Ease Deployment Video: https://youtu.be/_NIIkECHBzk
- Demo Video: https://youtu.be/84GJilGdbVE

