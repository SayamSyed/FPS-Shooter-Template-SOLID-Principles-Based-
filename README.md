# 🔫 Modular FPS Game (Unity 2022.3 LTS)

A simple but scalable and modular mobile-first FPS framework built in Unity.  
Supports SOLID principles, FSM-driven AI, ScriptableObject configs, mobile input, and clean separation of systems.

---

## 🎮 Player Controller

- Based on Unity Starter Assets
- Mobile-ready: movement joystick, swipe-look, tap buttons for shoot/reload
- Extended via `PlayerInputHandler` for clean input access
- Includes jump and crouch

### 🧠 SOLID Applied
- **SRP**: Input logic is decoupled from mechanics (`PlayerInputHandler`)
- **OCP**: Easily extendable to new input types (gamepad, VR, etc.)
- **ISP/DIP**: Systems depend on clean abstractions, not raw Unity input

---

## 🧩 Modular Weapon System

### 🔧 Components
- `WeaponBase`: abstract class for all weapons
- `RaycastWeapon`, `ProjectileWeapon`: implementations
- `WeaponController`: handles input-based firing
- `WeaponSwitcher`: handles weapon switching
- `WeaponHUD`: displays gun name and ammo count (TextMeshPro)
- `CrosshairUI`: swaps crosshair sprite + scale based on weapon config

### 🔁 Features
- Supports raycast, projectile, explosive weapons
- Event-based weapon system (muzzle flash, damage, UI)
- Swappable crosshair sprite per weapon
- Supports mobile tap-to-shoot and reload

### 🧠 SOLID Applied
- **SRP**: Each script (weapon, UI, switching) has one job
- **OCP**: Add new weapon types by inheriting from `WeaponBase`
- **LSP**: `WeaponController` uses `WeaponBase` polymorphically
- **DIP**: Weapon UI and input don't depend on specific weapon logic

---

## 📦 WeaponConfig (ScriptableObject)

```csharp
public string weaponName;
public float damage;
public float fireRate;
public int ammoCapacity;
public GameObject muzzleFlashPrefab;
public GameObject projectilePrefab;
public Sprite crosshairSprite;
public Vector2 crosshairScale;
```

### ✅ Benefits
- Fully data-driven weapon definitions
- Easy to balance & iterate without code
- Designer-friendly: no script edits required
- Crosshair UI updates dynamically

---

## 🧠 Enemy AI (FSM-Based)

### States
- `PatrolState`: waypoint-based navigation
- `DetectState`: FOV cone + optional SphereCast detection
- `ChaseState`: NavMeshAgent chase logic
- `AttackState`: shoot player using same weapon system
- `DieState`: play death animation/tween, despawn

### 🧠 SOLID Applied
- **SRP**: Each state is its own class with a single role
- **OCP**: New behaviors like Flee or Idle can be added easily
- **DIP**: `EnemyAI` depends on `IEnemyState`, not concrete states

---

## 📁 Enemy Configs

### `EnemyDetectionConfig`
```csharp
public float viewAngle;
public float detectionRange;
public float sphereCastRadius;
public int meshResolution;
public LayerMask ignoreVisionLayers;
```

### `EnemyCombatConfig`
```csharp
public float attackRange;
public float stoppingDistance;
public float attackCooldown;
public float attackDamage;
```

### ✅ Benefits
- Centralized, reusable data per enemy type
- Supports difficulty tiers and AI balancing
- Used for AI logic **and** FOV visual debugging

---

## ❤️ Health System

- `HealthComponent`: shared by both player and enemies
- Emits `OnDamaged` and `OnDeath` events
- `PlayerHealthUI`: UGUI-based real-time health bar
- `WeaponHUD`: real-time ammo + gun name (TextMeshPro)
- `EnemyTweenPlayer`: plays DoTween animations for "TakingDamage" and "Death"

### 🧠 SOLID Applied
- **SRP**: Health, UI, and VFX are fully separated
- **OCP**: Can easily extend with regen, armor, etc.
- **DIP**: Listeners subscribe to events — no hard coupling

---

## 📈 UI & Crosshair System

- Fully modular weapon HUD (ammo and name)
- Crosshair swaps based on weapon config
- Uses TextMeshPro + UGUI + Image

### UI Elements:
- `GunNameText` — weapon name (e.g., "AKM")
- `AmmoText` — format: `CurrentAmmo / TotalAmmo`
- `CrosshairUI` — changes sprite + scale based on weapon config

---

## 🎯 Visual Debug & FOV

- `FOVVisualizer`: Procedural mesh cone based on config
- Draws FOV angle, radius, and meshes dynamically
- Debug Gizmos:
  - View cone
  - Detection sphere
  - Stop range

---

## 🎬 VFX / Tween Integration

- Uses DoTween Pro
- `EnemyTweenPlayer.cs` plays:
  - **TakingDamage** tween (restarts every hit)
  - **Death** tween (once)
- Tween IDs assigned in inspector or via code

---

## 🧠 Summary: Why SOLID Matters Here

| Principle | How We Applied It |
|-----------|-------------------|
| **S** - Single Responsibility | Input, AI, UI, Health, Weapon all modular |
| **O** - Open/Closed           | New weapons, states, behaviors added without rewriting |
| **L** - Liskov Substitution   | Base class and interfaces cleanly interchangeable |
| **I** - Interface Segregation | Interfaces like `IEnemyState`, `IWeapon` are lean |
| **D** - Dependency Inversion  | Event systems and config decouple dependencies |

---

## ⚙️ Why This Architecture Works

✅ Designer-friendly via ScriptableObjects  
✅ Mobile-first with clean input separation  
✅ Scalable with difficulty levels or co-op AI  
✅ Clean debugging and visual feedback  
✅ Systems are loosely coupled, testable, and extendable

---

## 🚀 Optimization Features

To ensure smooth performance on lower-end devices (tested on a 3GB RAM Android phone):

- ✅ **Target FPS**: Maintains stable 55–60 FPS
- ✅ **URP Tweaks**: Custom lightweight URP asset
- ✅ **Shadows Disabled**: All real-time shadows turned off
- ✅ **Static Batching**: All environment GameObjects marked as `Static`
- ✅ **Baked Lighting**: Light baking tested (minimal performance improvement, retained default)
- ✅ **No Occlusion Culling**: Not applied yet, optional for expansion
- ✅ **Minimal Environment**: Greybox-style environment for clarity and performance
- ✅ **NavMesh Optimization**: Simplified navigation mesh baking
- ✅ **Texture Optimization**:
  - All textures are **Power-of-Two (POT)**
  - Used **ASTC compression (6x6 or 8x8)**
  - Texture resolutions capped between **256 to 1024 px**

These steps collectively result in a very lightweight and mobile-friendly FPS project.

---

## 🔮 Future Ideas

- ✅ Headshot/body-part damage multipliers
- ✅ Audio Manager & SFX layer
- 🔲 Shield/Armor/Status Effects
- 🔲 Multiplayer sync layer
- 🔲 Procedural enemy spawner

---

## 🧪 Dependencies

- Unity 2022.3.x LTS
- [DoTween Pro](http://dotween.demigiant.com/)
- TextMeshPro (Unity Package Manager)
- Universal Render Pipeline
- Starter Assets (First Person Controller)

---

## 👨‍💻 Credits

Built with ❤️ by Sayam Syed  
Modular FPS prototype based on Unity’s best practices and clean architecture
