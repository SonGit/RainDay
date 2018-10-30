using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDoor : MonoBehaviour {
	public static AutoDoor instance;
	Animator anim;
	void Awake()
	{
		if (instance == null)
			instance = this;
		anim = GetComponent<Animator> ();
	}
	public enum DoorID{
		RED,
		BLUE,
		YELLOW
	}
	[SerializeField]
	private DoorID id;
	[SerializeField]
	private float speed;

	[SerializeField]
	private bool isRay;

	public bool _isRay
	{

		get {
			return isRay;
		}

		set {
			isRay = value;

			if (anim != null) {
				anim.SetBool ("isRay",isRay);
			}
		}

	}

	// Use this for initialization
	public void openDoor() {
		if (!_isRay) {
			_isRay = true;
		}
	}
	public void closeDoor() {
		if (!_isRay) {
			_isRay = false;
		}
	}
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (_isRay)
			openDoor ();
		else
			closeDoor ();
	}
}
