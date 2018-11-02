using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour {

	public static Tutorial instance;

	public RectTransform[] tutorialPanels;

	public int CurrentTutorial;

	public TutorialBoy tutorialBoy;

	// Use this for initialization
	void Awake () {
		instance = this;
	}

	void Start()
	{
		Tutorial1 ();
	}

	public void Tutorial1()
	{
		SetActivePanel (0);
	}

	public void OnDoneTutorial1()
	{
		print ("OnDone1");
	}

	public void Tutorial2()
	{
		SetActivePanel (1);
	}

	public void OnDoneTutorial2()
	{

	}

	public void Tutorial3()
	{
		SetActivePanel (2);
	}

	public void OnDoneTutorial3()
	{

	}

	public void Tutorial4()
	{
		SetActivePanel (3);
	}

	public void OnDoneTutorial4()
	{

	}

	void SetActivePanel(int index)
	{
		for (int i = 0; i < tutorialPanels.Length; i++) {
			if (i == index) {
				CurrentTutorial = index;
				tutorialPanels [i].gameObject.SetActive (true);
			} else {
				tutorialPanels [i].gameObject.SetActive (false);
			}
		}
	}

}
