using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRocketEngine
{
	public abstract float Consumption { get; set; }
	public abstract float MaxConsumption { get; }
	public abstract float MaxPropulsion { get; }
	public abstract float Propultion { get; }
}

