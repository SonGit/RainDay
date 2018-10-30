using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour {

	[SerializeField]
	private GameObject pauseActive;
	[SerializeField]
	private GameObject pauseInactive;

	// Use this for initialization
	void Start () {
		UnpauseGame ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void PauseGame()
	{
		Time.timeScale = 0;
		pauseActive.SetActive (false);
		pauseInactive.SetActive (true);
	}

	public void UnpauseGame()
	{
		Time.timeScale = 1;
		pauseActive.SetActive (true);
		pauseInactive.SetActive (false);
	}

	public void OnClickBack()
	{
		pauseActive.SetActive (true);
		pauseInactive.SetActive (false);
	}
}
