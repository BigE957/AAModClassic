using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Dusts;
using AAModClassic.Items.Materials;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Enemies.Inferno
{
    public abstract class Flamespitter : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Flamespitter");
            Main.npcFrameCount[NPC.type] = 15;
		}

		public override void SetDefaults()
        {
            NPC.width = 40;
            NPC.height = 52;
            NPC.damage = 20;
			NPC.defense = 15;
			NPC.lifeMax = 100;
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath6;
            NPC.value = 240000f;
            NPC.knockBackResist = .30f;
            NPC.aiStyle = -1;
            NPC.noTileCollide = true;
            NPC.noGravity = true;
            NPC.lavaImmune = true;
            NPC.buffImmune[BuffID.OnFire] = true;
            Banner = NPC.type;
			BannerItem = ModContent.ItemType<FlamespitterBanner>();
        }

        public bool teleport = false;
        public bool digUp = false;


        public override void AI()
        {
            NPC.TargetClosest(true);

            BaseAI.LookAt(Main.player[NPC.target].Center, NPC, 1, 0f, 0f, true);
            int distFromPlayer = 20;
            bool checkGround = true;
            int teleportInterval = 650;
            int attackInterval = 100;
            int stopAttackInterval = 500;
            int frameHeight = 52;
            
            Func<int, int, bool> CanTeleportTo = null;
            NPC.velocity.X = NPC.velocity.X * 0.93f;

            if (NPC.velocity.X > -0.1 && NPC.velocity.X < 0.1)
            {
                NPC.velocity.X = 0f;
            }

            if (NPC.ai[0] == 0f)
            {
                NPC.ai[0] = Math.Max(0, Math.Max(teleportInterval, teleportInterval - 150));
            }

            if (NPC.ai[2] != 0f && NPC.ai[3] != 0f)
            {
                NPC.position.X = NPC.ai[2] * 16f - NPC.width / 2 + 8f;
                NPC.position.Y = NPC.ai[3] * 16f - NPC.height;
                NPC.velocity.X = 0f; NPC.velocity.Y = 0f;
                NPC.ai[2] = 0f; NPC.ai[3] = 0f;
            }

            if (NPC.justHit)
            {
                NPC.ai[0] = 0;
            }

            NPC.ai[0]++;
            if (attackInterval != -1 && NPC.ai[0] < stopAttackInterval && NPC.ai[0] % attackInterval == 0)
            {
                NPC.ai[1] = 30f;
                NPC.netUpdate = true;
            }
            else if (NPC.ai[0] >= teleportInterval && Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.ai[0] = 1f;
                if (teleport == true)
                {
                    int playerTileX = (int)Main.player[NPC.target].position.X / 16;
                    int playerTileY = (int)Main.player[NPC.target].position.Y / 16;
                    int tileX = (int)NPC.position.X / 16;
                    int tileY = (int)NPC.position.Y / 16;
                    int teleportCheckCount = 0;
                    bool hasTeleportPoint = false;
                    //player is too far away, don't teleport.
                    if (Vector2.Distance(NPC.Center, Main.player[NPC.target].Center) > 2000f)
                    {
                        teleportCheckCount = 100;
                        hasTeleportPoint = true;
                    }
                    while (!hasTeleportPoint && teleportCheckCount < 100)
                    {
                        teleportCheckCount++;
                        int tpTileX = Main.rand.Next(playerTileX - distFromPlayer, playerTileX + distFromPlayer);
                        int tpTileY = Main.rand.Next(playerTileY - distFromPlayer, playerTileY + distFromPlayer);
                        for (int tpY = tpTileY; tpY < playerTileY + distFromPlayer; tpY++)
                        {
                            if ((tpY < playerTileY - 4 || tpY > playerTileY + 4 || tpTileX < playerTileX - 4 || tpTileX > playerTileX + 4) && (tpY < tileY - 1 || tpY > tileY + 1 || tpTileX < tileX - 1 || tpTileX > tileX + 1) && (!checkGround || Main.tile[tpTileX, tpY].HasUnactuatedTile))
                            {
                                if ((CanTeleportTo != null && CanTeleportTo(tpTileX, tpY)) || (!Main.tile[tpTileX, tpY - 1].lava() && (!checkGround || Main.tileSolid[Main.tile[tpTileX, tpY].TileType]) && !Collision.SolidTiles(tpTileX - 1, tpTileX + 1, tpY - 4, tpY - 1)))
                                {
                                    if (attackInterval != -1) { NPC.ai[1] = 20f; }
                                    NPC.ai[2] = tpTileX;
                                    NPC.ai[3] = tpY;
                                    hasTeleportPoint = true;
                                    teleport = false;
                                    digUp = true;
                                    break;
                                }
                            }
                        }
                    }
                    NPC.netUpdate = true;
                }
            }
            
            if (attackInterval != -1 && NPC.ai[1] > 0f)
            {
                NPC.ai[1] -= 1f;
                if (NPC.ai[1] == 25f)
                {
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), new Vector2(NPC.position.X + 17f, NPC.position.Y + 18f), new Vector2(-6 + Main.rand.Next(-6, 0), -4 + Main.rand.Next(-4, 0)), ModContent.ProjectileType<Magma>(), 15, 3); ;
                }
            }

            NPC.frameCounter++;

            if (NPC.ai[0] >= teleportInterval && Main.netMode != NetmodeID.MultiplayerClient) //walk or charge
            {
                if (NPC.frameCounter >= 6)
                {
                    NPC.frameCounter = 0;
                    NPC.frame.Y += frameHeight;
                    if (NPC.frame.Y > (frameHeight * 14))
                    {
                        NPC.alpha = 255;
                        teleport = true;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                    }
                }
            }

            if (digUp) //walk or charge
            {
                if (NPC.frameCounter >= 6)
                {
                    NPC.frameCounter = 0;
                    NPC.frame.Y += frameHeight;
                    if (NPC.frame.Y > (frameHeight * 5))
                    {
                        NPC.alpha = 0;
                        digUp = false;
                        NPC.frameCounter = 0;
                        NPC.frame.Y = frameHeight * 6;
                    }
                }
            }

            if (attackInterval != -1 && NPC.ai[1] > 0f)
            {
                if (NPC.frameCounter >= 6)
                {
                    NPC.frameCounter = 0;
                    NPC.frame.Y += frameHeight;
                    if (NPC.frame.Y > (frameHeight * 5))
                    {
                        NPC.frameCounter = 0;
                        NPC.frame.Y = frameHeight * 6;
                    }
                }
            }

            else if (NPC.ai[1] == 25f)
            {
                NPC.frame.Y = frameHeight * 6;
            }
            else
            {
                NPC.frameCounter = 0;
            }
        }
        
		public override void HitEffect(NPC.HitInfo hit)
		{

            int dust1 = ModContent.DustType<BroodmotherDust>();
            if (NPC.life <= 0)
			{
                Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, dust1, 0f, 0f, 0);
                Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, dust1, 0f, 0f, 0);
                Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, dust1, 0f, 0f, 0);
            }
		}

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DragonScale>(), 10));
        }
    }
}