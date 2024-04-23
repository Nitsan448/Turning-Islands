using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CubeExtensions
{
    public static Cube FindAdjacentCubeByDirection(Cube cube, EDirection direction)
    {
        Cube adjacentCube = null;
        switch (direction)
        {
            case EDirection.Top:
                adjacentCube = cube.TopCube;
                break;
            case EDirection.Right:
                adjacentCube = cube.RightCube;
                break;
            case EDirection.Bottom:
                adjacentCube = cube.BottomCube;
                break;
            case EDirection.Left:
                adjacentCube = cube.LeftCube;
                break;
        }

        return adjacentCube;
    }
}