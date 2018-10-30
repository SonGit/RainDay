using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SharkJump : MonoBehaviour {

    private Rigidbody rb;
	private Rigidbody rb1;
	private Vector3 target;
	private Vector3 nextPos;
	public Vector3 left;
	public Vector3 right;
	public int rand;
	public float speed;
    public float h = 25;
    public float gravity = -18;
	HitFX hitFX;
	WaterFX waterFX;
	public bool debugPath;
	private Vector3 transFX;
	public bool hasInit;
	public float delay;
	public float holdTimeCount;
	public float holdTime;
	private AI ai;
	private Image cd;
	private Color _color;
	private float time;
	IEnumerator GetHitFX (Vector3 pos){
		hitFX = ObjectPool.instance.GetHitFX ();
		if (hitFX != null) {
			hitFX.transform.position = pos;
		}
		yield return null;
	}

	IEnumerator GetWaterFX (Vector3 pos){
		
		waterFX = ObjectPool.instance.GetWaterFX ();
		if (waterFX != null) {
			waterFX.transform.position = pos;
		}
		yield return null;
	}

	void OnEnable(){
		transform.position = new Vector3 (-10, -10, -10);
		Rigidbody shark = this.GetComponent<Rigidbody> ();
		shark.useGravity = false;
		shark.velocity = Vector3.zero;
		cd = GetComponentInChildren<Image>();
		cd.enabled = false;
		StartCoroutine (test ());
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.layer == 9) {
			RainGirl girl = col.gameObject.GetComponent<RainGirl> ();
			ai = col.gameObject.GetComponent<AI> ();
			if(ai.currentState != AI.RGState.GPS) {
				StartCoroutine(GetHitFX(col.transform.position + Vector3.up/2));
				if (girl != null) {
					ScoreManager.instance.SubtractScore (girl.type,50,transform.position + Vector3.up);
				}
				girl.Destroy ();
			}
		}
	}
	public IEnumerator MoveToStart(){
		rand = Random.Range (1, 5);
		if (rand < 5) {
			transform.eulerAngles = new Vector3 (0, 90, 0);
		}else transform.eulerAngles = new Vector3 (0,-90,0);

		switch (rand) {
		case 1:
			transform.position = left;
			break;
		case 2:
			transform.position = left + new Vector3 (0, 0, 1);
			break;
		case 3:
			transform.position = left + new Vector3 (0, 0, 4);
			break;
		case 4:
			transform.position = left + new Vector3 (0, 0, 5);
			break;
		case 5:
			transform.position = right;
			break;
		case 6:
			transform.position = right + new Vector3 (0, 0, 1);
			break;
		case 7:
			transform.position = right + new Vector3 (0, 0, 4);
			break;
		case 8:
			transform.position = right + new Vector3 (0, 0, 5);
			break;
		}
		yield return null;
	}

	public IEnumerator Ready() {
		if (rand < 5) {
			while (transform.position.x < -2) {
				transform.position = Vector3.MoveTowards (transform.position, new Vector3 (-2, -0.5f, transform.position.z), Time.deltaTime * speed);
				yield return null;
			}
		} else {
			while (transform.position.x > 7) {
				transform.position = Vector3.MoveTowards (transform.position, new Vector3 (7, -0.5f, transform.position.z), Time.deltaTime * speed);
				yield return null;
			}
		}
	}

	public IEnumerator Jump() {
		if (transform.position.y < 0) {
			h = 1.5f;
		} else {
			h = 1;
		}
		if (rand < 5) {
			target = new Vector3 (transform.position.x + 3, 0.25f, transform.position.z);
		} else {
			target = new Vector3 (transform.position.x - 3, 0.25f, transform.position.z);
		}
		yield return null;
	}

	public IEnumerator test() {
		rb1 = this.GetComponent<Rigidbody>();
		while (true) {
			yield return new WaitForSeconds (0.5f);
			WorldStates.instance.isStartle = true;
			StartCoroutine (MoveToStart ());
			StartCoroutine (Ready ());
			yield return new WaitForSeconds (2);
			yield return StartCoroutine (count (4));
			StartCoroutine (GetWaterFX (transform.position));
			StartCoroutine(Jump ());
			StartCoroutine(Launch (target));
			yield return new WaitForSeconds (1);
			StartCoroutine(Jump ());
			StartCoroutine(Launch (target));
			yield return new WaitForSeconds (1);
			StartCoroutine(Jump ());
			StartCoroutine(Launch (target));
			WorldStates.instance.isStartle = false;
			yield return new WaitForSeconds (0.7f);
			StartCoroutine (GetWaterFX (transform.position));
			yield return new WaitForSeconds (1f);
			rb1.useGravity = false;
			rb1.velocity = Vector3.zero;
			yield return new WaitForSeconds (delay);
		}
	}

	IEnumerator count(float length)
	{
		time = 0;
		while (time < length) {
			time += Time.deltaTime;
			cd.enabled = true;
			cd.fillAmount = 1 - time / length;
			_color.g = 1  - time / length;
			cd.color = _color;
			yield return new WaitForEndOfFrame ();
		}
	}

	GameObject[] GetAllGirl()
	{
		return GameObject.FindGameObjectsWithTag ("AI");
	}

	void Start()
	{
		StartCoroutine (test ());
		cd = GetComponentInChildren<Image>();
		_color = cd.color;
		cd.enabled = false;
	}


    void Update()
    {
      
    }

    void Init()
    {
        rb = this.GetComponent<Rigidbody>();
        rb.useGravity = false;
        hasInit = true;

    }
		

	public IEnumerator Launch(Vector3 target)
    {
        if(!hasInit)
        {
            Init();
        }

        this.target = target;
        Physics.gravity = Vector3.up * gravity;
        rb.useGravity = true;
        rb.velocity = CalculateLaunchData().initialVelocity;
		yield return 0;
    }
		
    LaunchData CalculateLaunchData()
    {
        float displacementY = target.y - rb.position.y;
        Vector3 displacementXZ = new Vector3(target.x - rb.position.x, 0, target.z - rb.position.z);
        float time = Mathf.Sqrt(-2 * h / gravity) + Mathf.Sqrt(2 * (displacementY - h) / gravity) ;
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * h);
        Vector3 velocityXZ = displacementXZ / time;

        return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);
    }

    void DrawPath()
    {
        LaunchData launchData = CalculateLaunchData();
        Vector3 previousDrawPoint = rb.position;

        int resolution = 30;
        for (int i = 1; i <= resolution; i++)
        {
            float simulationTime = i / (float)resolution * launchData.timeToTarget;
            Vector3 displacement = launchData.initialVelocity * simulationTime + Vector3.up * gravity * simulationTime * simulationTime / 2f;
            Vector3 drawPoint = rb.position + displacement;
            Debug.DrawLine(previousDrawPoint, drawPoint, Color.green);
            previousDrawPoint = drawPoint;
        }
    }

    struct LaunchData
    {
        public readonly Vector3 initialVelocity;
        public readonly float timeToTarget;

        public LaunchData(Vector3 initialVelocity, float timeToTarget)
        {
            this.initialVelocity = initialVelocity;
            this.timeToTarget = timeToTarget;
        }

    }
}
