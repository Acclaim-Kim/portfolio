# Pixel Plumbers - Fall 2024

**Mario Game - Sprint 4**  
**Pixel-Plumbers**

This project is a replica of the classic Mario game, written in C# using MonoGame.

---

## Mario Controls

- **Movement**:
  - `W`: Jump  
  - `A`: Move left  
  - `D`: Move right  
  - `S`: Crouch  

- **State Controls**:
  - `1`: Increase Mario's state (Small → Big → Fire)  
  - `2`: Decrease Mario's state (Fire → Big → Small → "Died")  

- **Special Actions**:
  - `Left Shift`: Shoot fireballs (Fire mode only)  
  - `Right Shift`: Grant Mario a star  

---

## Luigi Controls (Multiplayer Mode)

- **Movement**:
  - `Up Arrow`: Jump  
  - `Left Arrow`: Move left  
  - `Right Arrow`: Move right  
  - `Down Arrow`: Crouch  

---

## Game Controls

- **Menu Actions**:
  - Click **"Single Player"** or **"Multiplayer"** to start the game  
  - Click on the table in the bottom left corner to enter the Blackjack minigame  

- **Gameplay Actions**:
  - `R`: Reset the game  
  - `P`: Pause the game (also pauses music)  
  - `Q`: Quit the game  
  - `M`: Play music (press again to switch songs)  
  - `Z`: Mute all sounds  

---

## Known Bugs

- Some hitboxes are clunky  
- `Goomba2` and `GoombaStateMachine2` are temporary placeholders  
- Mario's gravity is not applied correctly when standing on top of obstacles or blocks without jumping  
- The click boxes for "level screen" and "main menu" are persistent  
- Mario may fall through the ground or off blocks when colliding with obstacles from the left or right
- Mario may fall through the incline steps instead of climbing up them if in small Mario form
- Mario's win animation does not play properly for level 1  

---
