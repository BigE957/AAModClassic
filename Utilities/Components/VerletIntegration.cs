using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;

namespace AAModClassic.Utilities.Components;

public class VerletPoint(Vector2 start, bool locked)
{
    public Vector2 Position = start, OldPosition = start;
    public bool Locked = locked;
    public List<(VerletPoint Point, float Length)> Connections = [];
}

public enum ObjectType
{
    Chain,
    Shape
}

public class VerletObject(List<VerletPoint> points, ObjectType type)
{
    public List<VerletPoint> Points = points;
    public readonly ObjectType Type = type;

    public int Count => Points.Count;
    public Vector2[] Positions => [.. Points.Select(x => x.Position)];

    public VerletPoint this[Index key]
    {
        get => Points[key];
        set => Points[key] = value;
    }
}

public static class VerletIntegration
{
    public static void VerletSimulation(List<VerletPoint> chain, int loops = 10, float gravity = 0.3f, bool windAffected = true)
    {
        foreach (VerletPoint point in chain)
        {
            if (point.Locked)
                continue;

            Vector2 posBeforeUpdate = point.Position;
            point.Position += point.Position - point.OldPosition;
            point.Position += Vector2.UnitY * gravity;
            if (windAffected)
                point.Position += Vector2.UnitX * Main.windSpeedCurrent;
            point.OldPosition = posBeforeUpdate;
        }

        for (int i = 0; i < loops; i++)
        {
            foreach (VerletPoint point in chain)
            {
                if (point.Connections.Count == 0)
                    continue;

                for (int j = 0; j < point.Connections.Count; j++)
                {
                    float segmentDistance = point.Connections[j].Length;
                    if (segmentDistance == -1)
                        continue;
                    Vector2 segmentCenter = (point.Position + point.Connections[j].Point.Position) / 2f;
                    Vector2 segmentDirection = (point.Position - point.Connections[j].Point.Position).SafeNormalize(Vector2.UnitY);

                    if (!point.Locked)
                        point.Position = segmentCenter + segmentDirection * segmentDistance / 2f;

                    if (!point.Connections[j].Point.Locked)
                        point.Connections[j].Point.Position = segmentCenter - segmentDirection * segmentDistance / 2f;

                }
            }
        }
    }

    public static void VerletSimulation(List<VerletPoint>[] chains, int loops = 10, float gravity = 0.3f, bool windAffected = true)
    {
        foreach (List<VerletPoint> chain in chains)
            VerletSimulation(chain, loops, gravity, windAffected);
    }

    public static void VerletSimulation(List<VerletObject> objs, int loops = 10, float gravity = 0.3f, bool windAffected = true)
    {
        foreach (VerletObject obj in objs)
            VerletSimulation(obj, loops, gravity, windAffected);
    }

    public static void VerletSimulation(VerletObject obj, int loops = 10, float gravity = 0.3f, bool windAffected = true)
    {
        VerletSimulation(obj.Points, loops, gravity, windAffected);
    }

    public static VerletObject CreateVerletChain(Vector2 startPos, Vector2 endPos, int count, float distBetween, bool lockStart = true, bool lockEnd = false)
    {
        List<VerletPoint> output = [];

        for (int i = 0; i < count; i++)
        {
            Vector2 spawnPos = Vector2.Lerp(startPos, endPos, i / (float)(count - 1));

            VerletPoint newPoint = new(spawnPos, i == 0 ? lockStart : i == count - 1 && lockEnd);
            output.Add(newPoint);
            if(i != 0)
                ConnectVerlets(output[i - 1], output[i], distBetween);
        }

        return new(output, ObjectType.Chain);
    }

    public static VerletObject CreateVerletBox(Rectangle r)
    {
        List<VerletPoint> output = [];

        output.Add(new(r.TopLeft(), false));
        output.Add(new(r.TopRight(), false));
        output.Add(new(r.BottomRight(), false));
        output.Add(new(r.BottomLeft(), false));

        ConnectVerlets(output[0], output[1], r.Width);
        ConnectVerlets(output[0], output[3], r.Height);
        ConnectVerlets(output[1], output[2], r.Height);
        ConnectVerlets(output[2], output[3], r.Width);
        ConnectVerlets(output[0], output[2], (r.TopLeft() - r.BottomRight()).Length());
        ConnectVerlets(output[1], output[3], (r.TopRight() - r.BottomLeft()).Length());

        return new(output, ObjectType.Shape);
    }

    public static void ConnectVerlets(VerletPoint a, VerletPoint b, float length)
    {
        a.Connections.Add((b, length));
        a.Connections.Add((b, -1));
    }

