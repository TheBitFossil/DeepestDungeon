<h3 align ="center"> Gameplay Programmer using Unreal Engine, Godot or Unity.</h3>
<p align="center">- 🔭 I’m currently a student at the <a href ="https://www.school4games.net/">School For Games in Berlin</a></p>
<p align="center">- ⚡ Fun fact <b>I use Linux to make games.</b></p>

<h3 align="center">Connect with me:</h3>
<p align="center">
<a href="https://linkedin.com/in/thebitfossil" target="blank"><img align="center" src="https://raw.githubusercontent.com/rahuldkjain/github-profile-readme-generator/master/src/images/icons/Social/linked-in-alt.svg" alt="thebitfossil" height="30" width="40" /></a>
</p>

<h3 align="center">Languages and Tools:</h3>
<p align="center"> <a href="https://www.w3schools.com/cpp/" target="_blank" rel="noreferrer"> <img src="https://raw.githubusercontent.com/devicons/devicon/master/icons/cplusplus/cplusplus-original.svg" alt="cplusplus" width="36" height="36"/> </a> <a href="https://www.w3schools.com/cs/" target="_blank" rel="noreferrer"> <img src="https://raw.githubusercontent.com/devicons/devicon/master/icons/csharp/csharp-original.svg" alt="csharp" width="36" height="36"/> </a> <a href="https://www.linux.org/" target="_blank" rel="noreferrer"> <img src="https://raw.githubusercontent.com/devicons/devicon/master/icons/linux/linux-original.svg" alt="linux" width="36" height="36"/> </a> <a href="https://unity.com/" target="_blank" rel="noreferrer"> <img src="https://www.vectorlogo.zone/logos/unity3d/unity3d-icon.svg" alt="unity" width="36" height="40"/> </a> <a href="https://unrealengine.com/" target="_blank" rel="noreferrer"> <img src="https://raw.githubusercontent.com/kenangundogan/fontisto/036b7eca71aab1bef8e6a0518f7329f13ed62f6b/icons/svg/brand/unreal-engine.svg" alt="unreal" width="36" height="36"/> </a> </p>

# Deepest Dungeon (Student Project S4G Berlin)

*3D Action Game made with Unity 3D*\
\
**Manage your inventory** and time your dodge right while fighting your way through \
**endless dungeons** and see how deep you can reach. Using different combos \
from light to heavy attacks, in this pure **melee oriented gameplay**.

# Gameplay
* Fast paced
* Melee Action
* A Viking-Warrior
* With a rewarding Loot System
* Intuitive Controls

# My Role
* Gameplay Programmer
* Systems Programmer
* Audio Engineer

# Engine/ Languages
Unity 3D, C#

# Responsibilities
- Gameplay
  * <a href ="https://github.com/TheBitFossil/DeepestDungeon/tree/master/Assets/_Source/Actors/Player/Scripts">Character Locomotion</a>
  * Dodge
  * <a href ="https://github.com/TheBitFossil/DeepestDungeon/tree/master/Assets/_Source/Actors/Player/Weapons">Melee System</a>
  * Combo Attacks
  * Worked with 3D Artists on the animations 
    - Compressed all attack animations to achieve a faster response time and better player feedback
    - Fine-tuned transitions inside animation controller for the combo system
  * AI <a href ="https://github.com/TheBitFossil/DeepestDungeon/tree/master/Assets/_Source/Actors/Enemies">State-Machine
    - <a href ="https://github.com/TheBitFossil/DeepestDungeon/blob/master/Assets/_Source/Actors/Enemies/States/AttackState.cs">Attack</a>
    - <a href ="https://github.com/TheBitFossil/DeepestDungeon/blob/master/Assets/_Source/Actors/Enemies/States/ChaseState.cs">Chase</a>
    - <a href ="https://github.com/TheBitFossil/DeepestDungeon/blob/master/Assets/_Source/Actors/Enemies/States/IdleState.cs">Idle</a>
- System
  * <a href ="https://github.com/TheBitFossil/DeepestDungeon/blob/master/Assets/_Source/Coordinators/Game.cs">Core</a> Game-Data
  * <a href ="https://github.com/TheBitFossil/DeepestDungeon/tree/master/Assets/_Source/Inventory/Scripts">Inventory System</a>
  * Created a flexible & versatile <a href ="https://github.com/TheBitFossil/DeepestDungeon/tree/master/Assets/_Source/Loot/ItemDatabase">database in C#</a> 
    - allowing Game Designers to add any combination of stats
    - leading to a wide range of lootable objects
  * Implemented a spawn system which uses underlying database
    - there is no limitationto how many different item combinations could be spawned
    - current version contains over 1.2 million different items each run
  * Created Tools for the Game Designers and 3D Artists 
   - Script to change item customization during runtime 
   * Event Based <a href ="https://github.com/TheBitFossil/DeepestDungeon/tree/master/Assets/_Source/Coordinators">communication</a>
   * <a href ="https://github.com/TheBitFossil/DeepestDungeon/blob/master/Assets/_Source/Coordinators/LoadingScreen.cs">Loading screen</a>

- Version Control
  * Onboarding Game Designers and Artists with Perforce Integration
  * Keeping commit messages clear, readable and structured
 
- Audio
  * Implemented Sounds
  * Created Mix-Channels 
  * General Mastering

# Build and Installation
* Get the game [HERE](https://s4g.itch.io/deepest-dungeon)

* Installation
  - Download the Version for your System
  - Extract to a folder
  - Execute, Play and Have Fun!

The repository can be downlaoded and run with Unity Engine, light maps are missing because of size restrictions. \
Feel free to look through my C# Code any comments or changes are always welcome.

# License
Note that the source in this repository is licensed by the **MIT license model** and covers only \
the **source code** in this repository.

**All assets** are only for personal projecte and **commercial usage is not allowed**.

# About this repository
This repository was created during my 2nd Semester Project at the **School For Games in Berlin** \
and is maintained by **René Hecker a.k.a. TheBitFossil**.
