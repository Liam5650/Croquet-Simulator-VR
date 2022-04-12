# Croquet Simulator VR

![Screenshot of Game](https://imgur.com/nHr9O1W.png)

_last modified 2022-04-12_

By Tyson Moll (500493254) and Liam Iles (501005188)

_A perfectly normal simulation of everyone's favourite lawn sport!_

## Description

Croquet Simulator VR is an exciting new way to experience golf croquet from the comfort of your home. Our game takes place in Croquette, a dainty little English village nestled deep in the Mallet mountains full of citizens who love nothing more than a good round of lawn sports. To play croquet, simply knock both blue and red croquet balls through the designated wickets on each course to complete each level. Strategically roquet your croquet balls by knocking them together to reduce your stroke count along the way. As with any perfect simulation, Croquet Simulator VR is not without its unexpected lifelike hazards: beware feral chickens and gravity wells as you line up your perfect shot down the pitch.

https://www.youtube.com/watch?v=A6HxzAtnkiI

https://vultures.itch.io/croquet-simulator
Access is restricted; use password ‘roquet_chicken’


## User Manual

### About

The game has been built using Unity version 2021.2.12f1 with VRTK v4 (Virtual Reality Toolkit, version 4) and playtested using an HP Reverb G2 mixed reality headset. Full attribution for all assets is listed at the end of this document.


__Players:__
This package should include an .exe file to open the project. Simply run this file to start the game.

__Evaluators:__
If this package includes the project files, they can be simply imported as a new project under the Unity Hub using the specified editor version if one wishes to examine or modify the project. All necessary packages and assets will be included by default so no further setup is necessary to start the complete project in Unity. 

### Game Setup

This package includes input mappings for a variety of vendors, such as Oculus, HTC, and other mixed reality kits. To start the game with correct mappings, the user must ensure that their VR kit is turned on and recognized by their computer before opening the game executable. The game will then open to the first level and the user should be able to move freely and see their controllers represented as objects within the gameworld. 

### Game Control

- Teleport / Menu Navigation - Left Thumbstick
- Open / Close Menu - Option 2 Button
- Grasp / Release Object - Grip 1 Button

#### World Navigation

When the player starts the game and loads into the first level, their real-world movements will be directly translated into exact gameworld positional locomotion by the headset and controllers. In order to move larger distances in the gameworld, the user is given the ability to teleport. To teleport, the user must first press and hold the left thumbstick. A laser will be fired out of the front of the left controller. The user may point the laser where they would like to teleport. If the user is pointing at a valid location to teleport, the laser will be colored blue and a blue circle will appear on the intersection point of the laser and ground to represent where the player will teleport. The player must then release the left thumbstick, and a teleport will be performed. If the laser is colored red, the user is not able to teleport to the intersected location. 

#### Menu Navigation

The player can access the main menu at any time by pressing the Options2 button on either controller. Here the player may access a variety of things such as level selection, option configurations, and quitting the game. To select an option, the player must press and hold in the left thumbstick to fire a laser. When a button in the menu is hit by the laser, it will be outlined in green to show that it has been selected. When the player releases the left thumbstick, any highlighted button will be executed.

#### Object Interaction

The player is given the ability to physically interact with a variety of objects such as the croquet mallet and balls. In order to pick up an object, the player must press the grip1 button on either of the controllers, depending on which hand they are trying to grab with. The player does not need to hold the button to hold the object - pressing once is sufficient. To release an object, the player must press the grip1 button again.


## Description of Implementation

### Scripts

__Gameplay Management Scripts__
* StatManager - level-local stat managements script tracking the current stroke count, the total number of wickets in a course, the progress of the croquet balls, as well as text displays relevant to this information
* MenuController - handles the menu input action from the PlayerController script. Opens and closes the menu appropriately depending on its current state
* BlackScreenController - controls the screen fading behavior as used in teleports and level transitions

__Controller Scripts__
* ControllerController - handles player-object interactions. Creates and destroys joints between the controllers and grabbed objects, as well as tracks controller velocity for an object to inherit when it is released
* LaserController - handles laser interactions such as when needed for teleporting or for menu button selection. When used for teleporting by hitting the ground, it creates a reference intersection point to pass to the TeleportController script. When used for menu selection, it stores a reference to the selected button and executes its respective script.
* PlayerController - handles all player input and directs it to their respective scripts to execute
* TeleportController - handles the actual teleportation of the player based on the LaserController reference, and invokes the BlackScreenController to fade in and out during the process

__Croquet Gameplay Scripts__
* BallTracker - basic script used by croquet balls to indicate which ball they are, what their next gate is to be, communicate with the stats manager when they score, and trigger basic audio when general collisions occur
* CroquetMallet - basic script used by croquet mallets to produce audio when hitting a croquet ball
* PortalManager - Used by wickets to track the progress of any croquet balls that have interacted with the wicket (as enumerated states). Ensures that wickets are scored as one-way portals and only when entered in the order designated.
* PortalMonitor - Used by wicket gate triggers to communicate when objects enter or exit their area

__Menu Button Scripts__
* ButtonController - acts as a superclass for the rest of the button scripts. Allows other scripts to not have to have a reference for every class of button for their respective execution functionality, and instead can simply call the ButtonController execute() method which is then overridden by other button scripts
* MenuButton - used for buttons that navigate to other sub-menus in the main menu
* ModifierButton - modifies the Y position of some object (in particular, the player height)
* QuitButton - sends an application.quit signal
* ResetButton - Resets the scene and all player stats
* SceneButton - loads a scene defined by ‘sceneToLoad’ when triggered

__Other Scripts__
* ChickenAnimator - State machine logic driving chicken behaviors (jumping, turning, object avoidance)
* ChickenSpawn - simple component function that spawns chickens when requested in a circular area.
* RotateTime - simple animation: rotates an object over time 

### Assets Used / Created

----- Models -----

Croquet Models, Furniture Props, Housing, Mountains by Tyson Moll 
(original creation, copyright of the Author)

----- Texture -----

Aged Wood Planks PBR Material by FreePBR under Free Non-Commercial license
https://freepbr.com/materials/aged-wood-planks-pbr-material/
https://freepbr.com/about-free-pbr/

Seamless Dirty Concrete Wall Texture by Seme Design Lab under Royalty Free License on Texturise
http://www.texturise.club/2017/01/seamless-dirty-concrete-wall-texture.html
http://www.texturise.club/p/licence_5.html

Stucco #1 PBR Material by FreePBR under Free Non-Commercial license
https://freepbr.com/materials/stucco-1/
https://freepbr.com/about-free-pbr/

Stylized Grass 1 PBR Material by FreePBR under Free Non-Commercial license
https://freepbr.com/materials/stylized-grass1/
https://freepbr.com/about-free-pbr/

Worn Out Old Brick Wall PBR Material by FreePBR under Free Non-Commercial license
https://freepbr.com/materials/worn-out-old-brick-wall-pbr-material/
https://freepbr.com/about-free-pbr/

----- Skybox -----

Skybox Series Free by Avionx under Standard Unity Asset Store EULA 
https://assetstore.unity.com/packages/2d/textures-materials/sky/skybox-series-free-103633
https://unity3d.com/legal/as_terms

----- Sound -----

Game ball tap by Envato Elements on Mixkit, licensed under Mixkit License
https://mixkit.co/free-sound-effects/ball/
https://mixkit.co/license/#sfxFree

Light impact on the ground by Envato Elements on Mixkit, licensed under Mixkit License
https://mixkit.co/free-sound-effects/ball/
https://mixkit.co/license/#sfxFree

Long pop by Envato Elements on Mixkit, licensed under Mixkit License
https://mixkit.co/free-sound-effects/pop/
https://mixkit.co/license/#sfxFree

Industrial machine engine hum loop by Envato Elements on Mixkit, licensed under Mixkit License
https://mixkit.co/free-sound-effects/hum/
https://mixkit.co/license/#sfxFree

Modern technology select by Envato Elements on Mixkit, licensed under Mixkit License
https://mixkit.co/free-sound-effects/click/?page=2
https://mixkit.co/license/#sfxFree

Retro arcade casino notification by Envato Elements on Mixkit, licensed under Mixkit License
https://mixkit.co/free-sound-effects/click/
https://mixkit.co/license/#sfxFree

Single classic click by Envato Elements on Mixkit, licensed under Mixkit License
https://mixkit.co/free-sound-effects/click/
https://mixkit.co/license/#sfxFree

Nature - Essentials by Nox_Sound under Standard Unity Asset Store EULA 
https://assetstore.unity.com/packages/audio/ambient/nature/nature-essentials-208227
https://unity3d.com/legal/as_terms

### Project File Navigation

Each level contains three organizational empty objects in its Hierarchy: DynamicObjects, Environment, and GameManagement. DynamicObjects consist of objects that can be manipulated by the player as well as objects that define a level (e.g. wickets). Environment contains the models that decorate the local landscape, including houses and the fenceline. Finally, GameManagement contains the player VR rig and the statistics tracking objects.

__Miscellaneous:__
Components involved in player interaction with the world are contained within the Player prefab.
Wickets’ portal logic is contained within their PortalTriggers child component
The Scripts description of this document should describe where the scripts are located, generally
Tutorial billboards are located in some levels to provide instructions on how to play as well as detail the current progress through the game.

### Further Development

To extend the project, we would like to consider adding the following:
- Background music for the main levels
- Goal celebration (confetti pop etc)
- Statistics tracking (to record records)
- curved laser
- SHIFT testing
- adjustment of teleportation graphic
- additional SFX
- shader(s)
- mallet collision improvements
- stroke cooldown timer to prevent double hits
- stroke distance check to allow accidental, mm-long taps
- ramp adjustments for ease-of-use
- hoops shouldn’t move
- teleport accuracy
- Increase size of rectangle wickets
