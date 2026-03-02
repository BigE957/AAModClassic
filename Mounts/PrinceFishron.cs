using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Mounts
{
	public class PrinceFishron : ModMount
	{
		public override void SetStaticDefaults()
		{
			MountData.spawnDust = 15;
			MountData.buff = Mod.Find<ModBuff>("PrinceFishron").Type;
			MountData.heightBoost = 14;
			MountData.flightTimeMax = int.MaxValue;
			MountData.fatigueMax = int.MaxValue;
			MountData.fallDamage = 0f;
			MountData.usesHover = true;
			MountData.runSpeed = 2f;
			MountData.dashSpeed = 1f;
			MountData.acceleration = 0.2f;
			MountData.jumpHeight = 4;
			MountData.jumpSpeed = 3f;
			MountData.swimSpeed = 24f;
			MountData.blockExtraJumps = true;
			MountData.totalFrames = 23;
			int[] array = new int[MountData.totalFrames];
			for (int num8 = 0; num8 < array.Length; num8++)
			{
				array[num8] = 12;
			}
			MountData.playerYOffsets = array;
			MountData.xOffset = 2;
			MountData.bodyFrame = 3;
			MountData.yOffset = 16;
			MountData.playerHeadOffset = 31;
			MountData.standingFrameCount = 1;
			MountData.standingFrameDelay = 12;
			MountData.standingFrameStart = 8;
			MountData.runningFrameCount = 7;
			MountData.runningFrameDelay = 14;
			MountData.runningFrameStart = 8;
			MountData.flyingFrameCount = 8;
			MountData.flyingFrameDelay = 16;
			MountData.flyingFrameStart = 0;
			MountData.inAirFrameCount = 8;
			MountData.inAirFrameDelay = 6;
			MountData.inAirFrameStart = 0;
			MountData.idleFrameCount = 0;
			MountData.idleFrameDelay = 0;
			MountData.idleFrameStart = 0;
			MountData.idleFrameLoop = false;
			MountData.swimFrameCount = 8;
			MountData.swimFrameDelay = 4;
			MountData.swimFrameStart = 15;
			if (Main.netMode != NetmodeID.Server)
			{
				MountData.backTexture = Mod.GetTexture("Mounts/PrinceFishron");
				MountData.backTextureGlow = Mod.GetTexture("Mounts/PrinceFishron_Glow");
				MountData.frontTexture = null;
				MountData.frontTextureExtra = null;
				MountData.textureWidth = MountData.backTexture.Width;
				MountData.textureHeight = MountData.backTexture.Height;
			}
		}
		
		public override bool UpdateFrame(Player mountedPlayer, int state, Vector2 velocity)
		{
			if (state == 4)
			{
				MountData.runSpeed = MountData.swimSpeed;
			}
			if (state == 2)
			{
				MountData.runSpeed = 13f;
			}
			return true;
		}

		public override void UpdateEffects(Player player)
		{
			if (Math.Abs(player.velocity.X) > 4f)
			{
				Rectangle rect = player.getRect();
				Dust.NewDust(new Vector2(rect.X, rect.Y), rect.Width, rect.Height, DustID.MagicMirror);
			}
		}
	}
}