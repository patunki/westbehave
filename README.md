# Project Westbehave
This is a personal project made in godot-mono using C#. The goal is to make a 2D top-down zombie survival game. 
![ksnip_20240123-025355](https://github.com/patunki/westbehave/assets/96471980/571447bd-d23c-4dca-bb80-d2cd8c2f6893)

# Info
I have tried to abstract as much of the code as possible using a mix of composition and inheritance. Most features also utilize godots exported variables which makes it incredibly simple to reuse code. 
For example all enteties (such as zombies, bandits, the player) inherit the Entity class which stores multiple exported variables. This then allows me to make a godot node an instance of that class, configure it in the editor and save it for later use.
The exorted variables can be of almost any type, which allows me to for example make a class such as Item, then have an exported array of items as the inventory.

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
I made a class called Item that inherits godot Resource. It stores values like texture, quantity, name, type (enum) etc. All items inherit from this class and add their own variables as needed. I do not have a script for each item but instead for each item type that stores the data that an instance of that item could need. So instead of having a script for each axe in the game, I just have a class Axe (that inherits Item) and have all my swords as an instance of that class with changes being made in the exported variables. <br><br>

Each item has a boolean value hasScene and a string scenePath. When an item is equipped, the EquippedItem node checks if the node has a scene, if it does, an instance of that scene is made. These scenes are also made for each type only. The EquippedItem node knows which item is selected and feeds that item to the scene as an resource. The item scene then has all the needed methods and gets the values from the item.
### Inventory system
To make the inventort system I made the following classes: <br>
-Inventory : Exports a Godot.Collections.Array of Item class <br>
-DragSlot : Handles the item that is currently being moved. <br>
-InventoryUi : Handles inventory ui. <br>
-InventorySlot : Responsible for displaying item texture and registering mouse inputs. <br>

Each slot only knows what's in it's corresponding slot in the inventory array. DragSlot works similarry but only has variables for the Item and a method to pick up an item and drop an item. There is only one DragSlot. It follows the mouse and is invisible unless there is an item in it. Each InventorySlot gets a reference trough a signal to the GameMaster singleton. Signals in godot are events that allow nodes to talk to each other. When a slot is clicked, the slot talks with the DragSlot to figure out what to do taking both items and the button used into account.
### Finite state machine
I made a class called State that has virtual methods for Enter, Process and Exit. The state machine itself has a reference to the entity it's controlling, and feeds that into any given state in the parameters for Enter. The state machine calls the current states Process method every frame. When the exit condition is met, the state sends a signal to the state machine, which tells it what state to move into.
### Crafting system.
This is one of the more temporary ones. Currently I have a dictionary that stores dictionaries with the item name as the key and the amount needed as the value. The workbech gets a reference to the inventory trough the InteractionManager sigleton. The item is requested by id and the workcbech checks if the player can craft it. If so, the item is crafted and materials reduced accordingly.
### Farming System
Currently the farming system only has one plant. The seed item has a method that checks if the tile in the tilemap node is dirt and if it is, it plants an eggplant which is a scene instance. this scene instance has a timer for growing and drying. Watering the plant refreshes the dryig timer.
### Hunger and thirst
Food items have a value for how much food and water they restore. The food item then simply runs a method on the hunger component when eaten.
### Health system
Similar to hunger and thirst, health is handled by a node that can be attached to any entity. It simply stores the health amount and handles what happens when you eat, drink, die etc.
### Work in progress
Most of the work done isn't something that can easily be shown yet, but I will continue to update this readme as I progress.
