# PRG281 Game Server Matchmaking System

## Overview
This project simulates a **multiplayer game server matchmaking system** in C#.  
It demonstrates core software engineering concepts:
- **CRUD operations** for player management
- **Queue engine** for matchmaking
- **Event-driven notifications** with custom exceptions
- **Backend monitoring** for server health and player tracking
- **Multithreading** for concurrent tasks

Each team member contributed a distinct module:
- **Member 1:** CRUD operations (Player creation, update, deletion, stats)
- **Member 2:** Queue engine (join/leave queue, match balancing)
- **Member 3:** Custom exceptions and event-driven notification system
- **Member 4:** Backend monitoring (server health checks, cleanup, logging)

---

## How to Run the Application
1. Clone the repository:
2. Open the solution in Visual Studio or VS Code.
3. Ensure all .cs files are included in the project (Program.cs, Player.cs, Manage_Players.cs, MatchMakingEvents.cs, MonitoringSystem.cs, etc.).
4. Build the solution:
      Visual Studio: Ctrl + Shift + B
5. Run the program
6. Observe console output:
      Player creation and monitoring
      Events firing (OnMatchFound, OnNotificationReceived)
      Health checks and cleanup logs


## Design Decisions
- Interfaces (I_CRUD_operations): Enforces a contract for CRUD functionality, ensuring consistency across player management.

- Events & Delegates: Used to decouple matchmaking logic from notification handling. Subscribers register with += and are triggered safely.

- Custom Exceptions: Provide meaningful error handling (QueueFullException, ServerOverloadException, etc.) instead of generic system crashes.

- Namespaces: Organized into crud_operations, GameServerMatchMaking.Events, and BackendMonitoring for clarity and modularity.

- Accessibility: All cross-namespace classes (Player, MatchmakingEventManager, MonitoringSystem) are declared public to avoid inconsistent accessibility errors.

## Multithreading
The system uses multithreading concepts to simulate concurrent server operations:

- Event Handling: Events (OnMatchFound, OnNotificationReceived) run asynchronously, allowing multiple subscribers to react without blocking the main thread.

- Monitoring Tasks: Server health checks and player tracking can be executed in parallel threads to mimic real-world server monitoring.

- Thread Safety: Shared resources (like player lists) are managed carefully to avoid race conditions. Future improvements could include lock statements or ConcurrentCollections.

## Custom Exceptions and Event-Driven Notification System
1. Custom Exception Handling (Exceptions/)
  - Exceptions: QueueFullException, ServerOverloadException, PlayerAlreadyInQueueException,          PlayerNotInQueueException
  - Purpose: Catch specific matchmaking errors and provide user-friendly messages (e.g., “You        are already in queue”).
  - Implementation: Each inherits from System.Exception and passes custom messages up the call       stack.
2. Events
  - Notification system using OnMatchFound and OnNotificationReceived.
  - MatchFoundEvent Data Packet: Contains MatchId, ServerIp, PlayerCount.
  - OnMatchFound Event: Public channel where subscribers register with +=.
  - Trigger Method (MatchFound): Safely invokes all subscribed listeners when a new match is         created.
