using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Score : Cacheable {

	public static Score instance;

	private TextMeshPro _text;

	bool isEnd;
	float a;

	void Awake () {
		_text = GetComponentInChildren<TextMeshPro> ();
	}
	// Update is called once per frame
	void Update () {
		if (isEnd) {
			Delete ();
		}
	}
	public void Delete(){
		if (a > 0) {
			a -= Time.deltaTime * 1.2f;
		}
		_text.color = new Color (_text.color.r,_text.color.g,_text.color.b,a);
		if (a < 0) {
			Invoke ("Destroy",2);
		}
	}
	public void End(){
		isEnd = true;
	}

	public override void OnLive ()
	{
		if (gameObject != null) {
			Reset ();
			gameObject.SetActive (true);
		}
	}

	void Reset()
	{
		_text.color = new Color (_text.color.r,_text.color.g,_text.color.b,1);
		a = _text.color.a;
		isEnd = false;
	}

	public override void OnDestroy ()
	{
		if (gameObject != null) {
			gameObject.SetActive (false);
		}
	}
}
