using AAModClassic._Content.Inferno.___PreHardmode.Items.Materials;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Items.Banners;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno.___PreHardmode.NPCs
{
    public class FlameBrute : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Flame Brute");
            Main.npcFrameCount[NPC.type] = 9;
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 120;
            NPC.damage = 25;
            NPC.defense = 10;
            NPC.knockBackResist = 0f;
            NPC.value = Item.sellPrice(0, 0, 6, 45);
            NPC.aiStyle = -1;
            NPC.width = 40;
            NPC.height = 60;
			NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;		
            NPC.lavaImmune = true;
            Banner = NPC.type;
			BannerItem = ModContent.ItemType<FlamebruteBanner>();
        }

		const int frameHeightPlusFluff = 78; //the 2 pixels per frame

        public override void AI()
        {
			Player player = Main.player[NPC.target];
			float playerDistX = Math.Abs(player.Center.X - NPC.Center.X);
			float playerDistY = Math.Abs(player.Center.Y - NPC.Center.Y);
			bool smashAttack = playerDistX < 15f && playerDistY < 40f;
            Lighting.AddLight(NPC.Center, Color.DarkOrange.R / 255, Color.DarkOrange.G / 255, Color.DarkOrange.B / 255);

            if (smashAttack) //Stop moving to smash players
			{
				NPC.velocity.X *= 0.9f;
				if(NPC.velocity.X < 0.2f) NPC.velocity.X = 0;
				NPC.spriteDirection = NPC.Center.X < player.Center.X ? 1 : -1;	
			}else
			{
				BaseAI.AIZombie(NPC, ref NPC.ai, false, true, -1, 0.1f, 2f, 5, 7, 120);	
				NPC.spriteDirection = NPC.velocity.X > 0 ? 1 : -1;				
			}

			int frameMax = smashAttack ? 8 : 5;
			NPC.frameCounter++;
			if (NPC.frameCounter >= frameMax)
			{
				NPC.frameCounter = 0;
				if(smashAttack)
				{
					NPC.frame.Y += frameHeightPlusFluff;
					if (NPC.frame.Y < frameHeightPlusFluff * 6 || NPC.frame.Y > frameHeightPlusFluff * 8)
					{
						NPC.frame.Y = frameHeightPlusFluff * 6;
					}
				}else
				{
					NPC.frame.Y += frameHeightPlusFluff;
					if (NPC.frame.Y > frameHeightPlusFluff * 5)
					{
						NPC.frame.Y = 0;
					}
				}
			}
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("FlamebruteGoreBackArm").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("FlamebruteGoreBackLeg").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("FlamebruteGoreBody").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("FlamebruteGoreFrontArm").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("FlamebruteGoreFrontLeg").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("FlamebruteGoreHead").Type, 1f);
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.Player.GetModPlayer<AAPlayer>().ZoneInferno && Main.dayTime ? 1f : 0f;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DragonScale>()));
        }
    }
}


