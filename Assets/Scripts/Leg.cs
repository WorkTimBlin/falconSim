using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leg : MonoBehaviour, IReRetractable
{
	[SerializeField]
	float fullExtendedPosition = 115;
	[SerializeField]
	float fullRetractedPosition = 0;
	float CurrentPosition
	{
		get => transform.rotation.eulerAngles.z;
		set
		{
			Vector3 angles = transform.rotation.eulerAngles;
			angles.z = value;
			transform.rotation = Quaternion.Euler(angles);
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

	bool retracted = false;

	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		if (!retracted)
		{
			StartRetraction();
			retracted = true;
		}
	}

	public void StartExtention()
	{
		if(currentCoroutine is not null) StopCoroutine(currentCoroutine);
		currentCoroutine = StartCoroutine(ExtentionCoroutine(100));
	}

	public void StartRetraction()
	{
		if(currentCoroutine is not null) StopCoroutine(currentCoroutine);
		currentCoroutine = StartCoroutine(ExtentionCoroutine(0));
	}

	IEnumerator ExtentionCoroutine(float extentionPercent)
	{
		while (CurrentExtentionPercent != extentionPercent)
		{
			CurrentExtentionPercent +=
				Mathf.Sign(extentionPercent - CurrentExtentionPercent) *
				retractionSpeed * Time.deltaTime;
			if (Mathf.Abs(CurrentExtentionPercent - extentionPercent) <=
				retractionSpeed * Time.deltaTime)
			{
				CurrentExtentionPercent = extentionPercent;
				yield break;
			}
			yield return null;
		}
	}
}
