using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	public GameObject[] birds;
	public GameObject Dots;
	public float moveSpeedCoef;
	public Joystick joystick;
	private readonly float offset = 2.37f;
	private float teta;
	private float angle;
	private float power;
	private GameObject[] predictDots;
	private int sit;
	private float time;
	private Vector3 pos;
	private IEnumerator enumerator;
	private int counter;

	private void Awake()
	{
		predictDots = new GameObject[15];

		for (int i = 0; i < 15; i++)
		{
			predictDots[i] = Instantiate(Dots, transform.position, Quaternion.identity);
			predictDots[i].SetActive(false);
		}

		birds[0].GetComponent<Bird>().situation = Bird.birdSituation.OnSlingShot;
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

	public void Shoot()
	{
		birds[counter++].GetComponent<Bird>().Throw(angle, power * moveSpeedCoef);

		if (counter < birds.Length)
		{
			IEnumerator coroutine;
			coroutine = SetOnSlingShot(counter);
			StartCoroutine(coroutine);
			birds[counter].GetComponent<Bird>().situation = Bird.birdSituation.OnSlingShot;
		}
	}

	private IEnumerator SetOnSlingShot(int bird)
	{
		yield return new WaitForSeconds(2);
		birds[bird].transform.position = new Vector2(transform.position.x, transform.position.y + offset);
	}

	public void MovePlayer(Vector2 _pos)
	{
		pos = new Vector3(_pos.x + transform.position.x, _pos.y + transform.position.y + offset,
						  birds[counter].transform.position.z);
		birds[counter].transform.position = pos;
	}

	public void Amounts(float _angle, float _power)
	{
		angle = _angle;
		power = _power;
	}

	private void StartPrediction()
	{
		time += Time.deltaTime;
		if (birds[counter].GetComponent<Bird>().bird == Bird.birdSituation.OnSlingShot)
		{
			for (int t = 0; t < predictDots.Length; t++)
			{
				predictDots[t].SetActive(true);
				predictDots[t].transform.position = ProjectilePositionCalculator(t);
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
		float y = -0.5f * predictDots[t].GetComponent<DotsScript>().G * Mathf.Pow(t + time, 2)
					+ v0y * (t + time)
					+ pos.y;
		return new Vector2(x, y);

	}

	private void StopPrediction()
	{
		for (int t = 0; t < predictDots.Length; t++)
		{
			predictDots[t].SetActive(false);
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
