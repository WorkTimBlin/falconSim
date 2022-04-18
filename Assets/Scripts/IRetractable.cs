using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IReRetractable: IExtendable, IRetractable { }

public interface IRetractable
{
    public void StartRetraction();
}

public interface IExtendable
{
    public void StartExtention();
}
