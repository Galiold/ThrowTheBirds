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
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);	}

}
