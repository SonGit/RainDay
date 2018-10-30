using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionMenuUI : MonoBehaviour {

	[SerializeField]
	private GameObject touchArea;

	public float time;

	RectTransform rectTransform;

	// Use this for initialization
	void Start () {
		rectTransform = this.GetComponent<RectTransform> ();
	}

	void Update()
	{

	}

	public void ShowOption()
	{
		Open ();
	}

	public void CloseOption()
	{
		Close ();
	}

	void Open()
	{
		
		iTween.ValueTo(gameObject, 
			iTween.Hash(
				"from", rectTransform.anchoredPosition,
				"to", new Vector2(-319, rectTransform.anchoredPosition.y),
				"time", time, 
				"easetype", iTween.EaseType.linear,
				"islocal",true,
				"onupdatetarget", this.gameObject, 
				"onupdate", "MovePos"));
		
		touchArea.SetActive (true);
	}

	void Close()
	{
		iTween.ValueTo(gameObject, 
			iTween.Hash(
				"from", rectTransform.anchoredPosition,
				"to", new Vector2(700, rectTransform.anchoredPosition.y),
				"time", time, 
				"easetype", iTween.EaseType.linear,
				"islocal",true,
				"onupdatetarget", this.gameObject, 
				"onupdate", "MovePos"));
		
		touchArea.SetActive (false);
	}

	public void MovePos(Vector2 position){
		rectTransform.anchoredPosition = position;
	}
}
