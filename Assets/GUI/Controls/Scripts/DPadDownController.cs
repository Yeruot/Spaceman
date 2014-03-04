using UnityEngine;
using System.Collections;

public class DPadDownController : InteractiveController {

	public float x, y, width, height;

	public override void OnTouched(){
		print ("down");
		PlayerController.Instance.moveDown();
	}
}
