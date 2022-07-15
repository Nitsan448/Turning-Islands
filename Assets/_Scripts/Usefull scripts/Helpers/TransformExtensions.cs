using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtensions
{
    public static void CopyTransform(Transform transformToCopyTo, Transform transformToCopyFrom)
	{
		transformToCopyTo.position = transformToCopyFrom.position;
		transformToCopyTo.rotation = transformToCopyFrom.rotation;
		transformToCopyTo.localScale = transformToCopyFrom.localScale;
	}

	public static Vector4 ConvertToVector4(Vector3 vectorToConvert, float wValue = 1)
	{
		Vector4 newVector = new Vector4(vectorToConvert.x, vectorToConvert.y, vectorToConvert.z, wValue);
		return newVector;
	}
}
