# Project Westbehave
This is a personal project made in godot-mono using C#. The goal is to make a 2D top-down zombie survival game. 

# Info
I have tried to abstract as much of the code as possible using a mix of composition and inheritance. Most features also utilize godots exported variables which makes it incredibly simple to reuse code. 
For example all enteties (such as zombies, bandits, the player) inherit the Entity class which stores multiple exported variables. This then allows me to make a godot node an instance of that class, configure it in the editor and save it for later use.
The exorted variables can be of almost any type, which allows me to make a class such as Item, then have an exported array of items as the inventory.

Features added so far:
```
-Inventory system.
-Item system.
-Finite state machine for enemies and npcs.
-Crafting system
-Farming system
-Tools such as: axe, hoe, watering can.
-Weapons
-Hunger and thirst system.
-Health System
```
### Item system
I made a class called Item that inherits godot Resource. It stores values like texture, quantity, name, type (enum) etc. All items inherit from this class and add their own variables as needed. I do not have a script for each item but instead for each item type that stores the data that an instance of that item could need. So instead of having a script for each sword in the game, I just have a class Sword (that inherits Item) and have all my swords as an instance of that class with cahnges being made in the exported variables. <br><br>

Each item has a boolean value hasScene and a string scenePath. When an item is equipped, the EquippedItem node checks if the node has a scene, if it does, an instance of that scene is made. These scenes are also made for each type only. The EquippedItem node knows which item is selected and feeds that item to the scene as an resource. The item scene then has all the needed methods and gets the values from the item.
### Inventory system
To make the inventort system I made the following classes: <br>
-Inventory : Exports a Godot.Collections.Array of Item class <br>
-DragSlot : Handles the item that is currently being moved. <br>
-InventoryUi : Handles inventory ui. <br>
-InventorySlot : Responsible for displaying item texture and reqistering mouse inputs. <br>
