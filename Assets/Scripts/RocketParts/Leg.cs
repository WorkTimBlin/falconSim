using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leg : MonoBehaviour, IReRetractable
{
	public float ExpectedExtendingTime => 
		fullExtendedPosition - fullRetractedPosition / retractionSpeed;
	public float ExpectedRetractingTime => 
		ExpectedExtendingTime;

	[SerializeField]
	float fullExtendedPosition = 115;
	[SerializeField]
	float fullRetractedPosition = 0;
	float CurrentPosition
	{
		get => transform.localRotation.eulerAngles.z;
		set
		{
			Vector3 angles = transform.localRotation.eulerAngles;
			angles.z = value;
			transform.localRotation = Quaternion.Euler(angles);
		}
	}
	float CurrentExtentionPercent
	{
		get => (CurrentPosition - fullRetractedPosition) * 100 /
			(fullExtendedPosition - fullRetractedPosition);
		set => CurrentPosition =
			fullRetractedPosition +
			(fullExtendedPosition - fullRetractedPosition) * value / 100;
	}


	[SerializeField]
	float retractionSpeed = 10;

	Coroutine currentCoroutine;

	public void StartExtention()
	{
		if(currentCoroutine != null) 
			StopCoroutine(currentCoroutine);
		currentCoroutine = StartCoroutine(ExtentionCoroutine(100));
	}

	public void StartRetraction()
	{
		if(currentCoroutine != null) 
			StopCoroutine(currentCoroutine);
		currentCoroutine = StartCoroutine(ExtentionCoroutine(0));
	}

	IEnumerator ExtentionCoroutine(float extentionPercent)
	{
		while (CurrentExtentionPercent != extentionPercent)
		{
			float extentionDelta = retractionSpeed * Time.deltaTime;
			if (Mathf.Abs(CurrentExtentionPercent - extentionPercent) <=
				extentionDelta)
			{
				CurrentExtentionPercent = extentionPercent;
				break;
			}
			CurrentExtentionPercent +=
				Mathf.Sign(extentionPercent - CurrentExtentionPercent) *
				extentionDelta;
			yield return null;
		}
		currentCoroutine = null;
	}
}
