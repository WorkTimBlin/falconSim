using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegsControl : MonoBehaviour
{
	[SerializeField]
	List<GameObject> legs = new List<GameObject>();
	[Header("Controls")]
	[SerializeField]
	KeyCode retract;
	[SerializeField]
	KeyCode extend;

	List<IReRetractable> reRetractables;
	// Start is called before the first frame update
	void Start()
	{
		reRetractables = new List<IReRetractable>() { Capacity = legs.Count };
		foreach(GameObject leg in legs)
		{
			reRetractables.Add(leg.GetComponent<IReRetractable>());
		}
	}

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
	}
}
