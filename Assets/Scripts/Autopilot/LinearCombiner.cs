using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LinearCombiner : MonoBehaviour, ITiltController
{
    [SerializeField, OneLine.OneLine]
    Control[] controls;

	public Vector2 ControlSignal =>
		controls.Select(
			c => (c.TiltController.ControlSignal, c.Weight))
		.Aggregate(
			(a, b) => 
			(a.ControlSignal * a.Weight + 
			b.ControlSignal * b.Weight, 
			1f))
		.ControlSignal;

    [Serializable]
    public class Control
	{
        [SerializeField]
        MonoBehaviour tiltController;
		public ITiltController TiltController =>
			tiltController as ITiltController;
        [SerializeField]
        float weight = 1;
		public float Weight => weight;

		public void OnValidate()
		{
			tiltController =
				InterfaceValidationHelper.
				GetInterfaceAsMonoBehaviour
				<ITiltController>(tiltController);
		}
	}

	private void OnValidate()
	{
		foreach(Control control in controls)
		{
			control.OnValidate();
		}
	}
}
