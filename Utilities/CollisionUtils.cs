using Microsoft.Xna.Framework;
using System;
using Terraria;

namespace AAModClassic.Utilities;

public static class CollisionUtils
{
    public static Vector2? RayCast(Vector2 startPosition, Vector2 rayDirection, float maxDist, out float distanceMoved)
    {
        distanceMoved = 0f;
        float dirX = rayDirection.X;
        float dirY = rayDirection.Y;

        float tDeltaX = (Math.Abs(dirX) > 1e-12f) ? 16f / Math.Abs(dirX) : float.MaxValue;
        float tDeltaY = (Math.Abs(dirY) > 1e-12f) ? 16f / Math.Abs(dirY) : float.MaxValue;

        int cellX = (int)Math.Floor(startPosition.X / 16f);
        int cellY = (int)Math.Floor(startPosition.Y / 16f);

        int stepX = (dirX > 0) ? 1 : (dirX < 0 ? -1 : 0);
        int stepY = (dirY > 0) ? 1 : (dirY < 0 ? -1 : 0);

        float boundaryX = (dirX > 0) ? (cellX + 1) * 16f : cellX * 16f;
        float boundaryY = (dirY > 0) ? (cellY + 1) * 16f : cellY * 16f;

        float tMaxX = (dirX != 0) ? (boundaryX - startPosition.X) / dirX : float.MaxValue;
        float tMaxY = (dirY != 0) ? (boundaryY - startPosition.Y) / dirY : float.MaxValue;

        if (WorldGen.InWorld(cellX, cellY) && WorldGen.SolidTile(new Point(cellX, cellY)))
        {
            distanceMoved = 0f;
            return startPosition;
        }

        float t = 0f;
        while (t < maxDist)
        {
            if (tMaxX < tMaxY)
            {
                cellX += stepX;
                t = tMaxX;
                tMaxX += tDeltaX;
            }
            else
            {
                cellY += stepY;
                t = tMaxY;
                tMaxY += tDeltaY;
            }

            if (WorldGen.InWorld(cellX, cellY) && WorldGen.SolidTile(new Point(cellX, cellY)))
            {
                distanceMoved = t;
                return startPosition + rayDirection * t;
            }
        }

        return null;
    }
}
