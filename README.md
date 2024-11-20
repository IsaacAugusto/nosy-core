This repository contains a collection of utility scripts designed to streamline and enhance game development workflows in Unity.
This repository is designed to improve development efficiency and provide reliable solutions for common challenges in game development. Contributions and suggestions are welcome!

## Features

### Bootstrap Scene Loader

Automatically loads a scene named "Bootstrap" as soon as the application starts or when entering Play Mode in the Unity Editor. This is ideal for initializing essential systems or configurations at the start of the game.

### Generic Event Bus

A robust implementation of a generic event bus that scans Unity's base assemblies for event interface implementations. It creates a static point of access where you can easily bind to events or trigger them programmatically. This approach simplifies event management across your project.

### Finite State Machine (FSM)

A dynamic implementation of a Finite State Machine (FSM) that can be constructed on demand.

### Custom Player Loop Utility

Enables the creation of custom player loops in Unity. This utility allows you to define static systems that are updated independently of scenes or GameObjects. You can specify whether your custom loop runs before or after a specific Unity loop (e.g., before Update or after Update but before LateUpdate), offering precise control over execution order.

### Timer Manager

A Timer Manager that leverages a custom player loop to process timers before the Update loop. This eliminates the need for calling Timer.Update(Time.deltaTime) manually. All timers are automatically registered and processed, simplifying the management of time-based events.
