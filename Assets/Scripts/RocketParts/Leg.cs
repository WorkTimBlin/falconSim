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
	[SerializeField]
	float retractionSpeed = 10;
	[SerializeField]
	bool startExtended;

	Coroutine currentCoroutine;
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
	public IReRetractable.StateEnum State { get; private set; }
	public bool StartExtended 
	{ 
		get => startExtended;
		set 
		{
			startExtended = value;
			AquireStartPosition();
		} 
	}

	public void StartExtention()
	{
		if(currentCoroutine != null) 
			StopCoroutine(currentCoroutine);
		currentCoroutine = StartCoroutine(ExtentionCoroutine());
		State = IReRetractable.StateEnum.Extending;
	}

	public void StartRetraction()
	{
		if(currentCoroutine != null) 
			StopCoroutine(currentCoroutine);
		currentCoroutine = StartCoroutine(RetractionCoroutine());
	}

	IEnumerator ExtentionCoroutine()
	{
		State = IReRetractable.StateEnum.Extending;
		foreach (object movingFrame in MovingCoroutine(100)) 
			yield return movingFrame;
		State = IReRetractable.StateEnum.Extended;
	}
	IEnumerator RetractionCoroutine()
	{
		State = IReRetractable.StateEnum.Retracting;
		foreach (object movingFrame in MovingCoroutine(0)) 
			yield return movingFrame;
		State = IReRetractable.StateEnum.Retracted;
	}

	IEnumerable MovingCoroutine(float extentionPercent)
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

	private void AquireStartPosition()
	{
		if (Application.isPlaying) return;
		if (startExtended) CurrentPosition = fullExtendedPosition;
		else CurrentPosition = fullRetractedPosition;
	}

	private void OnValidate()
	{
		AquireStartPosition();
	}
}
