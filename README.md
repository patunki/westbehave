This is a personal project made in godot-mono using C#. The goal is to make a 2D top-down zombie survival game. 

I have tried to abstract as much of the code as possible using a mix of composition and inheritance. Most features also utilize godots exported variables which makes it incredibly simple to reuse code. 
For example all enteties (such as zombies, bandits, the player) inherit the Entity class which stores multiple exported variables. This then allows me to make a godot node an instance of that class, configure it in the editor and save it for later use.
The exorted variables can be of almost any type, which allows me to make a class such as Item, then have an exported array of items as the inventory.

Features added so far:
-Inventory system.
-Item system.
-Finite state machine for enemies and npcs.
-Crafting system
-Farming system
-Tools such as: axe, hoe, watering can.
-Weapons
-Hunger and thirst system.
