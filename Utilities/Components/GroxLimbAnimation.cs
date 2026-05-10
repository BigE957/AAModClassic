using AAModClassic._Content.Mire._PostMoonlord.NPCs.__BossYamata;
using AAModClassic._Content.Mire._PostMoonlord.NPCs.__BossYamata.Awakened;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Utilities.Components
{
    public class AnimationInfo(int type, float aMult = 1f)
    {
        public int animType = type;
        public float movementRatio = 0f, movementRate = 0.01f, animMult = aMult;
        public float halfPI = (float)Math.PI / 2f;
        public bool[] fired = new bool[4];
        public float[] hitRatios = null;
        public bool flatJoint = false;
    }

    public class LimbInfo
    {
        public int limbType = 0;
        public Vector2 position, oldPosition;
        public Vector2 Center
        {
            get { return new Vector2(position.X + Hitbox.Width * 0.5f, position.Y + Hitbox.Height * 0.5f); }
            set { position = new Vector2(value.X - Hitbox.Width * 0.5f, value.Y - Hitbox.Height * 0.5f); }
        }
        public Rectangle Hitbox;
        public float rotation = 0f, movementRatio = 0f;
        public AnimationInfo overrideAnimation = null;
    }

    public class LegInfo : LimbInfo
    {
        Vector2 velocity, legOrigin;
        public float VelOffsetY { get; private set; } = 0f;
        private readonly float distanceToMove = 120f, distanceToMoveX = 50f;
        private readonly bool flying = false;
        public bool leftLeg = false;

        Vector2 pointToStandOn = default;
        public Vector2 LegJoint { get; private set; } = default;
        public static Asset<Texture2D>[] normalTextures = [];
        public static Asset<Texture2D>[] awakenedTextures = [];

        public LegInfo(int lType, Vector2 initialPos, bool awakened)
        {
            position = initialPos;
            pointToStandOn = position;
            limbType = lType;
            Hitbox = awakened ? new Rectangle(0, 0, 140, 76) : new Rectangle(0, 0, 70, 38);
            legOrigin = new Vector2(limbType == 1 || limbType == 3 ? Hitbox.Width - 12 : 12, 12);
        }

        public void MoveLegFlying(NPC npc)
        {
            Vector2 movementSpot = GetBodyConnector(npc) + new Vector2(limbType == 3 ? -35f - Hitbox.Width : limbType == 2 ? 35f : limbType == 1 ? -15f - Hitbox.Width : 15f, limbType == 1 || limbType == 0 ? 40f : 50f);
            float velLength = (npc.position - npc.oldPos[1]).Length();
            if (velLength > 8f)
            {
                position = movementSpot;
                velocity = default;
            }
            else
                if (Vector2.Distance(movementSpot, position) > 40 + (int)npc.velocity.Length())
                {
                    Vector2 velAddon = movementSpot - position; velAddon.Normalize(); velAddon *= 2f + velLength * 0.25f;
                    velocity += velAddon;
                    float velMax = 4f + velLength;
                    if (velocity.Length() > velMax) { velocity.Normalize(); velocity *= velMax; }
                    position += velocity;
                }
                else
                {
                    position = movementSpot;
                    velocity = default;
                }
        }

        public void UpdateVelOffsetY()
        {
            movementRatio += 0.04f;
            movementRatio = Math.Max(0f, Math.Min(1f, movementRatio));
            VelOffsetY = BaseUtility.MultiLerp(movementRatio, 0f, 30f, 0f);
        }

        public void MoveLegWalking(NPC npc, Vector2 standOnPoint)
        {
            UpdateVelOffsetY();
            if (pointToStandOn != default)
            {
                Vector2 velAddon = pointToStandOn - position; velAddon.Normalize(); velAddon *= 1.6f + npc.velocity.Length() * 0.5f;
                velocity += velAddon;
                float velMax = 4f + npc.velocity.Length();
                if (velocity.Length() > velMax) { velocity.Normalize(); velocity *= velMax; }
                if (Vector2.Distance(pointToStandOn, position) <= 15) { position = pointToStandOn; velocity = default; }
                position += velocity;
                if (position == pointToStandOn || Vector2.Distance(standOnPoint, position + new Vector2(Hitbox.Width * 0.5f, 0f)) > distanceToMove || Math.Abs(position.X - standOnPoint.X) > distanceToMoveX)
                {
                    pointToStandOn = default;
                }
            }
            if (pointToStandOn == default)
            {
                if (Vector2.Distance(standOnPoint, position + new Vector2(Hitbox.Width * 0.5f, 0f)) > distanceToMove || Math.Abs(position.X - standOnPoint.X) > distanceToMoveX)
                {
                    movementRatio = 0f;
                    pointToStandOn = standOnPoint;
                }
            }
        }

        public void UpdateLeg(NPC npc)
        {
            leftLeg = limbType == 1 || limbType == 3;
            if (Vector2.Distance(Center, npc.Center) > 499 || npc.ModNPC is YamataBody && YamataBody.TeleportMeBitch || npc.ModNPC is YamataABody && YamataABody.TeleportMeBitch) position = npc.Center; //prevent issues when the legs are WAY off.
            if (overrideAnimation != null)
            {
                if (overrideAnimation.movementRatio >= 1f) overrideAnimation = null;
            }
            else
            {
                rotation = 0f;
                Vector2 standOnPoint = GetStandOnPoint(npc);
                if (standOnPoint == default) //'flying' behavior but per leg
                {
                    MoveLegFlying(npc);
                }
                else
                {
                    MoveLegWalking(npc, standOnPoint);
                }
            }
            Vector2 bodyConnector = GetBodyConnector(npc);
            LegJoint = Vector2.Lerp(position, bodyConnector, 0.3f) + new Vector2(leftLeg ? 30 : 0f, -30);
            oldPosition = position;
        }

        public Vector2 GetStandOnPoint(NPC npc)
        {
            float scalar = npc.velocity.Length();
            float outerLegDefault = (npc.ModNPC is YamataABody ? 150f : 70f) + 0.5f * scalar;
            float innerLegDefault = (npc.ModNPC is YamataABody ? 120f : 50f) + 0.5f * scalar;
            float standOnX = npc.Center.X + (npc.ModNPC is YamataABody yamataA ? yamataA.topVisualOffset.X : 0) + (limbType == 3 ? -outerLegDefault - Hitbox.Width : limbType == 2 ? outerLegDefault + Hitbox.Width : limbType == 1 ? -innerLegDefault - Hitbox.Width : innerLegDefault + Hitbox.Width);

            int defaultTileY = (int)(npc.Bottom.Y / 16f);
            int tileY = WorldGenUtils.GetFirstTileFloor((int)(standOnX / 16f), (int)(npc.Bottom.Y / 16f));
            if (tileY - defaultTileY > (npc.ModNPC is YamataABody ? YamataABody.flyingTileCount : YamataBody.flyingTileCount)) { return default; } //'flying' behavior
            if (!flying)
            {
                tileY = (int)(tileY * 16f) / 16;
                float tilePosY = tileY * 16f;
                if (Main.tile[(int)(standOnX / 16f), tileY] == null || !Main.tile[(int)(standOnX / 16f), tileY].HasUnactuatedTile || !Main.tileSolid[Main.tile[(int)(standOnX / 16f), tileY].TileType]) tilePosY += 16f;
                return new Vector2(standOnX - Hitbox.Width * 0.5f, tilePosY - Hitbox.Height);
            }
            return default;
        }

        public Vector2 GetBodyConnector(NPC npc) => npc.Center + (npc.ModNPC is YamataABody yamataA ? yamataA.topVisualOffset : Vector2.Zero) + new Vector2(limbType == 3 || limbType == 1 ? -40f : 40f, 0f);

        /*
        public void DrawLeg(SpriteBatch sb, NPC npc)
        {
            Vector2 drawPos = position - new Vector2(0f, VelOffsetY);
            Color lightColor = npc.GetAlpha(BaseDrawing.GetLightColor(Center));
            bool awakened = npc.type == ModContent.NPCType<YamataABody>();
            Asset<Texture2D>[] textures = awakened ? awakenedTextures : normalTextures;
            if (!leftLeg)
            {
                BaseDrawing.DrawChain(sb, new Texture2D[] { null, textures[3].Value, null }, 0, drawPos + new Vector2(Hitbox.Width * 0.5f, 6f), LegJoint, 0f, null, 1f, false, null);
                BaseDrawing.DrawChain(sb, new Texture2D[] { textures[2].Value, textures[3].Value, textures[2].Value }, 0, LegJoint, GetBodyConnector(npc), 0f, null, 1f, false, null);
            }
            else
            {
                BaseDrawing.DrawChain(sb, new Texture2D[] { null, textures[1].Value, null }, 0, drawPos + new Vector2(Hitbox.Width * 0.5f, 6f), LegJoint, 0f, null, 1f, false, null);
                BaseDrawing.DrawChain(sb, new Texture2D[] { textures[0].Value, textures[1].Value, textures[0].Value }, 0, LegJoint, GetBodyConnector(npc), 0f, null, 1f, false, null);
            }
            BaseDrawing.DrawTexture(sb, textures[4].Value, 0, drawPos, Hitbox.Width, Hitbox.Height, npc.scale, rotation, limbType == 1 || limbType == 3 ? 1 : -1, 1, Hitbox, lightColor, false, legOrigin);
        }
        */
    }

}
