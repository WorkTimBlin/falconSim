using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
	[SerializeField]
	float maxLocalScale = 10;

	RocketEngine engine;

	
	
	// Start is called before the first frame update
	void Start()
	{
		engine = transform.parent.GetComponent<RocketEngine>();
	}

	// Update is called once per frame
	void Update()
	{
		Vector3 localScale = transform.localScale;
		localScale.y = engine.Propultion * maxLocalScale / engine.MaxPropulsion;
		localScale.y = Random.Range(localScale.y * 0.9f, localScale.y * 1.1f);
		transform.localScale = localScale;
	}
}
