# Vertigo Games - Technical Artist Demo

This repository contains the Unity Technical Artist Art Demo implementation featuring a responsive Battle Pass Road UI system and Weapon Trail/Particle VFX.

---

## 🛠️ Project Information

- **Unity Version**: `6000.3.18f1` (URP - Universal Render Pipeline)
- **Render Pipeline**: Universal Render Pipeline (URP)

---

## 🎬 How to Open & Test the Scenes

### 1. Task 1 - Battle Pass Road UI (`Assets/Scenes/L_UIDemoScene.unity`)

- **Location**: `Assets/Scenes/L_UIDemoScene.unity`
- **Features & Interactions**:
  - **Responsive Mobile Layout**: CanvasScaler is configured to `1920x1080` (`Match = 0.5`) supporting landscape display across various mobile and tablet aspect ratios.
  - **Reward Node States (`LevelColumn2` Prefab)**:
    Select any `LevelColumn2` object in the hierarchy or inspect `Assets/Prefabs/LevelColumn2.prefab` to test the state visualizer component:
    - **`isLocked`** (bool): Toggles lock icon overlay (`Icon_Lock`).
    - **`claimState`** (enum):
      - **`Claimable`**: Enables active shine shader effect (`M_UIShine.mat`), exclamation mark badge (`!`), and bright background.
      - **`CurrentProgress`**: Enables passive shine shader effect (`M_UIShinePassive.mat`) while maintaining background graphics.
      - **`Claimed`**: Displays green checkmark tick icon and applies claimed tinting.
      - **`Passive`**: Applies standard passive card styling.
    - **`backgroundType`** (enum): Instantly switches card rarity background sprites (`Epic`, `Legendary`, `Mythic`, `Uncommon`, `Rare`, `Collectable`).

### 2. Task 2 - Weapon VFX (`Assets/Scenes/L_FXDemoScene.unity`)

- **Location**: `Assets/Scenes/L_FXDemoScene.unity`
- **Features & Shader VFX**:
  - **Weapon Model**: `spcl_rif_mcx_topscorer` showcase with custom lighting and volumetric ambience.
  - **Flowing Wind Trail Shader**: Built using Shader Graph located at `Assets/Materials/ParticleMaterials/VertigoTrailEffect.shadergraph`.
  - **Secondary Particle Systems**: Integrated star and glow particle bursts (`StarFront`, `InSphereStar`, `MainPoint`).

---

## 📁 Repository Structure

```
Assets/
├── Editor/                        # Build & Automation Editor Tools
├── FONT/                          # Game Font Assets
├── Materials/
│   ├── ParticleMaterials/         # Weapon Trail ShaderGraph & Particle Materials
│   └── UIMaterials/               # UI Background & Shine Effect Shaders/Materials
├── Meshes/                        # 3D Models & Weapon Meshes
├── Prefabs/                       # Level Column & UI Node Prefabs
├── Scenes/
│   ├── L_UIDemoScene.unity        # Task 1: Battle Pass Screen Scene
│   └── L_FXDemoScene.unity        # Task 2: Weapon VFX Showcase Scene
├── Scripts/                       # C# Controllers & Auto Visualizers
└── Textures/                      # UI Sprites & Particle Textures
```
