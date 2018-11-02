using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public enum GirlType
{
//	RED,
	BLUE,
	YELLOW,
}
public class AI : MonoBehaviour {
	private RainGirl rain;
	public Transform Spawn_FX;
	public float speedscale;
	[SerializeField]
	private float hitTime = 3;
	[SerializeField]
	private float hitTimeCount = 0;

	[SerializeField]
	private float dizzyTime = 3;
	[SerializeField]
	private float dizzyTimeCount = 0;


	public float fallTime = 3;
	[SerializeField]
	private float fallTimeCount = 0;

	[SerializeField]
	private float dizzyAnimTime = 3;
	[SerializeField]
	private float dizzyAnimTimeCount = 0;
	[SerializeField]
	private Image cd;
	private Color _color;
	public bool isFall;
	public Transform roller;
	public Animator Anim;
	// Use this for initialization
	public RGMovementController movement;

	public enum RGState
	{
		WALK,
		WAIT,
		HIT,
		DIZZY_ANIM,
		DIZZY,
		START,
		FALLING,
		FELL,
		GPS,
		GO_HOME
	}

	public RGState currentState;

	float waitTimeCount;

	public Arrow[] arrows;

	public bool isDead;

	// Cache for raycast
	private Vector3 raycastDir;
	private Vector3 origin;
	private int layer_maskAI;
	private int layer_maskWall;
	private int layer_maskWalkable;
	private float range = .32f;
	private RaycastHit hit;

	void OnEnable(){

	}

	void Awake () {
		arrows = this.GetComponentsInChildren<Arrow> ();
		layer_maskAI = LayerMask.GetMask("AI");
		layer_maskWall = LayerMask.GetMask("Fence");
		layer_maskWalkable = LayerMask.GetMask("Walkable");
		rain = GetComponent<RainGirl> ();
	}

	// Update is called once per frame
	void Update () {



		PlayAnim(currentState);
		if (currentState != RGState.DIZZY) {
			if (dizzyFX != null) {
				dizzyFX.transform.SetParent(null);
				dizzyFX.Destroy();
			}	
		}
		if (isDead)
			return;
		if (currentState == RGState.GPS) {
			movement.speed = WorldStates.instance.speed *2;
		}
		if (currentState == RGState.FALLING) {
			
			fallTimeCount += Time.deltaTime;
			cd.enabled = true;
			cd.fillAmount = 1 - fallTimeCount / fallTime;
			_color.g = 1 - fallTimeCount / fallTime;
			cd.color = _color;
			if (fallTimeCount > fallTime) {
				
				Fell ();

				fallTimeCount = 0;
			}
		} else {
			cd.color = new Color (1, 1, 0);
			cd.enabled = false;
		}
		
		if (currentState == RGState.HIT) {
			
			hitTimeCount += Time.deltaTime;

			if (hitTimeCount > hitTime) {
				
				hitTimeCount = 0;

				WalkBack ();

			}
		}

		if (currentState == RGState.START) {
			transform.localScale = Vector3.one;
			raycasting = false;

			if (transform.position.y > 0) {
				transform.position += new Vector3 (0, -Time.deltaTime * 3, 0);
			} else {
				if (!WorldStates.instance.isTut) {
					GetFallFX (transform.position + Vector3.up * 0.5f);
					raycasting = true;
					Walk ();
				} else {
					Walk ();
					movement.Stop ();
					raycasting = true;
				}
			}

		}

		if (currentState == RGState.DIZZY_ANIM) {

			dizzyAnimTimeCount += Time.deltaTime;

			// if roller is not active, reset its position 
			if (!roller.gameObject.activeInHierarchy) {
				roller.gameObject.SetActive (true);
				roller.localPosition = new Vector3 (0,-0.5f,0);
			}

			// move roller to position
			if (roller.localPosition != Vector3.zero) {
				roller.localPosition = Vector3.MoveTowards (roller.localPosition, Vector3.zero,1.5f*Time.deltaTime);
			}

			// keep spinning it
			roller.localEulerAngles += new Vector3 (0,400,0) * Time.deltaTime;
			movement.mesh.localEulerAngles += new Vector3 (0,600,0) * Time.deltaTime;

			// Once anim dizzy is completed, get to dizzy state
			if (dizzyAnimTimeCount > dizzyAnimTime) {
				dizzyAnimTimeCount = 0;
				movement.Run ();
				GetDizzyFX (transform.position + Vector3.up*1.2f);
				currentState = RGState.DIZZY;
			}

		}

		// allow dizzy to manually rotate mesh
		if (currentState == RGState.DIZZY_ANIM) {
			movement.rotateToTarget = false;
			roller.gameObject.SetActive (true);
		} else {
			movement.rotateToTarget = true;
			roller.gameObject.SetActive (false);
		}
			

		if (currentState == RGState.DIZZY) {
			dizzyTimeCount += Time.deltaTime;

			if (dizzyTimeCount > dizzyTime) {
				dizzyTimeCount = 0;

				Walk ();
			}
		}

		if (currentState != RGState.DIZZY_ANIM && raycasting) {
			isOutOfBound ();
			RaycastAI ();
			RaycastCenter ();
		}



	}

