using System.Collections;
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
	// Use this for initialization
	void Awake () {
		instance = this;
	}

	void OnEnable(){
		SetActivePanel (0);
		ShowTutorialPanel ();
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
		for (int i = 0; i < 5; i++) {
			
			Vector3 pos = new Vector3 (0,-.01f,i);

			int rand = Random.Range (0, 2);

			GameObject _player;

			if (rand == 0) {
				_player = ObjectPool.instance.GetPlayerGirl ().gameObject;
			} else {
				_player = ObjectPool.instance.GetPlayerBoy ().gameObject;
			}

			AI ai = _player.GetComponent<AI> ();

			ai.fallTime = 100;

			_player.transform.position = pos;
			ai.raycasting = true;
			ai.Walk ();
			ai.movement.Run ();
			ai.WalkToDirection (Direction.LEFT);
		}
	}

	public void OnDoneTutorial2()
	{

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
				pos = new Vector3 (Random.Range(0,5),0,Random.Range(0,5));
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
	}

	public void OnDoneTutorial4()
	{

	}

	public void NextTut(){
		Time.timeScale = 0;
		if (CurrentTutorial < tutorialPanels.Length-1) {
			SetActivePanel (CurrentTutorial + 1);
		} else {
			SetActivePanel (0);
		}
		if (!isShow) {
			ShowTutorialPanel ();
		}
	}

	public void PreviousTut(){
		Time.timeScale = 0;
		if (CurrentTutorial == 0) {
			SetActivePanel (tutorialPanels.Length-1);
		} else {
			SetActivePanel (CurrentTutorial - 1);
		}
		if (!isShow) {
			ShowTutorialPanel ();
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