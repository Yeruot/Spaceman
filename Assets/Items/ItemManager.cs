using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ItemManager : MonoBehaviour {
	public List<Item> items = new List<Item>();
	private static ItemManager instance = null;


	// The below is code to make the player controller a singleton
	// this will allow multiple objects to refer to a single player
	// object and act accordingly
	public static ItemManager Instance
	{
		get {return instance;}
	}
	
	void Awake(){
		if (instance != null && instance != this){
			Destroy(this.gameObject);
			return;
		} else{
			instance = this;
		}

		DontDestroyOnLoad(this.gameObject);
	}

	void Start() {
		items.Add(new Item("Amulet Of Power", 0, "I have the power!", 2, 0, 1, 0, 0, Item.ItemType.Neck));
		items.Add(new Item("White Shirt", 1, "Its white!", 0, 0, 1, 1, 0, Item.ItemType.Chest));
		items.Add(new Item("Basic Club", 2, "The club can't even handle me right now", 1, 2, 1, 0, 0, Item.ItemType.Weapon));
		items.Add(new Item("Nut", 3, "You're going to love my nuts!", 0, 0, 0, 0, 100, Item.ItemType.Consumable));
		items.Add(new Item("Glasses", 4, "DJ TURN THE VOLUME UP!", 10000, 10000, 100000, 10000, 10000, Item.ItemType.Quest));
	}

	public Item GetItemWithId(int id){
		int index = items.FindIndex(item => item.itemID == id);

		if (index >= 0)
			return items[index];
		else
			return null;
	}
}
