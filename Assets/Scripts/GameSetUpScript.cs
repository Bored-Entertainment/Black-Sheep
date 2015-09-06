using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class GameSetUpScript : MonoBehaviour
{
		public int numberOfSheep = 12;

		void Awake ()
		{
				GameDataScript.totalSheep = numberOfSheep;
				GameObject[] sheep = GameObject.FindGameObjectsWithTag ("Prey");
				foreach (GameObject s in sheep) {
						s.SetActive (false);
				}
				int rand = UnityEngine.Random.Range (0, 10);
				int blackSheep;
				if (rand < 5)
						blackSheep = 2;
				else if (rand < 9)
						blackSheep = 3;
				else
						blackSheep = 4;
			
				GameObject[] shuffledSheep = sheep.OrderBy (a => Guid.NewGuid ()).ToArray ();
				for (int i = 0; i < numberOfSheep; i++) {
						
						shuffledSheep [i].SetActive (true);
						if (blackSheep > 0) {
								shuffledSheep [i].GetComponent<SheepColorScript> ().isBlack = true;
								blackSheep--;
						}
				}
		}
}