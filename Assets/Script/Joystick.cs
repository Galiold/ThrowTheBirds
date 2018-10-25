using System;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
public class Joystick : MonoBehaviour, IPointerUpHandler, IDragHandler, IPointerDownHandler
{
	public GameObject player;
	public Image joystickBackground;
	private Image joystick;
	private readonly float R = 3;
	private float teta;
	private float power;
	private int bird;

	private void Awake()
	{
		joystick = joystickBackground.transform.GetChild(0).GetComponent<Image>();
		joystickBackground.gameObject.SetActive(false);
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		Vector3 pos = Camera.main.ScreenToWorldPoint(eventData.position);
		joystickBackground.GetComponent<Image>().rectTransform.position = new Vector3(pos.x, pos.y, -Camera.main.transform.position.z);
		OnDrag(eventData);
	}

	public void OnDrag(PointerEventData eventData)
	{
		Vector2 mouseDragPosition = Camera.main.ScreenToWorldPoint(eventData.position);
		Vector2 pos = mouseDragPosition - new Vector2(joystickBackground.transform.position.x, joystickBackground.transform.position.y);
		Vector2 finalPos;
		teta = Mathf.Atan2(pos.y, pos.x);

		if (pos.magnitude > R)
		{
			finalPos = new Vector2(R * Mathf.Cos(teta), R * Mathf.Sin(teta));
			joystick.GetComponent<Image>().rectTransform.position = new Vector3(finalPos.x + joystickBackground.transform.position.x,
																				finalPos.y + joystickBackground.transform.position.y,
																				-Camera.main.transform.position.z);

		}
		else
		{
			finalPos = new Vector2(pos.magnitude * Mathf.Cos(teta), pos.magnitude * Mathf.Sin(teta));
			joystick.GetComponent<Image>().rectTransform.position = new Vector3(mouseDragPosition.x, mouseDragPosition.y, -Camera.main.transform.position.z);
		}
		power = Mathf.Clamp(Vector2.Distance(mouseDragPosition, joystickBackground.transform.position), 0.1f, R);

		if (eventData.dragging)
			player.GetComponent<Player>().MovePlayer(finalPos, bird);

		player.GetComponent<Player>().Amounts(teta + Mathf.PI, power);

		if (power > 1f)
			player.GetComponent<Player>().Sit = 1;
		else
			player.GetComponent<Player>().Sit = 0;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		joystick.GetComponent<Image>().rectTransform.position = joystickBackground.GetComponent<Image>().rectTransform.position;
		player.GetComponent<Player>().Sit = 0;
		player.GetComponent<Player>().Shoot(bird++);
	}
}
