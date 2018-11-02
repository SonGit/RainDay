using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutBoardDestroy : MonoBehaviour {
	AI ai;
	Home home;
	RainGirl girl;
	private Vector3 raycastDir;
	private Vector3 origin;
	private int layer_maskAI;
	private float range = .25f;
	private RaycastHit hit;
	public Direction dir;
	public AutoDoor door;
	public float distance;
			
			

	void RaycastCenter (Direction _dir)
	{
		raycastDir = Vector3.zero;
		origin = transform.position + new Vector3(0,.75f,0);

		switch (_dir) {
		case Direction.UP:
			raycastDir = transform.forward;
			break;
		case Direction.DOWN:
			raycastDir = -transform.forward;
			break;
		case Direction.LEFT:
			raycastDir = -transform.right;
			break;
		case Direction.RIGHT:
			raycastDir = transform.right;
			break;
		}

		Debug.DrawRay (origin, raycastDir * 5, Color.red);
		if (Physics.Raycast (origin, raycastDir, out hit, 5, layer_maskAI)) {
			if (hit.distance < distance) {
				door._isRay = true;
			//	return;
			}else {
				door._isRay = false;
			}	 
		}else {
			door._isRay = false;
		}	
	}


	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "AI") {
			girl = col.GetComponent<RainGirl> ();
			ai = col.GetComponent<AI> ();
			if (ai.gpsFX != null) {
				ai.gpsFX.transform.SetParent(null);
				ai.gpsFX.Destroy();
//				print ("arrive");
			}
			if (ai.currentState == AI.RGState.GPS) {
				ScoreManager.instance.AddScore (girl.type, 100, home.scorePoint.position + Random.insideUnitSphere * 0.75f);
				AudioManager_RG.instance.PlayClip (AudioManager_RG.SoundFX.RightHome,transform.position);
				girl.Destroy();
			}
			else
			if (home.hometype == girl.type) {
				ScoreManager.instance.AddScore (girl.type, 100,  home.scorePoint.position + Random.insideUnitSphere * 0.75f);
				AudioManager_RG.instance.PlayClip (AudioManager_RG.SoundFX.RightHome,transform.position);
				girl.Destroy();
			}

			if (home.hometype != girl.type) {
				ScoreManager.instance.SubtractScore (girl.type, 150,  home.scorePoint.position + Random.insideUnitSphere * 0.75f);
				AudioManager_RG.instance.PlayClip (AudioManager_RG.SoundFX.wronghome,transform.position);
				girl.Destroy();
			}
		}
	}
	void Awake () {
		layer_maskAI = LayerMask.GetMask("AI");
	}
	void Start () {
		home = GetComponentInParent<Home> ();
	}
	
	// Update is called once per frame
	void Update () {
		RaycastCenter(dir);
	}
}
