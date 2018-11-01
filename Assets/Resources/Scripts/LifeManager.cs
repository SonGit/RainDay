using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour {
	public static LifeManager instance;
	public RectTransform lifeRectTransform;
	public Sprite heart;
	public Sprite heartEmpty;

	private Image[] lifeHearts;
	private int maxLives = 3;
	[SerializeField]
	private int _currentlife;
	public int currentlife
	{
		get {
			return _currentlife;
		}

		set {
			_currentlife = value;

			if (_currentlife > 3) {
				_currentlife = 3;
			}
			if (_currentlife < 0) {
				_currentlife = 0;
			}
			UpdateLifeUI ();
		}
	}
	// Use this for initialization
	void  Awake(){
		if (instance == null) {
			instance = this;
		}
		currentlife = maxLives;
	}

	// Update is called once per frame
	void UpdateLifeUI () {
		
	
		lifeHearts = new Image[maxLives];

		for(int i = 0; i < maxLives; ++i)
		{
			lifeHearts[i] = lifeRectTransform.GetChild(i).GetComponent<Image>();
		}

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
		
}
