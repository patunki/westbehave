using Godot;
using System;

public partial class Workbench : StaticBody2D
{
    Entity player;
    InteractionArea interactionArea;
    Inventory playerInventory;
    ItemDatabase itemDatabase;
    ItemList itemList;
    Button button;
    Item itemToCraft;
    public int input;
    bool isOpen = false;


    public override void _Ready()
    {
        player = GetTree().GetFirstNodeInGroup("Player") as Entity;
        interactionArea = GetNode<InteractionArea>("InteractionArea");
        interactionArea.callable = Callable.From(() => interactionArea.Interact(this, "OnInteract"));
        playerInventory = player.inventory;
        itemDatabase = GetNode<ItemDatabase>("/root/ItemDatabase");
        itemList = GetNode<ItemList>("ItemList");
        itemList.Hide();
        button = GetNode<Button>("Button");
        button.Hide();
        interactionArea.BodyExited += Close;
        
        int i = 0;
        foreach (Item item in itemDatabase.itemDatabase){
            if (item.IS_CRAFTABLE){
                itemList.AddItem(item.ITEM_NAME,item.ITEM_TEXTURE,true);
                itemList.SetItemMetadata(i,item.ITEM_ID);
                i++;
            }
        }


    }

    public void OnInteract(){
        if (isOpen){
            Close(this);
        }
        else {
            Open();
        }
    }
    void Close(Node2D body){
        itemList.Hide();
        button.Hide();
        isOpen = false;
    }
    void Open(){
        itemList.Show();
        button.Show();
        isOpen = true;
    }
    public void CraftItem(){
        if (CanCraft(input)){
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

    void _on_item_list_item_selected(int index){
        input = (int)itemList.GetItemMetadata(index);
        itemToCraft = itemDatabase.GetItemById(input);
    }
    void _on_button_button_down(){
        GD.Print(input);
        CraftItem();
    }

}
