using AAModClassic.NPCs.Bosses.Toad;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Utilities.AbstractsLikeDigitalCircus.NPCs
{
    public static class NPCGeneralHelper
    {
        /// <summary>
        /// Automatically fades in or out an NPC based on the presence of other NPCs.
        /// </summary>
        /// <param name="npc">the NPC to get faded.</param>
        /// <param name="fadeInExtra">An extra boost to the fade in rate.</param>
        /// <param name="fadeOutExtra">An extra boost to the fade out rate.</param>
        /// <param name="parentIDs">The type IDs of any "parent" entities. While they are alive, the NPC will fade in, and when they are dead they will fade out.</param>
        public static void FadeInOutBasedOnAliveEntities(this NPC npc, int fadeInExtra = 0, int fadeOutExtra = 0, params int[] parentIDs)
        {
            bool anyParentsAlive = false;

            foreach (int type in parentIDs)
            {
                if (NPC.AnyNPCs(type))
                {
                    anyParentsAlive = true;
                    break;
                }
            }

            if (anyParentsAlive || parentIDs.Length <= 0)
            {
                if (npc.alpha > 0)
                    npc.alpha -= 5 + fadeInExtra;
                else
                    npc.alpha = 0;
            }
            else
            {
                npc.dontTakeDamage = true;
                if (npc.alpha < 255)
                    npc.alpha += 5 + fadeOutExtra;
                else
                    npc.active = false;
            }
        }

        public static void LookAtTargetWhileNotMovingLookTowardsDirectionWhileMoving(this NPC npc)
        {
            Player player = Main.player[npc.target];

            if (npc.velocity.Y != 0)
            {
                if (npc.velocity.X < 0)
                    npc.spriteDirection = -1;
                else if (npc.velocity.X > 0)
                    npc.spriteDirection = 1;
            }
            else
            {
                if (player.position.X < npc.position.X)
                    npc.spriteDirection = -1;
                else if (player.position.X > npc.position.X)
                    npc.spriteDirection = 1;
            }
        }

        /// <summary>
        /// Mimics the generic Slime AI as of Terraria version 1.3.4.1.
        /// <para>This code was adapted from BaseMod.</para> 
        /// </summary>
        /// <param name="npc">The NPC who should use this AI.</param>
        /// <param name="ai">A float array that stores AI data.</param> //TODO: do we need to pass this in? originally didnt use this, now that it does is this needed?
        /// <param name="fleeWhenDay">Whether or not the NPC should run away when it's day.</param> //TODO: rework to be a condition we push in to trigger running away?
        /// <param name="jumpTime">The cooldown time after the slime has jumped.</param>
        /// <param name="jumpVelX"></param>
        /// <param name="jumpVelY"></param>
        /// <param name="jumpVelHighX"></param>
        /// <param name="jumpVelHighY"></param>
        public static void AISlime(this NPC npc, ref float[] ai, bool fleeWhenDay = false, int jumpTime = 200, float jumpVelX = 2f, float jumpVelY = -6f, float jumpVelHighX = 3f, float jumpVelHighY = -8f)
        {
            //ai[0] is a timer that iterates after the npc has jumped. If it is >= 0, the npc will attempt to jump again.
            //ai[1] is used to determine what jump type to do. (if 2, large jump, else smaller jump.)
            //ai[2] is used for target updating.
            //ai[3] is used to house the landing position of the npc for bigger jumps. This is used to make it turn around when it hits
            //      an impassible wall.

            //if (jumpTime < 100) { jumpTime = 100; }
            bool getNewTarget = false; //getNewTarget is used to iterate the 'boredom' scale. If it's night, the npc is hurt, or it's
            //below a certain depth, it will attempt to find the nearest target to it.
            if (fleeWhenDay && !Main.dayTime || npc.life != npc.lifeMax || npc.position.Y > Main.worldSurface * 16.0)
            {
                getNewTarget = true;
            }
            if (ai[2] > 1f) { ai[2] -= 1f; }
            if (npc.wet)//if the npc is wet...
            {
                //handles the npc's 'bobbing' in water.
                if (npc.collideY) { npc.velocity.Y = -2f; }
                if (npc.velocity.Y < 0f && ai[3] == npc.position.X) { npc.direction *= -1; ai[2] = 200f; }
                if (npc.velocity.Y > 0f) { ai[3] = npc.position.X; }
                if (npc.velocity.Y > 2f) { npc.velocity.Y *= 0.9f; }
                npc.velocity.Y -= 0.5f;
                if (npc.velocity.Y < -4f) { npc.velocity.Y = -4f; }
                //if ai[2] is 1f, and we should get a target, target nearby players.
                if (ai[2] == 1f && getNewTarget) { npc.TargetClosest(); }
            }
            npc.aiAction = 0;
            //if ai[2] is 0f (just spawned)
            if (ai[2] == 0f)
            {
                ai[0] = -100f;
                ai[2] = 1f;
                npc.TargetClosest();
            }
            //if npc is not jumping or falling
            if (npc.velocity.Y == 0f)
            {
                if (ai[3] == npc.position.X) { npc.direction *= -1; ai[2] = 200f; }
                ai[3] = 0f;
                npc.velocity.X *= 0.8f;
                if (npc.velocity.X is > -0.1f and < 0.1f) { npc.velocity.X = 0f; }
                if (getNewTarget) { ai[0] += 1f; }
                ai[0] += 1f;
                if (ai[0] >= 0f)
                {
                    npc.netUpdate = true;
                    if (ai[2] == 1f && getNewTarget) { npc.TargetClosest(); }
                    if (ai[1] == 2f) //larger jump
                    {
                        npc.velocity.Y = jumpVelHighY;
                        npc.velocity.X += jumpVelHighX * npc.direction;
                        ai[0] = -jumpTime;
                        ai[1] = 0f;
                        ai[3] = npc.position.X;
                    }
                    else //smaller jump
                    {
                        npc.velocity.Y = jumpVelY;
                        npc.velocity.X += jumpVelX * npc.direction;
                        ai[0] = -jumpTime - 80f;
                        ai[1] += 1f;
                    }
                }
                else
                if (ai[0] >= -30f)
                {
                    npc.aiAction = 1;
                }
            }
            else //handle moving the npc while in air.
            if (npc.target < 255 && (npc.direction == 1 && npc.velocity.X < 3f || npc.direction == -1 && npc.velocity.X > -3f))
            {
                if (npc.direction == -1 && npc.velocity.X < 0.1 || npc.direction == 1 && npc.velocity.X > -0.1)
                {
                    npc.velocity.X += 0.2f * npc.direction;
                    return;
                }
                npc.velocity.X *= 0.93f;
            }
        }
    }
}
