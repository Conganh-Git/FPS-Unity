# Unity FPS Prototype

## Overview
A simple first-person shooter prototype built with Unity 

## Gameplay Features
- WASD movement with camera-relative direction
- Mouse look with yaw rotation and clamped vertical pitch
- Sprint system (Shift)
- Handgun firing system (animation + sound)
- Ammo system with UI display
- Ammo pickup interaction
- Dynamic crosshair UI
- Footstep audio while moving

## Systems Implemented
- Input system architecture (InputReader → PlayerController)
- Event-driven input handling
- Audio playback system
- Animation control via Animator
- Coroutine-based firing logic

## Tech Stack
- Unity
- C#
- Unity New Input System

## Architecture
InputReader handles input events  
PlayerController handles player movement and combat logic
 

## Status
Core FPS mechanics implemented.
Currently implementing core player mechanics.