using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour {
	public static LifeManager instance;
	public RectTransform lifeRectTransform;
	public Sprite heart;
	public Sprite heartEmpty;
	public int currentlife;
	private Image[] lifeHearts;
	private int maxLives = 3;

	// Use this for initialization
	void  Awake(){
		if (instance == null) {
			instance = this;
		}
	}
	void Start () {
		currentlife = maxLives;
		lifeHearts = new Image[maxLives];

		for(int i = 0; i < maxLives; ++i)
		{
			lifeHearts[i] = lifeRectTransform.GetChild(i).GetComponent<Image>();
		}
	}

	// Update is called once per frame
	void UpdateLifeUI () {

		for (int i = 0; i < maxLives; ++i)
		{
			if(currentlife > i)
			{
				lifeHearts[i].sprite = heart;
			}
			else
			{
				lifeHearts[i].sprite = heartEmpty;
			}
		}
	}

	void Update ()
	{
		UpdateLifeUI ();
	}

}
