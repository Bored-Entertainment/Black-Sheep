using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FSMBrain : MonoBehaviour
{
		internal AIBase[] states;
		public AIBase currentState;
		internal List<GameObject> nearbyPredators;
		//public Animator anima;

		void Start ()
		{
				states = GetComponents<AIBase> ();
				foreach (AIBase state in states) {
						state.enabled = false;
				}
				currentState = states [0];
				currentState.enabled = true;
				//	anima = GetComponentInChildren<Animator> ();
//				attackState = GetComponentInParent<Attack> ();
//				fleeState = GetComponentInParent<FleeState> ();
//				currentState.enabled = true;
	
		}
	
		public void SwitchState (AIBase newState)
		{
				currentState.enabled = false;
				currentState = newState;
				currentState.enabled = true;
		}

		public void SetAnim (int state)
		{
				//	anima.SetInteger ("AnimState", state);
		}

}
