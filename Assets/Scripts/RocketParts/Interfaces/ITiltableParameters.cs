using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface ITiltableParameters
{
	public abstract float MaxAngle { get; }
	// vector 2 with x, y within maxAngle-circle, representing the thrust vector
	public abstract Vector2 CurrentPosition { get; }
	public abstract Vector2 DesiredPosition { get; }
}
