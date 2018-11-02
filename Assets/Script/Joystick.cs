using System;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
public class Joystick : MonoBehaviour, IPointerUpHandler, IDragHandler, IPointerDownHandler
{
	public Image joystickBackground;
	public GameController gameController;
	public GameObject player;
	private Image joystick;
	private readonly float R = 6;
	private float teta;
	private float power;

	private void Awake()
	{
		joystick = joystickBackground.transform.GetChild(0).GetComponent<Image>();
		joystickBackground.gameObject.SetActive(false);
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (!gameController.GameIsOn)
			return;
		Vector3 pos = Camera.main.ScreenToWorldPoint(eventData.position);
		joystickBackground.GetComponent<Image>().rectTransform.position = new Vector3(pos.x, pos.y, -Camera.main.transform.position.z);
		OnDrag(eventData);
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (!gameController.GameIsOn)
			return;

		Vector2 mouseDragPosition = Camera.main.ScreenToWorldPoint(eventData.position);
		Vector2 pos = mouseDragPosition - new Vector2(joystickBackground.transform.position.x, joystickBackground.transform.position.y);
		Vector2 finalPos;
		teta = Mathf.Atan2(pos.y, pos.x);

		if (pos.magnitude > R)
		{
			finalPos = new Vector2(R * Mathf.Cos(teta), R * Mathf.Sin(teta));
			power = 6;
		}
		else
		{
			finalPos = new Vector2(pos.magnitude * Mathf.Cos(teta), pos.magnitude * Mathf.Sin(teta));
			power = pos.magnitude > 0.1 ? pos.magnitude : 0.1f;
		}

		if (eventData.dragging)
			player.GetComponent<Player>().MovePlayer(finalPos);

		player.GetComponent<Player>().Amounts(teta + Mathf.PI, power);

		if (power > 1f)
			player.GetComponent<Player>().Sit = 1;
		else
			player.GetComponent<Player>().Sit = 0;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (!gameController.GameIsOn)
			return;
		joystick.GetComponent<Image>().rectTransform.position = joystickBackground.GetComponent<Image>().rectTransform.position;
		player.GetComponent<Player>().Sit = 0;
		player.GetComponent<Player>().Shoot();
	}
}
