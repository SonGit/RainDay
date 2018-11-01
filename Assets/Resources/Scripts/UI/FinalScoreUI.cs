using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
public class FinalScoreUI : MonoBehaviour {

	public int highScore;

	void Update ()
	{
		GetComponentInChildren<TextMeshProUGUI>().text = "High Score: " + DataController.Instance.GetHighestPlayerScore ().ToString();
	}

}