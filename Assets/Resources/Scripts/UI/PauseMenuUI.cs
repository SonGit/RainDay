using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuUI : MonoBehaviour {
	public static PauseMenuUI instance;
	[SerializeField]
	private RectTransform rectTransform;
	[SerializeField]
	private float time;
	public void ShowPauseMenu(){
		AudioManager_RG.instance.PlayClip (AudioManager_RG.SoundFX.PressBtn,transform.position);
		iTween.ValueTo(gameObject, 
			iTween.Hash(
				"from", rectTransform.anchoredPosition,
				"to", new Vector2(rectTransform.anchoredPosition.x, 0),
				"time", time, 
				"easetype", iTween.EaseType.linear,
				"islocal",true,
				"onupdatetarget", this.gameObject, 
				"onupdate", "MovePos",
				"ignoretimescale", true));

	}
	public void ClosePauseMenu(){
		AudioManager_RG.instance.PlayClip (AudioManager_RG.SoundFX.PressBtn,transform.position);
		iTween.ValueTo(gameObject, 
			iTween.Hash(
				"from", rectTransform.anchoredPosition,
				"to", new Vector2(rectTransform.anchoredPosition.x, 550),
				"time", time, 
				"easetype", iTween.EaseType.linear,
				"islocal",true,
				"onupdatetarget", this.gameObject, 
				"onupdate", "MovePos",
				"ignoretimescale", true));

	}
	public void MovePos(Vector2 position){
		rectTransform.anchoredPosition = position;
	}

	void Awake () {
		if (instance == null) {
			instance = this;
		}
	}

	// Update is called once per frame
	void Update () {

	}
}
