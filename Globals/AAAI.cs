
using Microsoft.Xna.Framework;
using Terraria.Audio;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Microsoft.Xna.Framework.Graphics;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Utilities;

namespace AAModClassic.Globals
{
    public class AAAI
	{
        public static AAPlayer modPlayer = Main.LocalPlayer.GetModPlayer<AAPlayer>();
        public static void InfernoFighterAI(NPC npc, ref float[] ai, bool fleeWhenNight = true, bool allowBoredom = true, int openDoors = 1, float moveInterval = 0.07f, float velMax = 1f, int maxJumpTilesX = 3, int maxJumpTilesY = 4, int ticksUntilBoredom = 60, bool targetPlayers = true, int doorBeatCounterMax = 10, int doorCounterMax = 60, bool jumpUpPlatforms = false, Action<bool, bool, Vector2, Vector2> onTileCollide = null, bool ignoreJumpTiles = false)
        {
            bool xVelocityChanged = false;
            //This block of code checks for major X velocity/directional changes as well as periodically updates the npc.
            if (npc.velocity.Y == 0f && ((npc.velocity.X > 0f && npc.direction < 0) || (npc.velocity.X < 0f && npc.direction > 0)))
            {
                xVelocityChanged = true;
            }
            if (npc.position.X == npc.oldPosition.X || ai[3] >= ticksUntilBoredom || xVelocityChanged)
            {
                ai[3] += 1f;
            }
            else
            if (Math.Abs(npc.velocity.X) > 0.9 && ai[3] > 0f) { ai[3] -= 1f; }
            if (ai[3] > ticksUntilBoredom * 10) { ai[3] = 0f; }
            if (npc.justHit) { ai[3] = 0f; }
            if (ai[3] == ticksUntilBoredom) { npc.netUpdate = true; }

            bool notBored = ai[3] < ticksUntilBoredom;
            //if npc does not flee when it's day, if is night, or npc is not on the surface and it hasn't updated projectile pass, update target.
            if (targetPlayers && (!fleeWhenNight || Main.dayTime || npc.position.Y > Main.worldSurface * 16.0) && (fleeWhenNight && Main.dayTime ? notBored : (!allowBoredom || notBored)))
            {
                npc.TargetClosest(true);
            }
            else
            if (ai[2] <= 0f)//if 'bored'
            {
                if (fleeWhenNight && !Main.dayTime && npc.position.Y / 16f < Main.worldSurface && npc.timeLeft > 10)
                {
                    npc.timeLeft = 10;
                }
                if (npc.velocity.X == 0f)
                {
                    if (npc.velocity.Y == 0f)
                    {
                        ai[0] += 1f;
                        if (ai[0] >= 2f)
                        {
                            npc.direction *= -1;
                            npc.spriteDirection = npc.direction;
                            ai[0] = 0f;
                        }
                    }
                }
                else { ai[0] = 0f; }
                if (npc.direction == 0) { npc.direction = 1; }
            }
            //if velocity is less than -1 or greater than 1...
            if (npc.velocity.X < -velMax || npc.velocity.X > velMax)
            {
                //...and npc is not falling or jumping, slow down x velocity.
                if (npc.velocity.Y == 0f) { npc.velocity *= 0.8f; }
            }
            else
            if (npc.velocity.X < velMax && npc.direction == 1) //handles movement to the right. Clamps at velMaxX.
            {
                npc.velocity.X += moveInterval;
                if (npc.velocity.X > velMax) { npc.velocity.X = velMax; }
            }
            else
            if (npc.velocity.X > -velMax && npc.direction == -1) //handles movement to the left. Clamps at -velMaxX.
            {
                npc.velocity.X -= moveInterval;
                if (npc.velocity.X < -velMax) { npc.velocity.X = -velMax; }
            }
            BaseAI.WalkupHalfBricks(npc);
            //if allowed to open doors and is currently doing so, reduce npc velocity on the X axis to 0. (so it stops moving)
            if (openDoors != -1 && BaseAI.AttemptOpenDoor(npc, ref ai[1], ref ai[2], ref ai[3], ticksUntilBoredom, doorBeatCounterMax, doorCounterMax, openDoors))
            {
                npc.velocity.X = 0;
            }
            else //if no door to open, reset ai.
            if (openDoors != -1) { ai[1] = 0f; ai[2] = 0f; }
            //if there's a solid floor under us...
            if (BaseAI.HitTileOnSide(npc, 3))
            {
                //if the npc's velocity is going in the same direction as the npc's direction...
                if ((npc.velocity.X < 0f && npc.direction == -1) || (npc.velocity.X > 0f && npc.direction == 1))
                {
                    //...attempt to jump if needed.
                    Vector2 newVec = BaseAI.AttemptJump(npc.position, npc.velocity, npc.width, npc.height, npc.direction, npc.directionY, maxJumpTilesX, maxJumpTilesY, velMax, jumpUpPlatforms, jumpUpPlatforms && notBored ? Main.player[npc.target] : null, ignoreJumpTiles);
                    if (!npc.noTileCollide)
                    {
                        newVec = Collision.TileCollision(npc.position, newVec, npc.width, npc.height);
                        Vector4 slopeVec = Collision.SlopeCollision(npc.position, newVec, npc.width, npc.height);
                        Vector2 slopeVel = new Vector2(slopeVec.Z, slopeVec.W);
                        if (onTileCollide != null && npc.velocity != slopeVel) onTileCollide(npc.velocity.X != slopeVel.X, npc.velocity.Y != slopeVel.Y, npc.velocity, slopeVel);
                        npc.position = new Vector2(slopeVec.X, slopeVec.Y);
                        npc.velocity = slopeVel;
                    }
                    if (npc.velocity != newVec) { npc.velocity = newVec; npc.netUpdate = true; }
                }
            }
        }

        /*
		 * A method that slowly lowers an NPC's alpha while spawning dust to give a bursting into existence look. Works great with worms.
		 *
		 * DustType: What dust is used
         * DustFrequency: How much dust is spawned
		 * AlphaRate: How fast the npc lowers alpha. The higher it is, the faster the alpha drops
		 */

        public static void DustOnNPCSpawn(NPC npc, int DustType = 1, int DustFrequency = 1, int AlphaRate = 1)
        {
            if (npc.alpha != 0)
            {
                for (int spawnDust = 0; spawnDust < DustFrequency; spawnDust++)
                {
                    int num935 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, DustType, 0f, 0f, 100, default, 2f);
                    Main.dust[num935].noGravity = true;
                    Main.dust[num935].noLight = true;
                }
            }
            npc.alpha -= AlphaRate;
            if (npc.alpha < 0)
            {
                npc.alpha = 0;
            }
        }

