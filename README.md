# 💉 Unity Service Locator Pattern

This repository contains a simple, **priority-based Service Locator implementation** for Unity projects. It allows for automatic discovery, ordered initialization, and decoupled access to various Manager or Controller services within the scene.

## 🌟 Core Concepts

The system relies on three main components to enforce the Service Locator pattern:

1.  **`IService.cs`**: The core contract for any class that wants to be registered as a service.
    * Defines `Initialize()` which is called upon registration.
    * Defines `GetServiceOrder()` for priority-based loading.
    * Defines `GetServiceType()` to identify the service type for registration.

2.  **`Service<T>.cs`**: An abstract base class that all concrete services must inherit.
    * It inherits from `MonoBehaviour` (meaning all services must be attached to a GameObject).
    * It automatically fulfills the `IService` contract by returning its own type (`typeof(T)`) and providing a `ServicePriority` field for setting the load order in the Inspector.

3.  **`ServiceLocator.cs`**: The central access point and registration authority.
    * It is a **Singleton** (assumed via `Singleton<ServiceLocator>`).
    * In its `Awake()` method, it **automatically discovers** all `IService` instances in the scene.
    * It registers and initializes services based on their **`ServicePriority`** (lower numbers initialize first).
    * Provides the static `GetService<T>()` method for retrieving registered services.

---

## ⚙️ How Initialization Works

When the `ServiceLocator` is initialized (`Awake`):

1.  It searches the entire scene for all active and inactive `MonoBehaviour` scripts that implement `IService`.
2.  These services are **ordered** based on the value returned by `GetServiceOrder()` (`ServicePriority`).
3.  The services are registered one by one into an internal dictionary using their type as the key.
4.  Immediately after successful registration, the service's `Initialize()` method is called.

This ensures that services with dependencies (e.g., an `AudioService` relying on a `SaveService`) can be initialized in a guaranteed order.

---

## 🛠️ Usage Example

### 1. Define a Concrete Service

Create a script for your service (e.g., `PlayerProfileService`) and attach it to a GameObject in your scene.

```csharp
using HilamPrototypes;
using UnityEngine;

// T must be the class itself
public class PlayerProfileService : Service<PlayerProfileService>
{
    private string _playerName;

    public override void Initialize()
    {
        // This is called by the ServiceLocator during startup.
        Debug.Log("PlayerProfileService Initialized.");
        _playerName = "DefaultPlayer";
    }

    public string GetPlayerName()
    {
        return _playerName;
    }
}
