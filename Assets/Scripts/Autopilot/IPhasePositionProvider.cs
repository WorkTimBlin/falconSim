using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// provider of physics information about the craft
/// </summary>
public interface IPhasePositionProvider
{
    PhaseVector<Vector3> Position { get; }
    PhaseVector<Quaternion> Rotation { get; }
}

public struct PhaseVector<T>
{
    public T position;
    public T velocity;
}
