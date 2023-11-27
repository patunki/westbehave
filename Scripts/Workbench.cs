using Godot;
using System;

public partial class Workbench : StaticBody2D
{
    InteractionArea interactionArea;
    Inventory playerInventory;
    ItemDatabase itemDatabase;
    ItemList itemList;
    Button button;
    Item itemToCraft;
    public int testInput;

    public override void _Ready()
    {
        interactionArea = GetNode<InteractionArea>("InteractionArea");
        interactionArea.callable = Callable.From(() => interactionArea.Interact(this, "OnInteract"));
        playerInventory = GD.Load<Inventory>("res://Player/PlayerInventory.tres");
        itemDatabase = GetNode<ItemDatabase>("/root/ItemDatabase");
        itemList = GetNode<ItemList>("ItemList");
        itemList.Hide();
        button = GetNode<Button>("Button");
        button.Hide();
        interactionArea.BodyExited += Close;
        foreach (Item item in itemDatabase.itemDatabase){
            if (item.IS_CRAFTABLE){
                itemList.AddItem(item.ITEM_NAME,item.ITEM_TEXTURE,true);
            }
        }
        testInput = 9;
        itemToCraft = itemDatabase.GetItemById(testInput);
        

    }

    public void OnInteract(){
        Open();
    }
    void Close(Node2D body){
        itemList.Hide();
        button.Hide();
    }
    void Open(){
        itemList.Show();
        button.Show();
    }
    public void CraftItem(){
        if (CanCraft(testInput)){
            playerInventory.AddItem(itemToCraft,1);
        }
        
    }
    private bool CanCraft(int id){
        int need;
        int has;
        if (itemDatabase.Recipes.TryGetValue(id, out var ingredients)){
            has = 0;
            need = ingredients.Count;
            foreach(Item item in playerInventory.InventoryItems){
                foreach(string ingerdient in ingredients){
                    if (item != null && item.ITEM_NAME == ingerdient && item.ITEM_QUANTITY > 0){
                        has++;
                    }
                }   
            }
        }
        else {
            GD.Print("Itemiä ei löydu databasesta");
            return false;
        }
        if (has < need || itemToCraft.IS_CRAFTABLE == false){
            GD.Print("ei oo tarpeeksi tai ei voi kräftää");
            return false;
        }
        else {
            GD.Print("viedään maksu");
            foreach(Item item in playerInventory.InventoryItems){
                foreach(string ingerdient in ingredients){
                    if (item != null && item.ITEM_NAME == ingerdient){
                        item.DecQuant();
                        playerInventory.NullItemCheck();
                    }
                }   
            }
            return true;
        }
        
    }
    void _on_button_button_down(){
        CraftItem();
    }

}