	void Fell()
	{
		currentState = RGState.FELL;
		movement.enabled = false;
		isDead = true;
	}

	public void Dizzy()
	{
		if (currentState == RGState.DIZZY || currentState == RGState.START || currentState == RGState.HIT) {
			return;
		}

		if (currentState != RGState.DIZZY_ANIM ) {
			currentState = RGState.DIZZY_ANIM;
		}
		else 
		{
			return;
		}


		movement.Reverse ();
		movement.Stop ();
		ResetTimer ();
	}

	public bool raycasting = true;



	void RaycastAI ()
	{
		raycastDir = Vector3.zero;
		origin = transform.position + new Vector3(0,.75f,0);


		switch (movement.direction) {

		case Direction.UP:
			raycastDir = transform.forward;
			origin += -transform.forward * range;
			break;
		case Direction.DOWN:
			raycastDir = -transform.forward;
			origin += transform.forward * range;
			break;
		case Direction.LEFT:
			raycastDir = -transform.right;
			origin += transform.right * range;
			break;
		case Direction.RIGHT:
			raycastDir = transform.right;
			origin += -transform.right * range;
			break;

		}
	
		Debug.DrawRay (origin, raycastDir * 10, Color.red);

		RaycastHit hit;
	
		// Does the ray intersect any objects excluding the player layer
		if (Physics.Raycast(origin, raycastDir, out hit, 10,layer_maskAI))
		{
//			Debug.Log(transform.name + " Did Hit " + hit.transform.name + " hit.distance " + hit.distance);

//			float distanceToHit = Vector3.Distance (transform.position,hit.transform.position);

			if (hit.distance < 0.5f) {

				//Check if hit AI
				if (hit.transform.gameObject.layer == LayerMask.NameToLayer ("AI")) {
					
					// if the AI is not follow any path, play normal hit
					if (currentState != RGState.GPS) {
						OnHit ();
					}
					// else, just wait
					else {
						//Wait ();
					}
				}

			} 

		}
			
	}


