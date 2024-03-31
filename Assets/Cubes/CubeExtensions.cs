using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CubeExtensions
{
    public static Cube FindAdjacentCubeByDirection(Cube cube, eDirection direction)
    {
        Cube adjacentCube = null;
        switch (direction)
        {
            case eDirection.Top:
                adjacentCube = cube.TopCube;
                break;
            case eDirection.Right:
                adjacentCube = cube.RightCube;
                break;
            case eDirection.Bottom:
                adjacentCube = cube.BottomCube;
                break;
            case eDirection.Left:
                adjacentCube = cube.LeftCube;
                break;
        }

        return adjacentCube;
    }
}
