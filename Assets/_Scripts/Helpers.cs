using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helpers
{
	public static eDirection GetNewDirection(eDirection oldDirection, eDirection directionChange)
	{
		if (directionChange == eDirection.Right)
		{
			if (oldDirection == eDirection.Up)
			{
				return eDirection.Right;
			}
			if (oldDirection == eDirection.Right)
			{
				return eDirection.Down;
			}
			if (oldDirection == eDirection.Down)
			{
				return eDirection.Left;
			}
			if (oldDirection == eDirection.Left)
			{
				return eDirection.Up;
			}
		}
		else
		{
			if (oldDirection == eDirection.Up)
			{
				return eDirection.Left;
			}
			if (oldDirection == eDirection.Right)
			{
				return eDirection.Up;
			}
			if (oldDirection == eDirection.Down)
			{
				return eDirection.Right;
			}
			if (oldDirection == eDirection.Left)
			{
				return eDirection.Down;
			}
		}
		return eDirection.Down;
	}
}