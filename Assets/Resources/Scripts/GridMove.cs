using System.Collections;
using UnityEngine;
using Lean.Pool;

public class GridMove : MonoBehaviour {
	public float moveSpeed = 3f;
	public float gridSize = 1f;
	private enum Orientation {
		Horizontal,
		Vertical
	};
	private Orientation gridOrientation = Orientation.Horizontal;
	private bool allowDiagonals = false;
	private bool correctDiagonalSpeed = true;
	private Vector2 input;
	public bool isMoving = false;
	private Vector3 startPosition;
	private Vector3 endPosition;
	private float t;
	private float factor;
	public bool isBlocking;
	public int start_dir;

	public Vector2 dir;
	private void OnEnable ()
	{
		if(isMoving)
		isMoving = false;
	}
	void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.gameObject.layer == LayerMask.NameToLayer ("AI")) 
		{
			dir = -dir;
			isBlocking = true;
		}
	}

	IEnumerator Start()
	{
		start_dir = Random.Range (0, 4);
		if (start_dir == 0) {dir = new Vector2 (1, 0); }
		if (start_dir == 1) {dir = new Vector2 (-1, 0); }
		if (start_dir == 2) {dir = new Vector2 (0, 1); }
		if (start_dir == 3) {dir = new Vector2 (0, -1); }
		while (true) {
			input = dir;
			StartCoroutine(move(transform));
			yield return new WaitForSeconds (1);
		}
	}


	public IEnumerator move(Transform transform) {
		isMoving = true;
		startPosition = transform.position;
		t = 0;

		if(gridOrientation == Orientation.Horizontal) {
			endPosition = new Vector3(startPosition.x + System.Math.Sign(input.x) * gridSize,
				startPosition.y, startPosition.z + System.Math.Sign(input.y) * gridSize);
		} else {
			endPosition = new Vector3(startPosition.x + System.Math.Sign(input.x) * gridSize,
				startPosition.y + System.Math.Sign(input.y) * gridSize, startPosition.z);
		}

		if(allowDiagonals && correctDiagonalSpeed && input.x != 0 && input.y != 0) {
			factor = 0.7071f;
		} else {
			factor = 1f;
		}

		while (t < 1f) {
			t += Time.deltaTime * (moveSpeed/gridSize) * factor;
			transform.position = Vector3.Lerp(startPosition, endPosition, t);
			if(transform.position.x<-0.5f || transform.position.x>5.5f|| transform.position.z>0.5f|| transform.position.z<-5.5f)
			{
//				LeanPoolTestOF.instance.DespawnPrefab (LeanPoolTestOF.instance.StackRainGirlPrefabs,0);
			}
			yield return null;
		}

		isMoving = false;
		yield return 0;
	}
}