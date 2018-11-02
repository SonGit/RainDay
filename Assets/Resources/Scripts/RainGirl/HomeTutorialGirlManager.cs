using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeTutorialGirlManager : MonoBehaviour {

	AI[] girls;

	public Direction[] originDirs;

	Vector3[] originPos;

	// Use this for initialization
	void Start () {
		
		girls = this.GetComponentsInChildren<AI> ();
		originDirs = new Direction[girls.Length];
		originPos = new Vector3[girls.Length];

		for (int i = 0; i < girls.Length; i++) {
			originDirs [i] = girls [i].movement.direction;
			originPos [i] = girls [i].transform.position;
		}

		Off ();
	}
	
	public void On()
	{
		for (int i = 0; i < girls.Length; i++) {
			girls [i].gameObject.SetActive (true);
			girls [i].Reset ();
			girls [i].transform.position = originPos [i];
			girls [i].WalkToDirection (originDirs [i]);
			girls [i].movement.speed = 1;
		}
	}

	public void Off()
	{
		foreach (AI girl in girls) {
			girl.gameObject.SetActive (false);
		}
	}
}
