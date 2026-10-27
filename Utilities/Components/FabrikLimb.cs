using Microsoft.Xna.Framework;
using System;
using Terraria;

namespace AAModClassic.Utilities.Components
{
    public class FabrikLimb(int pointCount, float segLength, int solveIterations)
    {
        private readonly Vector2[] points = new Vector2[pointCount];
        private readonly float segmentLength = segLength;
        private readonly int iterations = solveIterations;

        public Vector2[] Points => points;

        public int Count => Points.Length;

        public float Length => Points.Length * segmentLength;

        public void Update(Vector2 start, Vector2 target)
        {
            points[^1] = target;
            for(int iter = 0; iter < iterations; iter++)
            {
                for(int i = points.Length - 2; i >= 0; i--)
                {
                    Vector2 toCurrent = points[i + 1].DirectionTo(points[i]);
                    points[i] = points[i + 1] + toCurrent * segmentLength;
                }

                points[0] = start;
                for (int i = 0; i < points.Length - 1; i++)
                {
                    Vector2 toNext = points[i].DirectionTo(points[i + 1]);
                    points[i + 1] = points[i] + toNext * segmentLength;
                }
            }
        }
    }
}
