# Service Locator For Unity  
**Singleton but Better**

## Table of Contents
- [About](#about)  
- [Why Use This](#why-use-this)  
- [Features](#features)  
- [Getting Started](#getting-started)  
  - [Installation](#installation)  
  - [Basic Usage](#basic-usage)  
- [API Overview](#api-overview)  
  - `IService`  
  - `Service<T>`  
  - `ServiceLocator`  
  - `Singleton<T>`  
- [Example](#example)  
- [Best Practices](#best-practices)  
- [Contribution](#contribution)  
- [License](#license)

## About  
Service Locator For Unity is a lightweight C# library aimed at simplifying the management of global services in Unity projects. It provides an alternative to the classic Singleton pattern—enabling clean service registration, lookup, and optional runtime swapping—while preserving the simplicity of globally accessible systems.

## Why Use This  
In Unity games, you often have systems like AudioManager, InputManager, GameStateManager, etc., which you want globally accessible. However, using many traditional singletons can lead to tight coupling and harder‐to‐test code.

This library allows you to:
- Register services in one central place.  
- Access services via interface or concrete type.  
- Replace implementations (e.g., for testing or modular projects) easily.  
- Avoid boilerplate singleton‐getters for each system.

## Features  
- A simple interface `IService` to mark services.  
- Generic base `Service<T>` to help create concrete services.  
- A `ServiceLocator` class to register, resolve, and optionally unregister services.  
- A `Singleton<T>` helper to build legacy singleton‐style behaviour if you still need it.  
- MIT Licensed.

## Getting Started

### Installation  
1. Clone or download this repository.  
2. Copy the `.cs` files (`IService.cs`, `Service.cs`, `ServiceLocator.cs`, `Singleton.cs`) into your Unity project (e.g., into a `Scripts/Framework/ServiceLocator` folder).  
3. In Unity, ensure the namespace (if any) aligns with your project structure.

### Basic Usage  
```csharp
// Define a service interface
public interface IAudioService : IService
{
    void PlaySound(string key);
}

// Create a concrete implementation
public class AudioService : Service<AudioService>, IAudioService
{
    public void PlaySound(string key)
    {
        // Implementation here
    }
}

// At startup (e.g., in an initialization MonoBehaviour)
ServiceLocator.Register<IAudioService>(new AudioService());

// Later, anywhere in the code
ServiceLocator.Resolve<IAudioService>().PlaySound("Explosion");
