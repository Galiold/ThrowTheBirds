using UnityEngine;
using System.Collections;

public class KinectOverlayer : MonoBehaviour
{
	//	public Vector3 TopLeft;
	//	public Vector3 TopRight;
	//	public Vector3 BottomRight;
	//	public Vector3 BottomLeft;

	public KinectWrapper.NuiSkeletonPositionIndex rightHandTrack = KinectWrapper.NuiSkeletonPositionIndex.HandRight;
	public KinectWrapper.NuiSkeletonPositionIndex leftHandTrack = KinectWrapper.NuiSkeletonPositionIndex.HandLeft;
	public KinectWrapper.NuiSkeletonPositionIndex headTrack = KinectWrapper.NuiSkeletonPositionIndex.Head;
	public Player player;	public float smoothFactor = 5f;
	public float power = 0;

	private bool nextShoot = true;
	private IEnumerator coroutine;
	private Vector2 firstRightHandPos;
	private float rightHandCameraDistance = 10f;

	void Start()
	{
		coroutine = SetInitialPosition();
		StartCoroutine(coroutine);
	}

	private IEnumerator SetInitialPosition()
	{
		yield return new WaitForSeconds(5);
		KinectManager manager = KinectManager.Instance;
		int rightHand = (int)rightHandTrack;

		firstRightHandPos = Track(manager, rightHand);
	}

	void Update()
	{
		KinectManager manager = KinectManager.Instance;

		if (manager && manager.IsInitialized())
		{
			int rightHand = (int)rightHandTrack;

			int leftHand = (int)leftHandTrack;

			int head = (int)headTrack;

			Vector2 birdPos = Track(manager, leftHand);

			Vector2 slingshotPos = Track(manager, head);

			Vector2 shootPos = Track(manager, rightHand);

			Vector2 project = slingshotPos - birdPos;

			float teta = Mathf.Atan2((birdPos.y - slingshotPos.y), (birdPos.x - slingshotPos.x));

			Vector2 finalPos;
			project /= 5;

			if (project.magnitude > 6)
			{
				finalPos = new Vector2(6 * Mathf.Cos(teta), 6 * Mathf.Sin(teta));
				power = 6;
			}
			else
			{
				finalPos = new Vector2(project.magnitude * Mathf.Cos(teta), project.magnitude * Mathf.Sin(teta));
				power = project.magnitude;
			}

			if (power > 1f)
				player.GetComponent<Player>().Sit = 1;
			else
				player.GetComponent<Player>().Sit = 0;


			player.GetComponent<Player>().Amounts(teta + Mathf.PI, project.magnitude);

			player.GetComponent<Player>().MovePlayer(finalPos);

			print((firstRightHandPos - shootPos).magnitude);

			if (System.Math.Abs((firstRightHandPos - shootPos).magnitude) < 5)
			{
				nextShoot = true;
				//print("FirstPos: " + firstLeftHandPos + " shootPos: " + shootPos + " diffrence: " + (firstLeftHandPos - shootPos).magnitude);
			}

			if ((firstRightHandPos - shootPos).magnitude > 20 && nextShoot)
			{
				player.GetComponent<Player>().Shoot();

				nextShoot = false;
			}
		}
	}

	private Vector2 Track(KinectManager manager, int iJointIndex)
	{

		if (manager.IsUserDetected())
		{
			uint userId = manager.GetPlayer1ID();

			if (manager.IsJointTracked(userId, iJointIndex))
			{
				Vector3 posJoint = manager.GetRawSkeletonJointPos(userId, iJointIndex);

				if (posJoint != Vector3.zero)
				{
					// 3d position to depth

					Vector2 posDepth = manager.GetDepthMapPosForJointPos(posJoint);

					// depth pos to color pos

					Vector2 posColor = manager.GetColorMapPosForDepthPos(posDepth);


					float scaleX = (float)posColor.x / KinectWrapper.Constants.ColorImageWidth;
					float scaleY = 1.0f - (float)posColor.y / KinectWrapper.Constants.ColorImageHeight;

					Vector3 vPosOverlay = Camera.main.ViewportToWorldPoint(new Vector3(scaleX, scaleY, rightHandCameraDistance));

					return vPosOverlay;


				}
			}

		}
		return Vector2.zero;
	}
}
