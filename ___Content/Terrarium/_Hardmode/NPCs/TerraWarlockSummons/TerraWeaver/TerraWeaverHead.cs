using Terraria;
using System;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Terrarium._Hardmode.NPCs.TerraWarlockSummons
{
    public class TerraWeaverHead : ModNPC
	{
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Terra Weaver");
        }

        public override void SetDefaults()
		{
            NPC.lifeMax = 350;
            NPC.defense = 20;
            NPC.damage = 50;
            NPC.width = 20;
            NPC.height = 18;
            NPC.aiStyle = -1;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0f;
            NPC.alpha = 255;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            Banner = NPC.type;
			BannerItem = ModContent.ItemType<Items.Banners.TerraWeaverBanner>();
        }
        public override bool PreAI()
        {
            Player player = Main.player[NPC.target];
            float dist = NPC.Distance(player.Center);

            NPC.rotation = (float)Math.Atan2(NPC.velocity.Y, NPC.velocity.X) + 1.57f;
            if (NPC.alpha != 0)
            {
                for (int spawnDust = 0; spawnDust < 2; spawnDust++)
                {
                    int num935 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, ModContent.DustType<Dusts.SummonDust>(), 0f, 0f, 100, default, 2f);
                    Main.dust[num935].noGravity = true;
                    Main.dust[num935].noLight = true;
                }
            }
            NPC.alpha -= 12;
            if (NPC.alpha < 0)
            {
                NPC.alpha = 0;
            }

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (NPC.ai[0] == 0)
                {
                    NPC.realLife = NPC.whoAmI;
                    int latestNPC = NPC.whoAmI;
                    int WormLength = 9;
                    for (int i = 0; i < WormLength; ++i)
                    {
                        latestNPC = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<TerraWeaverBody>(), NPC.whoAmI, 0, latestNPC);
                        Main.npc[latestNPC].realLife = NPC.whoAmI;
                        Main.npc[latestNPC].ai[3] = NPC.whoAmI;
                    }

                    latestNPC = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<TerraWeaverTail>(), NPC.whoAmI, 0, latestNPC);
                    Main.npc[latestNPC].realLife = NPC.whoAmI;
                    Main.npc[latestNPC].ai[3] = NPC.whoAmI;

                    NPC.ai[0] = 1;
                    NPC.netUpdate = true;
                }
            }
            
            int minTilePosX = (int)(NPC.position.X / 16.0) - 1;
			int maxTilePosX = (int)((NPC.position.X + NPC.width) / 16.0) + 2;
			int minTilePosY = (int)(NPC.position.Y / 16.0) - 1;
			int maxTilePosY = (int)((NPC.position.Y + NPC.height) / 16.0) + 2;
			if (minTilePosX < 0)
				minTilePosX = 0;
			if (maxTilePosX > Main.maxTilesX)
				maxTilePosX = Main.maxTilesX;
			if (minTilePosY < 0)
				minTilePosY = 0;
			if (maxTilePosY > Main.maxTilesY)
				maxTilePosY = Main.maxTilesY;

			bool collision = true;

			for (int i = minTilePosX; i < maxTilePosX; ++i)
			{
				for (int j = minTilePosY; j < maxTilePosY; ++j)
				{
					if (Main.tile[i, j] != null && (Main.tile[i, j].HasUnactuatedTile && (Main.tileSolid[Main.tile[i, j].TileType] || Main.tileSolidTop[Main.tile[i, j].TileType] && Main.tile[i, j].TileFrameY == 0) || Main.tile[i, j].LiquidAmount > 64))
					{
						Vector2 vector2;
						vector2.X = i * 16;
						vector2.Y = j * 16;
						if (NPC.position.X + NPC.width > vector2.X && NPC.position.X < vector2.X + 16.0 && NPC.position.Y + NPC.height > (double)vector2.Y && NPC.position.Y < vector2.Y + 16.0)
						{
							collision = true;
							if (Main.rand.NextBool(100) && Main.tile[i, j].HasUnactuatedTile)
								WorldGen.KillTile(i, j, true, true, false);
						}
					}
				}
			}
			float speed = 5f;
			float acceleration = 0.1f;

			Vector2 npcCenter = new Vector2(NPC.position.X + NPC.width * 0.5f, NPC.position.Y + NPC.height * 0.5f);
			float targetXPos = Main.player[NPC.target].position.X + Main.player[NPC.target].width / 2;
			float targetYPos = Main.player[NPC.target].position.Y + Main.player[NPC.target].height / 2;

			float targetRoundedPosX = (int)(targetXPos / 16.0) * 16;
			float targetRoundedPosY = (int)(targetYPos / 16.0) * 16;
			npcCenter.X = (int)(npcCenter.X / 16.0) * 16;
			npcCenter.Y = (int)(npcCenter.Y / 16.0) * 16;
			float dirX = targetRoundedPosX - npcCenter.X;
			float dirY = targetRoundedPosY - npcCenter.Y;
			NPC.TargetClosest(true);
			float length = (float)Math.Sqrt(dirX * dirX + dirY * dirY);

			float absDirX = Math.Abs(dirX);
			float absDirY = Math.Abs(dirY);
			float newSpeed = speed / length;
			dirX *= newSpeed * 2;
			dirY *= newSpeed * 2;
			if (NPC.velocity.X > 0.0 && dirX > 0.0 || NPC.velocity.X < 0.0 && dirX < 0.0 || NPC.velocity.Y > 0.0 && dirY > 0.0 || NPC.velocity.Y < 0.0 && dirY < 0.0)
			{
				if (NPC.velocity.X < dirX)
					NPC.velocity.X = NPC.velocity.X + acceleration;
				else if (NPC.velocity.X > dirX)
					NPC.velocity.X = NPC.velocity.X - acceleration;
				if (NPC.velocity.Y < dirY)
					NPC.velocity.Y = NPC.velocity.Y + acceleration;
				else if (NPC.velocity.Y > dirY)
					NPC.velocity.Y = NPC.velocity.Y - acceleration;
				if (Math.Abs(dirY) < speed * 0.2 && (NPC.velocity.X > 0.0 && dirX < 0.0 || NPC.velocity.X < 0.0 && dirX > 0.0))
				{
					if (NPC.velocity.Y > 0.0)
						NPC.velocity.Y = NPC.velocity.Y + acceleration * 2f;
					else
						NPC.velocity.Y = NPC.velocity.Y - acceleration * 2f;
				}
				if (Math.Abs(dirX) < speed * 0.2 && (NPC.velocity.Y > 0.0 && dirY < 0.0 || NPC.velocity.Y < 0.0 && dirY > 0.0))
				{
					if (NPC.velocity.X > 0.0)
						NPC.velocity.X = NPC.velocity.X + acceleration * 2f;
					else
						NPC.velocity.X = NPC.velocity.X - acceleration * 2f;
				}
			}
			else if (absDirX > absDirY)
			{
				if (NPC.velocity.X < dirX)
					NPC.velocity.X = NPC.velocity.X + acceleration * 1.1f;
				else if (NPC.velocity.X > dirX)
					NPC.velocity.X = NPC.velocity.X - acceleration * 1.1f;

				if (Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y) < speed * 0.5)
				{
					if (NPC.velocity.Y > 0.0)
						NPC.velocity.Y = NPC.velocity.Y + acceleration;
					else
						NPC.velocity.Y = NPC.velocity.Y - acceleration;
				}
			}
			else
			{
				if (NPC.velocity.Y < dirY)
					NPC.velocity.Y = NPC.velocity.Y + acceleration * 1.1f;
				else if (NPC.velocity.Y > dirY)
					NPC.velocity.Y = NPC.velocity.Y - acceleration * 1.1f;

				if (Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y) < speed * 0.5)
				{
					if (NPC.velocity.X > 0.0)
						NPC.velocity.X = NPC.velocity.X + acceleration;
					else
						NPC.velocity.X = NPC.velocity.X - acceleration;
				}
			}
            if (Main.player[NPC.target].dead || Math.Abs(NPC.position.X - Main.player[NPC.target].position.X) > 6000f || Math.Abs(NPC.position.Y - Main.player[NPC.target].position.Y) > 6000f)
            {
                
                NPC.velocity.Y = NPC.velocity.Y + 1f;
                if (NPC.position.Y > Main.rockLayer * 16.0)
                {
                    NPC.velocity.Y = NPC.velocity.Y + 1f;
                    speed = 30f;
                }
                if (NPC.position.Y > Main.rockLayer * 16.0)
                {
                    for (int num957 = 0; num957 < 200; num957++)
                    {
                        if (Main.npc[num957].aiStyle == NPC.aiStyle)
                        {
                            Main.npc[num957].active = false;
                        }
                    }
                }
            }

            if (collision)
			{
				if (NPC.localAI[0] != 1)
					NPC.netUpdate = true;
				NPC.localAI[0] = 1f;
			}
			if ((NPC.velocity.X > 0.0 && NPC.oldVelocity.X < 0.0 || NPC.velocity.X < 0.0 && NPC.oldVelocity.X > 0.0 || NPC.velocity.Y > 0.0 && NPC.oldVelocity.Y < 0.0 || NPC.velocity.Y < 0.0 && NPC.oldVelocity.Y > 0.0) && !NPC.justHit)
				NPC.netUpdate = true;

			return false;
		}
    }
    
}
