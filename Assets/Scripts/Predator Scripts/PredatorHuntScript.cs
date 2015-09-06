using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PredatorHuntScript : AIBase
{
		private GameObject closest;
		public float huntSpeed = 1f;
		public float acceleration;
		public float currentSpeed;
		FSMBrain brain;
		Transform tr;
		Rigidbody2D rb;
		public float rotationSpeed;
		private bool isScared;
		public float scaredDuration = 5f;
		private Vector2 threatPos;
		internal AudioSource[] audio;
		private Animator anim;

		public float wanderSpeed = 4f;
		public float counter;
		public Vector3 wanderPoint;
		public float wanderRange = 2f;

		private bool wanderPointIsValid;

		
		void Start ()
		{
				counter = Random.Range (2f, 7f);
				brain = GetComponent<FSMBrain> ();
				tr = GetComponent<Transform> ();
				rb = GetComponent<Rigidbody2D> ();
				StartCoroutine (Hunt ());
				audio = GetComponents<AudioSource> ();
				anim = GetComponentInChildren<Animator> ();
		}

		private IEnumerator Hunt ()
		{
				while (true) {
						if (!isScared) {
								GameObject[] prey = GameObject.FindGameObjectsWithTag ("Prey");
								Vector2 pos = new Vector2 (tr.position.x, tr.position.y);

								GameObject[] sortedPrey = (from p in prey
			 orderby Vector2.SqrMagnitude (pos - new Vector2 (p.transform.position.x, p.transform.position.y)) 
			 select p).ToArray ();

								for (int i = 0; i < sortedPrey.Length; i++) {
						
										Vector2 preyPos = new Vector2 (sortedPrey [i].transform.position.x, sortedPrey [i].transform.position.y);
										var layerMask = 1 << LayerMask.NameToLayer ("Predator");
										layerMask = ~ layerMask;
										RaycastHit2D hit = Physics2D.Raycast (pos, preyPos - pos, 100f, layerMask);
			
										if (hit.collider.tag == "Prey") {
									
												closest = sortedPrey [i];
												break;
										} else {
												if (closest != null) {
														closest = null;
														currentSpeed = 0f;
												}
												
										}
								}
								yield return new WaitForSeconds (1f);
						}
				}
		}

		void FixedUpdate ()
		{
				anim.speed = currentSpeed / 5;
				if (closest != null) {
						if (currentSpeed < huntSpeed)
								currentSpeed += acceleration * Time.deltaTime;
						MoveTo (closest.transform.position, currentSpeed);
				} else if (isScared) {
						MoveFrom (threatPos, huntSpeed);
				} else {
						//rb.velocity = new Vector2 (0f, 0f);
						currentSpeed = wanderSpeed;
						Wander ();
				}
		}

		private void Wander ()
		{
				counter -= Time.deltaTime;
				if (counter <= 0) {
						counter = Random.Range (2f, 7f);

						//	while (!wanderPointIsValid) {
						wanderPoint = new Vector3 (Random.Range (-100, 100), Random.Range (-100, 100), 0);
						wanderPoint = tr.position + (wanderPoint - tr.position).normalized * wanderRange;
						//			RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (wanderPoint), Vector2.zero);
						//		if (hit.collider.tag == "Ground") {
						//				Debug.Log ("gournd");
						//				wanderPointIsValid = true;
						//		} else {
						//				Debug.Log ("not ground");
						//				wanderPointIsValid = false;
						//		}
						//	}
						
				}
				MoveTo (wanderPoint, currentSpeed);
		
		}

		void OnTriggerEnter2D (Collider2D other)
		{
				if (other.tag == "Player") {
						StopAllCoroutines ();
						StartCoroutine (Scared ());
						threatPos = other.transform.position;
				}
		}

		private IEnumerator Scared ()
		{
				if (closest != null)
						closest = null;
				isScared = true;
				audio [0].Play ();
				yield return new WaitForSeconds (scaredDuration);
				isScared = false;
				currentSpeed = 0;
				rb.velocity = new Vector2 (0f, 0f);
				Quaternion ori = tr.rotation;
				tr.rotation = ori;
				StartCoroutine (Hunt ());
		}

		void MoveTo (Vector3 position, float speed)
		{
				Vector2 pos = new Vector2 (tr.position.x, tr.position.y);
				Vector2 dirToMove = new Vector2 (position.x, position.y) - pos;
		
				dirToMove.Normalize ();
		
				rb.MovePosition (pos + dirToMove * speed * Time.deltaTime);
				var targetAngle = Mathf.Atan2 (dirToMove.y, dirToMove.x) * Mathf.Rad2Deg - 90;
				tr.rotation = Quaternion.Slerp (tr.rotation, Quaternion.Euler (0, 0, targetAngle), 2f * Time.deltaTime);
		}

		void MoveFrom (Vector3 position, float speed)
		{
				Vector2 pos = new Vector2 (tr.position.x, tr.position.y);
				Vector2 dirToMove = pos - new Vector2 (position.x, position.y);
		
				dirToMove.Normalize ();
		
				rb.MovePosition (pos + dirToMove * speed * Time.deltaTime);
				var targetAngle = Mathf.Atan2 (dirToMove.y, dirToMove.x) * Mathf.Rad2Deg - 90;
				tr.rotation = Quaternion.Slerp (tr.rotation, Quaternion.Euler (0, 0, targetAngle), rotationSpeed * Time.deltaTime);
		}
}
