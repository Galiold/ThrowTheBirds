using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class Bird : MonoBehaviour
{
	public Sprite damagedBird;
	public Sprite flyBird;
	public GameObject trace;
	public enum birdSituation { WaitingToThrow, OnSlingShot, Flying, Hit };
	public birdSituation situation;
	private List<GameObject> traceDots;
	private readonly string BIRD_PLANET = "BirdPlanet";
	private readonly float EPSILON = 0.5f;
	private IEnumerator traceCoroutine;
	private Rigidbody2D rb;
	private SpriteRenderer sp;
	private CircleCollider2D cc;

	/*
	bird might fly for ever solution not pretty
	trajectory is not good
	traces are not fine
	planet gravity not nice
	no rubber behind
	no AI
	*/

	private void Awake()
	{
		traceDots = new List<GameObject>();
		sp = GetComponent<SpriteRenderer>();
		rb = gameObject.GetComponent<Rigidbody2D>();
		cc = gameObject.GetComponent<CircleCollider2D>();
		cc.isTrigger = true;
		rb.gravityScale = 0;
		situation = birdSituation.WaitingToThrow;
		rb.bodyType = RigidbodyType2D.Static;
	}

	public void Throw(float angle, float v0)
	{
		cc.isTrigger = false;
		rb.bodyType = RigidbodyType2D.Dynamic;
		float xpower = Mathf.Cos(angle);
		float ypower = Mathf.Sin(angle);
		rb.velocity = new Vector2(xpower * v0, ypower * v0);
		sp.sprite = flyBird;
		situation = birdSituation.Flying;
		traceDots.Add(Instantiate(trace, transform.position, Quaternion.identity));
		traceCoroutine = Emit();
		StartCoroutine(traceCoroutine);
	}

	private float kk;//make sure birds won't fly for ever
	private IEnumerator Emit()
	{
		while (situation == birdSituation.Flying)
		{
			transform.GetChild(0).GetComponent<ParticleSystem>().Play();
			transform.GetChild(0).GetComponent<ParticleSystem>().transform.eulerAngles = new Vector3(transform.eulerAngles.z - 180, 0);
			traceDots.Add(Instantiate(trace, transform.position, Quaternion.identity));
			float next = Mathf.Max(0.25f, rb.velocity.magnitude / 100);
			kk += next;
			if (kk > 20)
				situation = birdSituation.Hit;
			yield return new WaitForSeconds(next);
		}

		if (situation == birdSituation.Hit)
		{
			Destroy(transform.GetChild(0).GetComponent<ParticleSystem>());
			yield return new WaitForSeconds(5);
			StopCoroutine(traceCoroutine);
			TraceDotsDestroy();
		}
	}

	private void TraceDotsDestroy()
	{
		for (int i = 0; i < traceDots.Count; i++)
		{
			Destroy(traceDots[i]);
		}
	}

	private void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag != BIRD_PLANET)
		{
			sp.sprite = damagedBird;
			situation = birdSituation.Hit;
		}
		if (System.Math.Abs(rb.velocity.magnitude) < EPSILON && coll.gameObject.tag != BIRD_PLANET)
		{
			Destroy(gameObject);
		}
	}

	public birdSituation bird
	{
		get
		{
			return situation;
		}
	}


}