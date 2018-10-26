using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]

public class Pig : MonoBehaviour
{
	public Sprite damagedPig;
	public float health;
	private readonly string BIRD_TAG = "Bird";
	private Rigidbody2D rb;
	private SpriteRenderer sp;
	private CircleCollider2D cc;
	private float changeSpriteHealth;

	private void Awake()
	{
		sp = GetComponent<SpriteRenderer>();
		rb = GetComponent<Rigidbody2D>();
	}

	private void Start()
	{
		changeSpriteHealth = health - 30;
	}

	private void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == BIRD_TAG)
		{
			Destroy(gameObject);
		}
		else
		{
			float damage = coll.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude * 5 + rb.velocity.magnitude * 10;

			health -= damage;

			if (health < changeSpriteHealth)			{
				sp.sprite = damagedPig;
			}
			if (health <= 0)
			{
				Destroy(gameObject);
			}
		}
	}
}
