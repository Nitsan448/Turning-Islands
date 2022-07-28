using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DirectionExtensions
{
	public static eDirection GetNewDirection(eDirection oldDirection, int directionChange)
	{
		int newDirection = (int)oldDirection + directionChange;
		if (newDirection > 3)
		{
			newDirection = directionChange - 1;
		}
		else if(newDirection < 0)
		{
			newDirection = 4 - directionChange;
		}
		return (eDirection)newDirection;
	}

	public static int GetIntByDirection(eDirection direction)
	{
		if(direction == eDirection.Left)
		{
			return -1;
		}
		else
		{
			return 1;
		}
	}
}