using UnityEngine;
using System.Collections;

public class CameraButtonController : InteractiveController {
	
	void Start () {
	}
	
	public override void OnTouched(){
		print ("Camera button pressed");
		PlayerCameraController.Instance.ChangeView();
	}
}
