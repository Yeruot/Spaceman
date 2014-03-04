using UnityEngine;
using System.Collections;

public class DPadRightController : InteractiveController {

	public override void OnTouched(){
		print ("right");
		PlayerController.Instance.moveRight();
	}
}