        /*
		 * A method that's good for flamethrower-like projectiles
		 *
		 * ProjectileType: What Projectile is used
         * UseNPCVelocity: Uses the npc's velocity for speed instead of the player's position. Good for worms.
         * SpeedBoost: How much faster the projectile should be (1 = Flamethrower/Yamata)
		 * DamageReduction: How much to divide damage by to prevent one-shotting projectiles
		 */

        public static void BreatheFire(NPC npc, bool UseNPCVelocity = false, int ProjectileType = 85, float SpeedBoost = 1, float DamageReduction = 2)
        {
            int num429 = 1;
            if (npc.position.X + (npc.width / 2) < Main.player[npc.target].position.X + Main.player[npc.target].width)
            {
                num429 = -1;
            }
            Vector2 Origin = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
            float PlayerPosX = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2) + (num429 * 180) - Origin.X;
            float PlayerPosY = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - Origin.Y;
            float PlayerPos = (float)Math.Sqrt((PlayerPosX * PlayerPosX) + (PlayerPosY * PlayerPosY));
            float num433 = 6f;
            PlayerPosX = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2) - Origin.X;
            PlayerPosY = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - Origin.Y;
            PlayerPos = (float)Math.Sqrt(PlayerPosX * PlayerPosX + PlayerPosY * PlayerPosY);
            PlayerPos = num433 / PlayerPos;
            PlayerPosX *= PlayerPos;
            PlayerPosY *= PlayerPos;
            PlayerPosY += Main.rand.Next(-40, 41) * 0.01f;
            PlayerPosX += Main.rand.Next(-40, 41) * 0.01f;
            PlayerPosY += npc.velocity.Y * 0.5f;
            PlayerPosX += npc.velocity.X * 0.5f;
            Origin.X -= PlayerPosX * 1f;
            Origin.Y -= PlayerPosY * 1f;
            if (UseNPCVelocity)
            {
                PlayerPosX = npc.velocity.X;
                PlayerPosY = npc.velocity.Y;
            }
            Projectile.NewProjectile(npc.GetSource_FromThis(), Origin.X, Origin.Y, PlayerPosX * SpeedBoost, PlayerPosY * SpeedBoost, ProjectileType, (int)(npc.damage / DamageReduction), 0f, Main.myPlayer);
        }

        public static void AIShadowflameGhost(NPC npc, ref float[] ai, bool speedupOverTime = false, float distanceBeforeTakeoff = 660f, float velIntervalX = 0.3f, float velMaxX = 7f, float velIntervalY = 0.2f, float velMaxY = 4f, float velScalarY = 4f, float velScalarYMax = 15f, float velIntervalXTurn = 0.4f, float velIntervalYTurn = 0.4f, float velIntervalScalar = 0.95f, float velIntervalMaxTurn = 5f)
        {
            int npcAvoidCollision;
            for (int m = 0; m < 200; m = npcAvoidCollision + 1)
            {
                if (m != npc.whoAmI && Main.npc[m].active && Main.npc[m].type == npc.type)
                {
                    Vector2 dist = Main.npc[m].Center - npc.Center;
                    if (dist.Length() < 50f)
                    {
                        dist.Normalize();
                        if (dist.X == 0f && dist.Y == 0f)
                        {
                            if (m > npc.whoAmI)
                                dist.X = 1f;
                            else
                                dist.X = -1f;
                        }
                        dist *= 0.4f;
                        npc.velocity -= dist;
                        Main.npc[m].velocity += dist;
                    }
                }
                npcAvoidCollision = m;
            }
            if (speedupOverTime)
            {
                float timerMax = 120f;
                if (npc.localAI[0] < timerMax)
                {
                    if (npc.localAI[0] == 0f)
                    {
                        SoundEngine.PlaySound(SoundID.Item8, npc.Center);
                        npc.TargetClosest(true);
                        if (npc.direction > 0)
                        {
                            npc.velocity.X += 2f;
                        }
                        else
                        {
                            npc.velocity.X -= 2f;
                        }
                        for (int m = 0; m < 20; m = npcAvoidCollision + 1)
                        {
                            npcAvoidCollision = m;
                        }
                    }
                    npc.localAI[0] += 1f;
                    float timerPartial = 1f - npc.localAI[0] / timerMax;
                    float timerPartialTimes20 = timerPartial * 20f;
                    int nextNPC = 0;
                    while (nextNPC < timerPartialTimes20)
                    {
                        npcAvoidCollision = nextNPC;
                        nextNPC = npcAvoidCollision + 1;
                    }
                }
            }
            if (ai[0] == 0f)
            {
                npc.TargetClosest(true);
                ai[0] = 1f;
                ai[1] = npc.direction;
            }
            else if (ai[0] == 1f)
            {
                npc.TargetClosest(true);
                npc.velocity.X += ai[1] * velIntervalX;

                if (npc.velocity.X > velMaxX)
                    npc.velocity.X = velMaxX;
                else if (npc.velocity.X < -velMaxX)
                    npc.velocity.X = -velMaxX;

                float playerDistY = Main.player[npc.target].Center.Y - npc.Center.Y;
                if (Math.Abs(playerDistY) > velMaxY)
                    velScalarY = velScalarYMax;

                if (playerDistY > velMaxY)
                    playerDistY = velMaxY;
                else if (playerDistY < -velMaxY)
                    playerDistY = -velMaxY;

                npc.velocity.Y = (npc.velocity.Y * (velScalarY - 1f) + playerDistY) / velScalarY;
                if ((ai[1] > 0f && Main.player[npc.target].Center.X - npc.Center.X < -distanceBeforeTakeoff) || (ai[1] < 0f && Main.player[npc.target].Center.X - npc.Center.X > distanceBeforeTakeoff))
                {
                    ai[0] = 2f;
                    ai[1] = 0f;
                    if (npc.Center.Y + 20f > Main.player[npc.target].Center.Y)
                        ai[1] = -1f;
                    else
                        ai[1] = 1f;
                }
            }
            else if (ai[0] == 2f)
            {
                npc.velocity.Y += ai[1] * velIntervalYTurn;

                if (npc.velocity.Length() > velIntervalMaxTurn)
                    npc.velocity *= velIntervalScalar;

                if (npc.velocity.X > -1f && npc.velocity.X < 1f)
                {
                    npc.TargetClosest(true);
                    ai[0] = 3f;
                    ai[1] = npc.direction;
                }
            }
            else if (ai[0] == 3f)
            {
                npc.velocity.X += ai[1] * velIntervalXTurn;

                if (npc.Center.Y > Main.player[npc.target].Center.Y)
                    npc.velocity.Y -= velIntervalY;
                else
                    npc.velocity.Y += velIntervalY;

                if (npc.velocity.Length() > velIntervalMaxTurn)
                    npc.velocity *= velIntervalScalar;

                if (npc.velocity.Y > -1f && npc.velocity.Y < 1f)
                {
                    npc.TargetClosest(true);
                    ai[0] = 0f;
                    ai[1] = npc.direction;
                }
            }
        }

        public static void AIClaw (NPC npc, ref float[] ai, bool isDragonClaw = true, bool ignoreWet = false, float moveIntervalX = 0.1f, float moveIntervalY = 0.04f, float velMaxX = 4f, float velMaxY = 1.5f, float bounceScalarX = 1f, float bounceScalarY = 1f)
        {
            //controls the npc's bouncing when it hits a wall.
            if (npc.collideX)
            {
                npc.velocity.X = npc.oldVelocity.X * -0.5f;
                if (npc.direction == -1 && npc.velocity.X > 0f && npc.velocity.X < 2f) { npc.velocity.X = 2f; }
                if (npc.direction == 1 && npc.velocity.X < 0f && npc.velocity.X > -2f) { npc.velocity.X = -2f; }
                npc.velocity.X *= bounceScalarX;
            }
            //controls the npc's bouncing when it hits a floor or ceiling.
            if (npc.collideY)
            {
                npc.velocity.Y = npc.oldVelocity.Y * -0.5f;
                if (npc.velocity.Y > 0f && npc.velocity.Y < 1f) { npc.velocity.Y = 1f; }
                if (npc.velocity.Y < 0f && npc.velocity.Y > -1f) { npc.velocity.Y = -1f; }
                npc.velocity.Y *= bounceScalarY;
            }
            //if it should flee when it's day, and it is day, the npc's position is at or above the surface, it will flee.
            if (((isDragonClaw && !modPlayer.ZoneInferno && Main.dayTime) || (!isDragonClaw && Main.dayTime)) && npc.position.Y <= Main.worldSurface * 16.0)
            {
                if (npc.timeLeft > 10) { npc.timeLeft = 10; }
                npc.directionY = -1;
                if (npc.velocity.Y > 0f) { npc.direction = 1; }
                npc.direction = -1;
                if (npc.velocity.X > 0f) { npc.direction = 1; }
            }
            else
            {
                npc.TargetClosest(true);
                if (Main.player[npc.target].dead)
                {
                    if (npc.timeLeft > 10) { npc.timeLeft = 10; }
                    npc.directionY = -1;
                    if (npc.velocity.Y > 0f) { npc.direction = 1; }
                    npc.direction = -1;
                    if (npc.velocity.X > 0f) { npc.direction = 1; }
                }
            }
            //controls momentum when going left, and clamps velocity at -velMaxX.
            if (npc.direction == -1 && npc.velocity.X > -velMaxX)
            {
                npc.velocity.X -= moveIntervalX;
                if (npc.velocity.X > 4f) { npc.velocity.X -= 0.1f; }
                else
                    if (npc.velocity.X > 0f) { npc.velocity.X += 0.05f; }
                if (npc.velocity.X < -4f) { npc.velocity.X = -velMaxX; }
            }
            else //controls momentum when going right on the x axis and clamps velocity at velMaxX.
                if (npc.direction == 1 && npc.velocity.X < velMaxX)
            {
                npc.velocity.X += moveIntervalX;
                if (npc.velocity.X < -velMaxX) { npc.velocity.X += 0.1f; }
                else
                    if (npc.velocity.X < 0f) { npc.velocity.X -= 0.05f; }

                if (npc.velocity.X > velMaxX) { npc.velocity.X = velMaxX; }
            }
            //controls momentum when going up on the Y axis and clamps velocity at -velMaxY.
            if (npc.directionY == -1 && (double)npc.velocity.Y > -velMaxY)
            {
                npc.velocity.Y -= moveIntervalY;
                if ((double)npc.velocity.Y > velMaxY) { npc.velocity.Y -= 0.05f; }
                else
                    if (npc.velocity.Y > 0f) { npc.velocity.Y += 0.03f; }

                if ((double)npc.velocity.Y < -velMaxY) { npc.velocity.Y = -velMaxY; }
            }
            else //controls momentum when going down on the Y axis and clamps velocity at velMaxY.
                if (npc.directionY == 1 && (double)npc.velocity.Y < velMaxY)
            {
                npc.velocity.Y += moveIntervalY;
                if ((double)npc.velocity.Y < -velMaxY) { npc.velocity.Y += 0.05f; }
                else
                    if (npc.velocity.Y < 0f) { npc.velocity.Y -= 0.03f; }

                if ((double)npc.velocity.Y > velMaxY) { npc.velocity.Y = velMaxY; }
            }
            if (!ignoreWet && npc.wet) //if don't ignore being wet and is wet, accelerate upwards to get out.
            {
                if (npc.velocity.Y > 0f) { npc.velocity.Y *= 0.95f; }
                npc.velocity.Y -= 0.5f;
                if (npc.velocity.Y < -velMaxY * 1.5f) { npc.velocity.Y = -velMaxY * 1.5f; }
                npc.TargetClosest(true);
                return;
            }
        }

        public static void CorruptFighterAI(NPC npc, ref float[] ai, bool allowBoredom = true, int openDoors = 1, float moveInterval = 0.07f, float velMax = 1f, int maxJumpTilesX = 3, int maxJumpTilesY = 4, int ticksUntilBoredom = 60, bool targetPlayers = true, int doorBeatCounterMax = 10, int doorCounterMax = 60, bool jumpUpPlatforms = false, Action<bool, bool, Vector2, Vector2> onTileCollide = null, bool ignoreJumpTiles = false)
        {
            bool xVelocityChanged = false;
            Player player = Main.player[npc.target];
            //This block of code checks for major X velocity/directional changes as well as periodically updates the npc.
            if (npc.velocity.Y == 0f && ((npc.velocity.X > 0f && npc.direction < 0) || (npc.velocity.X < 0f && npc.direction > 0)))
            {
                xVelocityChanged = true;
            }
            if (npc.position.X == npc.oldPosition.X || ai[3] >= ticksUntilBoredom || xVelocityChanged)
            {
                ai[3] += 1f;
            }
            else
            if (Math.Abs(npc.velocity.X) > 0.9 && ai[3] > 0f) { ai[3] -= 1f; }
            if (ai[3] > ticksUntilBoredom * 10) { ai[3] = 0f; }
            if (npc.justHit) { ai[3] = 0f; }
            if (ai[3] == ticksUntilBoredom) { npc.netUpdate = true; }

            bool notBored = ai[3] < ticksUntilBoredom;
            //if npc does not flee when it's day, if is night, or npc is not on the surface and it hasn't updated projectile pass, update target.
            if (targetPlayers && (player.ZoneCorrupt || npc.position.Y > Main.worldSurface * 16.0) && (Main.dayTime ? notBored : (!allowBoredom || notBored)))
            {
                npc.TargetClosest(true);
            }
            else
            if (ai[2] <= 0f)//if 'bored'
            {
                if (!player.ZoneCorrupt && npc.position.Y / 16f < Main.worldSurface && npc.timeLeft > 10)
                {
                    npc.timeLeft = 10;
                }
                if (npc.velocity.X == 0f)
                {
                    if (npc.velocity.Y == 0f)
                    {
                        ai[0] += 1f;
                        if (ai[0] >= 2f)
                        {
                            npc.direction *= -1;
                            npc.spriteDirection = npc.direction;
                            ai[0] = 0f;
                        }
                    }
                }
                else { ai[0] = 0f; }
                if (npc.direction == 0) { npc.direction = 1; }
            }
            //if velocity is less than -1 or greater than 1...
            if (npc.velocity.X < -velMax || npc.velocity.X > velMax)
            {
                //...and npc is not falling or jumping, slow down x velocity.
                if (npc.velocity.Y == 0f) { npc.velocity *= 0.8f; }
            }
            else
            if (npc.velocity.X < velMax && npc.direction == 1) //handles movement to the right. Clamps at velMaxX.
            {
                npc.velocity.X += moveInterval;
                if (npc.velocity.X > velMax) { npc.velocity.X = velMax; }
            }
            else
            if (npc.velocity.X > -velMax && npc.direction == -1) //handles movement to the left. Clamps at -velMaxX.
            {
                npc.velocity.X -= moveInterval;
                if (npc.velocity.X < -velMax) { npc.velocity.X = -velMax; }
            }
            BaseAI.WalkupHalfBricks(npc);
            //if allowed to open doors and is currently doing so, reduce npc velocity on the X axis to 0. (so it stops moving)
            if (openDoors != -1 && BaseAI.AttemptOpenDoor(npc, ref ai[1], ref ai[2], ref ai[3], ticksUntilBoredom, doorBeatCounterMax, doorCounterMax, openDoors))
            {
                npc.velocity.X = 0;
            }
            else //if no door to open, reset ai.
            if (openDoors != -1) { ai[1] = 0f; ai[2] = 0f; }
            //if there's a solid floor under us...
            if (BaseAI.HitTileOnSide(npc, 3))
            {
                //if the npc's velocity is going in the same direction as the npc's direction...
                if ((npc.velocity.X < 0f && npc.direction == -1) || (npc.velocity.X > 0f && npc.direction == 1))
                {
                    //...attempt to jump if needed.
                    Vector2 newVec = BaseAI.AttemptJump(npc.position, npc.velocity, npc.width, npc.height, npc.direction, npc.directionY, maxJumpTilesX, maxJumpTilesY, velMax, jumpUpPlatforms, jumpUpPlatforms && notBored ? Main.player[npc.target] : null, ignoreJumpTiles);
                    if (!npc.noTileCollide)
                    {
                        newVec = Collision.TileCollision(npc.position, newVec, npc.width, npc.height);
                        Vector4 slopeVec = Collision.SlopeCollision(npc.position, newVec, npc.width, npc.height);
                        Vector2 slopeVel = new Vector2(slopeVec.Z, slopeVec.W);
                        if (onTileCollide != null && npc.velocity != slopeVel) onTileCollide(npc.velocity.X != slopeVel.X, npc.velocity.Y != slopeVel.Y, npc.velocity, slopeVel);
                        npc.position = new Vector2(slopeVec.X, slopeVec.Y);
                        npc.velocity = slopeVel;
                    }
                    if (npc.velocity != newVec) { npc.velocity = newVec; npc.netUpdate = true; }
                }
            }
        }

        public static void GripAI(NPC npc, ref float[] ai, float moveIntervalX = 0.1f, float moveIntervalY = 0.04f, float velMaxX = 4f, float velMaxY = 1.5f)
        {
            //if it should flee when it's day, and it is day, the npc's position is at or above the surface, it will flee.
            if (Main.dayTime && npc.position.Y <= Main.worldSurface * 16.0)
            {
                if (npc.timeLeft > 10) { npc.timeLeft = 10; }
                npc.directionY = -1;
                if (npc.velocity.Y > 0f) { npc.direction = 1; }
                npc.direction = -1;
                if (npc.velocity.X > 0f) { npc.direction = 1; }
            }
            else
            {
                npc.TargetClosest(true);
                if (Main.player[npc.target].dead)
                {
                    if (npc.timeLeft > 10) { npc.timeLeft = 10; }
                    npc.directionY = -1;
                    if (npc.velocity.Y > 0f) { npc.direction = 1; }
                    npc.direction = -1;
                    if (npc.velocity.X > 0f) { npc.direction = 1; }
                }
            }
            //controls momentum when going left, and clamps velocity at -velMaxX.
            if (npc.direction == -1 && npc.velocity.X > -velMaxX)
            {
                npc.velocity.X -= moveIntervalX;
                if (npc.velocity.X > 4f) { npc.velocity.X -= 0.1f; }
                else
                    if (npc.velocity.X > 0f) { npc.velocity.X += 0.05f; }
                if (npc.velocity.X < -4f) { npc.velocity.X = -velMaxX; }
            }
            else //controls momentum when going right on the x axis and clamps velocity at velMaxX.
            if (npc.direction == 1 && npc.velocity.X < velMaxX)
            {
                npc.velocity.X += moveIntervalX;
                if (npc.velocity.X < -velMaxX) { npc.velocity.X += 0.1f; }
                else
                    if (npc.velocity.X < 0f) { npc.velocity.X -= 0.05f; }

                if (npc.velocity.X > velMaxX) { npc.velocity.X = velMaxX; }
            }
            //controls momentum when going up on the Y axis and clamps velocity at -velMaxY.
            if (npc.directionY == -1 && (double)npc.velocity.Y > -velMaxY)
            {
                npc.velocity.Y -= moveIntervalY;
                if ((double)npc.velocity.Y > velMaxY) { npc.velocity.Y -= 0.05f; }
                else
                    if (npc.velocity.Y > 0f) { npc.velocity.Y += 0.03f; }

                if ((double)npc.velocity.Y < -velMaxY) { npc.velocity.Y = -velMaxY; }
            }
            else //controls momentum when going down on the Y axis and clamps velocity at velMaxY.
            if (npc.directionY == 1 && (double)npc.velocity.Y < velMaxY)
            {
                npc.velocity.Y += moveIntervalY;
                if ((double)npc.velocity.Y < -velMaxY) { npc.velocity.Y += 0.05f; }
                else
                    if (npc.velocity.Y < 0f) { npc.velocity.Y -= 0.03f; }

                if ((double)npc.velocity.Y > velMaxY) { npc.velocity.Y = velMaxY; }
            }
        }

        public static void AIWorm(NPC npc, int[] wormTypes, int wormLength = 3, float partDistanceAddon = 0f, float maxSpeed = 8f, float gravityResist = 0.07f, bool fly = false, bool split = false, bool ignoreTiles = false, bool spawnTileDust = true, bool soundEffects = true, bool rotateAverage = false, Action<NPC, int, bool> onChangeType = null)
        {
			bool diggingDummy = false;
			AIWorm(npc, ref diggingDummy, wormTypes, wormLength, partDistanceAddon, maxSpeed, gravityResist, fly, split, ignoreTiles, spawnTileDust, soundEffects, rotateAverage, onChangeType);
		}

        public static void AIWorm(NPC npc, ref bool isDigging, int[] wormTypes, int wormLength = 3, float partDistanceAddon = 0f, float maxSpeed = 8f, float gravityResist = 0.07f, bool fly = false, bool split = false, bool ignoreTiles = false, bool spawnTileDust = true, bool soundEffects = true, bool rotateAverage = false, Action<NPC, int, bool> onChangeType = null)
        {
            int[] wtypes = new int[(wormTypes.Length == 1 ? 1 : wormLength)];
            wtypes[0] = wormTypes[0];
			if (wormTypes.Length > 1)
			{
				wtypes[wtypes.Length - 1] = wormTypes[2];
				for (int m = 1; m < wtypes.Length - 1; m++)
				{
					wtypes[m] = wormTypes[1];
				}
			}
            AIWorm(npc, ref isDigging, wtypes, partDistanceAddon, maxSpeed, gravityResist, fly, split, ignoreTiles, spawnTileDust, soundEffects, rotateAverage, onChangeType);
        }

		public static void AIWorm(NPC npc, int[] wormTypes, float partDistanceAddon = 0f, float maxSpeed = 8f, float gravityResist = 0.07f, bool fly = false, bool split = false, bool ignoreTiles = false, bool spawnTileDust = true, bool soundEffects = true, bool rotateAverage = false, Action<NPC, int, bool> onChangeType = null)
        {
			bool diggingDummy = false;
			AIWorm(npc, ref diggingDummy, wormTypes, partDistanceAddon, maxSpeed, gravityResist, fly, split, ignoreTiles, spawnTileDust, soundEffects, rotateAverage, onChangeType);
		}

        public static void AIWorm(NPC npc, ref bool isDigging, int[] wormTypes, float partDistanceAddon = 0f, float maxSpeed = 8f, float gravityResist = 0.07f, bool fly = false, bool split = false, bool ignoreTiles = false, bool spawnTileDust = true, bool soundEffects = true, bool rotateAverage = false, Action<NPC, int, bool> onChangeType = null)
        {
			bool singlePiece = wormTypes.Length == 1;
			bool isHead = npc.type == wormTypes[0];
			bool isTail = npc.type == wormTypes[wormTypes.Length - 1];
			bool isBody = !isHead && !isTail;
            int wormLength = wormTypes.Length;

            if (split)
            {
                npc.realLife = -1;
            }else
            if (npc.ai[3] >= 0f)
				npc.realLife = (int)npc.ai[3];

			if(npc.ai[0] == -1f)
				npc.ai[0] = npc.whoAmI;
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead)
				npc.TargetClosest(true);
			if(isHead)
			{
				if((npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead) && npc.timeLeft > 300)
					npc.timeLeft = 300;
			}else
			{
				npc.timeLeft = 50;
			}
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
				if (!singlePiece)
				{
					//spawn pieces (flying)
					if (fly && isHead && npc.ai[0] == 0f)
					{
						npc.ai[3] = npc.whoAmI;
						npc.realLife = npc.whoAmI;
						int npcID = npc.whoAmI;
						for (int m = 1; m < wormLength - 1; m++)
						{
							int npcType = wormTypes[m];

							float ai0 = 0;
							float ai1 = npcID;
							float ai2 = 0;
							float ai3 = npc.ai[3];

							int newnpcID = NPC.NewNPC(npc.GetSource_FromThis(), (int)(npc.Center.X), (int)(npc.Center.Y), npcType, npc.whoAmI, ai0, ai1, ai2, ai3);
							Main.npc[npcID].ai[0] = newnpcID;
							Main.npc[npcID].netUpdate = true;
							//Main.npc[newnpcID].ai[3] = (float)npc.whoAmI;
							//Main.npc[newnpcID].realLife = npc.whoAmI;
							//Main.npc[newnpcID].ai[1] = (float)npcID;
							//Main.npc[npcID].ai[0] = (float)newnpcID;
							//Main.npc[newnpcID].netUpdate = true;
							//NetMessage.SendData(23, -1, -1, NetworkText.FromLiteral(""), newnpcID, 0f, 0f, 0f, 0);
							npcID = newnpcID;
						}
					}else //spawn pieces
					if ((isHead || isBody) && npc.ai[0] == 0f)
					{
						if(isHead)
						{
							if (!split)
							{
								npc.ai[3] = npc.whoAmI;
								npc.realLife = npc.whoAmI;
							}
							npc.ai[2] = wormLength - 1;
						}
						float ai0 = 0;
						float ai1 = npc.whoAmI;
						float ai2 = npc.ai[2] - 1f;
						float ai3 = npc.ai[3];
						if(split)
							npc.ai[3] = 0f;

						if (isHead)
						{
							npc.ai[0] = NPC.NewNPC(npc.GetSource_FromThis(), (int)(npc.Center.X), (int)(npc.Center.Y), wormTypes[1], npc.whoAmI, ai0, ai1, ai2, ai3);
						}else
						if (isBody && npc.ai[2] > 0f)
						{
							npc.ai[0] = NPC.NewNPC(npc.GetSource_FromThis(), (int)(npc.Center.X), (int)(npc.Center.Y), wormTypes[wormLength - (int)npc.ai[2]], npc.whoAmI, ai0, ai1, ai2, ai3);
						}else
						{
							npc.ai[0] = NPC.NewNPC(npc.GetSource_FromThis(), (int)(npc.Center.X), (int)(npc.Center.Y), wormTypes[wormTypes.Length - 1], npc.whoAmI, ai0, ai1, ai2, ai3);
						}
						/*if (!split)
						{
							Main.npc[(int)npc.ai[0]].ai[3] = npc.ai[3];
							Main.npc[(int)npc.ai[0]].realLife = npc.realLife;
						}
						Main.npc[(int)npc.ai[0]].ai[1] = (float)npc.whoAmI;
						Main.npc[(int)npc.ai[0]].ai[2] = npc.ai[2] - 1f;*/
					}
				}
				//if npc can split, check if pieces are dead and if so split.
				if (!singlePiece && split)
                {
                    if (!Main.npc[(int)npc.ai[1]].active && !Main.npc[(int)npc.ai[0]].active) //if this is in the middle and both parts are inactive, kill self
                    {
                        npc.life = 0;
                        npc.HitEffect(0, 10.0);
                        npc.active = false;
                    }
                    if (npc.type == wormTypes[0] && !Main.npc[(int)npc.ai[0]].active) //if this is the head and the bottom is inactive, kill self
                    {
                        npc.life = 0;
                        npc.HitEffect(0, 10.0);
                        npc.active = false;
                    }
                    if (npc.type == wormTypes[wormTypes.Length - 1] && !Main.npc[(int)npc.ai[1]].active) //if this is the tail and the top is inactive, kill self
                    {
                        npc.life = 0;
                        npc.HitEffect(0, 10.0);
                        npc.active = false;
                    }
                    if (isBody && !Main.npc[(int)npc.ai[1]].active) //if the body was just split, turn it into a head
                    {
						int oldType = npc.type;
                        npc.type = wormTypes[0];
                        int npcID = npc.whoAmI;
                        float lifePercent = npc.life / (float)npc.lifeMax;
                        float lastPiece = npc.ai[0];
                        npc.SetDefaults(npc.type);
                        npc.life = (int)(npc.lifeMax * lifePercent);
                        npc.ai[0] = lastPiece;
                        npc.TargetClosest(true);
                        npc.netUpdate = true;
                        npc.whoAmI = npcID;
						if(onChangeType != null) onChangeType(npc, oldType, true);
                    }
                    else
					if (isBody && !Main.npc[(int)npc.ai[0]].active) //if the body was just split, turn it into a tail
					{
						int oldType = npc.type;
						npc.type = wormTypes[wormTypes.Length - 1];
						int npcID = npc.whoAmI;
						float lifePercent = npc.life / (float)npc.lifeMax;
						float lastPiece = npc.ai[1];
						npc.SetDefaults(npc.type);
						npc.life = (int)(npc.lifeMax * lifePercent);
						npc.ai[1] = lastPiece;
						npc.TargetClosest(true);
						npc.netUpdate = true;
						npc.whoAmI = npcID;
						if(onChangeType != null) onChangeType(npc, oldType, false);
					}
                }
                else
				if (!singlePiece)
                {
                    if (npc.type != wormTypes[0] && (!Main.npc[(int)npc.ai[1]].active || Main.npc[(int)npc.ai[1]].aiStyle != npc.aiStyle))
                    {
                        npc.life = 0;
                        npc.HitEffect(0, 10.0);
                        npc.active = false;
                    }
                    if (npc.type != wormTypes[wormTypes.Length - 1] && (!Main.npc[(int)npc.ai[0]].active || Main.npc[(int)npc.ai[0]].aiStyle != npc.aiStyle))
                    {
                        npc.life = 0;
                        npc.HitEffect(0, 10.0);
                        npc.active = false;
                    }
                }
                if (!npc.active && Main.netMode == NetmodeID.Server)
					NetMessage.SendData(MessageID.DamageNPC, -1, -1, NetworkText.FromLiteral(""), npc.whoAmI, 1, 0f, 0f, -1);
            }
            int tileX = (int)(npc.position.X / 16f) - 1;
            int tileCenterX = (int)((npc.Center.X) / 16f) + 2;
            int tileY = (int)(npc.position.Y / 16f) - 1;
            int tileCenterY = (int)((npc.Center.Y) / 16f) + 2;
            if (tileX < 0) { tileX = 0; } if (tileCenterX > Main.maxTilesX) { tileCenterX = Main.maxTilesX; }
            if (tileY < 0) { tileY = 0; } if (tileCenterY > Main.maxTilesY) { tileCenterY = Main.maxTilesY; }
            bool canMove = false;
            if (fly || ignoreTiles) { canMove = true; }
            if (!canMove || spawnTileDust)
            {
                for (int tX = tileX; tX < tileCenterX; tX++)
                {
                    for (int tY = tileY; tY < tileCenterY; tY++)
                    {
						Tile checkTile = WorldGenUtils.GetTileSafely(tX, tY);
                        if (checkTile != null && ((checkTile.HasUnactuatedTile && (Main.tileSolid[checkTile.TileType] || (Main.tileSolidTop[checkTile.TileType] && checkTile.TileFrameY == 0))) || checkTile.LiquidAmount > 64))
                        {
                            Vector2 tPos;
                            tPos.X = tX * 16;
                            tPos.Y = tY * 16;
                            if (npc.position.X + npc.width > tPos.X && npc.position.X < tPos.X + 16f && npc.position.Y + npc.height > tPos.Y && npc.position.Y < tPos.Y + 16f)
                            {
                                canMove = true;
                                if (spawnTileDust && Main.rand.NextBool(100) && checkTile.HasUnactuatedTile)
                                {
                                    WorldGen.KillTile(tX, tY, true, true, false);
                                }
                            }
                        }
                    }
                }
            }
            if (!canMove && npc.type == wormTypes[0])
            {
                Rectangle rectangle = new Rectangle((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height);
                int playerCheckDistance = 1000;
                bool canMove2 = true;
                for (int m3 = 0; m3 < 255; m3++)
                {
                    if (Main.player[m3].active)
                    {
                        Rectangle rectangle2 = new Rectangle((int)Main.player[m3].position.X - playerCheckDistance, (int)Main.player[m3].position.Y - playerCheckDistance, playerCheckDistance * 2, playerCheckDistance * 2);
                        if (rectangle.Intersects(rectangle2))
                        {
                            canMove2 = false;
                            break;
                        }
                    }
                }
                if (canMove2) { canMove = true; }
            }
            if (fly)
            {
                if (npc.velocity.X < 0f) { npc.spriteDirection = 1; }else if (npc.velocity.X > 0f) { npc.spriteDirection = -1; }
            }
            Vector2 npcCenter = npc.Center;
            float playerCenterX = Main.player[npc.target].Center.X;
            float playerCenterY = Main.player[npc.target].Center.Y;
            playerCenterX = (int)(playerCenterX / 16f) * 16; playerCenterY = (int)(playerCenterY / 16f) * 16;
            npcCenter.X = (int)(npcCenter.X / 16f) * 16; npcCenter.Y = (int)(npcCenter.Y / 16f) * 16;
            playerCenterX -= npcCenter.X; playerCenterY -= npcCenter.Y;
            float dist = (float)Math.Sqrt(playerCenterX * playerCenterX + playerCenterY * playerCenterY);
			isDigging = canMove;
            if (npc.ai[1] >= 0f && npc.ai[1] < Main.npc.Length && npc.type != wormTypes[0])
            {
                try
                {
                    npcCenter = npc.Center;
                    playerCenterX = Main.npc[(int)npc.ai[1]].Center.X - npcCenter.X;
                    playerCenterY = Main.npc[(int)npc.ai[1]].Center.Y - npcCenter.Y;
                }catch
                {
                }
                if (!rotateAverage || npc.type == wormTypes[0])
                {
                    npc.rotation = (float)Math.Atan2(playerCenterY, playerCenterX) + 1.57f;
                }else
                {
                    NPC frontNPC = Main.npc[(int)npc.ai[1]];
                    Vector2 rotVec = BaseUtility.RotateVector(frontNPC.Center, frontNPC.Center + new Vector2(0f, 30f), frontNPC.rotation);
                    npc.rotation = BaseUtility.RotationTo(npc.Center, rotVec) + 1.57f;
                }
                dist = (float)Math.Sqrt(playerCenterX * playerCenterX + playerCenterY * playerCenterY);
                dist = (dist - npc.width - partDistanceAddon) / dist;
                playerCenterX *= dist;
                playerCenterY *= dist;
                npc.velocity = default(Vector2);
                npc.position.X = npc.position.X + playerCenterX;
                npc.position.Y = npc.position.Y + playerCenterY;
                if (fly)
                {
                    if (playerCenterX < 0f) { npc.spriteDirection = 1; return;  }else
                    if (playerCenterX > 0f) { npc.spriteDirection = -1; return; }
                }
            }else
            {
                if (!canMove)
                {
                    npc.TargetClosest(true);
                    npc.velocity.Y = npc.velocity.Y + 0.11f;
                    if (npc.velocity.Y > maxSpeed) { npc.velocity.Y = maxSpeed; }
                    if (Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y) < maxSpeed * 0.4)
                    {
                        if (npc.velocity.X < 0f) { npc.velocity.X = npc.velocity.X - gravityResist * 1.1f; } else { npc.velocity.X = npc.velocity.X + gravityResist * 1.1f; }
                    }
                    else
                        if (npc.velocity.Y == maxSpeed)
                        {
                            if (npc.velocity.X < playerCenterX) { npc.velocity.X = npc.velocity.X + gravityResist; }
                            else
                                if (npc.velocity.X > playerCenterX) { npc.velocity.X = npc.velocity.X - gravityResist; }
                        }
                        else
                            if (npc.velocity.Y > 4f)
                            {
                                if (npc.velocity.X < 0f) { npc.velocity.X = npc.velocity.X + gravityResist * 0.9f; } else { npc.velocity.X = npc.velocity.X - gravityResist * 0.9f; }
                            }
                }else
                {
                    if (soundEffects && npc.soundDelay == 0)
                    {
                        float distSoundDelay = dist / 40f;
                        if (distSoundDelay < 10f) { distSoundDelay = 10f; }
                        if (distSoundDelay > 20f) { distSoundDelay = 20f; }
                        npc.soundDelay = (int)distSoundDelay;
                        SoundEngine.PlaySound(SoundID.WormDig, npc.position);
                    }
                    dist = (float)Math.Sqrt(playerCenterX * playerCenterX + playerCenterY * playerCenterY);
                    float absPlayerCenterX = Math.Abs(playerCenterX);
                    float absPlayerCenterY = Math.Abs(playerCenterY);
                    float newSpeed = maxSpeed / dist;
                    playerCenterX *= newSpeed;
                    playerCenterY *= newSpeed;
                    bool dontFall = false;
                    if (fly)
                    {
                        if (((npc.velocity.X > 0f && playerCenterX < 0f) || (npc.velocity.X < 0f && playerCenterX > 0f) || (npc.velocity.Y > 0f && playerCenterY < 0f) || (npc.velocity.Y < 0f && playerCenterY > 0f)) && Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y) > gravityResist / 2f && dist < 300f)
                        {
                            dontFall = true;
                            if (Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y) < maxSpeed) { npc.velocity *= 1.1f; }
                        }
                        if (npc.position.Y > Main.player[npc.target].position.Y || Main.player[npc.target].position.Y / 16f > Main.worldSurface || Main.player[npc.target].dead)
                        {
                            dontFall = true;
                            if (Math.Abs(npc.velocity.X) < maxSpeed / 2f)
                            {
                                if (npc.velocity.X == 0f) { npc.velocity.X = npc.velocity.X - npc.direction; }
                                npc.velocity.X = npc.velocity.X * 1.1f;
                            }
                            else
                                if (npc.velocity.Y > -maxSpeed) { npc.velocity.Y = npc.velocity.Y - gravityResist; }
                        }
                    }
                    if (!dontFall)
                    {
                        if ((npc.velocity.X > 0f && playerCenterX > 0f) || (npc.velocity.X < 0f && playerCenterX < 0f) || (npc.velocity.Y > 0f && playerCenterY > 0f) || (npc.velocity.Y < 0f && playerCenterY < 0f))
                        {
                            if (npc.velocity.X < playerCenterX) { npc.velocity.X = npc.velocity.X + gravityResist; }
                            else
                                if (npc.velocity.X > playerCenterX) { npc.velocity.X = npc.velocity.X - gravityResist; }
                            if (npc.velocity.Y < playerCenterY) { npc.velocity.Y = npc.velocity.Y + gravityResist; }
                            else
                                if (npc.velocity.Y > playerCenterY) { npc.velocity.Y = npc.velocity.Y - gravityResist; }
                            if (Math.Abs(playerCenterY) < maxSpeed * 0.2 && ((npc.velocity.X > 0f && playerCenterX < 0f) || (npc.velocity.X < 0f && playerCenterX > 0f)))
                            {
                                if (npc.velocity.Y > 0f) { npc.velocity.Y = npc.velocity.Y + gravityResist * 2f; } else { npc.velocity.Y = npc.velocity.Y - gravityResist * 2f; }
                            }
                            if (Math.Abs(playerCenterX) < maxSpeed * 0.2 && ((npc.velocity.Y > 0f && playerCenterY < 0f) || (npc.velocity.Y < 0f && playerCenterY > 0f)))
                            {
                                if (npc.velocity.X > 0f) { npc.velocity.X = npc.velocity.X + gravityResist * 2f; } else { npc.velocity.X = npc.velocity.X - gravityResist * 2f; }
                            }
                        }
                        else
                            if (absPlayerCenterX > absPlayerCenterY)
                            {
                                if (npc.velocity.X < playerCenterX) { npc.velocity.X = npc.velocity.X + gravityResist * 1.1f; }
                                else
                                    if (npc.velocity.X > playerCenterX) { npc.velocity.X = npc.velocity.X - gravityResist * 1.1f; }

                                if (Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y) < maxSpeed * 0.5)
                                {
                                    if (npc.velocity.Y > 0f) { npc.velocity.Y = npc.velocity.Y + gravityResist; } else { npc.velocity.Y = npc.velocity.Y - gravityResist; }
                                }
                            }
                            else
                            {
                                if (npc.velocity.Y < playerCenterY) { npc.velocity.Y = npc.velocity.Y + gravityResist * 1.1f; }
                                else
                                    if (npc.velocity.Y > playerCenterY) { npc.velocity.Y = npc.velocity.Y - gravityResist * 1.1f; }
                                if (Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y) < maxSpeed * 0.5)
                                {
                                    if (npc.velocity.X > 0f) { npc.velocity.X = npc.velocity.X + gravityResist; } else { npc.velocity.X = npc.velocity.X - gravityResist; }
                                }
                            }
                    }
                }

                if (!rotateAverage || npc.type == wormTypes[0])
                {
                    npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X) + 1.57f;
                }else
                {
                    NPC frontNPC = Main.npc[(int)npc.ai[1]];
                    Vector2 rotVec = BaseUtility.RotateVector(frontNPC.Center, frontNPC.Center + new Vector2(0f, 30f), frontNPC.rotation);
                    npc.rotation = BaseUtility.RotationTo(npc.Center, rotVec) + 1.57f;
                }
                if (npc.type == wormTypes[0])
                {
                    if (canMove)
                    {
                        if (npc.localAI[0] != 1f) { npc.netUpdate = true; }
                        npc.localAI[0] = 1f;
                    }
                    else
                    {
                        if (npc.localAI[0] != 0f) { npc.netUpdate = true; }
                        npc.localAI[0] = 0f;
                    }
                    if (((npc.velocity.X > 0f && npc.oldVelocity.X < 0f) || (npc.velocity.X < 0f && npc.oldVelocity.X > 0f) || (npc.velocity.Y > 0f && npc.oldVelocity.Y < 0f) || (npc.velocity.Y < 0f && npc.oldVelocity.Y > 0f)) && !npc.justHit)
                    {
                        npc.netUpdate = true;
                        return;
                    }
                }
            }
        }

        public static void DrawAura(object sb, Texture2D texture, int shader, Entity codable, float auraPercent, float distanceScalar = 1f, float offsetX = 0f, float offsetY = 0f, Color? overrideColor = null)
        {
            int frameCount = codable is NPC ? Main.npcFrameCount[((NPC)codable).type] : 1;
            Rectangle frame = codable is NPC ? ((NPC)codable).frame : new Rectangle(0, 0, texture.Height, texture.Width);
            float scale = codable is NPC ? ((NPC)codable).scale : ((Projectile)codable).scale;
            float rotation = codable is NPC ? ((NPC)codable).rotation : ((Projectile)codable).rotation;
            int spriteDirection = codable is NPC ? ((NPC)codable).spriteDirection : ((Projectile)codable).spriteDirection;
            float offsetY2 = codable is NPC ? ((NPC)codable).gfxOffY : 0f;
            DrawAura(sb, texture, shader, codable.position + new Vector2(0f, offsetY2), codable.width, codable.height, auraPercent, distanceScalar, scale, rotation, spriteDirection, frameCount, frame, offsetX, offsetY, overrideColor);
        }

        public static void DrawAura(object sb, Texture2D texture, int shader, Vector2 position, int width, int height, float auraPercent, float distanceScalar = 1f, float scale = 1f, float rotation = 0f, int direction = 0, int framecount = 1, Rectangle frame = default, float offsetX = 0f, float offsetY = 0f, Color? overrideColor = null)
        {
            Color lightColor = overrideColor != null ? (Color)overrideColor : BaseDrawing.GetLightColor(position + new Vector2(width * 0.5f, height * 0.5f));
            float percentHalf = auraPercent * 5f * distanceScalar;
            float percentLight = MathHelper.Lerp(0.8f, 0.2f, auraPercent);
            lightColor.R = (byte)(lightColor.R * percentLight);
            lightColor.G = (byte)(lightColor.G * percentLight);
            lightColor.B = (byte)(lightColor.B * percentLight);
            lightColor.A = (byte)(lightColor.A * percentLight);
            Vector2 position2 = position;
            for (int m = 0; m < 4; m++)
            {
                float offX = offsetX;
                float offY = offsetY;
                switch (m)
                {
                    case 0: offX += percentHalf; break;
                    case 1: offX -= percentHalf; break;
                    case 2: offY += percentHalf; break;
                    case 3: offY -= percentHalf; break;
                }
                position2 = new Vector2(position.X + offX, position.Y + offY);
                BaseDrawing.DrawTexture(Main.spriteBatch, texture, shader, position2, width, height, scale, rotation, direction, framecount, frame, lightColor, true);
            }
        }
    }
}
