using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour {

	[SerializeField]
	private Transform planeLeft;

	[SerializeField]
	private Transform planeRight;

	[SerializeField]
	private Transform BGLeft;

	[SerializeField]
	private Transform MainMenuBtns;

	[SerializeField]
	private ParticleSystem menuRainParticle;
	[SerializeField]
	private ParticleSystem ingameRainParticle;


	public float time;

	public float timeHouseOpen;

	public float timeHouseClose;

	public static MainMenuUI instance;

	// Use this for initialization
	void Awake () {
		instance = this;
	}
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.A))
		{
			Open ();
		}

		if(Input.GetKeyDown(KeyCode.B))
		{
			Close ();
		}
	}

	public void BackToMainMenu(){
		CustomSound.instance.StopEndingSound ();
		CustomSound.instance.PlayThemeSound ();
		AudioManager_RG.instance.PlayClip (AudioManager_RG.SoundFX.PressBtn,transform.position);
		Close ();
		menuRainParticle.gameObject.SetActive (true);
		ingameRainParticle.gameObject.SetActive (false);
		WorldStates.instance.BackToMainMenu ();
	}

	public void StartGame()
	{
		AudioManager_RG.instance.PlayClip (AudioManager_RG.SoundFX.PressBtn,transform.position);
		Open ();
		WorldStates.instance.StartGame ();
		menuRainParticle.gameObject.SetActive (false);
		ingameRainParticle.gameObject.SetActive (true);
	}

//	public void Pause()
//	{
//		Close ();
//		menuRainParticle.gameObject.SetActive (true);
//		ingameRainParticle.gameObject.SetActive (false);
//	}

	public void Open()
	{
		iTween.MoveTo(planeLeft.gameObject, 
			iTween.Hash("x", 14, 
				"easetype", iTween.EaseType.linear,
				"islocal",true,
				"time",timeHouseOpen
			));
		iTween.MoveTo(BGLeft.gameObject, 
			iTween.Hash("x", -1300f, 
				"easetype", iTween.EaseType.linear,
				"islocal",true,
				"time",time
			));

		iTween.MoveTo(planeRight.gameObject, 
			iTween.Hash("x", 1300f, 
				"easetype", iTween.EaseType.linear,
				"islocal",true,
				"time",time
			));

		iTween.MoveTo(MainMenuBtns.gameObject, 
			iTween.Hash("x", 1300f, 
				"easetype", iTween.EaseType.linear,
				"islocal",true,
				"time",time
			));
	}

	public void Close()
	{
		iTween.MoveTo(planeLeft.gameObject, 
			iTween.Hash("x", 3.53, 
				"easetype", iTween.EaseType.linear,
				"islocal",true,
				"time",timeHouseClose
			));
		iTween.MoveTo(BGLeft.gameObject, 
			iTween.Hash("x", -338.2f, 
				"easetype", iTween.EaseType.linear,
				"islocal",true,
				"time",time

			));

		iTween.MoveTo(planeRight.gameObject, 
			iTween.Hash("x", 394.7f, 
				"easetype", iTween.EaseType.linear,
				"islocal",true,
				"time",time
			));

		iTween.MoveTo(MainMenuBtns.gameObject, 
			iTween.Hash("x", 570, 
				"easetype", iTween.EaseType.linear,
				"islocal",true,
				"time",time
			));
	}

}
