using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomSound : MonoBehaviour {

	public static CustomSound instance;
	public AudioSource[] audioSources;
	public string isOnMusic="t";
	// Use this for initialization

	void Awake()
	{
		instance = this;
	}

	IEnumerator Start () {
		yield return new WaitForSeconds (1);
		PlayThemeSound ();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateMusic ();
	}

	public void PlayThemeSound(){
		OnMusic (0);
		OnMusic (1);
	}

	public void StopThemeSound(){
		StopMusic (0);
		StopMusic (1);
	}
	public void PlayEndingSound(){
		OnMusic (2);
	}

	public void StopEndingSound(){
		StopMusic (2);
	}

	public void UpdateMusic(){
		if (isOnMusic == "f") {
			for (int i = 0; i < audioSources.Length; i++) {
				audioSources [i].volume = 0;
			}

		} else {
			for (int i = 0; i < audioSources.Length; i++) {
				audioSources [i].volume = 1;
			}
		}
	}

	public void OnMusic (int i)
	{
		audioSources [i].Play();
	}

	public void StopMusic (int i)
	{
		audioSources [i].Stop();
	}
}