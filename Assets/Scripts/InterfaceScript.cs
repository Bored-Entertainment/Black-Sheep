using UnityEngine;
using System.Collections;

public class InterfaceScript : MonoBehaviour
{

		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (Input.GetKey (KeyCode.Escape)) {
						if (Application.loadedLevel == 1) {
								Application.LoadLevel ("Interface Level");
						} else {
								Application.Quit ();
						}

				}
		}

		public void LoadLevel (int level)
		{
				Application.LoadLevel (level);
		}
}
