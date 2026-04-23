using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Linq;

namespace AAModClassic.Base.BaseMod.Base
{
    public class BaseAI
    {
        //------------------------------------------------------//
        //-------------------BASE AI CLASS----------------------//
        //------------------------------------------------------//
        // Contains methods for various AI functions for both   //
        // NPCs and Projectiles, such as adding lighting,       //
        // movement, etc.                                       //
        //------------------------------------------------------//
        //  Author(s): Grox the Great, Yoraiz0r                 //
        //------------------------------------------------------//

        #region Custom AI Methods
        public static void AIMinionFlier(Projectile projectile, ref float[] ai, Entity owner, bool pet = false, bool movementFixed = false, bool hover = false, int hoverHeight = 40, int lineDist = 40, int returnDist = 400, int teleportDist = 800, float moveInterval = -1f, float maxSpeed = -1f, float maxSpeedFlying = -1f, bool autoSpriteDir = true, bool dummyTileCollide = false, Func<Entity, Entity, Entity> getTarget = null, Func<Entity, Entity, Entity, bool> shootTarget = null)
        {
            if (moveInterval == -1f) { moveInterval = 0.08f * Main.player[projectile.owner].moveSpeed; }
            if (maxSpeed == -1f) { maxSpeed = Math.Max(Main.player[projectile.owner].maxRunSpeed, Main.player[projectile.owner].accRunSpeed); }
            if (maxSpeedFlying == -1f) { maxSpeedFlying = Math.Max(maxSpeed, Math.Max(Main.player[projectile.owner].maxRunSpeed, Main.player[projectile.owner].accRunSpeed)); }
            projectile.timeLeft = 10;
            bool tileCollide = projectile.tileCollide;
            AIMinionFlier(projectile, ref ai, owner, ref tileCollide, ref projectile.netUpdate, pet ? 0 : projectile.minionPos, movementFixed, hover, hoverHeight, lineDist, returnDist, teleportDist, moveInterval, maxSpeed, maxSpeedFlying, getTarget, shootTarget);
            if (!dummyTileCollide) projectile.tileCollide = tileCollide;
            if (autoSpriteDir) { projectile.spriteDirection = projectile.direction; }
            if (ai[0] == 1) { projectile.spriteDirection = owner.velocity.X == 0 ? projectile.spriteDirection : owner.velocity.X > 0 ? 1 : -1; }
            if ((getTarget == null || getTarget(projectile, owner) == null || getTarget(projectile, owner) == owner) && Math.Abs(projectile.velocity.X + projectile.velocity.Y) <= 0.025f) { projectile.spriteDirection = owner.Center.X > projectile.Center.X ? 1 : -1; }
        }

        /*
		 * Custom AI that works similarly to fighter minion AI. (uses ai[0, 1])
		 *
		 * owner : The Projectile or NPC who is this minion's owner.
		 * tileCollide : A bool, set to say wether or not the minion can tile collide or not.
		 * netUpdate : set to say wether or not the minion should sync if in multiplayer.
		 * gfxOffsetY : The graphics offset for Y, used for walking up slopes.
		 * stepSpeed : Used for walking up slopes.
		 * minionPos : The minion's position in the minion lineup.
		 * lineDist : The distance between each minion when they line up.
		 * returnDist : The distance to 'fly' back to the player.
		 * teleportDist : The distance to instantly teleport to the player.
		 * moveInterval : How much to move each tick.
		 * maxSpeed : The maxmimum speed of the minion.
		 * maxSpeedFlying : The maximum speed whist 'flying' back to the player.
		 * GetTarget : a Func(Entity codable, Entity owner), returns a Vector2 of the a target's position. If GetTarget is null or it returns default(Vector2) the target is assumed to be the owner.
		 */
        public static void AIMinionFlier(Entity codable, ref float[] ai, Entity owner, ref bool tileCollide, ref bool netUpdate, int minionPos, bool movementFixed, bool hover = false, int hoverHeight = 40, int lineDist = 40, int returnDist = 400, int teleportDist = 800, float moveInterval = 0.2f, float maxSpeed = 4.5f, float maxSpeedFlying = 4.5f, Func<Entity, Entity, Entity> getTarget = null, Func<Entity, Entity, Entity, bool> shootTarget = null)
        {
            float dist = Vector2.Distance(codable.Center, owner.Center);
            if (dist > teleportDist) { codable.Center = owner.Center; }
            int tileX = (int)(codable.Center.X / 16f), tileY = (int)(codable.Center.Y / 16f);
            Tile tile = Framing.GetTileSafely(tileX, tileY);
            bool inTile = tile is { HasUnactuatedTile: true } && Main.tileSolid[tile.TileType];
            float prevAI = ai[0];
            ai[0] = ai[0] == 1 && (dist > Math.Max(lineDist, returnDist / 2f) || !BaseUtility.CanHit(codable.Hitbox, owner.Hitbox)) || dist > returnDist || inTile ? 1 : 0;
            if (ai[0] != prevAI) { netUpdate = true; }
            if (ai[0] == 0 || ai[0] == 1)
            {
                if (ai[0] == 1) { moveInterval *= 1.5f; maxSpeedFlying *= 1.5f; }
                tileCollide = ai[0] == 0;
                Entity target = getTarget == null ? owner : getTarget(codable, owner);
                if (target == null) { target = owner; }
                Vector2 targetCenter = target.Center;
                bool isOwner = target == owner;
                bool dontMove = ai[0] == 0 && shootTarget != null && shootTarget(codable, owner, target);
                if (isOwner)
                {
                    targetCenter.Y -= hoverHeight;
                    if (hover) { targetCenter.X += (lineDist + lineDist * minionPos) * -target.direction; }
                }
                if (!hover || !isOwner)
                {
                    float dirDist = hover ? 1.2f : 1.8f;
                    float dir = dist < lineDist * minionPos + lineDist * dirDist ? codable.velocity.X > 0 ? 1f : -1f : target.Center.X > codable.Center.X ? 1f : -1f;
                    //Semierratic movement so it looks more like a swarm and less like synchronized swimmers.
                    targetCenter.X += (minionPos == 0 ? 0f : minionPos % 5 == 0 ? lineDist / 4f : minionPos % 4 == 0 ? lineDist / 2f : minionPos % 3 == 0 ? lineDist / 3f : 0f) * dir;
                    targetCenter.X += lineDist * 2f * dir;
                    targetCenter.Y -= hoverHeight / 4f * minionPos;
                    targetCenter.Y -= (codable.velocity.X < 0 ? lineDist * 0.25f : -lineDist * 0.25f) * (minionPos % 2 == 0 ? 1 : -1);
                }
                float targetDistX = Math.Abs(codable.Center.X - targetCenter.X);
                float targetDistY = Math.Abs(codable.Center.Y - targetCenter.Y);
                bool slowdownX = hover && owner.velocity.X < 0.025f && targetDistX < 8f * Math.Max(1f, maxSpeed / 4f);
                bool slowdownY = hover && owner.velocity.Y < 0.025f && targetDistY < 8f * Math.Max(1f, maxSpeed / 4f);
                Vector2 vel = AIVelocityLinear(codable, targetCenter, moveInterval, ai[0] == 0 ? maxSpeed : maxSpeedFlying, true);
                if (!dontMove && !slowdownX) { codable.velocity.X += vel.X * 0.125f; }
                if (!dontMove && !slowdownY) { codable.velocity.Y += vel.Y * 0.125f; }
                if (dontMove || slowdownX) { codable.velocity.X *= Math.Abs(codable.velocity.X) > 0.01f ? 0.85f : 0f; }
                if (vel.X > 0 && codable.velocity.X > vel.X || vel.X < 0 && codable.velocity.X < vel.X) { codable.velocity.X = vel.X; }
                if (dontMove || slowdownY) { codable.velocity.Y *= Math.Abs(codable.velocity.Y) > 0.01f ? 0.85f : 0f; }
                if (vel.Y > 0 && codable.velocity.Y > vel.Y || vel.Y < 0 && codable.velocity.X < vel.Y) { codable.velocity.Y = vel.Y; }
            }
        }

        public static void AIMinionFighter(Projectile projectile, ref float[] ai, Entity owner, bool pet = false, int jumpDistX = 4, int jumpDistY = 5, int lineDist = 40, int returnDist = 400, int teleportDist = 800, float moveInterval = -1f, float maxSpeed = -1f, float maxSpeedFlying = -1f, Func<Entity, Entity, Entity> getTarget = null)
        {
            if (moveInterval == -1f) { moveInterval = 0.08f * Main.player[projectile.owner].moveSpeed; }
            if (maxSpeed == -1f) { maxSpeed = Math.Max(Main.player[projectile.owner].maxRunSpeed, Main.player[projectile.owner].accRunSpeed); }
            if (maxSpeedFlying == -1f) { maxSpeedFlying = Math.Max(maxSpeed, Math.Max(Main.player[projectile.owner].maxRunSpeed, Main.player[projectile.owner].accRunSpeed)); }
            AIMinionFighter(projectile, ref ai, owner, ref projectile.tileCollide, ref projectile.netUpdate, ref projectile.gfxOffY, ref projectile.stepSpeed, pet ? 0 : projectile.minionPos, jumpDistX, jumpDistY, lineDist, returnDist, teleportDist, moveInterval, maxSpeed, maxSpeedFlying, getTarget);
            projectile.spriteDirection = projectile.direction;
            if (ai[0] == 1) { projectile.spriteDirection = owner.velocity.X == 0 ? projectile.spriteDirection : owner.velocity.X > 0 ? 1 : -1; }
            if ((getTarget == null || getTarget(projectile, owner) == null || getTarget(projectile, owner) == owner) && projectile.velocity.X is >= -0.025f or <= 0.025f && projectile.velocity.Y == 0) { projectile.spriteDirection = owner.Center.X > projectile.Center.X ? 1 : -1; }
        }


        /*
		 * Custom AI that works similarly to fighter minion AI. (uses ai[0, 1])
		 *
		 * owner : The Projectile or NPC who is this minion's owner.
		 * tileCollide : A bool, set to say wether or not the minion can tile collide or not.
		 * netUpdate : set to say wether or not the minion should sync if in multiplayer.
		 * gfxOffsetY : The graphics offset for Y, used for walking up slopes.
		 * stepSpeed : Used for walking up slopes.
		 * minionPos : The minion's position in the minion lineup.
		 * jumpDistX : The minion's max jump distance on the X axis.
		 * jumpDistY : The minion's max jump distance on the Y axis.
		 * lineDist : The distance between each minion when they line up.
		 * returnDist : The distance to 'fly' back to the player.
		 * teleportDist : The distance to instantly teleport to the player.
		 * moveInterval : How much to move each tick.
		 * maxSpeed : The maxmimum speed of the minion.
		 * maxSpeedFlying : The maximum speed whist 'flying' back to the player.
		 * GetTarget : a Func(Entity codable, Entity owner), returns a Vector2 of the a target's position. If GetTarget is null or it returns default(Vector2) the target is assumed to be the owner.
		 */
        public static void AIMinionFighter(Entity codable, ref float[] ai, Entity owner, ref bool tileCollide, ref bool netUpdate, ref float gfxOffY, ref float stepSpeed, int minionPos, int jumpDistX = 4, int jumpDistY = 5, int lineDist = 40, int returnDist = 400, int teleportDist = 800, float moveInterval = 0.2f, float maxSpeed = 4.5f, float maxSpeedFlying = 4.5f, Func<Entity, Entity, Entity> getTarget = null)
        {
            float dist = Vector2.Distance(codable.Center, owner.Center);
            if (dist > teleportDist) { codable.Center = owner.Center; }
            int tileX = (int)(codable.Center.X / 16f), tileY = (int)(codable.Center.Y / 16f);
            Tile tile = Framing.GetTileSafely(tileX, tileY);
            bool inTile = tile is { HasUnactuatedTile: true } && Main.tileSolid[tile.TileType];
            float prevAI = ai[0];
            ai[0] = ai[0] == 1 && (owner.velocity.Y != 0 || dist > Math.Max(lineDist, returnDist / 10f)) || dist > returnDist || inTile ? 1 : 0;
            if (ai[0] != prevAI) { netUpdate = true; }
            if (ai[0] == 0) //walking
            {
                tileCollide = true;
                Entity target = getTarget == null ? null : getTarget(codable, owner);
                Vector2 targetCenter = target == null ? default : target.Center;
                bool isOwner = target == null || targetCenter == owner.Center;
                if (targetCenter == default)
                {
                    targetCenter = owner.Center;
                    targetCenter.X += (owner.width + 10 + lineDist * minionPos) * -owner.direction;
                }
                float targetDistX = Math.Abs(codable.Center.X - targetCenter.X);
                float targetDistY = Math.Abs(codable.Center.Y - targetCenter.Y);
                int moveDirection = targetCenter.X > codable.Center.X ? 1 : -1;
                int moveDirectionY = targetCenter.Y > codable.Center.Y ? 1 : -1;
                if (isOwner && owner.velocity.X < 0.025f && codable.velocity.Y == 0f && targetDistX < 8f)
                {
                    codable.velocity.X *= Math.Abs(codable.velocity.X) > 0.01f ? 0.8f : 0f;
                }
                else
                if (codable.velocity.X < -maxSpeed || codable.velocity.X > maxSpeed)
                {
                    if (codable.velocity.Y == 0f) { codable.velocity *= 0.85f; }
                }
                else
                if (codable.velocity.X < maxSpeed && moveDirection == 1)
                {
                    if (codable.velocity.X < 0) { codable.velocity.X *= 0.85f; }
                    codable.velocity.X += moveInterval * (codable.velocity.X < 0 ? 2f : 1f);
                    if (codable.velocity.X > maxSpeed) { codable.velocity.X = maxSpeed; }
                }
                else
                if (codable.velocity.X > -maxSpeed && moveDirection == -1)
                {
                    if (codable.velocity.X > 0) { codable.velocity.X *= 0.8f; }
                    codable.velocity.X -= moveInterval * (codable.velocity.X > 0 ? 2f : 1f);
                    if (codable.velocity.X < -maxSpeed) { codable.velocity.X = -maxSpeed; }
                }
                WalkupHalfBricks(codable, ref gfxOffY, ref stepSpeed);
                if (HitTileOnSide(codable, 3))
                {
                    if (codable.velocity.X < 0f && moveDirection == -1 || codable.velocity.X > 0f && moveDirection == 1)
                    {
                        bool test = target != null && !isOwner && targetDistX < 50f && targetDistY > codable.height + codable.height / 2 && targetDistY < 16f * (jumpDistY + 1) && BaseUtility.CanHit(codable.Hitbox, target.Hitbox);
                        Vector2 newVec = AttemptJump(codable.position, codable.velocity, codable.width, codable.height, moveDirection, moveDirectionY, jumpDistX, jumpDistY, maxSpeed, true, target, test);
                        if (tileCollide)
                        {
                            newVec = Collision.TileCollision(codable.position, newVec, codable.width, codable.height);
                            Vector4 slopeVec = Collision.SlopeCollision(codable.position, newVec, codable.width, codable.height);
                            codable.position = new Vector2(slopeVec.X, slopeVec.Y);
                            codable.velocity = new Vector2(slopeVec.Z, slopeVec.W);
                        }
                        if (codable.velocity != newVec) { codable.velocity = newVec; netUpdate = true; }
                    }
                }
                else { codable.velocity.Y += 0.35f; } //gravity
            }
            else //flying
            {
                tileCollide = false;
                Vector2 targetCenter = owner.Center;
                if (owner.velocity.Y != 0f && dist < 80)
                {
                    targetCenter = owner.Center + BaseUtility.RotateVector(default, new Vector2(10, 0f), BaseUtility.RotationTo(codable.Center, owner.Center));
                }
                Vector2 newVel = BaseUtility.RotateVector(default, new Vector2(maxSpeedFlying, 0f), BaseUtility.RotationTo(codable.Center, targetCenter));
                if (owner.velocity.Y != 0f && (newVel.X > 0 && codable.velocity.X < 0 || newVel.X < 0 && codable.velocity.X > 0))
                {
                    codable.velocity *= 0.98f; newVel *= 0.02f; codable.velocity += newVel;
                }
                else { codable.velocity = newVel; }
                codable.position += owner.velocity;
            }
        }

