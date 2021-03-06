﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour {

	public static Tutorial instance;

	public RectTransform[] tutorialPanels;

	public int CurrentTutorial;

	public RectTransform rectTransform;
	[SerializeField]
	private float time;
	[SerializeField]
	private bool isShow;

	public HomeTutorialGirlManager tut1girls;

	public AI rainGirlTut4;

	// Use this for initialization
	void Awake () {
		instance = this;
	}

	void OnEnable(){
		SetActivePanel (0);
		ShowTutorialPanel ();
	}

	void OnDisable(){
		ClearAI ();
		OnDoneTutorial4 ();
		CurrentTutorial = 0;
	}

	void Start()
	{
		SetActivePanel (0);
		ShowTutorialPanel ();
	}

	public void Tutorial1()
	{
		StopAllCoroutines ();
		WorldStates.instance.tutAllFence.SetActive (true);
		WorldStates.instance.tutFence.SetActive (true);
		Time.timeScale = 1;
		ClearAI ();

		GameObject _player;
		_player = ObjectPool.instance.GetPlayerGirl ().gameObject;
		_player.transform.position = new Vector3(2,0,2);
	}

	public void OnDoneTutorial1()
	{
		print ("OnDone1");
	}

	public void Tutorial2()
	{
		StopAllCoroutines ();
		Time.timeScale = 1;
		ClearAI ();

		WorldStates.instance.tutAllFence.SetActive (false);
		WorldStates.instance.tutFence.SetActive (true);
		tut1girls.On ();
	}

	public void OnDoneTutorial2()
	{
		tut1girls.Off ();
	}

	public void Tutorial3()
	{
		ScoreManager.score = 0;
		StopAllCoroutines ();
		Time.timeScale = 1;
		ClearAI ();
		WorldStates.instance.tutAllFence.SetActive (true);
		WorldStates.instance.tutFence.SetActive (false);
		StartCoroutine (SpawnTut3());
	}

	IEnumerator SpawnTut3()
	{
		Vector3 pos;
		while (true) {
			

			for (int i = 0; i < 5; i++) {
				pos = new Vector3 (Random.Range(0,5),1.5f,Random.Range(0,5));
				int rand = Random.Range (0, 2);
				GameObject _player;
				if (rand == 0) {
					_player = ObjectPool.instance.GetPlayerGirl ().gameObject;
				} else {
					_player = ObjectPool.instance.GetPlayerBoy ().gameObject;
				}

				_player.transform.position = pos;

				yield return new WaitForSeconds (4);
			}

		}
	}

	public void OnDoneTutorial3()
	{

	}

	public void Tutorial4()
	{
		SetActivePanel (3);
		Time.timeScale = 1;
		rainGirlTut4.gameObject.SetActive (true);
		rainGirlTut4.transform.position = new Vector3 (3, 0, 2);
		rainGirlTut4.WalkToDirection (Direction.RIGHT);
		rainGirlTut4.TurnOffAllArrow ();
		rainGirlTut4.ForceDizzy ();
		WorldStates.instance.tutAllFence.SetActive (true);
	}

	public void OnDoneTutorial4()
	{
		if(rainGirlTut4 != null)
		rainGirlTut4.gameObject.SetActive (false);
		WorldStates.instance.tutAllFence.SetActive (false);
	}

	public void NextTut(){
		
		StopAllCoroutines();
		ClearAI ();

		Time.timeScale = 0;
		if (CurrentTutorial < tutorialPanels.Length-1) {
			SetActivePanel (CurrentTutorial + 1);
		} else {
			SetActivePanel (0);
		}
		if (!isShow) {
			ShowTutorialPanel ();
		}

		if (CurrentTutorial != 1) {
			tut1girls.Off ();
		}

		if (CurrentTutorial != 3) {
			OnDoneTutorial4 ();
		}
	}

	public void PreviousTut(){
		
		StopAllCoroutines();
		ClearAI ();
		Time.timeScale = 0;

		if (CurrentTutorial == 0) {
			SetActivePanel (tutorialPanels.Length-1);
		} else {
			SetActivePanel (CurrentTutorial - 1);
		}
		if (!isShow) {
			ShowTutorialPanel ();
		}

		if (CurrentTutorial != 1) {
			tut1girls.Off ();
		}

		if (CurrentTutorial != 3) {
			OnDoneTutorial4 ();
		}
	}

	void SetActivePanel(int index)
	{
		for (int i = 0; i < tutorialPanels.Length; i++) {
			if (i == index) {
				CurrentTutorial = index;
				tutorialPanels [i].gameObject.SetActive (true);
			} else {
				tutorialPanels [i].gameObject.SetActive (false);
			}
		}
	}
		



	public void ShowTutorialPanel(){
		iTween.ValueTo(gameObject, 
			iTween.Hash(
				"from", rectTransform.anchoredPosition,
				"to", new Vector2(-1200, rectTransform.anchoredPosition.y),
				"time", time, 
				"easetype", iTween.EaseType.linear,
				"islocal",true,
				"onupdatetarget", this.gameObject, 
				"onupdate", "MovePos",
				"ignoretimescale", true
			));

	}
	public void CloseTutorialPanel(){
		iTween.ValueTo(gameObject, 
			iTween.Hash(
				"from", rectTransform.anchoredPosition,
				"to", new Vector2(0, rectTransform.anchoredPosition.y),
				"time", time, 
				"easetype", iTween.EaseType.linear,
				"islocal",true,
				"onupdatetarget", this.gameObject, 
				"onupdate", "MovePos",
				"ignoretimescale", true
			));
		isShow = false;
	}
	public void MovePos(Vector2 position){
		rectTransform.anchoredPosition = position;
	}

	public GameObject[] GetAllGirl()
	{
		return GameObject.FindGameObjectsWithTag ("AI");
	}

	public void ClearAI(){
		GameObject[] girls = GetAllGirl ();
		foreach (GameObject girl in girls) {
			if (girl != null) {
				RainGirl rain = girl.GetComponent<RainGirl> ();
				if (rain != null)
				rain.Destroy ();
			}
		}
	}
}