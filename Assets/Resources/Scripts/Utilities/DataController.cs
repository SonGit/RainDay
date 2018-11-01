using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataController : MonoBehaviour {

	private static DataController _instance;

	//public FinalScoreUI finalScoreUI;

	public static DataController Instance
	{
		get { return _instance; }
	}

	private void Awake ()
	{
		if (_instance == null) 
		{
			_instance = this;
		}
	}
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		//LoadPlayerProgress ();
		LoadSoundSettingProgress ();
		LoadMusicSettingProgress ();
	}

//	public void SubmitNewPlayerScore(int newScore)
//	{
//		if (newScore > finalScoreUI.highScore) {
//			finalScoreUI.highScore = newScore;
//			SavePlayerProgress ();
//		}
//	}
//
//	public int GetHighestPlayerScore ()
//	{
//		return finalScoreUI.highScore;
//	}
//
//	public void LoadPlayerProgress ()
//	{
//		if (PlayerPrefs.HasKey ("HighestScore")) {
//			finalScoreUI.highScore = PlayerPrefs.GetInt ("HighestScore");
//		}
//	}
//
//	private void SavePlayerProgress ()
//	{
//		PlayerPrefs.SetInt ("HighestScore", finalScoreUI.highScore);
//	}

	public void SubmitSoundSetting(string a)
	{
		AudioManager_RG.instance.isOnSound = a;
		SaveSoundSettingProgress ();
	}

	public string GetSoundSetting ()
	{
		return AudioManager_RG.instance.isOnSound;

	}

	public void LoadSoundSettingProgress ()
	{
		if (PlayerPrefs.HasKey ("SettingSound")) {
			AudioManager_RG.instance.isOnSound = PlayerPrefs.GetString ("SettingSound");
		}
	}

	private void SaveSoundSettingProgress ()
	{
		PlayerPrefs.SetString ("SettingSound", AudioManager_RG.instance.isOnSound);
	}


	public void SubmitMusicSetting(string m)
	{
		CustomSound.instance.isOnMusic = m;
		SaveMusicSettingProgress ();
	}

	public string GetMusicSetting ()
	{
		return CustomSound.instance.isOnMusic;
	}

	public void LoadMusicSettingProgress ()
	{
		if (PlayerPrefs.HasKey ("SettingMusic")) {
			CustomSound.instance.isOnMusic = PlayerPrefs.GetString ("SettingMusic");
		}
	}

	private void SaveMusicSettingProgress ()
	{
		PlayerPrefs.SetString ("SettingMusic", CustomSound.instance.isOnMusic);
	}
}
