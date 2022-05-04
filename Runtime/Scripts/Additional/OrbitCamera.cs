/**
 * File Name: OrbitCamera.cs
 * Description: This script is for orbiting around a transform with a Camera
 * 
 * Authors: Catlike Coding, Will Lacey
 * Date Created: May 3, 2022
 * 
 * Additional Comments: 
 *		While this file has been updated to better fit this project, the original version can be found here:
 *		https://catlikecoding.com/unity/tutorials/movement/
 * 
 *		File Line Length: 120
 * 
 *		TODO: Generalize this script into a TransformOrbiter class
 **/

using UnityEngine;
using UnityEngine.InputSystem;

public class OrbitCamera : MonoBehaviour
{
	/************************************************************/
	#region Fields

	[Tooltip("transform to orbit around")]
	[SerializeField] Transform target = default;
	[Tooltip("how far to orbit around the target")]
	[SerializeField, Range(1f, 50f)] float distance = 5f;

	[SerializeField, Min(0f)] float focusRadius = 5f;

	[SerializeField, Range(0f, 1f)] float focusCentering = 0.5f;

	[SerializeField, Range(1f, 360f)] float rotationSpeed = 90f;

	[SerializeField, Range(-89f, 89f)] float minVerticalAngle = -45f, maxVerticalAngle = 45f;

	[SerializeField, Min(0f)] float alignDelay = 5f;

	[SerializeField, Range(0f, 90f)] float alignSmoothRange = 45f;

	[SerializeField, Min(0f)] float upAlignmentSpeed = 360f;

	[SerializeField] LayerMask obstructionMask = -1;

	Camera regularCamera;

	Vector3 focusPoint, previousFocusPoint;

	Vector2 orbitAngles = new Vector2(45f, 0f);

	float lastManualRotationTime;

	/* gravity variables */
	Quaternion gravityAlignment = Quaternion.identity;
	Quaternion orbitRotation;

	/* class 'controls' variables */
	Vector2 playerInput;

	#endregion
	/************************************************************/
	#region Properties

	Vector3 CameraHalfExtends
	{
		get
		{
			Vector3 halfExtends;
			halfExtends.y = regularCamera.nearClipPlane * Mathf.Tan(0.5f * Mathf.Deg2Rad * regularCamera.fieldOfView);
			halfExtends.x = halfExtends.y * regularCamera.aspect;
			halfExtends.z = 0f;
			return halfExtends;
		}
	}

    #endregion
    /************************************************************/
    #region Functions

    #region Unity Functions

    private void OnValidate()
	{
		if (maxVerticalAngle < minVerticalAngle) maxVerticalAngle = minVerticalAngle;
	}
	
	private void Awake()
	{
		regularCamera = GetComponent<Camera>();
		focusPoint = target.position;
		transform.localRotation = orbitRotation = Quaternion.Euler(orbitAngles);
	}

	private void LateUpdate()
	{
		UpdateGravityAlignment();

		UpdateFocusPoint();

		if (ManualRotation() || AutomaticRotation())
		{
			ConstrainAngles();
			orbitRotation = Quaternion.Euler(orbitAngles);
		}
		Quaternion lookRotation = gravityAlignment * orbitRotation;

		Vector3 lookDirection = lookRotation * Vector3.forward;
		Vector3 lookPosition = focusPoint - lookDirection * distance;

		Vector3 rectOffset = lookDirection * regularCamera.nearClipPlane;
		Vector3 rectPosition = lookPosition + rectOffset;
		Vector3 castFrom = target.position;
		Vector3 castLine = rectPosition - castFrom;
		float castDistance = castLine.magnitude;
		Vector3 castDirection = castLine / castDistance;

		if (Physics.BoxCast(castFrom, CameraHalfExtends, castDirection, out RaycastHit hit,
			lookRotation, castDistance, obstructionMask))
		{
			rectPosition = castFrom + castDirection * hit.distance;
			lookPosition = rectPosition - rectOffset;
		}

		SetPositionAndRotation(lookPosition, lookRotation);
	}

