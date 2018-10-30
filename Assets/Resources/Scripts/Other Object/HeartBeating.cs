using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBeating : MonoBehaviour {

	// Use this for initialization
	void Start () {
		iTween.ShakePosition ( gameObject, iTween.Hash (
			"x", 3f,
			"y", 2f,
			"delay", 1f,
			"time", 1f,
			"loopType","pingPong"));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
