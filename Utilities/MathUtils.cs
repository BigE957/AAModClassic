using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAModClassic.Utilities
{
    public static class MathUtils
    {
    }

    public class BezierCurve(params Vector2[] controls)
    {
        public Vector2[] ControlPoints = controls;

        public Vector2 Evaluate(float interpolant) => PrivateEvaluate(ControlPoints, MathHelper.Clamp(interpolant, 0f, 1f));

        public List<Vector2> GetPoints(int totalPoints)
        {
            float perStep = 1f / totalPoints;

            List<Vector2> points = [];

            for (float step = 0f; step <= 1f; step += perStep)
                points.Add(Evaluate(step));

            return points;
        }

        private static Vector2 PrivateEvaluate(Vector2[] points, float T)
        {
            while (points.Length > 2)
            {
                Vector2[] nextPoints = new Vector2[points.Length - 1];
                for (int k = 0; k < points.Length - 1; k++)
                    nextPoints[k] = Vector2.Lerp(points[k], points[k + 1], T);

                points = nextPoints;
            }

            if (points.Length <= 1)
                return Vector2.Zero;

            return Vector2.Lerp(points[0], points[1], T);
        }
    }
}
