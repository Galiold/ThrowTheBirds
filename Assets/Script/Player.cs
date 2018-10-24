using UnityEngine;

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

	private void Awake()
	{
		dots = new GameObject[15];
		for (int i = 0; i < 15; i++)
		{
			dots[i] = Instantiate(Dots, transform.position, Quaternion.identity);
			dots[i].SetActive(false);
		}
	}

	public void Shoot(int remainingbirds)
	{
		birds[remainingbirds].GetComponent<Bird>().Throw(angle, power * moveSpeedCoef);
	}

	public void MovePlayer(Vector2 pos, int currentBird)
	{
		birds[currentBird].transform.position = new Vector3(pos.x + transform.position.x, pos.y + transform.position.y + offset,
															 birds[currentBird].transform.position.z);
	}

	public void Amounts(float _angle, float _power)
	{
		angle = _angle;
		power = _power;
	}

	public void Predict()
	{
		if (birds[0].GetComponent<Bird>().bird == Bird.birdSit.WaitingToThrow)
		{
			for (int t = 0; t < (int)power * 5; t++)
			{
				dots[t].SetActive(true);
				float v0x = Mathf.Cos(angle) * power;
				float x = v0x * t + transform.position.x;
				float v0y = Mathf.Sin(angle) * power;
				float y = -0.5f * dots[t].GetComponent<DotsScript>().G * Mathf.Pow(t, 2) + v0y * t + transform.position.y + offset;
				dots[t].transform.position = new Vector2(x, y);
			}
		}
		for (int t = (int)power * 5; t < dots.Length; t++)
		{
			dots[t].SetActive(false);
		}
	}
}
