using UnityEngine;
using System.Collections;

public class IntroScript : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!audio.isPlaying)
		{     
			//	animation.PlayQueued("blink", QueueMode.PlayNow);
			//	animation.PlayQueued("fire", QueueMode.CompleteOthers);
			print("Audio Done");
			Application.LoadLevel ("MainScene");
		}
	}	
}
