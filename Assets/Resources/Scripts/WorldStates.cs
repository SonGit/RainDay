using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldStates : MonoBehaviour {

	public static WorldStates instance;

	public GameObject spawner;

	public bool isStartle;

	public Canvas gameplayCanvas;

	public float speed;

	[SerializeField]
	bool gameStarted;

	[SerializeField]
	private GameObject SharkJump;

	public float spawnerDelay = 2;


	void Awake()
	{
		instance = this;
	}
	void Start () {
		speed = .8f;
		gameplayCanvas.enabled = false;
	}
	void Update(){

		if(gameStarted)
		SpawnIncrease ();

	}

	[SerializeField]
	float spawnTimeCount = 0;
	[SerializeField]
	float spawnTime = 10;

	public float numSpawn = 1;

	void SpawnIncrease()
	{
		spawnTimeCount += Time.deltaTime;

		if (spawnTimeCount > spawnTime) {
			numSpawn++;

			if (numSpawn > 4) {
				numSpawn = 4;
			} else {
				spawnerDelay += 2;
			}

			spawnTimeCount = 0;
		}
	}

	void Reset()
	{
		speed = .8f;
		numSpawn = 1;
		spawnTimeCount = 0;
		spawnerDelay = 2;

		LifeManager.instance.currentlife = 3;

		SharkJump.SetActive (true);
		gameplayCanvas.enabled = true;
		spawner.SetActive (true);
	}

	void Continue()
	{
		gameplayCanvas.enabled = true;
		SharkJump.SetActive (true);
		spawner.SetActive (true);
	}


	public void StartGame()
	{
		gameStarted = true;

		Reset ();

		GameObject[] girls = GetAllGirl ();
		foreach (GameObject girl in girls) {
			RainGirl rainGirl = girl.GetComponent<RainGirl> ();

			if (rainGirl != null) {
				rainGirl.ai.Walk ();
				rainGirl.ai.raycasting = true;
			}
		}

		PowerManager.instance.FenceUp ();
		ScoreManager.instance.ResetScore ();
	}

	public void ReStartGame()
	{
		Reset ();
		SharkJump.SetActive (false);

		GameObject[] girls = GetAllGirl ();
		foreach (GameObject girl in girls) {
			if(girl !=null){
				Destroy(girl);
			}
			StartGame ();
			//scrore =0;
			//speed = 1;
		}
	}

	public void ContinuteGame(){
		
		Continue ();

		GameObject[] girls = GetAllGirl ();
		foreach (GameObject girl in girls) {
			RainGirl rainGirl = girl.GetComponent<RainGirl> ();

			if (rainGirl != null) {
				rainGirl.ai.Walk ();
			}
		}

		PowerManager.instance.GPSPower ();
	}
	public void GameOver()
	{
		gameStarted = false;
		spawner.SetActive (false);
//		gameplayCanvas.enabled = false;
		GameObject[] girls = GetAllGirl ();
		foreach (GameObject girl in girls) {
			RainGirl rainGirl = girl.GetComponent<RainGirl> ();

			if (rainGirl != null) {
				rainGirl.ai.Wait ();
			}
		}
		SharkJump.SetActive (false);
		GameOverMenuUI.instance.ShowGameOverMenu ();
		Time.timeScale = 1;
	}

	public void BackToMainMenu(){
		gameStarted = false;
		GameObject[] girls = GetAllGirl ();
		foreach (GameObject girl in girls) {
			if (girl != null) {
				Destroy (girl);
			}
		}
		SharkJump.SetActive (false);
		spawner.SetActive (false);
		gameplayCanvas.enabled = false;
	}

	GameObject[] GetAllGirl()
	{
		return GameObject.FindGameObjectsWithTag ("AI");
	}

	public IEnumerator Screenshot(){
		StartCoroutine( ScreenShot.Instance.TakeScreenShot ());
		yield return new WaitForSeconds (1.5f);
	}
}
