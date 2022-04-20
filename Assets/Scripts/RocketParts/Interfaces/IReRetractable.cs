using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IReRetractable: IExtendable, IRetractable { }

public interface IRetractable
{
    public float ExpectedRetractingTime { get; }
    public void StartRetraction();
}

public interface IExtendable
{
    public float ExpectedExtendingTime { get; }
    public void StartExtention();
}
