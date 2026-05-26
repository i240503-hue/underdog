# Setup Guide - Underdog Fighting Simulator

## Prerequisites

- **Unity 2022.3 LTS** or newer
- **TextMeshPro** (comes with Unity)

## Quick Setup

### 1. Clone Repository
```bash
git clone https://github.com/i240503-hue/underdog.git
cd underdog
```

### 2. Open in Unity Hub
- Click **Add** → Select the `underdog` folder
- Open the project (may take a few minutes first time)

### 3. Create Main Scene

1. Right-click in Project → Create → Scene
2. Name it `MainArena` and save in `Assets/Scenes/`
3. Make it the active scene

### 4. Scene Setup

**Add these GameObjects to your scene:**

#### Ground
- Create → 3D Object → Plane
- Scale: (10, 1, 10)
- Add BoxCollider
- Drag to 0,0,0

#### Player
- Create → 3D Object → Capsule
- Add **Rigidbody**
  - Body Type: Dynamic
  - Freeze Rotation: X, Y, Z
- Add these scripts:
  - PlayerController
  - CombatSystem
  - ProgressionSystem

#### Enemy
- Create → 3D Object → Capsule
- Position: (3, 1, 0)
- Add **Rigidbody**
  - Body Type: Dynamic
  - Freeze Rotation: X, Y, Z
- Add these scripts:
  - EnemyController
  - CombatSystem
  - AIBrain

#### GameManager
- Create → Empty GameObject
- Name: `GameManager`
- Add **GameManager** script
- In Inspector, assign:
  - Player → Player Controller field
  - Enemy → Enemy Controller field

#### InputManager
- Create → Empty GameObject
- Name: `InputManager`
- Add **InputManager** script

#### UICanvas
- Create → UI → Canvas
- Add **UIManager** script
- Create the following UI panels:

**PlayerHealthBar (Panel)**
- Add Image component (green)
- Add TextMeshProUGUI for text

**EnemyHealthBar (Panel)**
- Add Image component (red)
- Add TextMeshProUGUI for text

**StaminaBar (Panel)**
- Add Image component (cyan)
- Add TextMeshProUGUI for text

**GameOverScreen (Panel)**
- Add TextMeshProUGUI for result
- Add 2 Buttons: Restart, Menu

## Controls

- **WASD** - Move
- **Space** - Jump
- **Left Click** - Light Attack
- **Right Click** - Heavy Attack
- **Q** - Block
- **E** - Special Attack
- **Shift** - Sprint
- **ESC** - Pause

## First Test

1. Press **Play** in the editor
2. You should be able to move the player with WASD
3. Enemy will detect you and move toward you
4. Both can attack (check Console for debug logs)
5. Health bars update on Canvas

## Next Steps

- Add animations
- Implement hit detection and damage
- Add particle effects
- Add sound effects
- Create multiple enemy types
- Add progression system

Enjoy! 🎮