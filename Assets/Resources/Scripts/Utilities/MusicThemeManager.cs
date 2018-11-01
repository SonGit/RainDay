using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicThemeManager : MonoBehaviour {

	public static MusicThemeManager instance;

	[System.Serializable]
	public class Stem
	{
		public AudioSource source;
		public AudioClip clip;
	}

	public Stem[] stems;

	[HideInInspector]
	public string isOnMusic = "t";

	void Awake ()
	{
		instance = this;
	}

	// Use this for initialization
	IEnumerator Start () {
		yield return new WaitForSeconds (0.25f);
		PlayMusicTheme ();
		StartMusicTheme ();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateMusicTheme ();
	}
		
	public void UpdateMusicTheme ()
	{
//		for (int i = 0; i < stems.Length; i++) 
//		{
//			if (isOnMusic == "f") {
//				switch (stems[i].clip.name) 
//				{
//				case "ambient":
//					stems [i].source.volume = 0f;
//					break;
//				case "GameOver":
//					stems [i].source.volume = 0f;
//					break;
//				case "4321":
//					stems [i].source.volume = 0f;
//					break;
//				case "trumpet":
//					stems [i].source.volume = 0f;
//					break;
//				default:
//					break;
//				}
//			}
//			else
//			{
//				switch (stems[i].clip.name) 
//				{
//				case "trumpet":
//					stems [i].source.volume = 0.8f;
//					break;
//				default:
//					stems [i].source.volume = 1f;
//					break;
//				}
//
//			}
//		}
	}

	private void PlayMusicTheme ()
	{
		for (int i = 0; i < stems.Length; i++) {
			stems[i].source.clip = stems[i].clip;
			stems [i].source.Play ();
		}
	}

	private void StartMusicTheme ()
	{
		OnMusic (0);
		OnMusic (5);
		StopMusic (1);
		StopMusic (2);
		StopMusic (3);
		StopMusic (4);
	}
		
	public void OnMusic (int i)
	{
		stems [i].source.mute = false;
	}

	public void StopMusic (int i)
	{
		stems [i].source.mute = true;
	}
		
	public IEnumerator PauseMusicTrumpet ()
	{
		StopMusic (3);
		yield return new WaitForSeconds (0.7f);
		OnMusic (3);
	}
		
}
