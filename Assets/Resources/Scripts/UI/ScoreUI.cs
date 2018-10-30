using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreUI : MonoBehaviour {

	// Use this for initialization
	public static ScoreUI instance;

	private TextMeshProUGUI _score;
	private float score;

	void Awake(){
		if (instance == null) {
			instance = this;
		}
	}

	void Start () {
		_score = GetComponent<TextMeshProUGUI> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (score >= 0) {
			_score.text = score.ToString ();
		} else {
			score = 0;
			_score.text = "0";
		}
	}
}
