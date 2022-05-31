using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[ExecuteInEditMode]
public class LegsControl : MonoBehaviour
{
	[SerializeField]
	List<MonoBehaviour> legs;
	[Header("Controls")]
	[SerializeField]
	KeyCode retract;
	[SerializeField]
	KeyCode extend;
	[SerializeField]
	KeyCode toggle;
	[SerializeField]
	bool startExtended;

	IEnumerable<IReRetractable> reRetractables => 
		legs.Select(
			(x) => InterfaceValidationHelper.GetInterface<IReRetractable>(x));

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(retract))
		{
			foreach(IReRetractable reRetractable in reRetractables)
			{
				reRetractable.StartRetraction();
			}
		}
		if (Input.GetKeyDown(extend))
		{
			foreach (IReRetractable reRetractable in reRetractables)
			{
				reRetractable.StartExtention();
			}
		}
		if (Input.GetKeyDown(toggle))
		{
			foreach (IReRetractable reRetractable in reRetractables)
			{
				if(reRetractable.State < IReRetractable.StateEnum.Extending)
					reRetractable.StartExtention();
				else if(reRetractable.State > IReRetractable.StateEnum.Retracting)
					reRetractable.StartRetraction();
			}
		}
	}
	private void OnValidate()
	{
		legs = reRetractables.Cast<MonoBehaviour>().ToList();
		foreach (IReRetractable reRetractable in reRetractables)
		{
			reRetractable.StartExtended = startExtended;
		}
	}
}