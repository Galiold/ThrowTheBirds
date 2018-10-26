using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
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
			print("Won");
			score += birds.Length * 10000;
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
