using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WorldStates : MonoBehaviour {

	public static WorldStates instance;

	public GameObject spawner;

	public bool isStartle;

	public Canvas gameplayCanvas;

	public float speed;
	public bool isGO;
	public bool isAds;
	public bool isTut;
	public float countDownTime = 5f;
	public bool isCountdown;
	public TextMeshProUGUI countDownText;
	public GameObject pauseBtn;
	public GameObject objGameOverPanel;
	public GameObject objAds;
	public GameObject tutPanel;
	public GameObject tutFence;
	public GameObject tutAllFence;
	[SerializeField]
	bool gameStarted;

	[SerializeField]
	private GameObject SharkJump;

	public float spawnerDelay = 2;


	void Awake()
	{
		isGO = false;
		instance = this;
	}
	void Start () {
		speed = .8f;
		gameplayCanvas.enabled = false;
	}
	void Update(){

		if(gameStarted)
		SpawnIncrease ();

		CountDown ();
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
		pauseBtn.SetActive (true);
		tutAllFence.SetActive (true);
		countDownTime = 5;
		isGO = false;
		objGameOverPanel.SetActive (false);
		objAds.SetActive (false);
		isCountdown = false;
		isAds = false;
		CustomSound.instance.PlayThemeSound ();
		CustomSound.instance.StopEndingSound ();
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
		objGameOverPanel.SetActive (false);
		objAds.SetActive (false);
		isGO = false;
		CustomSound.instance.StopEndingSound ();
		CustomSound.instance.PlayThemeSound ();
		pauseBtn.SetActive (true);
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
				RainGirl rain = girl.GetComponent<RainGirl> ();
				if (rain != null)
					rain.Destroy ();
			}
			StartGame ();
			//scrore =0;
			//speed = 1;
		}
	}

	public void ContinueGame(){
		
		Continue ();

		GameObject[] girls = GetAllGirl ();
		foreach (GameObject girl in girls) {
			RainGirl rainGirl = girl.GetComponent<RainGirl> ();

			if (rainGirl != null) {
				if (rainGirl.transform.position.y > 0) {
					rainGirl.transform.position = new Vector3 (rainGirl.transform.position.x, 0,rainGirl.transform.position.z); //Start state
				}
				rainGirl.ai.Walk ();
			}
		}

		PowerManager.instance.GPSPower ();
	}
	public void GameOver()
	{
		if (!isAds) {
			objAds.SetActive (true);
		} else {
			objGameOverPanel.SetActive (true);
		}
		isGO = true;
		isCountdown = true;
		DataController.Instance.SubmitNewPlayerScore (ScoreManager.score);
		pauseBtn.SetActive (false);
		CustomSound.instance.StopThemeSound ();
		CustomSound.instance.PlayEndingSound ();
		gameStarted = false;
		spawner.SetActive (false);
//		gameplayCanvas.enabled = false;
		GameObject[] girls = GetAllGirl ();
		foreach (GameObject girl in girls) {
			RainGirl rainGirl = girl.GetComponent<RainGirl> ();

			if (rainGirl != null) {
				rainGirl.ai.raycasting = false;
				rainGirl.ai.Wait ();
			}
		}
		SharkJump.SetActive (false);
		GameOverMenuUI.instance.ShowGameOverMenu ();

		Time.timeScale = 1;
	}

	public void BackToMainMenu(){
		CustomSound.instance.StopEndingSound ();
		gameStarted = false;
		GameObject[] girls = GetAllGirl ();
		foreach (GameObject girl in girls) {
			if (girl != null) {
				RainGirl rain = girl.GetComponent<RainGirl> ();
				if (rain != null)
				rain.Destroy ();
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
		yield return new WaitForSeconds (0.5f);
	}

	public void CountDown ()
	{
		if (!isCountdown) 
		{
			return;
		}

		countDownTime -= Time.deltaTime;

		if (countDownTime <= 0) {
			StopCountDown ();
			//PlayMusicGameOver ();
			objAds.SetActive (false);
			objGameOverPanel.SetActive (true);
		}
		if(countDownText !=null)
			countDownText.text = "" + (int)countDownTime;
	}
		

	private void PlayCountDown ()
	{
		MusicThemeManager.instance.OnMusic (2);
	}

	public void StopCountDown ()
	{
		
		isCountdown = false;
		//MusicThemeManager.instance.StopMusic (2);
		countDownTime = 5;
	}

	private void PlayMusicGameOver ()
	{
//		if (Player.instance.currentLife <= 0) {
//			MusicThemeManager.instance.OnMusic (1);
//		}

	}

	public void ShowTut(){
		gameplayCanvas.enabled = true;
		isTut = true;
		pauseBtn.SetActive (false);
		tutPanel.SetActive (true);
		tutFence.SetActive (true);
	}

	public void CloseTut(){
		Time.timeScale = 1;
		gameplayCanvas.enabled = false;
		isTut = false;
		pauseBtn.SetActive (false);
		tutPanel.SetActive (false);
		tutFence.SetActive (false);
	}
}
