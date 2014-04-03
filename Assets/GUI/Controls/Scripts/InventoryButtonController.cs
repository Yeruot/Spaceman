using UnityEngine;
using System.Collections;

public class InventoryButtonController : InteractiveController {

	void Start () {
	}

	void Update () {
		if(Input.GetKeyDown("i")){
			Inventory.Instance.Toggle();
		}
	}

	public override void OnTouched(){
		print ("Inventory button pressed");
		Inventory.Instance.Toggle();
	}
}