        /*
		 * Custom AI that will cause the npc to rotate around a point in a fixed circle.
		 *
		 * rotation : The codable's rotation.
		 * moveRot : A value storing the internal rotation of the codable.
		 * rotateCenter : The center to be rotating around.
		 * absolute : If true, moves it by position instead of by velocity.
		 * rotDistance : How far from the rotateCenter to rotate.
		 * rotThreshold : Only used if absolute is false, used to determine how much 'give' the codable has before it forces itself back into the rotation.
		 * rotAmount : How much to rotate each tick.
		 * moveTowards : Only used if absolute is false, if outside the rotation, move towards it.
		 */
        public static void AIRotate(Entity codable, ref float rotation, ref float moveRot, Vector2 rotateCenter, bool absolute = false, float rotDistance = 50f, float rotThreshold = 20f, float rotAmount = 0.024f, bool moveTowards = true)
        {
            if (absolute)
            {
                moveRot += rotAmount;
                Vector2 rotVec = BaseUtility.RotateVector(default, new Vector2(rotDistance, 0f), moveRot) + rotateCenter;
                codable.Center = rotVec;
                rotVec = rotVec.SafeNormalize(Vector2.Zero);
                rotation = BaseUtility.RotationTo(codable.Center, rotateCenter) - 1.57f;
                codable.velocity *= 0f;
            }
            else
            {
                float dist = Vector2.Distance(codable.Center, rotateCenter);
                if (dist < rotDistance)//close enough to rotate
                {
                    if (rotDistance - dist > rotThreshold) //too close, get back into position
                    {
                        moveRot += rotAmount;
                        Vector2 rotVec = BaseUtility.RotateVector(default, new Vector2(rotDistance, 0f), moveRot) + rotateCenter;
                        float rot2 = BaseUtility.RotationTo(codable.Center, rotVec);
                        codable.velocity = BaseUtility.RotateVector(default, new Vector2(5f, 0f), rot2);
                        rotation = BaseUtility.RotationTo(codable.Center, codable.Center + codable.velocity);
                    }
                    else
                    {
                        moveRot += rotAmount;
                        Vector2 rotVec = BaseUtility.RotateVector(default, new Vector2(rotDistance, 0f), moveRot) + rotateCenter;
                        float rot2 = BaseUtility.RotationTo(codable.Center, rotVec);
                        codable.velocity = BaseUtility.RotateVector(default, new Vector2(5f, 0f), rot2);
                        rotation = BaseUtility.RotationTo(codable.Center, codable.Center + codable.velocity);
                    }
                }
                else
                if (moveTowards)
                {
                    codable.velocity = AIVelocityLinear(codable, rotateCenter, rotAmount, rotAmount, true);
                    rotation = BaseUtility.RotationTo(codable.Center, rotateCenter) - 1.57f;
                }
                else { codable.velocity *= 0.95f; }
            }
        }

        /*
         * Custom AI that will cause the npc to 'tackle' a specific point given. (uses ai[0, 1, 2])
         *
         * ai : A float array that stores AI data. (Note projectile array should be synced!)
         * point : the central point of which to 'gravitate'.
         * moveInterval : the amount to move by per tick.
         * maxSpeed : the maximum speed of the npc.
         * direct : If true npc's velocity is set so it moves in a straight line. If false, moves similarly to Flier AI.
         * tackleDelay : the amount of time between tackles in ticks.
         */
        public static void AITackle(NPC npc, ref float[] ai, Vector2 point, float moveInterval = 0.11f, float maxSpeed = 3f, bool direct = false, int tackleDelay = 50, float drift = 0.95f)
        {
            Vector2 destVec = new(ai[0], ai[1]);
            if (destVec != default && Vector2.Distance(npc.Center, destVec) <= Math.Max(5f, (npc.width + npc.height) / 2f * 0.45f))
            {
                ai[0] = 0f; ai[1] = 0f; destVec = default;
            }
            //if the destination vec is default (0, 0), get the current point.
            if (destVec == default)
            {
                npc.velocity *= drift;
                ai[2]--;
                if (ai[2] <= 0)
                {
                    ai[2] = tackleDelay;
                    destVec = point;
                    ai[0] = destVec.X; ai[1] = destVec.Y;
                }
                if (Main.netMode == NetmodeID.Server) { npc.netUpdate = true; }
            }
            else //otherwise move to the point.
            {
                npc.velocity = AIVelocityLinear(npc, destVec, moveInterval, maxSpeed, direct);
            }
        }


        public static Vector2 AIVelocityLinear(Entity codable, Vector2 destVec, float moveInterval, float maxSpeed, bool direct = false)
        {
            Vector2 returnVelocity = codable.velocity;
            bool tileCollide = codable is NPC nPC ? !nPC.noTileCollide : codable is Projectile projectile && projectile.tileCollide;
            if (direct)
            {
                Vector2 rotVec = BaseUtility.RotateVector(codable.Center, codable.Center + new Vector2(maxSpeed, 0f), BaseUtility.RotationTo(codable.Center, destVec));
                returnVelocity = rotVec - codable.Center;
            }
            else
            {
                if (codable.Center.X > destVec.X) { returnVelocity.X = Math.Max(-maxSpeed, returnVelocity.X - moveInterval); } else if (codable.Center.X < destVec.X) { returnVelocity.X = Math.Min(maxSpeed, returnVelocity.X + moveInterval); }
                if (codable.Center.Y > destVec.Y) { returnVelocity.Y = Math.Max(-maxSpeed, returnVelocity.Y - moveInterval); } else if (codable.Center.Y < destVec.Y) { returnVelocity.Y = Math.Min(maxSpeed, returnVelocity.Y + moveInterval); }
            }
            if (tileCollide)
            {
                returnVelocity = Collision.TileCollision(codable.position, returnVelocity, codable.width, codable.height);
            }
            return returnVelocity;
        }

        #endregion

        #region Vanilla Projectile AI Copy Methods
        /*-----------------------------------------
         *
         * These are methods of vanilla projectile AIs
         * cleaned up. If a method has Entity instead
         * of Projectile as it's first argument, it
         * means npcs can use the method too.
         *
         * ----------------------------------------
         */

        /*
         * A cleaned up (and edited) copy of Vilethorn AI. (Vilethorn, etc.)
         *
         * alphaInterval : The amount of alpha to add each tick. (higher values == faster spawning)
         * alphaReduction : The amount of alpha to reduce after spawning the next piece. (higher values == faster despawning)
         * length : How many segments to spawn.
         */
        public static void AIVilethorn(Projectile p, int alphaInterval = 50, int alphaReduction = 4, int length = 8)
        {
            if (p.ai[0] == 0f)
            {
                p.rotation = (float)Math.Atan2(p.velocity.Y, p.velocity.X) + 1.57f;
                p.alpha -= alphaInterval;
                if (p.alpha <= 0)
                {
                    p.alpha = 0;
                    p.ai[0] = 1f;
                    if (p.ai[1] == 0f) { p.ai[1] += 1f; p.position += p.velocity; }
                    if (p.ai[1] < length && Main.myPlayer == p.owner)
                    {
                        Vector2 rotVec = p.velocity;
                        int id = Projectile.NewProjectile(p.GetSource_FromAI(), p.Center.X + p.velocity.X, p.Center.Y + p.velocity.Y, rotVec.X, rotVec.Y, p.type, p.damage, p.knockBack, p.owner);
                        Main.projectile[id].damage = p.damage;
                        Main.projectile[id].ai[1] = p.ai[1] + 1f;
                        NetMessage.SendData(MessageID.SyncProjectile, -1, -1, NetworkText.FromLiteral(""), id);
                        p.position -= p.velocity;
                        return;
                    }
                }
            }
            else
            {
                p.alpha += alphaReduction;
                if (p.alpha >= 255) { p.Kill(); return; }
            }
            p.position -= p.velocity;
        }

        /*
         * A cleaned up (and edited) copy of Thrown Weapon AI. (throwing knife, shuriken, etc.)
         *
         * ai : A float array that stores AI data. (Note projectile array should be synced!)
         * spin : wether to continously spin with velocity or point in the direction of velocity until slowdown.
         * timeUntilDrop : How many ticks to move until slowing down.
         * xScalar : the scalar to slow down on the X axis.
         * yIncrement : the amount to speed up by on the Y axis.
         * maxSpeedY : the max speed of the projectile on the Y axis.
         */
        public static void AIThrownWeapon(Projectile p, ref float[] ai, bool spin = false, int timeUntilDrop = 10, float xScalar = 0.99f, float yIncrement = 0.25f, float maxSpeedY = 16f)
        {
            p.rotation += (Math.Abs(p.velocity.X) + Math.Abs(p.velocity.Y)) * 0.03f * p.direction;
            ai[0] += 1f;
            if (ai[0] >= timeUntilDrop)
            {
                p.velocity.Y += yIncrement;
                p.velocity.X *= xScalar;
            }
            else
            if (!spin) { p.rotation = BaseUtility.RotationTo(p.Center, p.Center + p.velocity) + 1.57f; }
            if (p.velocity.Y > maxSpeedY) { p.velocity.Y = maxSpeedY; }
        }

        /*
         * A cleaned up (and edited) copy of Boomerang AI.
         *
         * ai : A float array that stores AI data. (Note projectile array should be synced!)
         * center : the center of where the boomerang should return to.
         * playSound : If true, plays the air sound boomerangs make while in the air.
         * maxDistance : the maximum 'distance' for the projectile to go before it rebounds.
         * returnDelay : the amount of time in ticks until the projectile returns to it's source.
         * speedInterval : the amount to move the projectile by each tick.
         * rotationInterval : the amount for the projectile to rotate by each tick.
         * direct : If true, when returning simply reverses the boomerang velocity.
         */
        public static void AIBoomerang(Projectile p, ref float[] ai, Vector2 position = default, int width = -1, int height = -1, bool playSound = true, float maxDistance = 9f, int returnDelay = 35, float speedInterval = 0.4f, float rotationInterval = 0.4f, bool direct = false)
        {
            if (position == default) { position = Main.player[p.owner].position; }
            if (width == -1) { width = Main.player[p.owner].width; }
            if (height == -1) { height = Main.player[p.owner].height; }
            Vector2 center = position + new Vector2(width * 0.5f, height * 0.5f);
            if (playSound && p.soundDelay == 0)
            {
                p.soundDelay = 8;
                SoundEngine.PlaySound(SoundID.Item7, p.position);
            }
            if (ai[0] == 0f)
            {
                ai[1] += 1f;
                if (ai[1] >= returnDelay)
                {
                    ai[0] = 1f;
                    ai[1] = 0f;
                    p.netUpdate = true;
                }
            }
            else
            {
                p.tileCollide = false;
                float distPlayerX = center.X - p.Center.X;
                float distPlayerY = center.Y - p.Center.Y;
                float distPlayer = (float)Math.Sqrt(distPlayerX * distPlayerX + distPlayerY * distPlayerY);
                if (distPlayer > 3000f)
                {
                    p.Kill();
                }
                if (direct)
                {
                    p.velocity = BaseUtility.RotateVector(default, new Vector2(speedInterval, 0f), BaseUtility.RotationTo(p.Center, center));
                }
                else
                {
                    distPlayer = maxDistance / distPlayer;
                    distPlayerX *= distPlayer;
                    distPlayerY *= distPlayer;
                    if (p.velocity.X < distPlayerX)
                    {
                        p.velocity.X += speedInterval;
                        if (p.velocity.X < 0f && distPlayerX > 0f) { p.velocity.X += speedInterval; }
                    }
                    else
                    if (p.velocity.X > distPlayerX)
                    {
                        p.velocity.X -= speedInterval;
                        if (p.velocity.X > 0f && distPlayerX < 0f) { p.velocity.X -= speedInterval; }
                    }
                    if (p.velocity.Y < distPlayerY)
                    {
                        p.velocity.Y += speedInterval;
                        if (p.velocity.Y < 0f && distPlayerY > 0f) { p.velocity.Y += speedInterval; }
                    }
                    else
                    if (p.velocity.Y > distPlayerY)
                    {
                        p.velocity.Y -= speedInterval;
                        if (p.velocity.Y > 0f && distPlayerY < 0f) { p.velocity.Y -= speedInterval; }
                    }
                }
                if (Main.myPlayer == p.owner)
                {
                    Rectangle rectangle = p.Hitbox;
                    Rectangle value = new((int)position.X, (int)position.Y, width, height);
                    if (rectangle.Intersects(value)) { p.Kill(); }
                }
            }
            p.rotation += rotationInterval * p.direction;
        }

        /*
         * A cleaned up (and edited) copy of tile collison for Boomerangs.
         * bounce : Set to true if your projectile acts like Light Discs or the Thorn Chakram.
         */
        public static void TileCollideBoomerang(Projectile p, ref Vector2 velocity, bool bounce = false)
        {
            if (bounce)
            {
                if (p.velocity.X != velocity.X) { p.velocity.X = -velocity.X; }
                if (p.velocity.Y != velocity.Y) { p.velocity.Y = -velocity.Y; }
            }
            else
            {
                p.ai[0] = 1f;
                p.velocity.X = -velocity.X;
                p.velocity.Y = -velocity.Y;
            }
            p.netUpdate = true;
        }

        public static void AIFlail(Projectile p, ref float[] ai, bool noKill = false, float chainDistance = 160f)
        {
            if (Main.player[p.owner] != null)
            {
                if (Main.player[p.owner].dead) { p.Kill(); return; }
                Main.player[p.owner].itemAnimation = 10;
                Main.player[p.owner].itemTime = 10;
            }
            AIFlail(p, ref ai, Main.player[p.owner].Center, Main.player[p.owner].velocity, Main.player[p.owner].GetAttackSpeed(DamageClass.Melee), Main.player[p.owner].channel, noKill, chainDistance);
            Main.player[p.owner].direction = p.direction;
        }

        /*
         * A cleaned up (and edited) copy of Flail AI.
         *
         * ai : A float array that stores AI data. (Note projectile array should be synced!)
         * connectedPoint : The point for the flail to be 'attached' to, and rebound to, etc.
         * connectedPointVelocity : The velocity of the connected point, if it is moving.
         * GetAttackSpeed(DamageClass.Melee) : the GetAttackSpeed(DamageClass.Melee) of whatever is using the flail.
         * channel : Wether or not the source is 'channeling' (holding down the fire button) projectile flail.
         * noKill : If true, do not kill the projectile when it returns to the connected point.
         * chainDistance : How far for the flail to actually go.
         */
        public static void AIFlail(Projectile p, ref float[] ai, Vector2 connectedPoint, Vector2 connectedPointVelocity, float meleeSpeed, bool channel, bool noKill = false, float chainDistance = 160f)
        {
            p.direction = p.Center.X > connectedPoint.X ? 1 : -1;
            float pointX = connectedPoint.X - p.Center.X;
            float pointY = connectedPoint.Y - p.Center.Y;
            float pointDist = (float)Math.Sqrt(pointX * pointX + pointY * pointY);
            if (ai[0] == 0f)
            {
                p.tileCollide = true;
                if (pointDist > chainDistance)
                {
                    ai[0] = 1f;
                    p.netUpdate = true;
                }
                else
                {
                    if (!channel)
                    {
                        if (p.velocity.Y < 0f) { p.velocity.Y *= 0.9f; }
                        p.velocity.Y += 1f;
                        p.velocity.X *= 0.9f;
                    }
                }
            }
            else
            if (ai[0] == 1f)
            {
                float meleeSpeed1 = 14f / meleeSpeed;
                float meleeSpeed2 = 0.9f / meleeSpeed;
                float maxBallDistance = chainDistance + 140f;
                Math.Abs(pointX);
                Math.Abs(pointY);
                if (ai[1] == 1f) { p.tileCollide = false; }
                if (!channel || pointDist > maxBallDistance || !p.tileCollide)
                {
                    ai[1] = 1f;
                    if (p.tileCollide) { p.netUpdate = true; }
                    p.tileCollide = false;
                    if (!noKill && pointDist < 20f)
                    {
                        p.Kill();
                    }
                }
                if (!p.tileCollide) { meleeSpeed2 *= 2f; }
                if (pointDist > 60f || !p.tileCollide)
                {
                    pointDist = meleeSpeed1 / pointDist;
                    pointX *= pointDist;
                    pointY *= pointDist;
                    float pointX2 = pointX - p.velocity.X;
                    float pointY2 = pointY - p.velocity.Y;
                    float pointDist2 = (float)Math.Sqrt(pointX2 * pointX2 + pointY2 * pointY2);
                    pointDist2 = meleeSpeed2 / pointDist2;
                    pointX2 *= pointDist2;
                    pointY2 *= pointDist2;
                    p.velocity.X *= 0.98f;
                    p.velocity.Y *= 0.98f;
                    p.velocity.X += pointX2;
                    p.velocity.Y += pointY2;
                }
                else
                {
                    if (Math.Abs(p.velocity.X) + Math.Abs(p.velocity.Y) < 6f)
                    {
                        p.velocity.X *= 0.96f;
                        p.velocity.Y += 0.2f;
                    }
                    if (connectedPointVelocity.X == 0f) { p.velocity.X *= 0.96f; }
                }
            }
            p.rotation = (float)Math.Atan2(pointY, pointX) - p.velocity.X * 0.1f;
        }