	void RaycastCenter ()
	{
		raycastDir = Vector3.zero;
		origin = transform.position + new Vector3(0,.5f,0);

		switch (movement.direction) {

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

		Debug.DrawRay (origin, raycastDir * 10, Color.yellow);
		if (Physics.Raycast (origin, raycastDir, out hit, 10, layer_maskWall)) {
			if (hit.distance < 0.23f) {

				//Check if hit fence
				Fence fence = hit.transform.GetComponent<Fence> ();
				// If hit fence, push it down
				if (fence != null) {
					fence.PopDown ();
					OnHit ();
					fence.isPopDown = true;
					return;
				}
			}	
		}
	}

	public void GPS(Vector3 position)
	{
		if (currentState != AI.RGState.GPS) {
			currentState = AI.RGState.GPS;
		}
		raycasting = false;

		GetGPSFX (transform.position + Vector3.up/2);
		movement.GPS ();
		movement.FollowPath (position);
		ResetTimer ();
	}

//	void Wait()
//	{
//		currentState = RGState.WAIT;
//		movement.Stop ();
//
//		ResetTimer ();
//	}

	void StopWait()
	{
		currentState = RGState.WALK;
		movement.Run ();

	}

	void OnHit()
	{

		if (currentState != RGState.HIT) {
			
			currentState = RGState.HIT;
		}
		else 
		{
			return;
		}
		Vector3 pos = transform.position+ Vector3.up*1.5f;
		GetStunFX (pos);
		AudioManager_RG.instance.PlayClip (AudioManager_RG.SoundFX.Stun,transform.position);
		movement.Stop ();

		raycasting = false;

		ResetTimer ();

	}


	void ResetTimer()
	{
		hitTimeCount = 0;
		waitTimeCount = 0;
		dizzyTimeCount = 0;
		fallTimeCount = 0;
		lastFallDirection = Direction.NONE;
		if (dizzyFX != null) {
			dizzyFX.transform.SetParent(null);
			dizzyFX.Destroy();
		}
	}

	void WalkBack()
	{

		Anim.ResetTrigger ("Stun");
		Anim.ResetTrigger ("Dizzy");
		movement.Reverse ();
	
		currentState = RGState.WALK;


		Invoke ("DelayRaycast",.1f);

		ResetTimer ();
	}

	void DelayRaycast()
	{
		raycasting = true;
	}

	public void RotateMesh()
	{	
		switch (movement.direction){
		case Direction.UP:
			movement.mesh.localEulerAngles = Vector3.zero;
			break;
		case Direction.DOWN:
			movement.mesh.localEulerAngles = new Vector3 (0,180,0);
			break;
		case Direction.LEFT:
			movement.mesh.localEulerAngles = new Vector3 (0,270,0);
			break;
		case Direction.RIGHT:
			movement.mesh.localEulerAngles = new Vector3 (0,90,0);
			break;
		}
	}

	public void Walk()
	{

		currentState = RGState.WALK;
		movement.GoToDirection (movement.direction);
		ResetTimer ();
	}

	public void WalkToDirection(Direction dir)
	{

		currentState = RGState.WALK;
		movement.GoToDirection (dir);
		ResetTimer ();
	}
	public void ReverseDirection(Direction dir)
	{
		movement.GoReverse (dir);
	}
	[SerializeField]
	Direction lastFallDirection = Direction.NONE;

	void Falling()
	{
		if (lastFallDirection != Direction.NONE && lastFallDirection != movement.direction) {
			return;
		}

		if (currentState == RGState.FALLING || currentState == RGState.FELL || currentState == RGState.HIT) {
			return;
		} else {
			lastFallDirection = movement.direction;
			currentState = RGState.FALLING;
		}

		movement.Stop();

	}

	public void TurnOffAllArrow()
	{
		foreach (Arrow arrow in arrows) {
			if(arrow != null)
			arrow.gameObject.SetActive (false);
		}
	}

	public void TurnOnArrow(Direction direction)
	{
		foreach (Arrow arrow in arrows) {
			if(arrow != null)
			arrow.gameObject.SetActive (arrow.direction == direction);
		}
	}
	public void PlayAnim(RGState _currentState) {
		switch (_currentState) {
		case RGState.DIZZY:
			Anim.ResetTrigger ("Walk");
			Anim.ResetTrigger ("Idle");
			Anim.ResetTrigger ("Dizzy_Anim");
			Anim.SetTrigger ("Dizzy");
			break;
		case RGState.DIZZY_ANIM:
			Anim.ResetTrigger ("Walk");
			Anim.ResetTrigger ("StepFall");
			Anim.SetTrigger ("Dizzy_Anim");
			break;
		case RGState.FALLING:
			Anim.ResetTrigger ("Walk");
			Anim.ResetTrigger ("Dizzy");
			Anim.SetTrigger ("StepFall");
			break;
		case RGState.FELL:
			Anim.ResetTrigger ("StepFall");
			Anim.SetTrigger ("Fall");
			break;
		case RGState.HIT:
			Anim.ResetTrigger ("Idle");
			Anim.ResetTrigger ("Walk");
			Anim.SetTrigger ("Stun");
			break;
		case RGState.START:
			Anim.SetTrigger ("Idle");
			break;
		case RGState.WAIT:
			Anim.ResetTrigger ("Dizzy_Anim");
			Anim.ResetTrigger ("Walk");
			Anim.ResetTrigger ("StepFall");
			Anim.ResetTrigger ("Dizzy");
			Anim.SetTrigger ("Idle");
			break;
		case RGState.WALK:
			Anim.ResetTrigger ("Stun");
			Anim.ResetTrigger ("Idle");
			Anim.ResetTrigger ("Dizzy");
			Anim.ResetTrigger ("StepFall");
			Anim.SetTrigger ("Walk");
			break;
		}
	}
	public void ScaleSize(){
		iTween.ScaleTo(gameObject, 
			iTween.Hash(
				"scale", new Vector3(0.7f,0.7f,0.7f),  
				"time", 0.5f, 
				"easeType",iTween.EaseType.linear));
		if (LifeManager.instance.currentlife > 0) {
			LifeManager.instance.currentlife--;
		}
		if (LifeManager.instance.currentlife == 0 && !WorldStates.instance.isGO) {
			StartCoroutine( WorldStates.instance.Screenshot ());
			WorldStates.instance.GameOver ();
		}
	}

	public void EndFall(){
		// FX water


		RainGirl rainGirl = this.GetComponent<RainGirl> ();
		if (rainGirl != null) {
			ScoreManager.instance.SubtractScore (rainGirl.type,50,transform.position + Vector3.up);
		}


		Vector3 pos = Spawn_FX.position + new Vector3 (0, -0.2f, 0);
		AudioManager_RG.instance.PlayClip (AudioManager_RG.SoundFX.WaterSplash,transform.position);
		GetWaterFX (pos);
		// Player die
		StartCoroutine(Sinking());
		rain.Destroy ();

		//GameObject _death = (GameObject)Instantiate (FX, Spawn_FX.position+ new Vector3(0,-0.5f,0), Quaternion.Euler (new Vector3 (90, 0, 0)));
	}

	IEnumerator Sinking() {
		if (transform.position.y > -3f) {
			transform.position += movement.mesh.forward *Time.deltaTime*7.5f;
		}
		yield return new WaitForSeconds (0.25f);
	}
	public GpsFX gpsFX;
	void GetGPSFX (Vector3 pos){
		gpsFX = ObjectPool.instance.GetGpsFX ();
		if (gpsFX != null) {
			gpsFX.transform.position = pos;
			gpsFX.transform.parent = this.transform;
		}
	}

	WaterFX waterFX;
	void GetWaterFX (Vector3 pos){
		waterFX = ObjectPool.instance.GetWaterFX ();
		if (waterFX != null) {
			waterFX.transform.position = pos;
		}
	}

	StunFX stunFX;
	void GetStunFX (Vector3 pos){
		stunFX = ObjectPool.instance.GetStunFX ();
		if (stunFX != null) {
			stunFX.transform.position = pos;
		}
	}
	DizzyFX dizzyFX;
	void GetDizzyFX(Vector3 pos){
		dizzyFX = ObjectPool.instance.GetDizzyFX ();
		if (dizzyFX != null) {
			dizzyFX.transform.position = pos;
			dizzyFX.transform.parent = this.transform;
		}
	}
	FallFX fallfx;
	void GetFallFX (Vector3 pos){
		fallfx = ObjectPool.instance.GetFallFX ();
		if (fallfx != null) {
			fallfx.transform.position = pos;
		}
	}

	public void Wait()
	{
		currentState = RGState.WAIT;
		movement.Stop ();
		raycasting = false;
	}


	void isOutOfBound()
	{
		if (currentState == RGState.GPS)
			return; 

		Vector3 rayPos = transform.position + Vector3.up * 2;
		float length = .2f;

		switch (movement.direction) {

		case Direction.UP:
			rayPos += transform.forward * length;
			break;
		case Direction.DOWN:
			rayPos -= transform.forward* length;
			break;
		case Direction.LEFT:
			rayPos -= transform.right* length;
			break;
		case Direction.RIGHT:
			rayPos += transform.right* length;
			break;

		}

		Debug.DrawRay (rayPos, -transform.up * 10, Color.magenta);
		if (Physics.Raycast (rayPos, -transform.up, out hit, 10, layer_maskWalkable)) {

			if (currentState == RGState.FALLING) {
				Walk ();
			}
	
		} else {
			Falling ();
		}
	}

	public void Reset()
	{
		StopAllCoroutines ();
		isDead = false;
		currentState = RGState.START;
		cd = GetComponentInChildren<Image>();
		_color = cd.color;
		cd.enabled = false;
		TurnOffAllArrow ();
		movement.enabled = true;
		movement.Run ();
		transform.localScale = Vector3.one;

		// ("RotateMesh",1);
	}

	public void GoHome(Direction dir)
	{
		currentState = RGState.GO_HOME;
		movement.GoToDirection (dir);
		TurnOffAllArrow ();
		raycasting = false;
	}

}
	