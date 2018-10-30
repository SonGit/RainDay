using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXButton : MonoBehaviour {


	[SerializeField]
	private GameObject activeBtn;
	[SerializeField]
	private GameObject inactiveBtn;

	private bool _isActive;

	public bool isActive
	{
		get {
			return _isActive;
		}

		set {
			_isActive = value;

			if (_isActive) {
				activeBtn.SetActive (true);
				inactiveBtn.SetActive (false);
			} else {
				activeBtn.SetActive (false);
				inactiveBtn.SetActive (true);
			}
		}
	}

	// Use this for initialization
	void Start () {

	}

	public void OnClickMusicBtn(bool value)
	{
		isActive = value;
	}

	void TurnOnMusic()
	{

	}

	void TurnOffMusic()
	{

	}
}
