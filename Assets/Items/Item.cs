using UnityEngine;
using System.Collections;

[System.Serializable]
public class Item{
	public string name;
	public int itenID;
	public string itemDescription;
	public Texture2D itemIcon;
	public int power;
	public int damage;
	public int defense;
	public ItemType itemType; 

	public enum ItemType {
		Weapon,
		Consumable,
		Quest
	}
}
