using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DirectionExtensions
{
    public static EDirection GetNewDirection(EDirection oldDirection, int directionChange)
    {
        int newDirection = (int)oldDirection + directionChange;
        if (newDirection > 3)
        {
            newDirection = 0;
        }
        else if (newDirection < 0)
        {
            newDirection = 3;
        }

        return (EDirection)newDirection;
    }

    public static int GetIntByDirection(EDirection direction)
    {
        if (direction == EDirection.Left)
        {
            return -1;
        }
        else
        {
            return 1;
        }
    }

    public static Vector3 GetEulerAnglesByDirection(this EDirection direction)
    {
        switch (direction)
        {
            case EDirection.Top:
                return Vector3.zero;
            case EDirection.Right:
                return new Vector3(0, 0, 90);
            case EDirection.Bottom:
                return new Vector3(0, 0, 180);
            case EDirection.Left:
                return new Vector3(0, 0, 270);
        }

        return Vector3.zero;
    }
}