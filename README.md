# ChatApp API

A real-time chat application backend built with ASP.NET Core Web API, Entity Framework Core, SQL Server, JWT Authentication, and SignalR.

## Technologies Used

- ASP.NET Core / .NET 9
- C#
- Entity Framework Core
- SQL Server / Azure SQL
- JWT Authentication
- SignalR
- REST APIs
- LINQ
- Async/Await

## Features

### Authentication
- User registration
- User login
- JWT-based authentication
- JWT stored in an HttpOnly cookie
- Authenticated user information
- Protected API endpoints

### Chat

- One-to-one conversations
- Conversation creation/retrieval
- Conversation members
- Message persistence
- Sender identification
- Message timestamps
- Conversation history

### Real-Time Messaging

SignalR is used for real-time message delivery.

When a user opens a conversation, their SignalR connection joins the corresponding conversation group.

Conversation 25
       ↓
SignalR Group "25"
       ↓
 ┌───────────────┐
 │ User A        │
 │ User B        │
 └───────────────┘
 
 
#### Datebase STRUCTURE

 Conversation
     │
     ├── ConversationMember
     │        │
     │        └── User
     │
     └── Message
              │
              └── User (Sender)

#### api
POST /api/Auth/register
POST /api/Auth/login
GET  /api/Auth/checkLogin
GET  /api/Auth/GetAuthUser

hub
/chatHub

##### MESSAGE FLOW
React Client
     │
     │ SendMessage
     ▼
SignalR Hub
     │
     ▼
Chat Service
     │
     ▼
Database
     │
     ▼
Saved Message
     │
     ▼
SignalR Group
     │
 ┌───┴────┐
 ▼        ▼
User A   User B