        /*
         * A cleaned up (and edited) copy of tile collison for Flails.
         */
        public static void TileCollideFlail(Projectile p, ref Vector2 velocity, bool playSound = true)
        {
            if (velocity != p.velocity)
            {
                bool updateAndCollide = false;
                if (velocity.X != p.velocity.X)
                {
                    if (Math.Abs(velocity.X) > 4f) { updateAndCollide = true; }
                    p.position.X += p.velocity.X;
                    p.velocity.X = -velocity.X * 0.2f;
                }
                if (velocity.Y != p.velocity.Y)
                {
                    if (Math.Abs(velocity.Y) > 4f) { updateAndCollide = true; }
                    p.position.Y += p.velocity.Y;
                    p.velocity.Y = -velocity.Y * 0.2f;
                }
                p.ai[0] = 1f;
                if (updateAndCollide)
                {
                    p.netUpdate = true;
                    Collision.HitTiles(p.position, p.velocity, p.width, p.height);
                    if (playSound) { SoundEngine.PlaySound(SoundID.Dig, p.position); }
                }
            }
        }

        #endregion

        #region Vanilla NPC AI Copy Methods

        public static void AISpaceOctopus(NPC npc, ref float[] ai, float moveSpeed = 0.15f, float velMax = 5f, float hoverDistance = 250f, float shootProjInterval = 70f, Action<NPC, Vector2> fireProj = null)
        {
            npc.TargetClosest();
            AISpaceOctopus(npc, ref ai, Main.player[npc.target].Center, moveSpeed, velMax, hoverDistance, shootProjInterval, fireProj);
        }

        public static void AISpaceOctopus(NPC npc, ref float[] ai, Vector2 targetCenter = default, float moveSpeed = 0.15f, float velMax = 5f, float hoverDistance = 250f, float shootProjInterval = 70f, Action<NPC, Vector2> fireProj = null)
        {
            Vector2 wantedVelocity = targetCenter - npc.Center + new Vector2(0f, -hoverDistance);
            float dist = wantedVelocity.Length();
            if (dist < 20f)
            {
                wantedVelocity = npc.velocity;
            }
            else if (dist < 40f)
            {
                wantedVelocity = wantedVelocity.SafeNormalize(Vector2.Zero);
                wantedVelocity *= velMax * 0.35f;
            }
            else if (dist < 80f)
            {
                wantedVelocity = wantedVelocity.SafeNormalize(Vector2.Zero);
                wantedVelocity *= velMax * 0.65f;
            }
            else
            {
                wantedVelocity = wantedVelocity.SafeNormalize(Vector2.Zero);
                wantedVelocity *= velMax;
            }
            npc.SimpleFlyMovement(wantedVelocity, moveSpeed);
            npc.rotation = npc.velocity.X * 0.1f;
            if (fireProj != null && shootProjInterval > -1 && (ai[0] += 1f) >= shootProjInterval)
            {
                ai[0] = 0f;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Vector2 projVelocity = Vector2.Zero;
                    while (Math.Abs(projVelocity.X) < 1.5f)
                    {
                        projVelocity = Vector2.UnitY.RotatedByRandom(1.5707963705062866) * new Vector2(5f, 3f);
                    }
                    fireProj(npc, projVelocity);
                }
            }
        }

        public static void AIElemental(NPC npc, ref float[] ai, bool? noDamageMode = null, int noDamageTimeMax = 120, bool gravityChange = true, bool tileCollideChange = true, float startPhaseDist = 800f, float stopPhaseDist = 600f, int idleTooLong = 180, float velSpeed = 2f)
        {
            int timerDummy = (int)npc.localAI[0];
            AIElemental(npc, ref ai, ref timerDummy, noDamageMode, noDamageTimeMax, gravityChange, tileCollideChange, startPhaseDist, stopPhaseDist, idleTooLong, velSpeed);
            npc.localAI[0] = timerDummy;
        }

        /*
		 * A cleaned up (and edited) copy of Elemental AI. (aiStyle 91) (Granite Elemental, etc.)
		 *
		 * idleTimer : A localized value, which is randomly ticked up to 5.
		 * noDamageMode : A bool?. Set to true to force on no damage mode, set to false to force it off, return null to have it only on in expert.
		 * noDamageTimeMax : The maximum amount of ticks before no damage mode returns to normal. (default 120)
		 * gravityChange : if true, npc.noGravity is changed during immortality states. If false, nothing is changed.
		 * tileCollideChange : if true, npc.noTileCollide is changed between phasing through tiles and not. If false, nothing is changed.
		 * startPhaseDist : the distance at which the npc begins phasing through tiles to get near the player. (default 800)
		 * stopPhaseDist : The distance at which the npc stops phasing through tiles to get near the player. (default 600)
		 * idleTooLong : The maximum amount of ticks the npc can be 'idle' before it attempts to change movement modes. (default 180)
		 * velSpeed : The speed of the entity when moving to the player. This value is used for all states; changing it speeds or slows the npc in all of them.
		 */
        public static void AIElemental(NPC npc, ref float[] ai, ref int idleTimer, bool? noDamageMode = null, int noDamageTimeMax = 120, bool gravityChange = true, bool tileCollideChange = true, float startPhaseDist = 800f, float stopPhaseDist = 600f, int idleTooLong = 180, float velSpeed = 2f)
        {
            bool noDmg = noDamageMode == null ? Main.expertMode : (bool)noDamageMode;
            if (gravityChange) npc.noGravity = true;
            if (tileCollideChange) npc.noTileCollide = false;
            if (noDmg) npc.dontTakeDamage = false;
            Player targetPlayer = npc.target < 0 ? null : Main.player[npc.target];
            Vector2 playerCenter = targetPlayer == null ? npc.Center + new Vector2(0, 5f) : targetPlayer.Center;

            if (npc.justHit && Main.netMode != NetmodeID.MultiplayerClient && noDmg && Main.rand.NextBool(6))
            {
                npc.netUpdate = true;
                ai[0] = -1f;
                ai[1] = 0f;
            }
            if (ai[0] == -1f) //immortal
            {
                if (noDmg) npc.dontTakeDamage = true;
                if (gravityChange) npc.noGravity = false;
                npc.velocity.X *= 0.98f;
                ai[1] += 1f;
                if (ai[1] >= noDamageTimeMax)
                {
                    ai[0] = ai[1] = ai[2] = ai[3] = 0f;
                }
            }
            else if (ai[0] == 0f) //targeting mode (chosing how to act)
            {
                npc.TargetClosest();
                targetPlayer = Main.player[npc.target];
                playerCenter = targetPlayer.Center;
                if (Collision.CanHit(npc.Center, 1, 1, playerCenter, 1, 1))
                {
                    ai[0] = 1f;
                    return;
                }
                Vector2 centerDiff = playerCenter - npc.Center;
                centerDiff.Y -= targetPlayer.height / 4;
                float centerDist = centerDiff.Length();
                if (centerDist > startPhaseDist)
                {
                    ai[0] = 2f;
                    return;
                }
                Vector2 npcCenter = npc.Center;
                npcCenter.X = playerCenter.X;
                Vector2 npcCentDiff = npcCenter - npc.Center;
                if (npcCentDiff.Length() > 8f && Collision.CanHit(npc.Center, 1, 1, npcCenter, 1, 1))
                {
                    ai[0] = 3f;
                    ai[1] = npcCenter.X;
                    ai[2] = npcCenter.Y;
                    Vector2 npcCenter2 = npc.Center;
                    npcCenter2.Y = playerCenter.Y;
                    if (npcCentDiff.Length() > 8f && Collision.CanHit(npc.Center, 1, 1, npcCenter2, 1, 1) && Collision.CanHit(npcCenter2, 1, 1, targetPlayer.position, 1, 1))
                    {
                        ai[0] = 3f;
                        ai[1] = npcCenter2.X;
                        ai[2] = npcCenter2.Y;
                    }
                }
                else
                {
                    npcCenter = npc.Center;
                    npcCenter.Y = playerCenter.Y;
                    if ((npcCenter - npc.Center).Length() > 8f && Collision.CanHit(npc.Center, 1, 1, npcCenter, 1, 1))
                    {
                        ai[0] = 3f;
                        ai[1] = npcCenter.X;
                        ai[2] = npcCenter.Y;
                    }
                }
                if (ai[0] == 0f)
                {
                    npc.localAI[0] = 0f;
                    centerDiff = centerDiff.SafeNormalize(Vector2.Zero);
                    centerDiff *= 0.5f;
                    npc.velocity += centerDiff;
                    ai[0] = 4f;
                    ai[1] = 0f;
                }
            }
            else if (ai[0] == 1f) //move to player
            {
                Vector2 distDiff = playerCenter - npc.Center;
                float distLength = distDiff.Length();
                float velSpeed2 = velSpeed; velSpeed2 += distLength / 200f;
                float speedAdjuster = 50f;
                distDiff = distDiff.SafeNormalize(Vector2.Zero);
                distDiff *= velSpeed2;
                npc.velocity = (npc.velocity * (speedAdjuster - 1) + distDiff) / speedAdjuster;
                if (!Collision.CanHit(npc.Center, 1, 1, playerCenter, 1, 1))
                {
                    ai[0] = 0f;
                    ai[1] = 0f;
                }
            }
            else if (ai[0] == 2f) //phase slowly through tiles to player
            {
                npc.noTileCollide = true;
                Vector2 distDiff = playerCenter - npc.Center;
                float distLength = distDiff.Length();
                float velSpeedPhase = velSpeed;
                float speedAdjusterPhase = 4f;
                distDiff = distDiff.SafeNormalize(Vector2.Zero);
                distDiff *= velSpeedPhase;
                npc.velocity = (npc.velocity * (speedAdjusterPhase - 1) + distDiff) / speedAdjusterPhase;
                if (distLength < stopPhaseDist && !Collision.SolidCollision(npc.position, npc.width, npc.height))
                {
                    ai[0] = 0f;
                }
            }
            else if (ai[0] == 3f) // horizontal floating to player
            {
                Vector2 targetLoc = new(ai[1], ai[2]);
                Vector2 targetDiff = targetLoc - npc.Center;
                float targetLength = targetDiff.Length();
                float velSpeedHorizontal = velSpeed < 1f ? velSpeed * 0.5f : Math.Max(0.1f, velSpeed - 1f);
                float speedAdjusterHorizontal = 3f;
                targetDiff = targetDiff.SafeNormalize(Vector2.Zero);
                targetDiff *= velSpeedHorizontal;
                npc.velocity = (npc.velocity * (speedAdjusterHorizontal - 1f) + targetDiff) / speedAdjusterHorizontal;
                if (npc.collideX || npc.collideY)
                {
                    ai[0] = 4f;
                    ai[1] = 0f;
                }
                if (targetLength < velSpeedHorizontal || targetLength > startPhaseDist || Collision.CanHit(npc.Center, 1, 1, playerCenter, 1, 1))
                {
                    ai[0] = 0f;
                }
            }
            else if (ai[0] == 4f) //idle floating
            {
                if (npc.collideX) npc.velocity.X *= -0.8f;
                if (npc.collideY) npc.velocity.Y *= -0.8f;
                Vector2 velVec;
                if (npc.velocity.X == 0f && npc.velocity.Y == 0f)
                {
                    velVec = playerCenter - npc.Center;
                    velVec.Y -= targetPlayer.height / 4;
                    velVec = velVec.SafeNormalize(Vector2.Zero);
                    npc.velocity = velVec * 0.1f;
                }
                float velSpeedIdle = velSpeed < 1f ? velSpeed * 0.75f : Math.Max(0.1f, velSpeed - 0.5f);
                float speedAdjusterIdle = 20f;
                velVec = npc.velocity;
                velVec = velVec.SafeNormalize(Vector2.Zero);
                velVec *= velSpeedIdle;
                npc.velocity = (npc.velocity * (speedAdjusterIdle - 1f) + velVec) / speedAdjusterIdle;
                ai[1] += 1f;
                if (ai[1] > idleTooLong)
                {
                    ai[0] = 0f;
                    ai[1] = 0f;
                }
                if (Collision.CanHit(npc.Center, 1, 1, playerCenter, 1, 1))
                {
                    ai[0] = 0f;
                }
                idleTimer += 1;
                if (idleTimer >= 5 && !Collision.SolidCollision(npc.position - new Vector2(10f, 10f), npc.width + 20, npc.height + 20))
                {
                    idleTimer = 0;
                    Vector2 npcCenter = npc.Center;
                    npcCenter.X = playerCenter.X;
                    if (Collision.CanHit(npc.Center, 1, 1, npcCenter, 1, 1) && Collision.CanHit(npc.Center, 1, 1, npcCenter, 1, 1) && Collision.CanHit(playerCenter, 1, 1, npcCenter, 1, 1))
                    {
                        ai[0] = 3f;
                        ai[1] = npcCenter.X;
                        ai[2] = npcCenter.Y;
                        return;
                    }
                    npcCenter = npc.Center;
                    npcCenter.Y = playerCenter.Y;
                    if (Collision.CanHit(npc.Center, 1, 1, npcCenter, 1, 1) && Collision.CanHit(playerCenter, 1, 1, npcCenter, 1, 1))
                    {
                        ai[0] = 3f;
                        ai[1] = npcCenter.X;
                        ai[2] = npcCenter.Y;
                    }
                }
            }
        }

        public static void AISpore(NPC npc, ref float[] ai, float moveIntervalX = 0.1f, float moveIntervalY = 0.02f, float maxSpeedX = 5f, float maxSpeedY = 1f)
        {
            npc.TargetClosest();
            AISpore(npc, ref ai, Main.player[npc.target].Center, Main.player[npc.target].width, moveIntervalX, moveIntervalY, maxSpeedX, maxSpeedY);
        }

        /*
         * A cleaned up (and edited) copy of Spore AI. (Fungi Spore, Plantera Spore, etc.) (AIStyle 50)
         *
         * ai : A float array that stores AI data. (Note projectile array should be synced!)
		 * target : The center of the target.
		 * targetWidth : The width of the target.
		 * moveIntervalX : The amount to move by on the X axis each tick.
		 * moveIntervalY : The amount to move by on the Y axis each tick.
		 * maxSpeedX : The maximum speed of the codable on the X axis.
		 * maxSpeedY : The maximum speed of the codable on the Y axis.
         */
        public static void AISpore(Entity codable, ref float[] ai, Vector2 target, int targetWidth = 16, float moveIntervalX = 0.1f, float moveIntervalY = 0.02f, float maxSpeedX = 5f, float maxSpeedY = 1f)
        {
            codable.velocity.Y += moveIntervalY;
            if (codable.velocity.Y < 0f) codable.velocity.Y *= 0.99f;
            if (codable.velocity.Y > 1f) codable.velocity.Y = 1f;
            int widthHalf = targetWidth / 2;
            if (codable.position.X + codable.width < target.X - widthHalf)
            {
                if (codable.velocity.X < 0) codable.velocity.X *= 0.98f;
                codable.velocity.X += moveIntervalX;
            }
            else if (codable.position.X > target.X + widthHalf)
            {
                if (codable.velocity.X > 0) codable.velocity.X *= 0.98f;
                codable.velocity.X -= moveIntervalX;
            }
            if (codable.velocity.X > maxSpeedX || codable.velocity.X < -maxSpeedX) codable.velocity.X *= 0.97f;
        }

