using UnityEngine;
using System.Collections;

[System.Serializable]
public class Item{
	public string itemName;
	public int itemID;
	public string itemDescription;
	public Texture2D itemIcon;
	public int itemPower;
	public int itemDamage;
	public int itemDefense;
	public int itemSpeed;
	public int itemRestore;
	public ItemType itemType; 

	public enum ItemType {
		Weapon,
		Chest,
		Neck,
		Consumable,
		Quest
	}

	public Item(string name, int id, string desc,
	            int power, int damage, int defense,
	            int speed, int restore, ItemType type){

		itemName = name;
		itemID = id;
		itemDescription = desc;
		itemPower = power;
		itemDamage = damage;
		itemDefense = defense;
		itemSpeed = speed;
		itemRestore = restore;
		itemType = type;
		itemIcon = Resources.Load<Texture2D>("Item Icons/" + itemName);
	}

	public Item(){}
}
