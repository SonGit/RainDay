using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour {
	public static ScoreManager instance;

	public GameObject[] scorePrefab;

	public static int score;

	public static int hiScore;

	public TextMeshProUGUI totalScore;

	void Awake(){
		if (instance == null) {
			instance = this;
		}
	}



	void Start () {
		hiScore = score;
	}
	void Update ()
	{
//		if (Input.GetMouseButtonDown (0)) {
//			Instantiate (scorePrefab [0],scorePrefab[0].transform.position+ Random.insideUnitSphere*0.5f, Quaternion.identity);
//		}
	}

	public void ResetScore()
	{
		score = 0;
		totalScore.text = "0";
	}

	public void AddScore(GirlType type, int amount, Vector3 position)
	{

		score += amount;

		GameObject scoreObject = MakeScoreObject (type,position);

		TextMeshPro textMesh = scoreObject.GetComponentInChildren<TextMeshPro> ();

		if (textMesh != null) {
			textMesh.text = "+ " + amount;
		}

		totalScore.text = score + "";
	}


	public void SubtractScore(GirlType type,int amount, Vector3 position)
	{
		score -= amount;

		GameObject scoreObject = MakeScoreObject (type,position);

		TextMeshPro textMesh = scoreObject.GetComponentInChildren<TextMeshPro> ();

		if (textMesh != null) {
			textMesh.text = "- " + amount;
		}

		totalScore.text = score + "";
	}

	GameObject MakeScoreObject(GirlType type, Vector3 position)
	{
		//go = (GameObject)Instantiate (scorePrefab [0], scorePrefab [0].transform.position + Random.insideUnitSphere * 0.75f, Quaternion.identity);
		Score go;
		switch (type) {

//		case GirlType.RED:
//			go = ObjectPool.instance.GetRedScoreFX ();
//			break;
		case GirlType.BLUE:
			go = ObjectPool.instance.GetBlueScoreFX ();
			break;
		case GirlType.YELLOW:
			go = ObjectPool.instance.GetYellowScoreFX ();
			break;
		default:
			go = ObjectPool.instance.GetBlueScoreFX ();
			break;

		}
		go.transform.position = position;
		return go.gameObject;
	}
}
