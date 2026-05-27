using AAModClassic._Content.Inferno.__Hardmode.Items.Materials;
using AAModClassic._Content.Inferno.World.Biomes;
using AAModClassic.Items.Banners;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno.__Hardmode.NPCs._Underground.Wyrm
{
    public class WyrmHead : ModNPC
	{
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Wyrm");

        }

		public override void SetDefaults()
		{
			NPC.noTileCollide = true;
			NPC.height = 32;
			NPC.width = 44;
			NPC.aiStyle = -1;
			NPC.netAlways = true;
            NPC.damage = 20;
            NPC.defense = 20;
            NPC.lifeMax = 4000;
            NPC.value = Item.sellPrice(0, 0, 90, 0);
            NPC.knockBackResist = 0f;
            NPC.aiStyle = -1;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.behindTiles = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.Item124;
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.alpha = 255;
            NPC.lavaImmune = true;
            Banner = NPC.type;
			BannerItem = ModContent.ItemType<WyrmBanner>();
            SpawnModBiomes = new int[1] { ModContent.GetInstance<InfernoBiome>().Type };
        }
        public override bool PreAI()
        {
            Lighting.AddLight(NPC.Center, Color.DarkOrange.R / 255, Color.DarkOrange.G / 255, Color.DarkOrange.B / 255);
            Player player = Main.player[NPC.target];
            if (NPC.alpha != 0)
            {
                for (int spawnDust = 0; spawnDust < 2; spawnDust++)
                {
                    int num935 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, ModContent.DustType<Dusts.AkumaDust>(), 0f, 0f, 100, default, 2f);
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
                    int segment = 0;
                    int WyrmLength = Main.expertMode ? 5 : 3;
                    for (int i = 0; i < WyrmLength; ++i)
                    {
                        latestNPC = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<WyrmBody1>(), NPC.whoAmI, 0, latestNPC);
                        Main.npc[latestNPC].realLife = NPC.whoAmI;
                        Main.npc[latestNPC].ai[3] = NPC.whoAmI;
                        segment += 1;
                    }
                    latestNPC = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<WyrmBody2>(), NPC.whoAmI, 0, latestNPC);
                    Main.npc[latestNPC].realLife = NPC.whoAmI;
                    Main.npc[latestNPC].ai[3] = NPC.whoAmI;

                    latestNPC = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<WyrmBody3>(), NPC.whoAmI, 0, latestNPC);
                    Main.npc[latestNPC].realLife = NPC.whoAmI;
                    Main.npc[latestNPC].ai[3] = NPC.whoAmI;

                    latestNPC = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<WyrmBody4>(), NPC.whoAmI, 0, latestNPC);
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
			float speed = 7f;
			float acceleration = 0.13f;

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
            if (Main.player[NPC.target].dead)
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

            NPC.rotation = (float)Math.Atan2(NPC.velocity.Y, NPC.velocity.X) + 1.57f;
            if (NPC.velocity.X < 0f)
            {
                NPC.spriteDirection = 1;

            }
            else
            {
                NPC.spriteDirection = -1;
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

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture = TextureAssets.Npc[NPC.type].Value;
            var effects = NPC.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            spriteBatch.Draw(texture, NPC.Center - Main.screenPosition, NPC.frame, drawColor, NPC.rotation, NPC.frame.Size() / 2, NPC.scale, NPC.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            return false;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DragonFire>(), 1, 1, 3));
            npcLoot.Add(ItemDropRule.Common(ItemID.SoulofFlight, 1, 15, 30));
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {

                NPC.position.X = NPC.position.X + NPC.width / 2;
                NPC.position.Y = NPC.position.Y + NPC.height / 2;
                NPC.width = 44;
                NPC.height = 78;
                NPC.position.X = NPC.position.X - NPC.width / 2;
                NPC.position.Y = NPC.position.Y - NPC.height / 2;
                int dust1 = ModContent.DustType<Dusts.AkumaDust>();
                int dust2 = ModContent.DustType<Dusts.AkumaDust>();
                Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, dust1, 0f, 0f, 0);
                Main.dust[dust1].velocity *= 0.5f;
                Main.dust[dust1].scale *= 1.3f;
                Main.dust[dust1].fadeIn = 1f;
                Main.dust[dust1].noGravity = false;
                Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, dust2, 0f, 0f, 0);
                Main.dust[dust2].velocity *= 0.5f;
                Main.dust[dust2].scale *= 1.3f;
                Main.dust[dust2].fadeIn = 1f;
                Main.dust[dust2].noGravity = true;
            }
        }

        
    }
}
