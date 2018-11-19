using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityField : MonoBehaviour
{
	public Vector2 initialScale;
	private float EPSILON;
	private GameObject[] gravityField;
	private bool gCheck;

	private void Awake()
	{
		print(transform.GetComponentInParent<Transform>().localScale.x);
		EPSILON = transform.GetComponentInParent<Transform>().localScale.x - 1.3f;
		gravityField = new GameObject[2];
		gravityField[0] = transform.GetChild(0).gameObject;
		gravityField[1] = transform.GetChild(1).gameObject;
		initialScale = gravityField[0].transform.localScale;
	}

	private void Update()
	{
		gravityField[0].transform.localScale = new Vector2(gravityField[0].transform.localScale.x - Time.deltaTime / 5,
														   gravityField[0].transform.localScale.y - Time.deltaTime / 5);

		if (gravityField[0].transform.localScale.magnitude < 0.65f * initialScale.magnitude)
		{
			gCheck = true;
		}

		if (System.Math.Abs(gravityField[0].transform.localScale.magnitude) < EPSILON)
		{
			gravityField[0].transform.localScale = initialScale;
		}

		if (gCheck)
		{
			gravityField[1].transform.localScale = new Vector2(gravityField[1].transform.localScale.x - Time.deltaTime / 5,
															   gravityField[1].transform.localScale.y - Time.deltaTime / 5);
			if (System.Math.Abs(gravityField[1].transform.localScale.magnitude) < EPSILON)
			{
				gravityField[1].transform.localScale = initialScale;
			}
		}
	}

}
