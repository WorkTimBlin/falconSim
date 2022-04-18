using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class legJointBehaviour : MonoBehaviour
{
    HingeJoint joint;

    [SerializeField]
    float fullExtendedPosition = 0;
    [SerializeField]
    float fullRetractedPosition = -135;
    float CurrentPosition
	{
        get => joint.limits.min;
		set
		{
            JointLimits limits = joint.limits;
            limits.min = value;
            limits.max = value;
            joint.limits = limits;
		}
    }
    float CurrentExtentionPercent {
        get => (CurrentPosition - fullRetractedPosition) * 100 / 
            (fullExtendedPosition - fullRetractedPosition);
        set => CurrentPosition = 
            fullRetractedPosition + 
            (fullExtendedPosition - fullRetractedPosition) * value / 100;
    }
    [SerializeField]
    float retractionSpeed = 10;
    bool retracted = false;
    // Start is called before the first frame update
    void Start()
    {
        joint = GetComponent<HingeJoint>();
    }

    // Update is called once per frame
    void Update()
    {
		if (!retracted)
		{
            StartCoroutine(ExtentionCoroutine(0));
            retracted = true;
		}
    }

	IEnumerator ExtentionCoroutine(float extentionPercent)
	{
        while(CurrentExtentionPercent != extentionPercent)
		{
            CurrentExtentionPercent += 
                Mathf.Sign(extentionPercent - CurrentExtentionPercent) * 
                retractionSpeed * Time.deltaTime;
            if(Mathf.Abs(CurrentExtentionPercent - extentionPercent) <= 
                retractionSpeed * Time.deltaTime)
			{
                CurrentExtentionPercent = extentionPercent;
                yield break;
			}
            yield return null;
		}
	}
}