    public static void SetupMidPointConnection(VerletObject holder, int holderIndex, VerletPoint endPoint, float length)
    {
        Vector2 startPos = (holder.Points[holderIndex].Position + endPoint.Position) / 2f;
        VerletPoint midPoint1 = new(startPos, false);
        ConnectVerlets(holder.Points[holderIndex], midPoint1, length);
        ConnectVerlets(midPoint1, endPoint, length);
        holder.Points.Add(midPoint1);
    }

    public static void BreakVerletConnection(VerletPoint a, VerletPoint b)
    {
        int index = a.Connections.FindIndex(c => c.Point == b);
        if (index != -1)
            a.Connections.RemoveAt(index);
        index = b.Connections.FindIndex(c => c.Point == a);
        if (index != -1)
            b.Connections.RemoveAt(index);
    }

    public static void RemoveVerletPoint(this List<VerletPoint> myList, int index)
    {
        VerletPoint p = myList[index];
        for(int i = 0; i < p.Connections.Count; i++)
            BreakVerletConnection(p, p.Connections[i].Point);

        myList.RemoveAt(index);
    }
    public static void RemoveVerletPoint(this List<VerletPoint> myList, VerletPoint p)
    {
        myList.RemoveVerletPoint(myList.FindIndex(c => c == p));
    }

    public static bool AffectVerletObject(VerletObject obj, float dampening, float cap, bool isChain = true)
    {
        
        if (obj == null)
            return false;

        bool notableMove = false;

        foreach (Player p in Main.ActivePlayers)
        {
            bool temp = MoveObjectBasedOnEntity(obj, p, dampening / 2f, cap / 2f, isChain);
            if(temp)
                notableMove = true;
        }
                
        foreach (Projectile proj in Main.ActiveProjectiles)
        {
            if (proj.velocity == Vector2.Zero)
                continue;

            bool temp = MoveObjectBasedOnEntity(obj, proj, dampening, cap, isChain);
            if (temp)
                notableMove = true;
        }
                
        return notableMove;
    }

    public static bool MoveObjectBasedOnEntity(VerletObject obj, Entity e, float dampening = 0.425f, float cap = 5f, bool isChain = true)
    {
        Vector2 entityVelocity = (e.velocity * dampening).ClampMagnitude(0f, cap);
        bool velocityAtCap = entityVelocity.Length() >= cap;
        bool notableMove = false;

        if (!obj.Positions.Any(p => e.Hitbox.Contains(p.ToPoint())))
            return false;


        for (int i = 0; i < obj.Points.Count; i++)
        {
            VerletPoint segment = obj[i];

            if (segment.Locked)
                continue;

            if (isChain)
            {
                bool mainSegmentHit = false;

                for (int j = 0; j < segment.Connections.Count; j++)
                {
                    if (segment.Connections[j].Length == -1)
                        continue;

                    float distanceToEntity = e.Distance(segment.Position);
                    if (distanceToEntity > 48f) // Adjust threshold as needed
                        continue;

                    VerletPoint next = segment.Connections[j].Point;
                    float _ = 0f;
                    if (Collision.CheckAABBvLineCollision(e.TopLeft, e.Size, segment.Position, next.Position, 8f, ref _))
                    {
                        // Weigh the entity's distance between the two segments.
                        float distanceBetweenSegments = segment.Position.Distance(next.Position);
                        float currentMovementOffsetInterpolant = Utils.GetLerpValue(distanceToEntity, distanceBetweenSegments, distanceBetweenSegments * 0.2f, true);
                        float nextMovementOffsetInterpolant = 1f - currentMovementOffsetInterpolant;

                        // Move the segments based on the weight values.
                        if (!mainSegmentHit)
                        {
                            segment.Position += entityVelocity * currentMovementOffsetInterpolant;
                            mainSegmentHit = true;
                            if(velocityAtCap)
                                notableMove = true;
                        }
                        if (!next.Locked)
                            next.Position += entityVelocity * nextMovementOffsetInterpolant;
                    }
                }
            }
            else
            {
                for (int j = 0; j < segment.Connections.Count; j++)
                {
                    if (segment.Connections[j].Length == -1)
                        continue;

                    VerletPoint next = segment.Connections[j].Point;

                    float _ = 0f;
                    if (Collision.CheckAABBvLineCollision(e.TopLeft, e.Size, segment.Position, next.Position, 20f, ref _))
                    {
                        segment.Position += entityVelocity;

                        if (velocityAtCap)
                            notableMove = true;

                        foreach (var conn in segment.Connections)
                            conn.Point.Position += entityVelocity;

                        return notableMove;
                    }
                }
            }
        }

        return notableMove;
    }
}
