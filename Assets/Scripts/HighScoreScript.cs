﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class HighScoreScript : MonoBehaviour
{
		public int numberOfHighScores = 3;
		private List <string> names;
		private List<int> scores;
		private int currentScore, oldHighScore;
		public Text currentScoreText;
		public Text score1, name1, score2, name2, score0, name0;
		private bool newHighScore;
		public RectTransform GetNamePanel, ShowScorePanel;
		public Text inputText;
		int numSaved;
		int totalHumans;
	
		void Start ()
		{
				numSaved = GameDataScript.score;
				//totalHumans = 30;
		
				oldHighScore = PlayerPrefs.GetInt ("score0", 0);
				currentScoreText.text = "You scored " + numSaved.ToString () + ((numSaved == 1) ? " point" : " points");
		
				names = new List<string> ();
				scores = new List<int> ();
		
				for (int i = 0; i < numberOfHighScores; i++) {
						scores.Add (PlayerPrefs.GetInt ("score" + i, 0));
						names.Add (PlayerPrefs.GetString ("name" + i, "Empty"));
				}
		
				for (int i = 0; i < numberOfHighScores; i++) {
						if (numSaved > scores [i]) {
								newHighScore = true;
								break;
						}
				}
		
				if (!newHighScore) {
						ShowHighScores ();
				} else {
						GetNamePanel.anchoredPosition = new Vector2 (0, 0);
				}	
		}
	
		public void EnterName () // called by a UI button press
		{
				if (inputText.text.Length != 0) {
						string name = inputText.text;
						GetNamePanel.anchoredPosition = new Vector2 (1000f, 1000f);
			
						names.Add (name);
						scores.Add (numSaved);
			
						ShowHighScores ();
				}
		}
	
		private void ShowHighScores ()
		{
				List <int> newScores = new List<int> ();
				List <string> newNames = new List<string> ();
		
				while (scores.Count > 0) {
						int maxScore = scores.Max ();
						int indexOfMax = scores.IndexOf (maxScore);
			
						newScores.Add (maxScore);
						scores.Remove (maxScore);
						newNames.Add (names [indexOfMax]);
						names.Remove (names [indexOfMax]);
				}
		
				score0.text = newScores [0].ToString ();
				score1.text = newScores [1].ToString ();
				score2.text = newScores [2].ToString ();
		
				name0.text = newNames [0];
				name1.text = newNames [1];
				name2.text = newNames [2];
		
				for (int i = 0; i < numberOfHighScores; i++) {
						PlayerPrefs.SetInt ("score" + i, newScores [i]);
						PlayerPrefs.SetString ("name" + i, newNames [i]);
				}
		
				ShowScorePanel.anchoredPosition = new Vector2 (0, 0);
		}
	
		void Update ()
		{
				if (Input.GetKeyDown (KeyCode.Return) && inputText.text.Length != 0)
						EnterName ();
		}
}