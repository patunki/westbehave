using Godot;
using System;

public partial class Workbench : StaticBody2D
{
    InteractionArea interactionArea;
    Inventory playerInventory;
    ItemDatabase itemDatabase;
    ItemList itemList;
    Button button;
    Item testItem;
    int testInput;

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
            itemList.AddItem(item.ITEM_NAME,item.ITEM_TEXTURE,true);
        }
        testInput = 2;
        testItem = itemDatabase.GetItem(testInput);
        

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
            playerInventory.AddItem(testItem,1);
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
                    if (item != null && item.ITEM_NAME == ingerdient){
                        has++;
                    }
                }   
            }
        }
        else {
            GD.Print("ei löäyty");
            return false;
        }
        if (has < need){
            GD.Print("ei oo tarpeeksi");
            return false;
        }
        else {
            GD.Print("on tarpeeksi");
            return true;
        }
        
    }
    void _on_button_button_down(){
        CraftItem();
    }

}
