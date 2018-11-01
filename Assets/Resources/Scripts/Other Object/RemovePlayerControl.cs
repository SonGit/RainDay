using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemovePlayerControl : MonoBehaviour {
	
	public Direction direction;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "AI") {
			AI rainGirl = other.GetComponent<AI> ();

			if (rainGirl != null) {
				if(rainGirl.currentState != AI.RGState.GPS)
				rainGirl.GoHome (direction);
			}
				
		}
	}
}
