//using UnityEngine;
//using System.Collections;
//
//public class PredatorWanderScript : MonoBehaviour
//{
//		private Vector3[] waypoints;
//		private Vector2 nextWaypoint;
//		public float distanceToNext = 4f;
//		private Transform tr;
//
//		// Use this for initialization
//		void Start ()
//		{
//				tr = GetComponent<Transform> ();
//				waypoints = GameObject.FindGameObjectsWithTag ("Waypoints");
//		}
//
//		void OnEnable ()
//		{
//				nextWaypoint = waypoints [Random.Range (0, waypoints.Length)];
//		}
//	
//		// Update is called once per frame
//		void FixedUpdate ()
//		{
//				if (nextWaypoint != null) {
//						Vector2 pos = new Vector2 (tr.position.x, tr.position.y);
//						if (Vector2.SqrMagnitude (pos - nextWaypoint) > distanceToNext) {
//							
//						}
//
//				}
//		}
//}