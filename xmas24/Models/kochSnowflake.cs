﻿using System;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using Blazor.Extensions.Canvas;

public class KochSnowflake
{
    /// <summary>
    ///     Method to render the Koch snowflake to a bitmap. To save the
    ///     bitmap the command 'GetKochSnowflake().Save("KochSnowflake.png")' can be used.
    /// </summary>
    /// <param name="bitmapWidth">The width of the rendered bitmap.</param>
    /// <param name="steps">The number of iterations.</param>
    /// <returns>The bitmap of the rendered Koch snowflake.</returns>
    public static List<Vector2> GetKochSnowflake(int bitmapWidth = 600, int steps = 5)
    {
        var offsetX = bitmapWidth / 10f;
        var offsetY = bitmapWidth / 3.7f;
        var vector1 = new Vector2(offsetX, offsetY);
        var vector2 = new Vector2(bitmapWidth / 2, (float)Math.Sin(Math.PI / 3) * bitmapWidth * 0.8f + offsetY);
        var vector3 = new Vector2(bitmapWidth - offsetX, offsetY);
        List<Vector2> initialVectors = new() { vector1, vector2, vector3, vector1 };
        List<Vector2> vectors = Iterate(initialVectors, steps);
        return vectors;
    }
    public static List<Vector2> Iterate(List<Vector2> initialVectors, int steps = 2)
    {
        List<Vector2> vectors = initialVectors;
        for (var i = 0; i < steps; i++)
        {
            vectors = IterationStep(vectors);
        }

        return vectors;
    }

  

    /// <summary>
    ///     Loops through each pair of adjacent vectors. Each line between two adjacent
    ///     vectors is divided into 4 segments by adding 3 additional vectors in-between
    ///     the original two vectors. The vector in the middle is constructed through a
    ///     60 degree rotation so it is bent outwards.
    /// </summary>
    /// <param name="vectors">
    ///     The vectors composing the shape to which
    ///     the algorithm is applied.
    /// </param>
    /// <returns>The transformed vectors after the iteration-step.</returns>
    private static List<Vector2> IterationStep(List<Vector2> vectors)
    {
        List<Vector2> newVectors = new();
        for (var i = 0; i < vectors.Count - 1; i++)
        {
            var startVector = vectors[i];
            var endVector = vectors[i + 1];
            newVectors.Add(startVector);
            var differenceVector = endVector - startVector;
            newVectors.Add(startVector + differenceVector / 3);
            newVectors.Add(startVector + differenceVector / 3 + Rotate(differenceVector / 3, 60));
            newVectors.Add(startVector + differenceVector * 2 / 3);
        }

        newVectors.Add(vectors[^1]);
        return newVectors;
    }

    /// <summary>
    ///     Standard rotation of a 2D vector with a rotation matrix
    ///     (see https://en.wikipedia.org/wiki/Rotation_matrix ).
    /// </summary>
    /// <param name="vector">The vector to be rotated.</param>
    /// <param name="angleInDegrees">The angle by which to rotate the vector.</param>
    /// <returns>The rotated vector.</returns>
    private static Vector2 Rotate(Vector2 vector, float angleInDegrees)
    {
        var radians = angleInDegrees * (float)Math.PI / 180;
        var ca = (float)Math.Cos(radians);
        var sa = (float)Math.Sin(radians);
        return new Vector2(ca * vector.X - sa * vector.Y, sa * vector.X + ca * vector.Y);
    }

    
}