	#endregion

	#region Other Functions

	private void SetPositionAndRotation(Vector3 position, Quaternion rotation)
	{
		transform.SetPositionAndRotation(position, rotation);
	}

	private void UpdateFocusPoint()
	{
		previousFocusPoint = focusPoint;
		Vector3 targetPoint = target.position;
		if (focusRadius > 0f)
		{
			float distance = Vector3.Distance(targetPoint, focusPoint);
			float t = 1f;
			if (distance > 0.01f && focusCentering > 0f) t = Mathf.Pow(1f - focusCentering, Time.unscaledDeltaTime);
			if (distance > focusRadius) t = Mathf.Min(t, focusRadius / distance);
			focusPoint = Vector3.Lerp(targetPoint, focusPoint, t);
		}
		else
		{
			focusPoint = targetPoint;
		}
	}

	private bool ManualRotation()
	{
		playerInput = Mouse.current.delta.ReadValue();
		Vector2 input = new Vector2( playerInput.y, playerInput.x);
		const float e = 0.001f;
		if (input.x < -e || input.x > e || input.y < -e || input.y > e)
		{
			orbitAngles += rotationSpeed * Time.unscaledDeltaTime * input;
			lastManualRotationTime = Time.unscaledTime;
			return true;
		}
		return false;
	}

	private bool AutomaticRotation()
	{
		if (Time.unscaledTime - lastManualRotationTime < alignDelay) return false;

		Vector3 alignedDelta = Quaternion.Inverse(gravityAlignment) * (focusPoint - previousFocusPoint);

		Vector2 movement = new Vector2(alignedDelta.x, alignedDelta.z);
		float movementDeltaSqr = movement.sqrMagnitude;
		if (movementDeltaSqr < 0.0001f) return false;

		float headingAngle = GetAngle(movement / Mathf.Sqrt(movementDeltaSqr));
		float deltaAbs = Mathf.Abs(Mathf.DeltaAngle(orbitAngles.y, headingAngle));
		float rotationChange = rotationSpeed * Mathf.Min(Time.unscaledDeltaTime, movementDeltaSqr);

		if (deltaAbs < alignSmoothRange) rotationChange *= deltaAbs / alignSmoothRange;
		else if (180f - deltaAbs < alignSmoothRange) rotationChange *= (180f - deltaAbs) / alignSmoothRange;

		orbitAngles.y = Mathf.MoveTowardsAngle(orbitAngles.y, headingAngle, rotationChange);

		return true;
	}

	private void ConstrainAngles()
	{
		orbitAngles.x = Mathf.Clamp(orbitAngles.x, minVerticalAngle, maxVerticalAngle);

		if (orbitAngles.y < 0f) orbitAngles.y += 360f;
		else if (orbitAngles.y >= 360f) orbitAngles.y -= 360f;
	}

	private void UpdateGravityAlignment()
	{
		Vector3 fromUp = gravityAlignment * Vector3.up;
		Vector3 toUp = Vector3.up;//CustomGravity.GetUpAxis(focusPoint);

		float dot = Mathf.Clamp(Vector3.Dot(fromUp, toUp), -1f, 1f);
		float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;
		float maxAngle = upAlignmentSpeed * Time.deltaTime;

		Quaternion newAlignment = Quaternion.FromToRotation(fromUp, toUp) * gravityAlignment;

		if (angle <= maxAngle) gravityAlignment = newAlignment;
		else gravityAlignment = Quaternion.SlerpUnclamped(gravityAlignment, newAlignment, maxAngle / angle);
	}

	static float GetAngle(Vector2 direction)
	{
		float angle = Mathf.Acos(direction.y) * Mathf.Rad2Deg;
		return direction.x < 0f ? 360f - angle : angle;
	}

    #endregion

    #endregion
    /************************************************************/
}