using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]

public class Planet : MonoBehaviour
{
	private Rigidbody2D rb;
	private readonly string DOT_TAG = "Dot";

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		rb.bodyType = RigidbodyType2D.Static;
	}

	private void Update()
	{
		if (Input.GetKey(KeyCode.A))
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}	}

	private void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == DOT_TAG)
		{
			//coll.gameObject.GetComponent<DotsScript>().G = 0.05f;
		}

	}

	private void OnTriggerExit2D(Collider2D coll)
	{
		if (coll.gameObject.tag == DOT_TAG)
		{
			coll.gameObject.GetComponent<DotsScript>().G = 0;
		}

	}
}
