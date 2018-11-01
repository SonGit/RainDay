using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenuUI : MonoBehaviour {
	public static GameOverMenuUI instance;
	// Use this for initialization

	[SerializeField]
	private RectTransform rectTransform;
	[SerializeField]
	private float time;

	public void ShowGameOverMenu(){
		iTween.ValueTo(gameObject, 
			iTween.Hash(
				"from", rectTransform.anchoredPosition,
				"to", new Vector2(rectTransform.anchoredPosition.x, -50),
				"time", time, 
				"easetype", iTween.EaseType.linear,
				"islocal",true,
				"onupdatetarget", this.gameObject, 
				"onupdate", "MovePos",
				"delay",0.3));

	}
	public void CloseGameOverMenu(){
		iTween.ValueTo(gameObject, 
			iTween.Hash(
				"from", rectTransform.anchoredPosition,
				"to", new Vector2(rectTransform.anchoredPosition.x, 1100),
				"time", time, 
				"easetype", iTween.EaseType.linear,
				"islocal",true,
				"onupdatetarget", this.gameObject, 
				"onupdate", "MovePos",
				"delay",0.3));

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
