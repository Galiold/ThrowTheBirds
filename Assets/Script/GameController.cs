using UnityEngine;
using UnityEngine.UI;
using System;

public class GameController : MonoBehaviour
{
	public Text scoreText;
	private float score;
	private bool gameInOn;

	void Awake()
	{
		gameInOn = true;
	}


	private void Update()
	{
		Pig[] pigs = FindObjectsOfType<Pig>();
		Bird[] birds = FindObjectsOfType<Bird>();

		if (pigs.Length == 0)
		{
			score += birds.Length * 10000;
			scoreText.text = "Score: " + Convert.ToString(score);
		}
		else
		{
			foreach (Bird b in birds)
			{
				if (b.situation != Bird.birdSituation.Hit)
				{
					return;
				}
			}
			gameInOn = false;
			print("Lost");
		}	}

	public bool GameIsOn
	{
		get
		{
			return gameInOn;
		}
	}
}
