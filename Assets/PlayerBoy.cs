using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoy : Cacheable {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void OnLive ()
	{
		gameObject.SetActive (true);
	}

	public override void OnDestroy ()
	{
		gameObject.SetActive (false);
	}
}
