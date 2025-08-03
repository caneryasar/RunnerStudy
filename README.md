# ⚔️ Runner Game Study

This is a runner game prototype developed in Unity as a personal growth project.

The game showcases smooth character movement, a dynamic point calculation system as the main gameplay mechanic, and a unique health system influenced by collisions and score balance.

---

## 🎮 Core Features

### ✅ Runner Character Controller

* **Movement**: Controlled via mouse or finger swipe on the horizontal axis
* **Camera**: Cinemachine Follow for smooth third-person tracking

### ✅ Points & Health System

* **Collecting Points**: Collision with correct elements increases the player’s score using a base-2 progression
* **Health Loss**: Colliding with incorrect elements decreases health based on a ratio between current score and the collided object’s value

### ✅ Endless Runner System

* **Object pooled track and elements**
* **Obstacles and point objects** placed dynamically
* **Increasing difficulty** over time based on points held

### ✅ UI Integration

* **Health Bar**: Displays current health status
* **Best Score**: Shows the all-time highest score
* **Real-Time Stats**: Points are displayed on both the player and collectible elements

---

## 🛠️ Tech Stack

| Tool / Framework        | Description                     |
| ----------------------- | ------------------------------- |
| **Unity**               | Unity 6 or later                |
| **Render Pipeline**     | URP (Universal Render Pipeline) |
| **Language**            | C#                              |
| **Input System**        | Unity's New Input System        |
| **Camera System**       | Cinemachine                     |
| **Additional Packages** | UniRx, DOTween                  |
| **Version Control**     | Git + GitHub                    |
| **IDE / Editor**        | JetBrains Rider                 |
