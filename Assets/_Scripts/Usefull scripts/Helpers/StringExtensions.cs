using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;

public static class StringExtensions
{
    public static string NumberOfDecimals = "F" + Managers.UI.numberOfDecimals.ToString();

    public static void UpdateNumberOfDecimals()
    {
        NumberOfDecimals = "F" + Managers.UI.numberOfDecimals.ToString();
    }

    public static string FloatToString(float number)
	{
        return number.ToString(NumberOfDecimals);
    }

    public static Matrix4x4 StringToMatrix(string transform)
    {
        Matrix4x4 result = new Matrix4x4();
        string[] vectors = transform.Split('\n');

        result.SetRow(0, StringToVector4(vectors[0]));
        result.SetRow(1, StringToVector4(vectors[1]));
        result.SetRow(2, StringToVector4(vectors[2]));
        result.SetRow(3, StringToVector4(vectors[3]));

        return result;
    }

    public static Vector4 StringToVector4(string vector)
    {
        List<float> floatList = VectorStringToFloatList(vector);
        return new Vector4(floatList[0], floatList[1], floatList[2], floatList[3]);
    }

    public static List<float> VectorStringToFloatList(string vector)
    {
        RemoveParenthesis(ref vector);

        string[] vectorValues = vector.Split(',');

        List<float> result = new List<float>();
        for (int i = 0; i < vectorValues.Length; i++)
        {
            result.Add(float.Parse(vectorValues[i]));
        }
        return result;
    }

    public static void RemoveParenthesis(ref string vector)
    {
        vector = vector.Replace("(", string.Empty);
        vector = vector.Replace(")", string.Empty);
    }

    public static string MatrixToString(Matrix4x4 matrix)
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(Vector4ToString(matrix.GetRow(0)));
        stringBuilder.Append('\n');
        stringBuilder.Append(Vector4ToString(matrix.GetRow(1)));
        stringBuilder.Append('\n');
        stringBuilder.Append(Vector4ToString(matrix.GetRow(2)));
        stringBuilder.Append('\n');
        stringBuilder.Append(Vector4ToString(matrix.GetRow(3)));

        return stringBuilder.ToString();
    }

    public static bool IsMatrixStringFormatValid(string transform)
    {
        string[] vectors = transform.Split('\n');
        foreach (string vector in vectors)
        {
            if (!IsVectorStringFormatValid(vector))
            {
                return false;
            }
        }
        return true;
    }

    public static bool IsVectorStringFormatValid(string vector)
    {
        RemoveParenthesis(ref vector);
        string[] vectorValues = vector.Split(',');

        for (int i = 0; i < vectorValues.Length; i++)
        {
            float nextValue;
            bool floatInputWasValid = float.TryParse(vectorValues[i], out nextValue);
            if (!floatInputWasValid)
            {
                return false;
            }
        }

        return true;
    }

    public static Vector3 StringToVector3(string vector)
    {
        List<float> floatList = VectorStringToFloatList(vector);
        return new Vector3(floatList[0], floatList[1], floatList[2]);
    }

    public static string Vector4ToString(Vector4 vector)
    {
        StringBuilder stringBuilder = new StringBuilder(Vector3ToString(vector));
        stringBuilder.Remove(stringBuilder.Length - 1, 1);
        stringBuilder.Append(", ");
        stringBuilder.Append(vector.w.ToString(NumberOfDecimals));
        stringBuilder.Append(")");
        return stringBuilder.ToString();
    }

    public static string Vector3ToString(Vector3 vector)
    {
        StringBuilder stringBuilder = new StringBuilder("(");
        stringBuilder.Append(vector.x.ToString(NumberOfDecimals));
        stringBuilder.Append(", ");
        stringBuilder.Append(vector.y.ToString(NumberOfDecimals));
        stringBuilder.Append(", ");
        stringBuilder.Append(vector.z.ToString(NumberOfDecimals));
        stringBuilder.Append(")");
        return stringBuilder.ToString();
    }

    public static Matrix4x4 LinearTransformationStringToMatrix(string linearTransformation)
    {
        Matrix4x4 result = new Matrix4x4();
        string[] linearTransformationValues = linearTransformation.Split(',');
        result.SetRow(0, LinearTransformationValueToMatrixRow(linearTransformationValues[0]));
        result.SetRow(1, LinearTransformationValueToMatrixRow(linearTransformationValues[1]));
        result.SetRow(2, LinearTransformationValueToMatrixRow(linearTransformationValues[2]));
        result.SetRow(3, new Vector4(0, 0, 0, 1));

        Debug.Log(result.ToString());
        return result;
    }

    private static Vector4 LinearTransformationValueToMatrixRow(string row)
    {
        RemoveIrrelevantChars(ref row);
        Vector4 result;

        result.x = GetAndRemoveCoefficient(ref row, 'x');
        result.y = GetAndRemoveCoefficient(ref row, 'y');
        result.z = GetAndRemoveCoefficient(ref row, 'z');

        bool wValueExists = float.TryParse(row, out float wValue);
        if (wValueExists)
        {
            result.w = wValue;
        }
        else
        {
            result.w = 0;
        }

        return result;
    }

    private static void RemoveIrrelevantChars(ref string row)
	{
        row = row.Replace(" ", string.Empty);
        row = row.Replace("+", string.Empty);
        row = row.Replace("\n", string.Empty);
        RemoveParenthesis(ref row);
    }

    //Change method name
    private static float GetAndRemoveCoefficient(ref string row, char valueToFindCoefficientOf)
    {
        float result;
        int indexOfChar = row.IndexOf(valueToFindCoefficientOf);
        if (indexOfChar == -1)
        {
            result = 0;
        }
        else if (indexOfChar == 0)
        {
            result = 1;
            row = row.Remove(0, indexOfChar + 1);
        }
        else
        {
            result = float.Parse(row.Substring(0, indexOfChar));
            row = row.Remove(0, indexOfChar + 1);
        }
        return result;
    }

    public static string MatrixToLinearTransformationString()
    {
        return string.Empty;
    }
}
