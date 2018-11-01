using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSource_RG : Cacheable {

	public AudioSource audioSource;

	public override void OnLive ()
	{
		gameObject.SetActive(true);
		audioSource.Play();
	}

	public override void OnDestroy ()
	{
		gameObject.SetActive(false);
	}


	// Use this for initialization
	void Start () {
	}

	private float timeCount = 0;

	// Update is called once per frame
	void Update () {

		if (!_living)
			return;

		timeCount += Time.deltaTime;

		if (timeCount > audioSource.clip.length) {
			timeCount = 0;
			Destroy ();
		}

	}
}