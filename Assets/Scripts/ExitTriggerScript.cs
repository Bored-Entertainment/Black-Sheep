using UnityEngine;
using System.Collections;

public class ExitTriggerScript : MonoBehaviour
{
		private FSMBrain brain;

		void Start ()
		{
				brain = GetComponentInParent<FSMBrain> ();
		}
		void OnTriggerExit2D (Collider2D other)
		{
				if (other.tag == "Player") {
						brain.SwitchState (brain.states [0]);
				}
		}

}
