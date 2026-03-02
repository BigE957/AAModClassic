using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Mounts
{
	public class BegPony : ModMount
	{
		public override void SetStaticDefaults()
		{
            MountData.spawnDust = DustID.Smoke;
            MountData.buff = ModContent.BuffType<Buffs.PrettyPony>();
            MountData.heightBoost = 44;
            MountData.flightTimeMax = 0;
            MountData.fallDamage = 0f;
            MountData.runSpeed = 6f;
            MountData.dashSpeed = 16f;
            MountData.acceleration = 0.5f;
            MountData.jumpHeight = 14;
            MountData.jumpSpeed = 9.01f;
            MountData.totalFrames = 16;
            int[] array = new int[MountData.totalFrames];
            for (int num6 = 0; num6 < array.Length; num6++)
            {
                array[num6] = 28;
            }
            array[3] += 2;
            array[4] += 2;
            array[7] += 2;
            array[8] += 2;
            array[12] += 2;
            array[13] += 2;
            array[15] += 4;
            MountData.playerYOffsets = array;
            MountData.xOffset = 5;
            MountData.bodyFrame = 3;
            MountData.yOffset = 3;
            MountData.playerHeadOffset = 31;
            MountData.standingFrameCount = 1;
            MountData.standingFrameDelay = 12;
            MountData.standingFrameStart = 0;
            MountData.runningFrameCount = 7;
            MountData.runningFrameDelay = 15;
            MountData.runningFrameStart = 1;
            MountData.dashingFrameCount = 6;
            MountData.dashingFrameDelay = 40;
            MountData.dashingFrameStart = 9;
            MountData.flyingFrameCount = 6;
            MountData.flyingFrameDelay = 6;
            MountData.flyingFrameStart = 1;
            MountData.inAirFrameCount = 1;
            MountData.inAirFrameDelay = 12;
            MountData.inAirFrameStart = 15;
            MountData.idleFrameCount = 0;
            MountData.idleFrameDelay = 0;
            MountData.idleFrameStart = 0;
            MountData.idleFrameLoop = false;
            MountData.swimFrameCount = MountData.inAirFrameCount;
            MountData.swimFrameDelay = MountData.inAirFrameDelay;
            MountData.swimFrameStart = MountData.inAirFrameStart;
            if (Main.netMode != 2)
            {
                MountData.backTexture = Mod.GetTexture("Mounts/BegPony");
                MountData.backTextureExtra = null;
                MountData.frontTexture = null;
                MountData.frontTextureExtra = null;
                MountData.textureWidth = MountData.backTexture.Width;
                MountData.textureHeight = MountData.backTexture.Height;
            }
        }

		public override void UpdateEffects(Player player)
		{
            player.GetJumpState(ExtraJump.UnicornMount).Enabled = true/* tModPorter Suggestion: Call Enable() if setting this to true, otherwise call Disable(). */;
            if (Math.Abs(player.velocity.X) > player.mount.DashSpeed - player.mount.RunSpeed / 2f)
            {
                player.noKnockback = true;
            }
            if (player.dashDelay > 0)
            {
                player.dashDelay--;
            }
            else
            {
                float num4 = 0;
                bool flag = false;
                if (player.dashTime > 0)
                {
                    player.dashTime--;
                }
                else if (player.dashTime < 0)
                {
                    player.dashTime++;
                }
                if (player.controlRight && player.releaseRight)
                {
                    if (player.dashTime > 0)
                    {
                        num4 = 1.4f;
                        flag = true;
                        player.dashTime = 0;
                    }
                    else
                    {
                        player.dashTime = 15;
                    }
                }
                else if (player.controlLeft && player.releaseLeft)
                {
                    if (player.dashTime < 0)
                    {
                        num4 = -1.4f;
                        flag = true;
                        player.dashTime = 0;
                    }
                    else
                    {
                        player.dashTime = -15;
                    }
                }
                if (flag)
                {
                    player.velocity.X = 16.9f * num4;
                    Point point = Utils.ToTileCoordinates(player.Center + new Vector2(num4 * player.width / 2 + 2, player.gravDir * -player.height / 2f + player.gravDir * 2f));
                    Point point2 = Utils.ToTileCoordinates(player.Center + new Vector2(num4 * player.width / 2 + 2, 0f));
                    if (WorldGen.SolidOrSlopedTile(point.X, point.Y) || WorldGen.SolidOrSlopedTile(point2.X, point2.Y))
                    {
                        player.velocity.X /= 2f;
                    }
                    player.dashDelay = 300;
                }
            }
        }
	}
}