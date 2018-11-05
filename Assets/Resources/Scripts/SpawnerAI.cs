using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class SpawnerAI : MonoBehaviour {


	private Vector3 pos;

	// Update is called once per frame
	void Update () {
		
	}

	void OnEnable()
	{
		StopAllCoroutines ();
		StartCoroutine (Spawn());
	}

	IEnumerator Spawn()
	{
		while (true) {
			yield return new WaitForSeconds (WorldStates.instance.spawnerDelay);

			for (int i = 0; i < WorldStates.instance.numSpawn; i++) {
				pos = new Vector3 (Random.Range(0,5),1.5f,Random.Range(0,5));
				int rand = Random.Range (0, 2);
				GameObject _player;
				if (rand == 0) {
					_player = ObjectPool.instance.GetPlayerGirl ().gameObject;
				} else {
					_player = ObjectPool.instance.GetPlayerBoy ().gameObject;
				}

				_player.transform.position = pos;

				yield return new WaitForSeconds (1.25f);
			}
		
		}
	}

//	void GetPlayerBoy (Vector3 pos){
//		if (playerBoy = null) {
//			return;
//		}
//		playerBoy = ObjectPool.instance.GetPlayerBoy ();
//		playerBoy.Init ();
//		playerBoy.transform.position = pos;
//		movement = playerBoy.GetComponent<RGMovementController> ();
//		movement.GoToRandDirection ();
//	}
//
//	void GetPlayerGirl (Vector3 pos){
//		if (playerGirl = null) {
//			return;
//		}
//		playerGirl = ObjectPool.instance.GetPlayerGirl ();
//		playerGirl.Init ();
//		playerGirl.transform.position = pos;
//		movement = playerGirl.GetComponent<RGMovementController> ();
//		movement.GoToRandDirection ();
//	}
}
