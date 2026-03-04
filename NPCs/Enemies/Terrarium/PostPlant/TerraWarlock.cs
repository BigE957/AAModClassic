using System.IO;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic.NPCs.Enemies.Terrarium.PostPlant
{
    public class TerraWarlock : ModNPC
    {
		public override void SendExtraAI(BinaryWriter writer)
		{
			base.SendExtraAI(writer);
			if(Main.netMode == NetmodeID.Server || Main.dedServ)
			{
				writer.Write(internalAI[0]);
				writer.Write(internalAI[1]);
			}
		}

        public override void PostAI()
        {
            Player player = Main.LocalPlayer;
            if (!player.GetModPlayer<AAPlayer>().Terrarium)
            {
                NPC.life = 0;
            }
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Terra Warlock");
            Main.npcFrameCount[NPC.type] = 15;
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 600;
            NPC.defense = 40;
            NPC.damage = 120;
            NPC.width = 38;
            NPC.height = 60;
            NPC.aiStyle = -1;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.4f;
            Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("TerraWarlockBanner").Type;

        }
      
		public static int AISTATE_WALK = 0, AISTATE_SUMMON = 1;
		public float[] internalAI = new float[2];
        public int SummonThis = 0;
		
        public override void AI()
        {
            
            Player player = Main.player[NPC.target]; // makes it so you can reference the player the npc is targetting
            NPC.frameCounter++;
            if (internalAI[1] != AISTATE_SUMMON) //walk or charge
            {
				if (NPC.frameCounter >= 10)
				{
					NPC.frameCounter = 0;
					NPC.frame.Y += 60;
					if (NPC.frame.Y > (60 * 7))
					{
						NPC.frameCounter = 0;
						NPC.frame.Y = 0;
					}
				}
                if(NPC.velocity.Y != 0)
                {
                    NPC.frame.Y = 0;
                }
            }
            else //jump
            {
                if (NPC.frameCounter >= 10)
                {
                    NPC.frameCounter = 0;
                    NPC.frame.Y += 60;
                    if (NPC.frame.Y > (60 * 14))
                    {

                        Vector2 spawnAt = NPC.Center + new Vector2(0f, NPC.height / 2f);
                        if (Main.expertMode)
                        {
                            SummonThis = Main.rand.Next(4);

                            switch (SummonThis)
                            {
                                case 0:
                                    SummonThis = Mod.Find<ModNPC>("Minion1").Type;
                                    break;
                                case 1:
                                    SummonThis = Mod.Find<ModNPC>("Minion2").Type;
                                    break;
                                case 2:
                                    SummonThis = Mod.Find<ModNPC>("Minion3").Type;
                                    break;
                                default:
                                    SummonThis = Mod.Find<ModNPC>("Minion4").Type;
                                    break;
                            }
                            NPC.NewNPC((int)spawnAt.X - 10, (int)spawnAt.Y - 10, SummonThis);
                        }
                        internalAI[1] = AISTATE_WALK;
                    }
                    if (NPC.frame.Y > (60 * 14) || NPC.frame.Y < (60 * 8))
                    {
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 60 * 8;
                    }
                }
            }
            if (player.Center.X > NPC.Center.X) // so it faces the player
            {
                NPC.spriteDirection = -1;
            }else
            {
                NPC.spriteDirection = 1;
            }
			if(Main.netMode != NetmodeID.MultiplayerClient)
			{
				internalAI[0]++;
				if (internalAI[0] >= 240)
				{
					internalAI[0] = 0;
                    if (internalAI[1] == AISTATE_SUMMON)
                    {
                        internalAI[1] = AISTATE_WALK;
                    }
                    if (internalAI[1] == AISTATE_WALK)
                    {
                        internalAI[1] = AISTATE_SUMMON;
                    }
					NPC.ai = new float[4];
					NPC.netUpdate = true;
				}
			}
			if(internalAI[1] == AISTATE_WALK) //fighter
			{
                BaseAI.AIZombie(NPC, ref NPC.ai, false, false, 0, 0.07f, 3f, 3, 4, 60, true, 10, 60, true, null, false);				
			}
            else
			{
                NPC.velocity.X = 0;
            }
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("TerraWarlockGore1"), 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("TerraWarlockGore2"), 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("TerraWarlockGore3"), 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("TerraWarlockGore4"), 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("TerraWarlockGore5"), 1f);
                NPC.position.X = NPC.position.X + NPC.width / 2;
                NPC.position.Y = NPC.position.Y + NPC.height / 2;
                NPC.width = 44;
                NPC.height = 78;
                NPC.position.X = NPC.position.X - NPC.width / 2;
                NPC.position.Y = NPC.position.Y - NPC.height / 2;
                int dust1 = ModContent.DustType<Dusts.SummonDust>();
                int dust2 = ModContent.DustType<Dusts.SummonDust>();
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


        public override void OnKill()
        {
            if (Main.rand.Next(40) == 0)
            {
                Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<TerraCrystal>());
            }
            if (Main.rand.Next(20) == 0)
            {
                Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<Items.Summoning.TerraGauntlet>());
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(ModContent.BuffType<Buffs.Terrablaze>(), 300);
        }
    }
}


