using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IReRetractable: IExtendable, IRetractable
{
	public new StateEnum State { get; }
	public new enum StateEnum
	{
		Retracted = 0,
		Retracting = 1,
		Extending = 2,
		Extended = 3
	}
	IRetractable.StateEnum IRetractable.State =>
		this.State > StateEnum.Retracting ?
		IRetractable.StateEnum.Extended :
		(IRetractable.StateEnum)(byte)State;
	IExtendable.StateEnum IExtendable.State =>
		this.State < StateEnum.Extending ?
		IExtendable.StateEnum.Retracted :
		(IExtendable.StateEnum)(byte)State;
}

public interface IRetractable
{
	public float ExpectedRetractingTime { get; }
	public void StartRetraction();
	public StateEnum State { get; }
	public enum StateEnum : byte
	{
		Retracted = 0,
		Retracting = 1,
		Extended = 3
	}
}

public interface IExtendable
{
	public float ExpectedExtendingTime { get; }
	public void StartExtention();
	public StateEnum State { get; }
	public enum StateEnum : byte
	{
		Retracted = 0,
		Extending = 2,
		Extended = 3
	}
}