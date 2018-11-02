using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPanel : MonoBehaviour {

	public RectTransform panel;

	// Use this for initialization
	void Start () {
		
	}
	
	public void Close()
	{
		panel.gameObject.SetActive (false);
	}
}
