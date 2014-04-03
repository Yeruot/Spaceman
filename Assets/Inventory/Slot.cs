using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Slot :  MonoBehaviour {
	public Item item;
	public string tooltip;
	public bool showTooltip;
	

	void CreateToolTip(){
		tooltip = item.itemName;
	}
}
