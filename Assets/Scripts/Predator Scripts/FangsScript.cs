using UnityEngine;
using System.Collections;

public class FangsScript : MonoBehaviour
{
		public float eatingDuration = 1f;

		void OnTriggerEnter2D (Collider2D other)
		{
				if (other.tag == "PreySafeTrigger") {
						//other.gameObject.GetComponent<FSMBrain> ().enabled = false;
						//	yield return new WaitForSeconds (eatingDuration);
						GetComponentInParent<PredatorHuntScript> ().audio [1].Play ();
						other.GetComponentInParent<ZebraFleeState> ().Die ();

						//Destroy (other.transform.parent.gameObject);
            
				}
		}


}
