using UnityEngine;

//[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]

public class Wood : MonoBehaviour
{
	public float health;
	public Sprite damagedWood;
	private readonly string BIRD_TAG = "Bird";
	private float damageHealth;
	private SpriteRenderer sp;
	private Rigidbody2D rb;

	private void Awake()
	{
		sp = GetComponent<SpriteRenderer>();
		rb = GetComponent<Rigidbody2D>();
		damageHealth = health - 10;
	}

	private void Start()
	{
		rb.gravityScale = 0;
	}

	private void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == BIRD_TAG)
		{
			float damage = coll.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude * 10 + rb.velocity.magnitude * 20;

			health -= damage;

			if (health < damageHealth)
			{
				sp.sprite = damagedWood;
			}

			if (health <= 0)
			{
				Destroy(gameObject);
			}
		}
	}

}
