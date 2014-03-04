using UnityEngine;
using System.Collections;

public class InteractiveController : MonoBehaviour {

	public virtual void OnTouched(){
		print("I got touched, but OnTouched is not implemented");
	}

	public virtual void OnHold(){
		print("I got held, but OnHold is not implemented");
	}
}