        /*
		 * *UNTESTED, MAY NOT WORK PROPERLY*
		 *
         * A cleaned up (and edited) copy of Charger AI. (Unicorns, wolves, etc.) (AIStyle 26)
         *
         * ai : A float array that stores AI data. (Note projectile array should be synced!)
         * moveInterval : How much to move each tick.
		 * maxSpeed : The maxium speed the npc can move per tick.
         * allowBoredom : If false, npc will not get 'bored' trying to harass a target and wander off.
         * ticksUntilBoredom : the amount of ticks until the npc gets 'bored' following a target.
         */
        public static void AICharger(NPC npc, ref float[] ai, float moveInterval = 0.07f, float maxSpeed = 6f, bool allowBoredom = true, int ticksUntilBoredom = 30)
        {
            bool isMoving = false;
            if (npc.velocity.Y == 0f && (npc.velocity.X > 0f && npc.direction < 0 || npc.velocity.X < 0f && npc.direction > 0))
            {
                isMoving = true;
                ++ai[3];
            }
            if (npc.position.X == npc.oldPosition.X || ai[3] >= ticksUntilBoredom || isMoving) ++ai[3];
            else if (ai[3] > 0f) --ai[3];
            if (ai[3] > ticksUntilBoredom * 10) ai[3] = 0f;
            if (npc.justHit) ai[3] = 0f;
            if (ai[3] == ticksUntilBoredom) npc.netUpdate = true;
            Vector2 npcCenter = npc.Center;
            float distX = Main.player[npc.target].Center.X - npcCenter.X;
            float distY = Main.player[npc.target].Center.Y - npcCenter.Y;
            float dist = (float)Math.Sqrt(distX * distX + distY * distY);
            if (dist < 200f) ai[3] = 0f;
            if (!allowBoredom || ai[3] < ticksUntilBoredom)
            {
                npc.TargetClosest();
            }
            else
            {
                if (npc.velocity.X == 0f)
                {
                    if (npc.velocity.Y == 0f)
                    {
                        ++ai[0];
                        if (ai[0] >= 2.0) { npc.direction *= -1; npc.spriteDirection = npc.direction; ai[0] = 0f; }
                    }
                }
                else ai[0] = 0f;
                npc.directionY = -1;
                if (npc.direction == 0) npc.direction = 1;
            }
            if (npc.velocity.Y == 0f || npc.wet || npc.velocity.X <= 0f && npc.direction < 0 || npc.velocity.X >= 0f && npc.direction > 0)
            {
                if (npc.velocity.X < -maxSpeed || npc.velocity.X > maxSpeed)
                {
                    if (npc.velocity.Y == 0f) npc.velocity *= 0.8f;
                }
                else if (npc.velocity.X < maxSpeed && npc.direction == 1)
                {
                    npc.velocity.X += moveInterval;
                    if (npc.velocity.X > maxSpeed) npc.velocity.X = maxSpeed;
                }
                else if (npc.velocity.X > -maxSpeed && npc.direction == -1)
                {
                    npc.velocity.X -= moveInterval;
                    if (npc.velocity.X < -maxSpeed) npc.velocity.X = -maxSpeed;
                }
            }
        }

        /*
		 * A cleaned up (and edited) copy of Eater of Souls AI. (EoS, Corruptor, etc.) (AIStyle 5)
		 *
		 * ai : A float array that stores AI data. (Note projectile array should be synced!)
		 * moveInterval : how much to move each tick.
		 * distanceDivider : The amount that is divided by the distance; determines velocity.
		 * bounceScalar : scalar for how big a 'bounce' is if the npc hits a tile.
		 * fleeAtDay : If true, npc will flee if it becomes day.
		 * ignoreWet : If true, npc will ignore being wet.
		 */
        public static void AIEater(NPC npc, ref float[] ai, float moveInterval = 0.022f, float distanceDivider = 4.2f, float bounceScalar = 0.7f, bool fleeAtDay = false, bool ignoreWet = false)
        {
            if (npc.target is < 0 or byte.MaxValue || Main.player[npc.target].dead) { npc.TargetClosest(); }
            float distX = Main.player[npc.target].Center.X;
            float distY = Main.player[npc.target].Center.Y;
            Vector2 npcCenter = npc.Center;
            float distDx = (int)(distX / 8f) * 8f;
            float distDy = (int)(distY / 8f) * 8f;
            npcCenter.X = (int)(npcCenter.X / 8f) * 8f;
            npcCenter.Y = (int)(npcCenter.Y / 8f) * 8f;
            float distX2 = distDx - npcCenter.X;
            float distY2 = distDy - npcCenter.Y;
            float dist = (float)Math.Sqrt(distX2 * distX2 + distY2 * distY2);
            float speedX1;
            float speedY1;
            if (dist == 0f)
            {
                speedX1 = npc.velocity.X;
                speedY1 = npc.velocity.Y;
            }
            else
            {
                float distScalar = distanceDivider / dist;
                speedX1 = distX2 * distScalar;
                speedY1 = distY2 * distScalar;
            }
            ++ai[0];
            if (ai[0] > 0f) { npc.velocity.Y += 23f / 1000f; } else { npc.velocity.Y -= 23f / 1000f; }
            if (ai[0] < -100f || (double)ai[0] > 100f) { npc.velocity.X += 23f / 1000f; } else { npc.velocity.X -= 23f / 1000f; }
            if (ai[0] > 200f) { ai[0] = -200f; }
            if (dist < 150f) { npc.velocity.X += speedX1 * 0.007f; npc.velocity.Y += speedY1 * 0.007f; }
            if (Main.player[npc.target].dead)
            {
                speedX1 = npc.direction * distanceDivider / 2f;
                speedY1 = -distanceDivider / 2f;
            }
            if (npc.velocity.X < speedX1) { npc.velocity.X += moveInterval; }
            else
            if (npc.velocity.X > speedX1) { npc.velocity.X -= moveInterval; }
            if (npc.velocity.Y < speedY1) { npc.velocity.Y += moveInterval; }
            else
            if (npc.velocity.Y > speedY1) { npc.velocity.Y -= moveInterval; }
            npc.rotation = (float)Math.Atan2(speedY1, speedX1) - 1.57f;
            if (npc.collideX)
            {
                npc.netUpdate = true;
                npc.velocity.X = npc.oldVelocity.X * -bounceScalar;
                if (npc.direction == -1 && npc.velocity.X is > 0f and < 2f) { npc.velocity.X = 2f; }
                if (npc.direction == 1 && npc.velocity.X is < 0f and > -2f) { npc.velocity.X = -2f; }
            }
            if (npc.collideY)
            {
                npc.netUpdate = true;
                npc.velocity.Y = npc.oldVelocity.Y * -bounceScalar;
                if (npc.velocity.Y is > 0f and < 1.5f) { npc.velocity.Y = 2f; }
                if (npc.velocity.Y is < 0f and > -1.5f) { npc.velocity.Y = -2f; }
            }
            if (!ignoreWet && npc.wet)
            {
                if (npc.velocity.Y > 0f) { npc.velocity.Y *= 0.95f; }
                npc.velocity.Y -= 0.3f;
                if (npc.velocity.Y < -2f) { npc.velocity.Y = -2f; }
            }
            if (fleeAtDay && Main.dayTime || Main.player[npc.target].dead)
            {
                npc.velocity.Y -= moveInterval * 2f;
                if (npc.timeLeft > 10) { npc.timeLeft = 10; }
            }
            if ((npc.velocity.X <= 0f || npc.oldVelocity.X >= 0f) && (npc.velocity.X >= 0f || npc.oldVelocity.X <= 0f) && (npc.velocity.Y <= 0f || npc.oldVelocity.Y >= 0f) && (npc.velocity.Y >= 0.0 || npc.oldVelocity.Y <= 0f) || npc.justHit)
                return;
            npc.netUpdate = true;
        }

        /*
		 * A cleaned up (and edited) copy of Skull AI. (Cursed Skull) (AIStyle 10)
		 *
		 * ai : A float array that stores AI data. (Note projectile array should be synced!)
		 * tacklePlayer : If true, the npc will occasionally charge at the player.
		 * maxDistanceAmt : The maxmimum amount of 'distance' the npc is allowed to wander to from the player.
		 * maxDistance : The maximum amount of distance from the player until the npc begins to speed up.
		 * increment : the amount to move per tick.
		 * closeIncrement : the amount to move per tick when close to the player.
		 */
        public static void AISkull(NPC npc, ref float[] ai, bool tacklePlayer = true, float maxDistanceAmt = 4f, float maxDistance = 350f, float increment = 0.011f, float closeIncrement = 0.019f)
        {
            float distanceAmt = 1f;
            npc.TargetClosest();
            float distX = Main.player[npc.target].Center.X - npc.Center.X;
            float distY = Main.player[npc.target].Center.Y - npc.Center.Y;
            float dist = (float)Math.Sqrt(distX * distX + distY * distY);
            ai[1] += 1f;
            if (ai[1] > 600f)
            {
                if (tacklePlayer)
                {
                    increment *= 8f;
                    distanceAmt = 4f;
                    if (ai[1] > 650f) { ai[1] = 0f; }
                }
                else { ai[1] = 0f; }
            }
            else
            if (dist < 250f)
            {
                ai[0] += 0.9f;
                if (ai[0] > 0f) { npc.velocity.Y += closeIncrement; } else { npc.velocity.Y -= closeIncrement; }
                if (ai[0] < -100f || ai[0] > 100f) { npc.velocity.X += closeIncrement; } else { npc.velocity.X -= closeIncrement; }
                if (ai[0] > 200f) { ai[0] = -200f; }
            }
            if (dist > maxDistance)
            {
                distanceAmt = maxDistanceAmt + maxDistanceAmt / 4f;
                increment = 0.3f;
            }
            else
            if (dist > maxDistance - maxDistance / 7f)
            {
                distanceAmt = maxDistanceAmt - maxDistanceAmt / 4f;
                increment = 0.2f;
            }
            else
            if (dist > maxDistance - 2 * (maxDistance / 7f))
            {
                distanceAmt = maxDistanceAmt / 2.66f;
                increment = 0.1f;
            }
            dist = distanceAmt / dist;
            distX *= dist; distY *= dist;
            if (Main.player[npc.target].dead)
            {
                distX = npc.direction * distanceAmt / 2f;
                distY = -distanceAmt / 2f;
            }
            if (npc.velocity.X < distX) { npc.velocity.X += increment; }
            else
            if (npc.velocity.X > distX) { npc.velocity.X -= increment; }
            if (npc.velocity.Y < distY) { npc.velocity.Y += increment; }
            else
            if (npc.velocity.Y > distY) { npc.velocity.Y -= increment; }
        }

        /*
		 * A cleaned up (and edited) copy of Floater AI. (Pixie, Gastropod, etc.) (AIStyle 22)
		 *
		 * ai : A float array that stores AI data. (Note projectile array should be synced!)
		 * ignoreWet : If true, does not slow down in liquids.
		 * moveInterval : how much to move each tick.
		 * maxSpeedX/maxSpeedY : the max speed of the npc on the X and Y axis, respectively.
		 * hoverInterval : how much to hover by each tick.
		 * hoverMaxSpeed : the maximum speed to hover by.
		 * hoverHeight : the amount of tiles below an npc before it needs ground to hover over.
		 */
        public static void AIFloater(NPC npc, Entity target, ref float[] ai, bool ignoreWet = false, float moveInterval = 0.2f, float maxSpeedX = 2f, float maxSpeedY = 1.5f, float hoverInterval = 0.04f, float hoverMaxSpeed = 1.5f, int hoverHeight = 3)
        {
            bool flyUpward = false;
            if (npc.justHit)
                ai[2] = 0f;
            if (ai[2] >= 0f)
            {
                int tileDist = 16;
                bool inRangeX = false;
                bool inRangeY = false;
                if (npc.position.X > ai[0] - tileDist && npc.position.X < ai[0] + tileDist)
                    inRangeX = true;
                else
                {
                    if (npc.velocity.X < 0f && npc.direction > 0 || npc.velocity.X > 0f && npc.direction < 0)
                        inRangeX = true;
                }
                tileDist += 24;
                if (npc.position.Y > ai[1] - tileDist && npc.position.Y < ai[1] + tileDist)
                {
                    inRangeY = true;
                }
                if (inRangeX && inRangeY)
                {
                    ai[2] += 1f;
                    //i'm pretty sure projectile is never called, but it's in the original so...
                    if (ai[2] >= 30f && tileDist == 16)
                    {
                        flyUpward = true;
                    }
                    if (ai[2] >= 60f)
                    {
                        ai[2] = -200f;
                        npc.direction *= -1;
                        npc.velocity.X *= -1f;
                        npc.collideX = false;
                    }
                }
                else
                {
                    ai[0] = npc.position.X;
                    ai[1] = npc.position.Y;
                    ai[2] = 0f;
                }

                npc.direction = target.Center.X > npc.Center.X ? -1 : 1;
                npc.directionY = target.Center.Y > npc.Center.Y ? -1 : 1;
            }
            else
            {
                ai[2] += 1f;
                if (target.Center.X > npc.Center.X)
                {
                    npc.direction = -1;
                }
                else
                {
                    npc.direction = 1;
                }
            }
            int tileX = (int)(npc.Center.X / 16f) + npc.direction * 2;
            int tileY = (int)((npc.position.Y + npc.height) / 16f);
            bool tileBelowEmpty = true;
            for (int tY = tileY; tY < tileY + hoverHeight; tY++)
            {
                if (Framing.GetTileSafely(tileX, tY).HasUnactuatedTile && Main.tileSolid[Framing.GetTileSafely(tileX, tY).TileType] || Framing.GetTileSafely(tileX, tY).LiquidAmount > 0)
                {
                    tileBelowEmpty = false;
                    break;
                }
            }
            if (flyUpward)
            {
                tileBelowEmpty = true;
            }
            if (tileBelowEmpty)
            {
                npc.velocity.Y += moveInterval;
                if (npc.velocity.Y > maxSpeedY)
                    npc.velocity.Y = maxSpeedY;
            }
            else
            {
                if (npc.directionY < 0 && npc.velocity.Y > 0f)
                    npc.velocity.Y -= moveInterval;
                if (npc.velocity.Y < -maxSpeedY)
                    npc.velocity.Y = -maxSpeedY;
            }
            if (!ignoreWet && npc.wet)
            {
                npc.velocity.Y -= moveInterval;
                if (npc.velocity.Y < -maxSpeedY * 0.75f) { npc.velocity.Y = -maxSpeedY * 0.75f; }
            }
            if (npc.collideX)
            {
                npc.velocity.X = npc.oldVelocity.X * -0.4f;
                if (npc.direction == -1 && npc.velocity.X is > 0f and < 1f) { npc.velocity.X = 1f; }
                if (npc.direction == 1 && npc.velocity.X is < 0f and > -1f) { npc.velocity.X = -1f; }
            }
            if (npc.collideY)
            {
                npc.velocity.Y = npc.oldVelocity.Y * -0.25f;
                if (npc.velocity.Y is > 0f and < 1f) { npc.velocity.Y = 1f; }
                if (npc.velocity.Y is < 0f and > -1f) { npc.velocity.Y = -1f; }
            }
            if (npc.direction == -1 && npc.velocity.X > -maxSpeedX)
            {
                npc.velocity.X -= moveInterval * 0.5f;
                if (npc.velocity.X > maxSpeedX) { npc.velocity.X -= 0.1f; }
                else
                    if (npc.velocity.X > 0f) { npc.velocity.X += 0.05f; }
                if (npc.velocity.X < -maxSpeedX) { npc.velocity.X = -maxSpeedX; }
            }
            else
                if (npc.direction == 1 && npc.velocity.X < maxSpeedX)
            {
                npc.velocity.X += moveInterval * 0.5f;
                if (npc.velocity.X < -maxSpeedX) { npc.velocity.X += 0.1f; }
                else
                    if (npc.velocity.X < 0f) { npc.velocity.X -= 0.05f; }
                if (npc.velocity.X > maxSpeedX) { npc.velocity.X = maxSpeedX; }
            }
            if (npc.directionY == -1 && (double)npc.velocity.Y > -hoverMaxSpeed)
            {
                npc.velocity.Y -= hoverInterval;
                if ((double)npc.velocity.Y > hoverMaxSpeed) { npc.velocity.Y -= 0.05f; }
                else
                    if (npc.velocity.Y > 0f) { npc.velocity.Y += hoverInterval - 0.01f; }
                if ((double)npc.velocity.Y < -hoverMaxSpeed) { npc.velocity.Y = -hoverMaxSpeed; }
            }
            else
                if (npc.directionY == 1 && (double)npc.velocity.Y < hoverMaxSpeed)
            {
                npc.velocity.Y += hoverInterval;
                if ((double)npc.velocity.Y < -hoverMaxSpeed) { npc.velocity.Y += 0.05f; }
                else
                    if (npc.velocity.Y < 0f) { npc.velocity.Y -= hoverInterval - 0.01f; }
                if ((double)npc.velocity.Y > hoverMaxSpeed) { npc.velocity.Y = hoverMaxSpeed; }
            }
        }

