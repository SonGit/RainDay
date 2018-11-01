using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainGirl : Cacheable {

	public GirlType type;
	private SkinnedMeshRenderer skin;
	public AI ai;
	private Texture[] rand = new Texture[4];
	private Animator Anim;
	float t;

	// Use this for initialization
	void Awake () {
		ai = this.GetComponent<AI> ();
		skin = this.GetComponentInChildren<SkinnedMeshRenderer> ();
		Anim = this.GetComponentInChildren<Animator> ();
		LoadTexture ();

		//print (mat);
		if (this.gameObject.name == "Player_Boy") {
			RandomBoyType ();
		} else {
			RandomGirlType ();
		}
	}

	void LoadTexture()
	{

		rand[0]  = Resources.Load<Texture> ("Materials/Player/Boy_tex_yellow");
		rand[1]  = Resources.Load<Texture> ("Materials/Player/Boy_tex_green");


		rand[2] = Resources.Load<Texture> ("Materials/Player/Girl_tex_yellow");
		rand[3] = Resources.Load<Texture> ("Materials/Player/Girl_tex_green");
	}

	void Start(){

	}

	// Update is called once per frame
	void Update () {
		Startle ();
	}

	public void Init (){
		
		transform.localScale = new Vector3 (1,1,1);
		ai.isDead = false;
		ai.movement.enabled = true;

	}


	public void FollowPathHome(Vector3 target)
	{
		ai.Anim.ResetTrigger ("Stun");
		ai.Anim.SetTrigger ("GPS");
		if (ai.currentState != AI.RGState.GPS) {
			ai.currentState = AI.RGState.GPS;
		}
		ai.GPS (target);
	}

	void RandomBoyType()
	{
		int rand = Random.Range (0,2);
		switch (rand) {

		case 0:
			SetBoyType (GirlType.BLUE);
			break;
		case 1:
			SetBoyType (GirlType.YELLOW);
			break;
		}
	}

	void SetBoyType(GirlType newType)
	{
		
		switch (newType) {

		case GirlType.YELLOW:
			skin.materials[0].mainTexture =  rand[0];
			break;
		case GirlType.BLUE:
			skin.materials[0].mainTexture =  rand[1];
			break;
		}

		type = newType;
	}

	void RandomGirlType()
	{
		int rand = Random.Range (0,2);
		switch (rand) {

		case 0:
			SetGirlType (GirlType.BLUE);
			break;
		case 1:
			SetGirlType (GirlType.YELLOW);
			break;
		}
	}

	void SetGirlType(GirlType newType)
	{

		switch (newType) {

		case GirlType.YELLOW:
			skin.materials[0].mainTexture = rand[2];
			break;
		case GirlType.BLUE:
			skin.materials[0].mainTexture = rand[3];
			break;
		}

		type = newType;
	}
	private void Startle(){
		if (WorldStates.instance.isStartle == true) {
			if (t <= 1) {
				Anim.SetFloat ("IsStartle", t += Time.deltaTime*0.5f);
			}
		} else {
			if (t >= 0) {
				Anim.SetFloat ("IsStartle", t -= Time.deltaTime*0.5f);
			}
		}
	}

	public override void OnLive ()
	{
		if (gameObject != null) {
			gameObject.SetActive (true);
		}
		if (this.gameObject.name == "Player_Boy") {
			RandomBoyType ();
		} else {
			RandomGirlType ();
		}

		ai.Reset ();
		ai.movement.GoToRandDirection ();
		ai.RotateMesh ();
	}

	public override void OnDestroy ()
	{
		if (gameObject != null) {
			gameObject.SetActive (false);
		}
	}

}
