using UnityEngine;

public class DotsScript : MonoBehaviour
{
	private float g;
	private Rigidbody2D rb;
	private CircleCollider2D cc;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		cc = GetComponent<CircleCollider2D>();
	}

	private void Start()
	{
		rb.gravityScale = 0;
		cc.isTrigger = true;
	}

	public float G
	{
		set
		{
			g = value;
		}
		get
		{
			return g;
		}
	}

}
