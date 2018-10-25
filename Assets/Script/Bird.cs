using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class Bird : MonoBehaviour
{
	public Sprite damagedBird;
	public Sprite flyBird;
	public GameObject trace;
	public enum birdSit { Flying, WaitingToThrow, Hit, OnSlingShot };
	public birdSit sit;
	private readonly string BIRD_PLANET = "BirdPlanet";
	private readonly float EPSILON = 0.5f;
	private Rigidbody2D rb;
	private float destroyTime;
	private IEnumerator traceCoroutine;
	private IEnumerator destroyCoroutine;
	private SpriteRenderer sp;
	private float _angle;
	private float _v0;

	private void Awake()
	{
		sp = GetComponent<SpriteRenderer>();
		rb = gameObject.GetComponent<Rigidbody2D>();
		rb.gravityScale = 0;
		destroyTime = 0;
		sit = birdSit.WaitingToThrow;
	}

	public void Throw(float angle, float v0)
	{
		_angle = angle;
		_v0 = v0;
		float xpower = Mathf.Cos(angle);
		float ypower = Mathf.Sin(angle);
		rb.velocity = new Vector2(xpower * v0, ypower * v0);
		sp.sprite = flyBird;
		sit = birdSit.Flying;
		traceCoroutine = Emit();
		StartCoroutine(traceCoroutine);
	}

	private IEnumerator Emit()
	{
		while (sit == birdSit.Flying)
		{
			Instantiate(trace, transform.position, Quaternion.identity);
			float next = Mathf.Max(0.25f, rb.velocity.magnitude / 100);
			yield return new WaitForSeconds(next);
		}
	}

	private void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag != BIRD_PLANET)
		{
			sp.sprite = damagedBird;
			sit = birdSit.Hit;
		}
		if (System.Math.Abs(rb.velocity.magnitude) < EPSILON && coll.gameObject.tag != BIRD_PLANET)
		{
			Destroy(gameObject);
		}
	}

	public birdSit bird
	{
		get
		{
			return sit;
		}
	}


}