        /*
		 * A cleaned up (and edited) copy of Flier AI. (Bat, Demon, etc.) (AIStyle 14)
		 * 
		 * ai : A float array that stores AI data. (Note projectile array should be synced!)
		 * sporadic : If true, npc will overshoot targets.
		 * maxSpeedX/maxSpeedY : the max speed of the npc on the X and Y axis, respectively.
		 * slowdownIncrementX/slowdownIncrementY : the slowdown increment on the X and Y axis, respectively.
		 */
        public static void AIFlier(NPC npc, ref float[] ai, bool sporadic = true, float moveIntervalX = 0.1f, float moveIntervalY = 0.04f, float maxSpeedX = 4f, float maxSpeedY = 1.5f, bool canBeBored = true, int timeUntilBoredom = 300)
        {
            if (npc.collideX)
            {
                npc.velocity.X = npc.oldVelocity.X * -0.5f;
                float max = maxSpeedX * 0.5f;
                if (npc.direction == -1 && npc.velocity.X > 0f && npc.velocity.X < max) { npc.velocity.X = max; }
                if (npc.direction == 1 && npc.velocity.X < 0f && npc.velocity.X > -max) { npc.velocity.X = -max; }
            }
            if (npc.collideY)
            {
                npc.velocity.Y = npc.oldVelocity.Y * -0.5f;
                float max = maxSpeedY * 0.66f;
                if (npc.velocity.Y > 0f && npc.velocity.Y < max) { npc.velocity.Y = max; }
                if (npc.velocity.Y < 0f && npc.velocity.Y > -max) { npc.velocity.Y = -max; }
            }
            npc.TargetClosest(true);
            Action move = () =>
            {
                if (npc.direction == -1 && npc.velocity.X > -maxSpeedX)
                {
                    npc.velocity.X -= moveIntervalX;
                    if (npc.velocity.X > maxSpeedX) { npc.velocity.X -= moveIntervalX; }
                    else
                    if (npc.velocity.X > 0f) { npc.velocity.X += moveIntervalX * 0.5f; }
                    if (npc.velocity.X < -maxSpeedX) { npc.velocity.X = -maxSpeedX; }
                }
                else
                if (npc.direction == 1 && npc.velocity.X < maxSpeedX)
                {
                    npc.velocity.X += moveIntervalX;
                    if (npc.velocity.X < -maxSpeedX) { npc.velocity.X += moveIntervalX; }
                    else
                    if (npc.velocity.X < 0f) { npc.velocity.X -= moveIntervalX * 0.5f; }
                    if (npc.velocity.X > maxSpeedX) { npc.velocity.X = maxSpeedX; }
                }
                if (npc.directionY == -1 && (double)npc.velocity.Y > -maxSpeedY)
                {
                    npc.velocity.Y -= moveIntervalY;
                    if ((double)npc.velocity.Y > maxSpeedY) { npc.velocity.Y -= moveIntervalY; }
                    else
                    if (npc.velocity.Y > 0f) { npc.velocity.Y += moveIntervalY * 0.5f; }
                    if ((double)npc.velocity.Y < -maxSpeedY) { npc.velocity.Y = -maxSpeedY; }
                }
                else
                if (npc.directionY == 1 && (double)npc.velocity.Y < maxSpeedY)
                {
                    npc.velocity.Y += moveIntervalY;
                    if ((double)npc.velocity.Y < -maxSpeedY) { npc.velocity.Y += moveIntervalY; }
                    else
                    if (npc.velocity.Y < 0f) { npc.velocity.Y -= moveIntervalY * 0.5f; }
                    if ((double)npc.velocity.Y > maxSpeedY) { npc.velocity.Y = maxSpeedY; }
                }
            };
            if (canBeBored) { ai[0] += 1f; }
            if (canBeBored && ai[0] > timeUntilBoredom)
            {
                if (!Main.player[npc.target].wet && Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
                {
                    ai[0] = 0f;
                }
                if (ai[0] > timeUntilBoredom * 2) { ai[0] = 0f; }
                npc.direction = Main.player[npc.target].Center.X < npc.Center.X ? 1 : -1;
                npc.directionY = Main.player[npc.target].Center.Y < npc.Center.Y ? 1 : -1;
                move();
            }
            else
            {
                move();
                if (sporadic)
                {
                    if (npc.wet)
                    {
                        if (npc.velocity.Y > 0f) { npc.velocity.Y = npc.velocity.Y * 0.95f; }
                        npc.velocity.Y = npc.velocity.Y - 0.5f;
                        if (npc.velocity.Y < -maxSpeedX) { npc.velocity.Y = -maxSpeedX; }
                        npc.TargetClosest(true);
                    }
                    move();
                }
            }
        }

        /*
         * A cleaned up (and edited) copy of Fish AI. (Goldfish, Angler Fish, etc.)
         *
         * ai : A float array that stores AI data. (Note projectile array should be synced!)
         * hostile : If true, will target players.
         * ignoreNonWetPlayer : If false, npc will target players even if they are out of water.
         * ignoreWater : If true, npc will not be bound to water. (ie npc flies)
         * velMaxX/velMaxY : the max velocities on the X and Y axis, respectively.
         */
        public static void AIFish(NPC npc, ref float[] ai, bool hostile = false, bool ignoreNonWetPlayer = true, bool ignoreWater = false, float velMaxX = 3f, float velMaxY = 2f)
        {
            //if the npc is hostile and it has no direction, target the closest player.
            if (hostile && npc.direction == 0) { npc.TargetClosest(); }
            if (ignoreWater || npc.wet)//if wet or ignore water is true...
            {
                bool hasTarget = false;
                //if hostile, get a target and check that the player found is wet.
                if (hostile)
                {
                    npc.TargetClosest(false);
                    if ((!ignoreNonWetPlayer || Main.player[npc.target].wet) && !Main.player[npc.target].dead) { hasTarget = true; }
                }
                //if the target is wet or there is no target...
                if (!hasTarget)
                {
                    if (npc.collideX)
                    {
                        npc.velocity.X *= -1f;
                        npc.direction *= -1;
                        npc.netUpdate = true;
                    }
                    if (npc.collideY)
                    {
                        npc.netUpdate = true;
                        int mult = npc.velocity.Y > 0f ? -1 : 1;
                        npc.velocity.Y = Math.Abs(npc.velocity.Y) * mult;
                        npc.directionY = 1 * mult;
                        ai[0] = 1f * mult;
                    }
                }
                //if the npc has a target that fits the requirements, attempt to move toward that target.
                if (hasTarget)
                {
                    npc.TargetClosest();
                    npc.velocity.X += npc.direction * 0.1f;
                    npc.velocity.Y += npc.directionY * 0.1f;
                    if (npc.velocity.X > velMaxX) { npc.velocity.X = velMaxX; }
                    if (npc.velocity.X < -velMaxX) { npc.velocity.X = -velMaxX; }
                    if (npc.velocity.Y > velMaxY) { npc.velocity.Y = velMaxY; }
                    if (npc.velocity.Y < -velMaxY) { npc.velocity.Y = -velMaxY; }
                }
                else//otherwise, move horizontally, slowly bobbing up and down as well.
                {
                    npc.velocity.X += npc.direction * 0.1f;
                    if (npc.velocity.X is < -1f or > 1f) { npc.velocity.X *= 0.95f; }
                    if (ai[0] == -1f)
                    {
                        npc.velocity.Y -= 0.01f;
                        if (npc.velocity.Y < -0.3)
                        {
                            ai[0] = 1f;
                        }
                    }
                    else
                    {
                        npc.velocity.Y += 0.01f;
                        if (npc.velocity.Y > 0.3)
                        {
                            ai[0] = -1f;
                        }
                    }
                    int tileX = (int)(npc.Center.X / 16);
                    int tileY = (int)(npc.Center.Y / 16);
                    if (Main.tile[tileX, tileY - 1].LiquidAmount > 128)
                    {
                        if (Main.tile[tileX, tileY + 1].HasUnactuatedTile || Main.tile[tileX, tileY + 2].HasUnactuatedTile) { ai[0] = -1f; }
                    }
                    //if npc's y speed goes above max velocity, slow the npc down.
                    if (npc.velocity.Y > velMaxY || npc.velocity.Y < -velMaxY) { npc.velocity.Y *= 0.95f; }
                }
            }
            else
            {
                //if y velocity is 0, set the npc's velocity to a random number to get it started.
                if (Main.netMode != NetmodeID.MultiplayerClient && npc.velocity.Y == 0f)
                {
                    npc.velocity.Y = Main.rand.Next(-50, -20) * 0.1f;
                    npc.velocity.X = Main.rand.Next(-20, 20) * 0.1f;
                    npc.netUpdate = true;
                }
                npc.velocity.Y += 0.3f;
                if (npc.velocity.Y > 10f) { npc.velocity.Y = 10f; }
                ai[0] = 1f;
            }
            npc.rotation = npc.velocity.Y * npc.direction * 0.1f;
            if (npc.rotation < -0.2) { npc.rotation = -0.2f; }
            if (npc.rotation > 0.2) { npc.rotation = 0.2f; }
        }

        /*
         * A cleaned up (and edited) copy of Zombie AI. (Stripped Fighter AI)
         *
         * ai : A float array that stores AI data. (Note projectile array should be synced!)
         * fleeWhenDay : If true, flees when it is daytime.
         * allowBoredom : If false, npc will not get 'bored' trying to harass a target and wander off.
         * openDoors : -1 == do not interact with doors, 0 == go up to door but do not break it, 1 == attempt to break down doors, 2 == attempt to open doors.
         * velMaxX : the maximum velocity on the X axis.
         * maxJumpTilesX/maxJumpTilesY : The max tiles it can jump across and over, respectively.
         * ticksUntilBoredom : the amount of ticks until the npc gets 'bored' following a target.
         * targetPlayers : If false, will not target players actively.
         * doorBeatCounterMax : how many beat ticks until the door is opened/broken.
         * doorCounterMax : how many ticks to iterate doorBeatCounter.
         * jumpUpPlatforms : If true, the npc will jump up if a platform is above it and it is within jumping range.
         */
        public static void AIZombie(NPC npc, ref float[] ai, bool fleeWhenDay = true, bool allowBoredom = true, int openDoors = 1, float moveInterval = 0.07f, float velMax = 1f, int maxJumpTilesX = 3, int maxJumpTilesY = 4, int ticksUntilBoredom = 60, bool targetPlayers = true, int doorBeatCounterMax = 10, int doorCounterMax = 60, bool jumpUpPlatforms = false, Action<bool, bool, Vector2, Vector2> onTileCollide = null, bool ignoreJumpTiles = false)
        {
            bool xVelocityChanged = false;
            //This block of code checks for major X velocity/directional changes as well as periodically updates the npc.
            if (npc.velocity.Y == 0f && (npc.velocity.X > 0f && npc.direction < 0 || npc.velocity.X < 0f && npc.direction > 0))
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
            if (targetPlayers && (!fleeWhenDay || !Main.dayTime || npc.position.Y > Main.worldSurface * 16.0) && (fleeWhenDay && Main.dayTime ? notBored : !allowBoredom || notBored))
            {
                npc.TargetClosest();
            }
            else
            if (ai[2] <= 0f)//if 'bored'
            {
                if (fleeWhenDay && Main.dayTime && npc.position.Y / 16f < Main.worldSurface && npc.timeLeft > 10)
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
            WalkupHalfBricks(npc);
            //if allowed to open doors and is currently doing so, reduce npc velocity on the X axis to 0. (so it stops moving)
            if (openDoors != -1 && AttemptOpenDoor(npc, ref ai[1], ref ai[2], ref ai[3], ticksUntilBoredom, doorBeatCounterMax, doorCounterMax, openDoors))
            {
                npc.velocity.X = 0;
            }
            else //if no door to open, reset ai.
            if (openDoors != -1) { ai[1] = 0f; ai[2] = 0f; }
            //if there's a solid floor under us...
            if (HitTileOnSide(npc, 3))
            {
                //if the npc's velocity is going in the same direction as the npc's direction...
                if (npc.velocity.X < 0f && npc.direction == -1 || npc.velocity.X > 0f && npc.direction == 1)
                {
                    //...attempt to jump if needed.
                    Vector2 newVec = AttemptJump(npc.position, npc.velocity, npc.width, npc.height, npc.direction, npc.directionY, maxJumpTilesX, maxJumpTilesY, velMax, jumpUpPlatforms, jumpUpPlatforms && notBored ? Main.player[npc.target] : null, ignoreJumpTiles);
                    if (!npc.noTileCollide)
                    {
                        newVec = Collision.TileCollision(npc.position, newVec, npc.width, npc.height);
                        Vector4 slopeVec = Collision.SlopeCollision(npc.position, newVec, npc.width, npc.height);
                        Vector2 slopeVel = new(slopeVec.Z, slopeVec.W);
                        if (onTileCollide != null && npc.velocity != slopeVel) onTileCollide(npc.velocity.X != slopeVel.X, npc.velocity.Y != slopeVel.Y, npc.velocity, slopeVel);
                        npc.position = new Vector2(slopeVec.X, slopeVec.Y);
                        npc.velocity = slopeVel;
                    }
                    if (npc.velocity != newVec) { npc.velocity = newVec; npc.netUpdate = true; }
                }
            }
        }

        /*
         * A cleaned up copy of Demon Eye AI. (Flier AI)
         *
         * ai : A float array that stores AI data. (Note projectile array should be synced!)
         * fleeWhenDay : If true, npc will lose interest in players and fly away.
         * ignoreWet : If true, ignores code for forcing the npc out of water.
         * velMaxX, velMaxY : the maximum velocity on the X and Y axis, respectively.
         * bounceScalarX, bounceScalarY : scalars to increase the amount of velocity from bouncing on the X and Y axis, respectively.
         */
        public static void AIEye(NPC npc, ref float[] ai, bool fleeWhenDay = true, bool ignoreWet = false, float moveIntervalX = 0.1f, float moveIntervalY = 0.04f, float velMaxX = 4f, float velMaxY = 1.5f, float bounceScalarX = 1f, float bounceScalarY = 1f)
        {
            //controls the npc's bouncing when it hits a wall.
            if (npc.collideX)
            {
                npc.velocity.X = npc.oldVelocity.X * -0.5f;
                if (npc.direction == -1 && npc.velocity.X is > 0f and < 2f) { npc.velocity.X = 2f; }
                if (npc.direction == 1 && npc.velocity.X is < 0f and > -2f) { npc.velocity.X = -2f; }
                npc.velocity.X *= bounceScalarX;
            }
            //controls the npc's bouncing when it hits a floor or ceiling.
            if (npc.collideY)
            {
                npc.velocity.Y = npc.oldVelocity.Y * -0.5f;
                if (npc.velocity.Y is > 0f and < 1f) { npc.velocity.Y = 1f; }
                if (npc.velocity.Y is < 0f and > -1f) { npc.velocity.Y = -1f; }
                npc.velocity.Y *= bounceScalarY;
            }
            //if it should flee when it's day, and it is day, the npc's position is at or above the surface, it will flee.
            if (fleeWhenDay && Main.dayTime && npc.position.Y <= Main.worldSurface * 16.0)
            {
                if (npc.timeLeft > 10) { npc.timeLeft = 10; }
                npc.directionY = -1;
                if (npc.velocity.Y > 0f) { npc.direction = 1; }
                npc.direction = -1;
                if (npc.velocity.X > 0f) { npc.direction = 1; }
            }
            else
            {
                npc.TargetClosest();
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
                npc.TargetClosest();
            }
        }

        #endregion

        #region Vanilla NPC AI Code Excerpts
        //Code Excerpts are pieces of code from vanilla AI that were converted into standalone methods.

        public static void WalkupHalfBricks(NPC npc)
        {
            WalkupHalfBricks(npc, ref npc.gfxOffY, ref npc.stepSpeed);
        }

        /*
		 *  Code based on vanilla halfbrick walkup code, checks for and attempts to walk over half tiles.
		 */
        private static void WalkupHalfBricks(Entity codable, ref float gfxOffY, ref float stepSpeed)
        {
            if (codable == null)
                return;
            if (codable.velocity.Y >= 0f)
            {
                int offset = 0;
                if (codable.velocity.X < 0f) offset = -1;
                if (codable.velocity.X > 0f) offset = 1;
                Vector2 pos = codable.position;
                pos.X += codable.velocity.X;
                int tileX = (int)((pos.X + (double)(codable.width / 2) + (codable.width / 2 + 1) * offset) / 16.0);
                int tileY = (int)((pos.Y + (double)codable.height - 1.0) / 16.0);

                if (tileX * 16 < pos.X + (double)codable.width && tileX * 16 + 16 > (double)pos.X && (Main.tile[tileX, tileY].HasUnactuatedTile && Main.tile[tileX, tileY].Slope == 0 && Main.tile[tileX, tileY - 1].Slope == 0 && Main.tileSolid[Main.tile[tileX, tileY].TileType] && !Main.tileSolidTop[Main.tile[tileX, tileY].TileType] || Main.tile[tileX, tileY - 1].IsHalfBlock && Main.tile[tileX, tileY - 1].HasUnactuatedTile) && (!Main.tile[tileX, tileY - 1].HasUnactuatedTile || !Main.tileSolid[Main.tile[tileX, tileY - 1].TileType] || Main.tileSolidTop[Main.tile[tileX, tileY - 1].TileType] || Main.tile[tileX, tileY - 1].IsHalfBlock && (!Main.tile[tileX, tileY - 4].HasUnactuatedTile || !Main.tileSolid[Main.tile[tileX, tileY - 4].TileType] || Main.tileSolidTop[Main.tile[tileX, tileY - 4].TileType])) && (!Main.tile[tileX, tileY - 2].HasUnactuatedTile || !Main.tileSolid[Main.tile[tileX, tileY - 2].TileType] || Main.tileSolidTop[Main.tile[tileX, tileY - 2].TileType]) && (!Main.tile[tileX, tileY - 3].HasUnactuatedTile || !Main.tileSolid[Main.tile[tileX, tileY - 3].TileType] || Main.tileSolidTop[Main.tile[tileX, tileY - 3].TileType]) && (!Main.tile[tileX - offset, tileY - 3].HasUnactuatedTile || !Main.tileSolid[Main.tile[tileX - offset, tileY - 3].TileType]))
                {
                    float tileWorldY = tileY * 16;
                    if (Main.tile[tileX, tileY].IsHalfBlock)
                        tileWorldY += 8f;
                    if (Main.tile[tileX, tileY - 1].IsHalfBlock)
                        tileWorldY -= 8f;
                    if (tileWorldY < pos.Y + (double)codable.height)
                    {
                        float tileWorldYHeight = pos.Y + codable.height - tileWorldY;
                        float heightNeeded = 16.1f;
                        if (tileWorldYHeight <= (double)heightNeeded)
                        {
                            gfxOffY += codable.position.Y + codable.height - tileWorldY;
                            codable.position.Y = tileWorldY - codable.height;
                            stepSpeed = tileWorldYHeight >= 9.0 ? 2f : 1f;
                        }
                    }
                    else
                    {
                        gfxOffY = Math.Max(0f, gfxOffY - stepSpeed);
                    }
                }
                else
                {
                    gfxOffY = Math.Max(0f, gfxOffY - stepSpeed);
                }
            }
            else
            {
                gfxOffY = Math.Max(0f, gfxOffY - stepSpeed);
            }
        }

        /*
         *  Code based on vanilla jumping code, checks for and attempts to jump over tiles and gaps when needed.
         *
         *  direction/directionY : the direction and directionY of the object jumping (usually an NPC)
         *  tileDistX/tileDistY : the tile amounts the object can jump across and over, respectively.
         *  float maxSpeedX : The maximum speed of the npc.
         */
        public static Vector2 AttemptJump(Vector2 position, Vector2 velocity, int width, int height, int direction, float directionY = 0, int tileDistX = 3, int tileDistY = 4, float maxSpeedX = 1f, bool jumpUpPlatforms = false, Entity target = null, bool jumpUpToVector = false, Vector2 vector = default)
        {
            try
            {
                tileDistX -= 2;
                Vector2 newVelocity = velocity;
                int tileX = Math.Max(10, Math.Min(Main.maxTilesX - 10, (int)((position.X + width * 0.5f + (width * 0.5f + 8f) * direction) / 16f)));
                int tileY = Math.Max(10, Math.Min(Main.maxTilesY - 10, (int)((position.Y + height - 15f) / 16f)));
                int tileItX = Math.Max(10, Math.Min(Main.maxTilesX - 10, tileX + direction * tileDistX));
                int tileItY = Math.Max(10, Math.Min(Main.maxTilesY - 10, tileY - tileDistY));
                int lastY = tileY;
                int tileHeight = (int)(height / 16f);
                if (height > tileHeight * 16) { tileHeight += 1; }

                Rectangle hitbox = new((int)position.X, (int)position.Y, width, height);
                //attempt to jump over walls if possible.

                if (jumpUpToVector)
                {
                    if (target != null)
                    {
                        if (Math.Abs(position.X + width * 0.5f - target.Center.X) < width + 120)
                        {
                            float dist = (int)Math.Abs(position.Y + height * 0.5f - target.Center.Y) / 16;
                            if (dist < tileDistY && target.Bottom.Y < position.Y)
                                newVelocity.Y = -6f + dist * -0.5f;
                        }
                    }
                    else
                    {
                        if (vector != default)
                        {
                            if (Math.Abs(position.X + width * 0.5f - vector.X) < width + 120)
                            {
                                float dist = (int)Math.Abs(position.Y + height * 0.5f - vector.Y) / 16;
                                if (dist < tileDistY && vector.Y < position.Y)
                                    newVelocity.Y = -6f + dist * -0.5f;
                            }
                        }
                    }
                }
                if (newVelocity.Y == velocity.Y)
                {
                    for (int y = tileY; y >= tileItY; y--)
                    {
                        Tile tile = Framing.GetTileSafely(tileX, y);
                        Tile tileNear = Main.tile[Math.Min(Main.maxTilesX, tileX - direction), y];
                        if (tile.HasUnactuatedTile && (y != tileY || tile.Slope == SlopeType.Solid) && Main.tileSolid[tile.TileType] && (jumpUpPlatforms || !Main.tileSolidTop[tile.TileType]))
                        {
                            if (!Main.tileSolidTop[tile.TileType])
                            {
                                Rectangle tileHitbox = new(tileX * 16, y * 16, 16, 16)
                                {
                                    Y = hitbox.Y
                                };
                                if (tileHitbox.Intersects(hitbox)) { newVelocity = velocity; break; }
                            }
                            if (tileNear.HasUnactuatedTile && Main.tileSolid[tileNear.TileType] && !Main.tileSolidTop[tileNear.TileType])
                            {
                                newVelocity = velocity;
                                break;
                            }
                            if (target != null && y * 16 < target.Center.Y)
                                continue;
                            lastY = y;
                            newVelocity.Y = -(5f + (tileY - y) * (tileY - y > 3 ? 1f - (tileY - y - 2) * 0.0525f : 1f));
                        }
                        //else
                        //if (lastY - y >= tileHeight) { break; }
                    }
                }
                // if the npc isn't jumping already...
                if (newVelocity.Y == velocity.Y)
                {
                    //...and there's a gap in front of the npc, attempt to jump across it.
                    if (directionY < 0 && (!Main.tile[tileX, tileY + 1].HasUnactuatedTile || !Main.tileSolid[Main.tile[tileX, tileY + 1].TileType]) && (!Main.tile[tileX + direction, tileY + 1].HasUnactuatedTile || !Main.tileSolid[Main.tile[tileX + direction, tileY + 1].TileType]))
                    {
                        if (!Main.tile[tileX + direction, tileY + 2].HasUnactuatedTile || !Main.tileSolid[Main.tile[tileX, tileY + 2].TileType] || target == null || target.Center.Y + target.height * 0.25f < tileY * 16f)
                        {
                            newVelocity.Y = -8f;
                            newVelocity.X *= 1.5f * (1f / maxSpeedX);
                            if (tileX <= tileItX)
                            {
                                for (int x = tileX; x < tileItX; x++)
                                {
                                    Tile tile = Framing.GetTileSafely(x, tileY + 1);
                                    if (x != tileX && !tile.HasUnactuatedTile)
                                    {
                                        newVelocity.Y -= 0.0325f;
                                        newVelocity.X += direction * 0.255f;
                                    }
                                }
                            }
                            else
                            if (tileX > tileItX)
                            {
                                for (int x = tileItX; x < tileX; x++)
                                {
                                    Tile tile = Framing.GetTileSafely(x, tileY + 1);
                                    if (x != tileItX && !tile.HasUnactuatedTile)
                                    {
                                        newVelocity.Y -= 0.0325f;
                                        newVelocity.X += direction * 0.255f;
                                    }
                                }
                            }
                        }
                    }
                }
                return newVelocity;
            }
            catch (Exception e)
            {
                BaseUtility.LogFancy("Redemption~ ATTEMPT JUMP ERROR:", e);
                return velocity;
            }
        }

        /*
         * Attempts to interact with a door.
         *
         * Returns : true if it found and is trying to open a door, false otherwise.
         * doorBeatCounter : counter that goes from 0-10. When it hits 10 or more the door is opened.
         * doorCounter : counter that goes from 0-60. When it hits 60 it increments doorBeatCounter by one.
         * tickUpdater : counter that goes from 0-60+. See AIZombie() on what projectile is.
         * ticksUntilBoredom : See AIZombie() on what projectile is.
         * interactDoorStyle : 0 == hit door but don't break it, 1 == smash down door, 2 == open door.
         */
        public static bool AttemptOpenDoor(NPC npc, ref float doorBeatCounter, ref float doorCounter, ref float tickUpdater, float ticksUntilBoredom, int doorBeatCounterMax = 10, int doorCounterMax = 60, int interactDoorStyle = 0)
        {
            bool hitTile = HitTileOnSide(npc, 3);
            if (hitTile)
            {
                int tileX = (int)((npc.Center.X + (npc.width / 2 + 8f) * npc.spriteDirection) / 16f);
                int tileY = (int)((npc.position.Y + npc.height - 15f) / 16f);

                int type = Framing.GetTileSafely(tileX, tileY - 1).TileType;
                bool isTallGate = type is TileID.TallGateClosed;
                bool isDoor = type is TileID.ClosedDoor || TileID.Sets.CloseDoorID.Contains(Framing.GetTileSafely(tileX, tileY - 1).TileType);

                if (Framing.GetTileSafely(tileX, tileY - 1).HasUnactuatedTile && (isTallGate || isDoor))
                {
                    doorCounter += 1f;
                    tickUpdater = 0f;
                    if (doorCounter >= doorCounterMax)
                    {
                        npc.velocity.X = 0.5f * -npc.spriteDirection;
                        doorBeatCounter += 1f;
                        doorCounter = 0f;
                        bool attemptOpenDoor = false;
                        if (doorBeatCounter >= doorBeatCounterMax)
                        {
                            attemptOpenDoor = true;
                            doorBeatCounter = 10f;
                        }
                        WorldGen.KillTile(tileX, tileY - 1, true);
                        if (attemptOpenDoor && Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            bool openedDoor = false;
                            if (interactDoorStyle != 0)
                            {
                                if (interactDoorStyle == 1)
                                {
                                    if (!isTallGate)
                                        WorldGen.KillTile(tileX, tileY);
                                    openedDoor = !Main.tile[tileX, tileY].HasUnactuatedTile;
                                }
                                else
                                {
                                    if (isTallGate)
                                        openedDoor = WorldGen.ShiftTallGate(tileX, tileY, false);
                                    else
                                        openedDoor = WorldGen.OpenDoor(tileX, tileY, npc.spriteDirection);
                                }
                            }
                            if (!openedDoor)
                            {
                                tickUpdater = ticksUntilBoredom;
                                npc.netUpdate = true;
                            }
                            if (Main.netMode == NetmodeID.Server && openedDoor)
                                NetMessage.SendData(MessageID.ToggleDoorState, -1, -1, NetworkText.FromLiteral(""), 0, tileX, tileY, npc.spriteDirection);
                        }
                    }
                    return true;
                }
            }
            return false;
        }

        #endregion

        /*
         * Checks if a Entity object (Player, NPC, Item or Projectile) has hit a tile on it's sides.
         *
         * noYMovement : If true, will not calculate unless the Entity is not moving on the Y axis.
         */
        public static bool HitTileOnSide(Entity codable, int dir, bool noYMovement = true, bool acceptTopSurfaces = true)
        {
            if (!noYMovement || codable.velocity.Y == 0f)
            {
                Vector2 dummyVec = default;
                return HitTileOnSide(codable.position, codable.width, codable.height, dir, ref dummyVec, acceptTopSurfaces);
            }
            return false;
        }

        /*
         * Checks if a bounding box has hit a tile on it's sides.
         *
         * position : the position of the bounding box.
         * width : the width of the bounding box.
         * height : the height of the bounding box.
         * dir : The direction to check. 0 == left, 1 == right, 2 == up, 3 == down.
         * hitTilePos : A Vector2 that is set to the hit tile position, if it hit one.
         */
        private static bool HitTileOnSide(Vector2 position, int width, int height, int dir, ref Vector2 hitTilePos, bool acceptTopSurfaces = true)
        {
            switch (dir)
            {
                case 0:
                    if (Collision.SolidCollision(position - new Vector2(1, 0), 8, height))
                        return true;
                    break;
                case 1:
                    if (Collision.SolidCollision(position + new Vector2(width - 8, 0), 9, height))
                        return true;
                    break;
                case 2:
                    if (Collision.SolidCollision(position - new Vector2(0, 1), width, 8))
                        return true;
                    break;
                case 3:
                    if (Collision.SolidCollision(position + new Vector2(0, height - 8), width, 9, acceptTopSurfaces))
                        return true;
                    break;
            }
            int tilePosX = 0;
            int tilePosY = 0;
            int tilePosWidth = 0;
            int tilePosHeight = 0;
            if (dir == 0) //left
            {
                tilePosX = (int)(position.X - 8f) / 16;
                tilePosY = (int)position.Y / 16;
                tilePosWidth = tilePosX + 1;
                tilePosHeight = (int)(position.Y + height) / 16;
            }
            else
            if (dir == 1) //right
            {
                tilePosX = (int)(position.X + width + 8f) / 16;
                tilePosY = (int)position.Y / 16;
                tilePosWidth = tilePosX + 1;
                tilePosHeight = (int)(position.Y + height) / 16;
            }
            else
            if (dir == 2) //up, ie ceiling
            {
                tilePosX = (int)position.X / 16;
                tilePosY = (int)(position.Y - 8f) / 16;
                tilePosWidth = (int)(position.X + width) / 16;
                tilePosHeight = tilePosY + 1;
            }
            else
            if (dir == 3) //down, ie floor
            {
                tilePosX = (int)position.X / 16;
                tilePosY = (int)(position.Y + height + 8f) / 16;
                tilePosWidth = (int)(position.X + width) / 16;
                tilePosHeight = tilePosY + 1;
            }
            for (int x2 = tilePosX; x2 < tilePosWidth; x2++)
            {
                for (int y2 = tilePosY; y2 < tilePosHeight; y2++)
                {
                    if (Framing.GetTileSafely(x2, y2) == null)
                        return false;
                    bool solidTop = dir == 3 && Main.tileSolidTop[Framing.GetTileSafely(x2, y2).TileType];
                    if (Framing.GetTileSafely(x2, y2).HasUnactuatedTile && (Main.tileSolid[Framing.GetTileSafely(x2, y2).TileType] && (!solidTop || acceptTopSurfaces)))
                    {
                        hitTilePos = new Vector2(x2, y2);
                        return true;
                    }
                }
            }
            return false;
        }

        /*
         *  Damages the NPC by the given amount.
         *
         *  dmgAmt : The amount of damage to inflict.
         *  knockback : The amount of knockback to inflict.
         *  hitDirection : The direction of the damage.
         *  damager : the thing actually doing damage (Player, Projectile or null)
         *  dmgVariation : If true, the damage will vary based on Main.DamageVar().
         *  hitThroughDefense : If true, boosts damage to get around npc defense.
         */
        private static void DamageNPC(NPC npc, int dmgAmt, float knockback, int hitDirection, Entity damager, bool dmgVariation = true, bool hitThroughDefense = false, bool crit = false, Item item = null)
        {
            item ??= new Item(ItemID.WoodenSword);
            if (npc.dontTakeDamage || (npc.immortal && npc.type != NPCID.TargetDummy))
                return;

            DamageClass damageClass = DamageClass.Default;
            if (damager is Projectile proj)
                damageClass = proj.DamageType;
            else if (damager is Player player)
                damageClass = player.HeldItem.DamageType;
            NPC.HitModifiers stat = npc.GetIncomingStrikeModifiers(damageClass, hitDirection);
            NPC.HitInfo strike;
            if (hitThroughDefense)
                stat.ScalingArmorPenetration += 1f;
            if (damager == null || damager is NPC)
            {
                if (damager is NPC damagerNPC)
                {
                    NPCLoader.ModifyHitNPC(damager as NPC, npc, ref stat);
                    if (npc.SupportsNPCTargets)
                        npc.target = damagerNPC.WhoAmIToTargettingIndex;
                }

                strike = stat.ToHitInfo(dmgAmt, crit, knockback, dmgVariation);
                npc.StrikeNPC(strike, false, true);
                if (Main.netMode != NetmodeID.SinglePlayer)
                    NetMessage.SendStrikeNPC(npc, in strike);

                if (damager is NPC)
                    NPCLoader.OnHitNPC(damager as NPC, npc, in strike);
            }
            else if (damager is Projectile p)
            {
                if (CombinedHooks.CanHitNPCWithProj(p, npc) != false)
                {
                    CombinedHooks.ModifyHitNPCWithProj(p, npc, ref stat);

                    if (p.TryGetOwner(out var player))
                    {
                        strike = stat.ToHitInfo(dmgAmt, crit, knockback, dmgVariation, player.luck);

                        int dmg = npc.StrikeNPC(strike, false);
                        if (Main.netMode != NetmodeID.SinglePlayer)
                            NetMessage.SendStrikeNPC(npc, in strike);

                        if (p.minion || ProjectileID.Sets.MinionShot[p.type] || ProjectileID.Sets.SentryShot[p.type])
                        {
                            bool flag19 = false;
                            bool flag2 = false;
                            bool flag3 = false;
                            bool flag4 = false;
                            bool flag5 = false;
                            bool flag6 = false;
                            bool flag7 = false;
                            bool flag8 = false;
                            for (int j = 0; j < 5; j++)
                            {
                                if (npc.buffTime[j] >= 1)
                                {
                                    switch (npc.buffType[j])
                                    {
                                        case 307:
                                            flag19 = true;
                                            break;
                                        case 309:
                                            flag2 = true;
                                            break;
                                        case 313:
                                            flag3 = true;
                                            break;
                                        case 310:
                                            flag4 = true;
                                            break;
                                        case 315:
                                            flag5 = true;
                                            break;
                                        case 326:
                                            flag6 = true;
                                            break;
                                        case 319:
                                            flag7 = true;
                                            break;
                                        case 316:
                                            flag8 = true;
                                            break;
                                    }
                                }
                            }
                            if (flag19)
                            {
                                dmg += 4;
                            }
                            if (flag5)
                            {
                                dmg += 6;
                            }
                            if (flag6)
                            {
                                dmg += 7;
                            }
                            if (flag2)
                            {
                                dmg += 9;
                            }
                            if (flag7)
                            {
                                dmg += 5;
                                if (Main.rand.NextBool(20))
                                {
                                    crit = true;
                                }
                            }
                            if (flag4)
                            {
                                int num7 = 10;
                                int num8 = Projectile.NewProjectile(p.GetSource_FromAI(), npc.Center, Vector2.Zero, ProjectileID.ScytheWhipProj, num7, 0f, p.owner);
                                Main.projectile[num8].localNPCImmunity[p.owner] = -1;
                                Projectile.EmitBlackLightningParticles(npc);
                            }
                            if (flag8)
                            {
                                int num9 = 20;
                                dmg += num9;
                                if (Main.rand.NextBool(10))
                                {
                                    crit = true;
                                }
                                ParticleOrchestraSettings particleOrchestraSettings = default;
                                particleOrchestraSettings.PositionInWorld = p.Center;
                                ParticleOrchestraSettings settings = particleOrchestraSettings;
                                ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.RainbowRodHit, settings);
                            }
                            if (flag3)
                            {
                                npc.RequestBuffRemoval(313);
                                int num10 = (int)((float)dmg * 1.75f);
                                int num12 = Projectile.NewProjectile(p.GetSource_FromAI(), npc.Center, Vector2.Zero, ProjectileID.FireWhipProj, num10, 0f, p.owner);
                                Main.projectile[num12].localNPCImmunity[p.owner] = -1;
                                dmg += num10;
                            }
                        }
                        if (npc.canGhostHeal)
                        {
                            if (Main.player[p.owner].ghostHeal && !Main.player[p.owner].moonLeech)
                            {
                                p.ghostHeal(dmg, new Vector2(npc.Center.X, npc.Center.Y), npc);
                            }
                            if (Main.player[p.owner].ghostHurt)
                            {
                                p.ghostHurt(dmg, new Vector2(npc.Center.X, npc.Center.Y), npc);
                            }
                        }
                        if (p.DamageType == DamageClass.Melee && Main.player[p.owner].beetleOffense && !npc.immortal)
                        {
                            if (Main.player[p.owner].beetleOrbs == 0)
                            {
                                Main.player[p.owner].beetleCounter += dmg * 3;
                            }
                            else if (Main.player[p.owner].beetleOrbs == 1)
                            {
                                Main.player[p.owner].beetleCounter += dmg * 2;
                            }
                            else
                            {
                                Main.player[p.owner].beetleCounter += dmg;
                            }
                            Main.player[p.owner].beetleCountdown = 0;
                        }
                        if (player.accDreamCatcher)
                            player.addDPS(dmg);
                        if (!npc.immortal && npc.canGhostHeal && p.DamageType == DamageClass.Magic && player.setNebula && player.nebulaCD == 0 && Main.rand.NextBool(3))
                        {
                            player.nebulaCD = 30;
                            int num35 = Utils.SelectRandom(Main.rand, 3453, 3454, 3455);
                            int num36 = Item.NewItem(p.GetSource_OnHit(npc), (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, num35);
                            Main.item[num36].velocity.Y = Main.rand.Next(-20, 1) * 0.2f;
                            Main.item[num36].velocity.X = Main.rand.Next(10, 31) * 0.2f * hitDirection;
                            if (Main.netMode == NetmodeID.MultiplayerClient)
                            {
                                NetMessage.SendData(MessageID.SyncItem, -1, -1, null, num36);
                            }
                        }

                        CombinedHooks.OnHitNPCWithProj(p, npc, strike, dmgAmt);
                    }
                    else
                    {
                        strike = stat.ToHitInfo(dmgAmt, crit, knockback, dmgVariation, 0);
                        npc.StrikeNPC(strike, false, true);
                        if (Main.netMode != NetmodeID.SinglePlayer)
                            NetMessage.SendStrikeNPC(npc, in strike);

                        CombinedHooks.OnHitNPCWithProj(p, npc, strike, dmgAmt);
                    }

                    if (p.penetrate != 1) { npc.immune[p.whoAmI] = 10; }
                }
            }
            else if (damager is Player player)
            {
                if (player.whoAmI == Main.myPlayer && CombinedHooks.CanPlayerHitNPCWithItem(player, item, npc) != false)
                {
                    player.ApplyDamageToNPC(npc, dmgAmt, knockback, hitDirection, crit, item.DamageType, dmgVariation);

                    npc.immune[player.whoAmI] = player.itemAnimation;
                }
            }
        }

        /*
         * Convenience method that handles killing an NPC and having it drop loot.
         * If you want the NPC to just dissapear, use KillNPC().
         */
        public static void KillNPCWithLoot(NPC npc)
        {
            DamageNPC(npc, npc.lifeMax + npc.defense + 1, 0f, 0, null, false, true);
        }

        /*
         * Convenience method that handles killing an NPC without loot.
         */
        public static void KillNPC(NPC npc)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient) return;
            npc.active = false;
            int npcID = npc.whoAmI;
            Main.npc[npcID] = new NPC();
            if (Main.netMode == NetmodeID.Server) NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, npcID);
        }

        public static int[] GetProjectiles(Vector2 center, int projType = -1, int owner = -1, float distance = 500f, Func<Projectile, bool> canAdd = null)
        {
            return GetProjectiles(center, projType, owner, default, distance, canAdd);
        }
        /*
		 * Gets the all Projectiles with the given type within the given distance from the center.
		 *
         * projType : If -1, check for ANY projectiles in the area. If not, check for the projectiles who match the type given.
         * projsToExclude : An array of projectile whoAmIs to exclude from the search.
         * distance : The distance to check.
		 */
        private static int[] GetProjectiles(Vector2 center, int projType = -1, int owner = -1, int[] projsToExclude = default, float distance = 500f, Func<Projectile, bool> canAdd = null)
        {
            List<int> allProjs = new();
            for (int i = 0; i < Main.projectile.Length; i++)
            {
                Projectile proj = Main.projectile[i];
                if (proj is { active: true } && (projType == -1 || proj.type == projType) && (owner == -1 || proj.owner == owner) && (distance == -1 || proj.Distance(center) < distance))
                {
                    bool add = true;
                    if (projsToExclude != default(int[]))
                    {
                        foreach (int m in projsToExclude)
                        {
                            if (m == proj.whoAmI) { add = false; break; }
                        }
                    }
                    if (add && canAdd != null && !canAdd(proj)) { continue; }
                    if (add) { allProjs.Add(i); }
                }
            }
            return allProjs.ToArray();
        }

        public static int GetNPC(Vector2 center, int npcType = -1, float distance = -1, Func<NPC, bool> canAdd = null)
        {
            return GetNPC(center, npcType, default, distance, canAdd);
        }

        /*
         * Gets the closest NPC with the given type within the given distance from the center. If distance is -1, it gets the closest NPC.
         *
         * npcType : If -1, check for ANY npcs in the area. If not, check for the npcs who match the type given.
         * npcsToExclude : An array of npc whoAmIs to exclude from the search.
         * distance : The distance to check.
         */
        private static int GetNPC(Vector2 center, int npcType = -1, int[] npcsToExclude = default, float distance = -1, Func<NPC, bool> canAdd = null)
        {
            int currentNPC = -1;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc is { active: true, life: > 0 } && (npcType == -1 || npc.type == npcType) && npc.type != NPCID.TargetDummy && (distance == -1f || npc.Distance(center) < distance))
                {
                    bool add = true;
                    if (npcsToExclude != default(int[]))
                    {
                        foreach (int m in npcsToExclude)
                        {
                            if (m == npc.whoAmI) { add = false; break; }
                        }
                    }
                    if (add && canAdd != null && !canAdd(npc)) { continue; }
                    if (add)
                    {
                        distance = npc.Distance(center);
                        currentNPC = i;
                    }
                }
            }
            return currentNPC;
        }

        public static int[] GetNPCs(Vector2 center, int npcType = -1, float distance = 500F, Func<NPC, bool> canAdd = null)
        {
            return GetNPCs(center, npcType, Array.Empty<int>(), distance, canAdd);
        }
        
        /*
         * Gets all NPCs of the given type within a given distance from the center.
         *
         * npcType : If -1, check for ANY npcs in the area. If not, check for the npcs who match the type given.
         * npcsToExclude : an array of npc whoAmIs to exclude from the search.
         * distance : the distance to check.
         */
        public static int[] GetNPCs(Vector2 center, int npcType = -1, int[] npcsToExclude = default, float distance = 500F, Func<NPC, bool> canAdd = null)
        {
            List<int> allNPCs = new();
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc is { active: true, life: > 0 } && (npcType == -1 || npc.type == npcType) && npc.type != NPCID.TargetDummy && (distance == -1 || npc.Distance(center) < distance))
                {
                    bool add = true;
                    if (npcsToExclude != default(int[]))
                    {
                        foreach (int m in npcsToExclude)
                        {
                            if (m == npc.whoAmI) { add = false; break; }
                        }
                    }
                    if (add && canAdd != null && !canAdd(npc)) { continue; }
                    if (add) { allNPCs.Add(i); }
                }
            }
            return allNPCs.ToArray();
        }

        public static int GetPlayer(Vector2 center, float distance = -1, Func<Player, bool> canAdd = null)
        {
            return GetPlayer(center, default, true, distance, canAdd);
        }

        /*
         * Gets the closest player within the given distance from the center. If distance is -1, it gets the closest player.
         *
         * playersToExclude : An array of player whoAmis that will be excluded from the search.
         * aliveOnly : If true, it only returns the player whoAmI if the player is not dead.
         * distance : The distance to search.
         */
        private static int GetPlayer(Vector2 center, int[] playersToExclude = default, bool activeOnly = true, float distance = -1, Func<Player, bool> canAdd = null)
        {
            int currentPlayer = -1;
            for (int i = 0; i < Main.player.Length; i++)
            {
                Player player = Main.player[i];
                if (player != null && (!activeOnly || player.active && !player.dead) && (distance == -1f || player.Distance(center) < distance))
                {
                    bool add = true;
                    if (playersToExclude != default(int[]))
                    {
                        foreach (int m in playersToExclude)
                        {
                            if (m == player.whoAmI) { add = false; break; }
                        }
                    }
                    if (add && canAdd != null && !canAdd(player)) { continue; }
                    if (add)
                    {
                        distance = player.Distance(center);
                        currentPlayer = i;
                    }
                }
            }
            return currentPlayer;
        }

        public static int[] GetPlayers(Vector2 center, float distance = 500F, Func<Player, bool> canAdd = null)
        {
            return GetPlayers(center, default, true, distance, canAdd);
        }
        
        /*
         * Gets all players within a given distance from the center.
         *
         * playersToExclude is an array of player ids you do not want included in the array.
         * aliveOnly : If true, it only returns the player whoAmI if the player is not dead.
         */
        public static int[] GetPlayers(Vector2 center, int[] playersToExclude = default, bool aliveOnly = true, float distance = 500F, Func<Player, bool> canAdd = null)
        {
            List<int> allPlayers = new();
            for (int i = 0; i < Main.player.Length; i++)
            {
                Player player = Main.player[i];
                if (player is { active: true } && (!aliveOnly || !player.dead) && player.Distance(center) < distance)
                {
                    bool add = true;
                    if (playersToExclude != default(int[]))
                    {
                        foreach (int m in playersToExclude)
                        {
                            if (m == player.whoAmI) { add = false; break; }
                        }
                    }
                    if (add && canAdd != null && !canAdd(player)) { continue; }
                    if (add) { allPlayers.Add(i); }
                }
            }
            return allPlayers.ToArray();
        }

        /*
         * Sets the npc's target to the given target and adjusts the according variables.
         */
        public static void SetTarget(NPC npc, int target)
        {
            npc.target = target;
            if (npc.target is < 0 or >= 255) { npc.target = 0; }
            npc.targetRect = Main.player[npc.target].Hitbox;
            if (npc.target != npc.oldTarget && !npc.collideX && !npc.collideY)
            {
                npc.netUpdate = true;
            }
        }

        public static void Look(NPC npc, int lookType = 0, float rotAddon = 0f, float rotAmount = 0.1f, bool flipSpriteDir = false)
        {
            Look(npc, ref npc.rotation, ref npc.spriteDirection, lookType, rotAddon, rotAmount, flipSpriteDir);
        }

        /*
         * Makes the rotation value and sprite direction 'look' based on factors from the Entity.
         * lookType : the type of look code to run.
         *        0 -> Rotates the entity and changes spriteDirection based on velocity.
         *        1 -> changes spriteDirection based on velocity.
         *        2 -> Rotates the entity based on velocity.
         *        3 -> Smoothly rotate and change sprite direction based on velocity.
         *        4 -> Smoothly rotate based on velocity.
         * rotAddon : the amount to add to the rotation. (only used by lookType 3/4)
         * rotAmount: the amount to rotate by. (only used by lookType 3/4)
         */
        private static void Look(Entity c, ref float rotation, ref int spriteDirection, int lookType = 0, float rotAddon = 0f, float rotAmount = 0.1f, bool flipSpriteDir = false)
        {
            LookAt(c.position + c.velocity, c.position, ref rotation, ref spriteDirection, lookType, rotAddon, rotAmount, flipSpriteDir);
        }

        public static void LookAt(Vector2 lookTarget, Entity c, int lookType = 0, float rotAddon = 0f, float rotAmount = 0.1f, bool flipSpriteDir = false)
        {
            int spriteDirection = c is NPC nPc1 ? nPc1.spriteDirection : c is Projectile projectile1 ? projectile1.spriteDirection : 0;
            float rotation = c is NPC nPc ? nPc.rotation : c is Projectile projectile ? projectile.rotation : 0f;
            LookAt(lookTarget, c.Center, ref rotation, ref spriteDirection, lookType, rotAddon, rotAmount, flipSpriteDir);
            if (c is NPC nPc2)
            {
                nPc2.spriteDirection = spriteDirection;
                nPc2.rotation = rotation;
            }
            else
            if (c is Projectile projectile2)
            {
                projectile2.spriteDirection = spriteDirection;
                projectile2.rotation = rotation;
            }
        }

        /*
         * Makes the rotation value and sprite direction 'look' at the given target.
         * lookType : the type of look code to run.
         *        0 -> Rotate the entity and change sprite direction based on the look target.
         *        1 -> change spriteDirection based on the look target.
         *        2 -> Rotate the entity based on the look target.
         *        3 -> Smoothly rotate and change sprite direction based on the look target.
         *        4 -> Smoothly rotate based on the look target.
         * rotAddon : the amount to add to the rotation. (only used by lookType 3/4)
         * rotAmount: the amount to rotate by. (only used by lookType 3/4)
         */
        private static void LookAt(Vector2 lookTarget, Vector2 center, ref float rotation, ref int spriteDirection, int lookType = 0, float rotAddon = 0f, float rotAmount = 0.075f, bool flipSpriteDir = false)
        {
            if (lookType == 0)
            {
                if (lookTarget.X > center.X) { spriteDirection = -1; } else { spriteDirection = 1; }
                if (flipSpriteDir) { spriteDirection *= -1; }
                float rotX = lookTarget.X - center.X;
                float rotY = lookTarget.Y - center.Y;
                rotation = -((float)Math.Atan2(rotX, rotY) - 1.57f + rotAddon);
                if (spriteDirection == 1) { rotation -= (float)Math.PI; }
            }
            else
            if (lookType == 1)
            {
                if (lookTarget.X > center.X) { spriteDirection = -1; } else { spriteDirection = 1; }
                if (flipSpriteDir) { spriteDirection *= -1; }
            }
            else
            if (lookType == 2)
            {
                float rotX = lookTarget.X - center.X;
                float rotY = lookTarget.Y - center.Y;
                rotation = -((float)Math.Atan2(rotX, rotY) - 1.57f + rotAddon);
            }
            else
            if (lookType is 3 or 4)
            {
                int oldDirection = spriteDirection;
                if (lookType == 3 && lookTarget.X > center.X) { spriteDirection = -1; } else { spriteDirection = 1; }
                if (lookType == 3 && flipSpriteDir) { spriteDirection *= -1; }
                if (oldDirection != spriteDirection)
                {
                    rotation += (float)Math.PI * spriteDirection;
                }
                float pi2 = (float)Math.PI * 2f;
                float rotX = lookTarget.X - center.X;
                float rotY = lookTarget.Y - center.Y;
                float rot = (float)Math.Atan2(rotY, rotX) + rotAddon;
                if (spriteDirection == 1) { rot += (float)Math.PI; }
                if (rot > pi2) { rot -= pi2; } else if (rot < 0) { rot += pi2; }
                if (rotation > pi2) { rotation -= pi2; } else if (rotation < 0) { rotation += pi2; }
                if (rotation < rot)
                {
                    if ((double)(rot - rotation) > (float)Math.PI) { rotation -= rotAmount; } else { rotation += rotAmount; }
                }
                else
                if (rotation > rot)
                {
                    if ((double)(rotation - rot) > (float)Math.PI) { rotation += rotAmount; } else { rotation -= rotAmount; }
                }
                if (rotation > rot - rotAmount && rotation < rot + rotAmount) { rotation = rot; }
            }
        }

        public static Vector2 TracePlayer(Vector2 start, float distance, float rotation, int ignorePlayer, bool npcCheck = true, bool tileCheck = true, bool playerCheck = true, bool ignorePlatforms = true)
        {
            Vector2 end = BaseUtility.RotateVector(start, start + new Vector2(distance, 0f), rotation);
            return Trace(start, end, ignorePlayer, 0, npcCheck, tileCheck, playerCheck, 1F, ignorePlatforms);
        }

        private static Vector2 Trace(Vector2 start, Vector2 end, object ignore, int ignoreType, bool npcCheck = true, bool tileCheck = true, bool playerCheck = true, float jump = 1F, bool ignorePlatforms = true)
        {
            return Trace(start, end, ignore, ignoreType, null, npcCheck, tileCheck, playerCheck, false, jump, ignorePlatforms);
        }

        private static Vector2 Trace(Vector2 start, Vector2 end, object ignore, int ignoreType, object dim, bool npcCheck = true, bool tileCheck = true, bool playerCheck = true, bool returnCenter = false, float jump = 1F, bool ignorePlatforms = true)
        {
            return Trace(start, end, ignore, ignoreType, dim, npcCheck, tileCheck, playerCheck, returnCenter, ignorePlatforms ? new[] { 19 } : default, jump); //ignores wooden platforms
        }

        /* **Code edited from Yoraiz0r's 'Holowires' Mod!**
         *
         * From the start point, it iterates to the end point. If it hits anything on the way, it will return the collision point. If not it returns the end point.
         *
         * dim : a Rectangle instance of the collision's dimensions. Can be null.
         * npcCheck : If true, Check for npc collision while iterating.
         * tileCheck : If true, check for tile collision while iterating.
         * playerCheck : If true, check for player collision while iterating.
         * returnCenter : If true, if it hits anything it returns it's center instead of where it hit.
         * tileTypesToIgnore : An array of tile types that it will assume it can trace through.
         * Jump: The amount to iterate by.
         */
        private static Vector2 Trace(Vector2 start, Vector2 end, object ignore, int ignoreType, object dim, bool npcCheck = true, bool tileCheck = true, bool playerCheck = true, bool returnCenter = false, int[] tileTypesToIgnore = default, float jump = 1F)
        {
            try
            {
                if (ignore == null) { return start; }
                if (dim == null) { dim = new Rectangle(0, 0, 1, 1); }
                if (start.X < 0) { start.X = 0; }
                if (start.X > Main.maxTilesX * 16) { start.X = Main.maxTilesX * 16; }
                if (start.Y < 0) { start.Y = 0; }
                if (start.Y > Main.maxTilesY * 16) { start.Y = Main.maxTilesY * 16; }
                if (end.X < 0) { end.X = 0; }
                if (end.X > Main.maxTilesX * 16) { end.X = Main.maxTilesX * 16; }
                if (end.Y < 0) { end.Y = 0; }
                if (end.Y > Main.maxTilesY * 16) { end.Y = Main.maxTilesY * 16; }
                Vector2 tc = new(1, 1);
                Vector2 pstart = start;
                Vector2 pend = end;
                Vector2 dir = pend - pstart;
                dir = dir.SafeNormalize(Vector2.Zero);
                float length = Vector2.Distance(pstart, pend);
                float way = 0f;
                while (way < length)
                {
                    Vector2 v = pstart + dir * way + tc;
                    Rectangle dimensions = (Rectangle)dim;
                    Rectangle posRect = new((int)v.X - (dimensions.Width == 1 ? 0 : dimensions.Width / 2), (int)v.Y - (dimensions.Height == 1 ? 0 : dimensions.Height / 2), dimensions.Width, dimensions.Height);
                    if (tileCheck)
                    {
                        int vecX = (int)v.X / 16;
                        int vecY = (int)v.Y / 16;
                        Rectangle rect = new((int)v.X, (int)v.Y, 16, 16);
                        if (posRect.Intersects(rect))
                        {
                            Vector2 vec = ignoreType == 1 ? (Vector2)ignore : new Vector2(-1, -1);
                            if ((int)vec.X != vecX && (int)vec.Y != vecY)
                            {
                                Tile tile = Framing.GetTileSafely(vecX, vecY);
                                if (tile is { HasUnactuatedTile: true })
                                {
                                    bool ignoreTile = tileTypesToIgnore is { Length: > 0 } && BaseUtility.InArray(tileTypesToIgnore, tile.TileType);
                                    if (!ignoreTile && Main.tileSolid[tile.TileType])
                                    {
                                        return returnCenter ? new Vector2(vecX * 16 + 8, vecY * 16 + 8) : v;
                                    }
                                }
                            }
                        }
                    }
                    if (npcCheck)
                    {
                        int[] npcs = GetNPCs(v, -1, 5F);
                        for (int i = 0; i < npcs.Length; i++)
                        {
                            NPC npc = Main.npc[npcs[i]];
                            if (!npc.active || npc.life <= 0) { continue; }
                            if (ignoreType == 2 && npc.whoAmI == (int)ignore) { continue; }
                            Rectangle npcRect = new((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height);
                            if (posRect.Intersects(npcRect)) { return returnCenter ? npc.Center : v; }
                        }
                    }
                    if (playerCheck)
                    {
                        int[] players = GetPlayers(v, 5F);
                        for (int i = 0; i < players.Length; i++)
                        {
                            Player player = Main.player[players[i]];
                            if (player.dead || !player.active) { continue; }
                            if (ignoreType == 0 && player.whoAmI == (int)ignore) { continue; }
                            Rectangle playerRect = new((int)player.position.X, (int)player.position.Y, player.width, player.height);
                            if (posRect.Intersects(playerRect)) { return returnCenter ? player.Center : v; }
                        }
                    }
                    way += jump;
                }
            }
            catch (Exception e)
            {
                BaseUtility.LogFancy("Redemption~ TRACE ERROR:", e);
            }
            return end;
        }

        /*
         * Shoots a projectile from an NPC aiming at fireTarget.
         * 
         * position/width/height : the target's position, width, and height, respectively.
         * projName : name of the projectile to fire.
         * delayTimer : a float value used to tick down before firing.
         * delayTimerMax : the amount of ticks until firing.
         * damage : how much damage to do.
         * speed : how fast the projectile flies.
         * checkCanHit : If true, check if the codable can see the target point before firing.
         * offset : offset from the center of the codable that the projectile should spawn at.
         */
        public static int ShootPeriodic(Entity codable, Vector2 position, int width, int height, int projType, ref float delayTimer, float delayTimerMax = 100f, int damage = -1, float speed = 10f, bool checkCanHit = true, Vector2 offset = default(Vector2))
        {
            int pID = -1;
            if (damage == -1) { Projectile proj = new Projectile(); proj.SetDefaults(projType); damage = proj.damage; }
            bool properSide = (codable is NPC ? Main.netMode != NetmodeID.MultiplayerClient : codable is Projectile ? ((Projectile)codable).owner == Main.myPlayer : true);
            if (properSide)
            {
                Vector2 targetCenter = position + new Vector2(width * 0.5f, height * 0.5f);
                delayTimer--;
                if (delayTimer <= 0)
                {
                    if (!checkCanHit || Collision.CanHit(codable.position, codable.width, codable.height, position, width, height))
                    {
                        Vector2 fireTarget = codable.Center + offset;
                        float rot = BaseUtility.RotationTo(codable.Center, targetCenter);
                        fireTarget = BaseUtility.RotateVector(codable.Center, fireTarget, rot);
                        pID = BaseAI.FireProjectile(targetCenter, fireTarget, projType, damage, 0f, speed);
                    }
                    delayTimer = delayTimerMax;
                    if (codable is NPC) { ((NPC)codable).netUpdate = true; }
                }
            }
            return pID;
        }

        /*
         * Shoots a projectile from a start position aiming at fireTarget. 
         * 
         * fireTarget : The position the projectile is shooting at.
         * position : The position the projectile is shooting from.
         * projectileTypeObj : Either an int of the projectile's type, or the projectile's name, to be fired.
         * damage : How much damage the projectile should inflict.
         * knockback : How much knockback the projectile should influct.
         * speedScalar : A scalar for how fast the projectile is shot.
         * hostility : The hostility of the projectile.
         *             0 -> use default projectile hostility
         *             1 -> hurt NPCS but not Players/Townies
         *            -1 -> hurt Players/Townies but not NPCs
         *             2 -> hurt BOTH Players/Townies and NPCs
         *             3 -> hurt NEITHER Players/Townies and NPCs (inert projectile)
         */
        public static int FireProjectile(Vector2 fireTarget, Vector2 position, int projectileType, int damage, float knockback, float speedScalar = 1f, int hostility = 0, int owner = -1, Vector2 targetOffset = default(Vector2))
        {
            Vector2 rotVec = BaseUtility.RotateVector(position, position + new Vector2(speedScalar, 0f), BaseUtility.RotationTo(position, fireTarget));
            rotVec -= position;
            int projectileID = Projectile.NewProjectile(Projectile.GetSource_None(), position.X, position.Y, rotVec.X, rotVec.Y, projectileType, damage, knockback, (owner != -1 ? owner : Main.myPlayer));
            Projectile proj = Main.projectile[projectileID];
            proj.velocity = rotVec;
            if (hostility != 0)
            {
                proj.friendly = (hostility == 1 || hostility == 2);
                proj.hostile = (hostility == -1 || hostility == 2);
                if (Main.netMode != NetmodeID.SinglePlayer) { MNet.SendBaseNetMessage(0, proj.owner, proj.identity, proj.friendly, proj.hostile); }
            }
            proj.netUpdate2 = true;
            Main.projectile[projectileID] = proj;
            return projectileID;
        }

        /*
         * Shoots a projectile from an NPC aiming at fireTarget.
         * 
         * projectileType : The type of projectile to be fired.
         * soundGroup / sound : The sound group and sound ID of a sound to play when shot. if either is -1, it does not produce sound.
         */
        public static int FireProjectile(Vector2 fireTarget, NPC npc, int projectileType, int damage, float knockback, float speedScalar = 1.0F, int soundGroup = 0, int sound = -1, int hostility = 0, int owner = -1)
        {
            /*
            if (Main.netMode != 2 && soundGroup != -1 && sound != -1)
            {
                Main.PlaySound(soundGroup, (int)npc.Center.X, (int)npc.Center.Y, sound);
            }
            */
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                int projectileID = FireProjectile(fireTarget, npc.Center, projectileType, damage, knockback, speedScalar, hostility, owner);
                npc.netUpdate = true;
                return projectileID;
            }
            return -1;
        }
    }
}
