using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
	public GUISkin slotSkin;
	public int slotsX, slotsY;
	public List<Item> inventory = new List<Item>();
	public List<Item> slots = new List<Item>();
	public bool showInventory;
	public bool showTooltip;
	public string tooltip;
	private Rect slotRect;
	private Vector2 tooltipPosition;
	private ItemManager itemManager;

	private static Inventory instance = null;

	// The below is code to make the player controller a singleton
	// this will allow multiple objects to refer to a single player
	// object and act accordingly
	public static Inventory Instance {
		get {return instance;}
	}
	
	void Awake() {
		if (instance != null && instance != this){
			Destroy(this.gameObject);
			return;
		} else{
			instance = this;
		}
		
		DontDestroyOnLoad(this.gameObject);
	}

	void Start() {
		for(int i = 0; i < (slotsX * slotsY); i++){
			slots.Add(new Item());
			inventory.Add (new Item());
		}

		showInventory = false;

		itemManager = ItemManager.Instance;
		AddItem(1);
		AddItem(2);
		AddItem(3);
	}
	
	void OnGUI() {
		showTooltip = false;
		GUI.skin = slotSkin;
		if(showInventory){
			DrawInventory();
		}
		if(showTooltip){
			GUI.Box (new Rect(tooltipPosition.x + 100, tooltipPosition.y + 100, 200, 200), tooltip, slotSkin.GetStyle("Tooltip"));
		}
	}

	void DrawInventory(){
		int i = 0;
		for(int y = 0; y < slotsY; y++){
			for(int x = 0; x < slotsX; x++){
				Rect slotRect = new Rect(x * 110, y * 110, 100, 100);
				GUI.Box(slotRect, "", slotSkin.GetStyle("Slot"));
				slots[i] = Inventory.Instance.inventory[i];

				if(slots[i].itemName != null){
					GUI.DrawTexture(slotRect, slots[i].itemIcon);
					for(int k = 0; k < Input.touchCount; k++){
						Touch touch = Input.GetTouch(k);
						if(slotRect.Contains(touch.position)){
							CreateToolTip(slots[i]);
							showTooltip = true;
							tooltipPosition = touch.position;
						}
					}
						
					if(slotRect.Contains(Event.current.mousePosition)){
						CreateToolTip(slots[i]);
						showTooltip = true;
						tooltipPosition = Event.current.mousePosition;
					}
						
				}
				i++;
			}
		}
	}

	public void Toggle(){
		showInventory = !showInventory;
	}

	public void AddItem(int id){
		for(int i = 0; i < inventory.Count; i++){
			if (inventory[i].itemName == null){
				Item returnItem = itemManager.GetItemWithId(id);
				if(returnItem != null){
					inventory[i] = returnItem;
				}
				break;
			}
		}
	}

	public void RemoveItem(int id){
		for(int i = 0; i < inventory.Count; i++){
			if (contains(id)){
				inventory[GetIndexOfItemID(id)] = new Item();
			}
		}
	}

	// returns index if inventory contains the item
	// -1 otherwise
	public bool contains(int id){
		int index = inventory.FindIndex(item => item.itemID == id);
		
		if (index >= 0)
			return true;
		else
			return false;
	}

	// returns index if inventory contains the item
	// -1 otherwise
	public int GetIndexOfItemID(int id){
		int index = inventory.FindIndex(item => item.itemID == id);
		
		if (index >= 0)
			return index;
		else
			return -1;
	}
	
	public int GetIndexOfItemName(string name){
		int index = inventory.FindIndex(item => item.itemName == name);
		
		if (index >= 0)
			return index;
		else
			return -1;
	}

	public void CreateToolTip(Item it){
		tooltip = "";
		tooltip = "<color=##686868>" + it.itemName + "</color>\n"
			+ "<color=#ffffff>" + "\"" + it.itemDescription + "\"" + "</color>\n\n";
				
		if(it.itemType != Item.ItemType.Consumable){
				tooltip = tooltip + "<color=#ffffff>" + "Defense: " + "</color>" + "<color=#000fff>" + it.itemDefense + "</color>\n"
						+ "<color=#ffffff>" + "Power: " + "</color>" + "<color=#000fff>" + it.itemPower + "</color>\n";
		}
		if(it.itemType == Item.ItemType.Chest){
			tooltip = tooltip + "<color=#ffffff>" + "Speed: " + "</color>" + "<color=#000fff>" + it.itemSpeed + "</color>\n";
		}
		if(it.itemType == Item.ItemType.Weapon){
			tooltip = tooltip + "<color=#ffffff>" + "Damage: " + "</color>" + "<color=#000fff>" + it.itemDamage + "</color>\n";
		}
		if(it.itemType == Item.ItemType.Consumable){
			tooltip = tooltip + "<color=#ffffff>" + "Restore: " + "</color>" + "<color=#000fff>" + it.itemRestore + "</color>\n";
		}
	}


}
