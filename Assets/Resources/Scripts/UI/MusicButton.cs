using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicButton : MonoBehaviour {

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
		AudioManager_RG.instance.PlayClip (AudioManager_RG.SoundFX.PressBtn,transform.position);
		isActive = value;
	}

	public void TurnOnMusic()
	{
		DataController.Instance.SubmitMusicSetting ("t");
	}

	public void TurnOffMusic()
	{
		DataController.Instance.SubmitMusicSetting ("f");
	}


	void Update (){
		if (DataController.Instance.GetMusicSetting () == "t") {
			isActive = true;
		} else {
			isActive = false;
		}
	}
}
