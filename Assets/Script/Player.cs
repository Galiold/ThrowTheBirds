using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	public GameObject[] birds;
	public GameObject Dots;
	public float moveSpeedCoef;
	private readonly float offset = 2.37f;
	private float teta;
	private float angle;
	private float power;
	private GameObject[] dots;
	private int sit;
	private int counter = 14;
	private float time;
	private Vector3 pos;

	/*
	birds move on slingshot

	*/


	private void Awake()
	{
		dots = new GameObject[15];
		for (int i = 0; i < 15; i++)
		{
			dots[i] = Instantiate(Dots, transform.position, Quaternion.identity);
			dots[i].SetActive(false);
		}
		birds[0].GetComponent<Bird>().sit = Bird.birdSit.OnSlingShot;
	}

	private void Update()
	{
		if (sit == 1)
		{
			StartPrediction();
		}
		else
		{
			StopPrediction();
		}
	}

	public void Shoot(int remainingbirds)
	{
		birds[remainingbirds].GetComponent<Bird>().Throw(angle, power * moveSpeedCoef);
		if (remainingbirds + 1 < birds.Length)
		{
			IEnumerator coroutine;
			coroutine = SetOnSlingShot(remainingbirds + 1);
			StartCoroutine(coroutine);
			birds[remainingbirds + 1].GetComponent<Bird>().sit = Bird.birdSit.OnSlingShot;
		}
	}

	private IEnumerator SetOnSlingShot(int bird)
	{
		yield return new WaitForSeconds(2);
		birds[bird].transform.position = new Vector2(transform.position.x, transform.position.y + offset);
		birds[bird].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
	}

	public void MovePlayer(Vector2 _pos, int currentBird)
	{
		pos = new Vector3(_pos.x + transform.position.x, _pos.y + transform.position.y + offset,
						  birds[currentBird].transform.position.z);
		birds[currentBird].transform.position = pos;
	}

	public void Amounts(float _angle, float _power)
	{
		angle = _angle;
		power = _power;
	}

	private void StartPrediction()
	{
		time += Time.deltaTime;
		if (birds[0].GetComponent<Bird>().bird == Bird.birdSit.WaitingToThrow)
		{
			for (int t = 0; t < dots.Length; t++)
			{
				dots[t].SetActive(true);
				dots[t].transform.position = ProjectilePositionCalculator(t);
			}
			if (time > 1)
				time = 0;

		}
	}

	private Vector2 ProjectilePositionCalculator(int t)
	{
		float v0x = Mathf.Cos(angle) * power;
		float x = v0x * (t + time) + pos.x;
		float v0y = Mathf.Sin(angle) * power;
		float y = -0.5f * dots[t].GetComponent<DotsScript>().G * Mathf.Pow(t + time, 2)
					+ v0y * (t + time)
					+ pos.y;
		return new Vector2(x, y);
	}

	private void StopPrediction()
	{

		for (int t = 0; t < dots.Length; t++)
		{
			dots[t].SetActive(false);
		}
	}


	public int Sit
	{
		set
		{
			sit = value;
		}
	}
}
