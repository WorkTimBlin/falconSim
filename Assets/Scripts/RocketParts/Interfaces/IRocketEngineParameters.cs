using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IRocketEngineParameters
{
	public abstract float Consumption { get; set; }
	public abstract float MaxConsumption { get; }
	public abstract float MaxPropulsion { get; }
	public abstract float Propultion { get; }
}

