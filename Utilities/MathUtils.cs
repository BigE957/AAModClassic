using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
namespace AAModClassic.Utilities
{
    public static class MathUtils
    {
        public static bool TryGetLaunchVelocity(Vector2 goal, float initialYVelocity, float gravity, out Vector2 velocity)
        {
            if (MathF.Abs(gravity) == 0)
            {
                velocity = Vector2.Zero;
                return false;
            }

            float vyisq = initialYVelocity * initialYVelocity;

            float d = vyisq + 2 * gravity * goal.Y;

            if (d < 0)
            {
                velocity = Vector2.Zero;
                return false;
            }

            float vyf = -MathF.Sqrt(d);
            float t = Math.Abs((vyf - initialYVelocity) / gravity);

            if (t <= float.Epsilon)
            {
                velocity = Vector2.Zero;
                return false;
            }

            float vxi = goal.X / t;

            if (float.IsNaN(vxi) || float.IsInfinity(vxi))
            {
                velocity = Vector2.Zero;
                return false;
            }

            velocity = new Vector2(vxi, -initialYVelocity);
            return true;
        }

        public static float CircOutEasing(float amount) => (float)Math.Sqrt(1.0 - Math.Pow(amount - 1f, 2.0));

        public static float ExpInEasing(float amount)
        {
            if (amount != 0f)
            {
                return (float)Math.Pow(2.0, 10f * amount - 10f);
            }

            return 0f;
        }
        public static float ExpOutEasing(float amount)
        {
            if (amount != 1f)
            {
                return 1f - (float)Math.Pow(2.0, -10f * amount);
            }

            return 1f;
        }

        public static float SineInEasing(float amount) => 1f - (float)Math.Cos(amount * MathF.PI / 2f);
        public static float SineOutEasing(float amount) => (float)Math.Sin(amount * MathF.PI / 2f);
        public static float SineInOutEasing(float amount) => (0f - ((float)Math.Cos(amount * MathF.PI) - 1f)) / 2f;
        public static float SineBumpEasing(float amount) => (float)Math.Sin(amount * MathF.PI);

        public static float PolyInEasing(float amount, float degree) => (float)Math.Pow(amount, degree);
        public static float PolyOutEasing(float amount, float degree) => 1f - (float)Math.Pow(1f - amount, degree);
        public static float PolyInOutEasing(float amount, float degree) => amount < 0.5f ? (float)Math.Pow(2, degree - 1) * (float)Math.Pow(amount, degree) : 1f - (float)Math.Pow(-2 * amount + 2, degree) / 2f;

        public static Vector2 ClampMagnitude(this Vector2 v, float min, float max)
        {
            return v.SafeNormalize(Vector2.UnitY) * MathHelper.Clamp(v.Length(), min, max);
        }
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
