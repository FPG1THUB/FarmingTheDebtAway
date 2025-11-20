 # FarmingTheDebtAway
## What is it?
This will be a farming game in which you are an farmer attempting to raise enough money to get out of debt via the use of an old farm you obtained. You must plant, water, harvest and expand as your income and your debt grows.

## Naming Conventions
### Branches

* Feature/ 
* Bugfix/ 
* Version/ 
* Test/ 
* Old/ 
* Merge/ 

### Files
| File Type | File extension |
| ---------- | :-------------:|
| Static | .fbx |
| Dynamic | .fbx |
| Animation | .fbx/anim | 
| Texture | .png | 
| Code | .cs | 
| Audio | .mp3/wav/ogg | 
| 2D sprites | .png | 

### Commits
* Mod 
The player should now be able to walk, run, crouch, and jump with varying speeds. The player should now also be able to crawl, and climb on solid environments. 
* Fix 
There was previously an error where when a player jumped in a specific spot in the environment, they would clip through the floor and die. This was due to the colliders with the environment and player. This commit has since fixed this bug. 
* File_Change 
Added in a script, prefab, and materials folder and created new materials 
* Refactor 
Added in comments explaining the code and changed the damage of the shotgun from 25 damage to 30 damage. 
* Ui 
Created the health, stamina and mana bar, and implemented the PlayerStats script to them. 
### Scripts
| Collection | Naming Convention |
| -----------| :------------: |
| Script | Function.cs |
| Class, struct, interface, delegate | Pascal Casing - e.g. "Player Movement" |
| Public Variables | Camel casing - e.g. "moveForward" |
| Private and internal variables | Camel casing and "_" - e.g. "_gatherPlant" |
### File and Folder organisation
File Naming Convention: 

Chest_Untextured_V1.Ma 
Chest_Textured_V2.Ma 
[Client]_[ProjectName].Doc 

 
Folder Formatting Convention: 
Root 
   ⌊ [Client/Project] 
                   ⌊  Models 
                            ⌊  Animation 
                            ⌊  Textured 
                            ⌊  Untextured 
                   ⌊  Textures 
                   ⌊  Code 
                   ⌊  Document 
## Assigned Tasks
| Member Name | Assigned Tasks |
| ---------- | :-----------:|
| Zachary Androvich | Programmer, assigned to walking, camera, interaction, inventory, and shop screen. |
| Ethan Flemming | Programmer, assigned to Planting, Growing, and Harvesting, Watering, Time and Save/Load progress mechanics. |
| Brianna Zarman | Art & Narrative Direction; responsible for third-party placeholder asset implementation, audio sourcing, 2D assets design/implementation, and general management of art assets. |
| Elijah McGowan | |
| Benjamin Semler | UI art, UI implementation and HUD implementation. |
