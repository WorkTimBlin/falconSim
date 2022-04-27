using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// provider of physics information about the craft
/// </summary>
public interface IPhasePositionProvider
{
    Vector3 Position { get; }
    Quaternion Rotation { get; }
}
