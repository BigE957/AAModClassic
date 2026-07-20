using AAModClassic._Content.Terrarium.__Hardmode.Items.Materials;
using AAModClassic._Content.Terrarium.__Hardmode.Items.Weapons;
using AAModClassic._Content.Terrarium.Buffs;
using AAModClassic._Content.Terrarium.World.Biomes;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Utilities.Interfaces;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using static AAModClassic.Utilities.ItemDropRuleConditionUtils;


namespace AAModClassic._Content.Terrarium.__Hardmode.NPCs
{
    public class TerraWizard : ModNPC, IBannerNPC
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Terra Wizard");
			Main.npcFrameCount[NPC.type] = 6;
		}

		public override void SetDefaults()
		{
            NPC.lifeMax = 600;
            NPC.defense = 40;
            NPC.damage = 90;
            NPC.width = 22;
            NPC.height = 56;
            NPC.aiStyle = -1;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.4f;
            NPC.noGravity = true;
            //Banner = NPC.type;
			//BannerItem = ModContent.ItemType<AAModClassic.Items.Banners.TerraWizardBanner>();
            SpawnModBiomes = [ModContent.GetInstance<TerrariumBiome>().Type];
        }

        public float[] shootAI = new float[4];

        public override void AI()
        {
            if (NPC.velocity.X < 0f)
            {
                NPC.spriteDirection = -1;

            }
            else
            {
                NPC.spriteDirection = 1;
            }
            NPC.noGravity = true;
            NPC.TargetClosest(true);
            Player player = Main.player[NPC.target];
            BaseAI.AISpaceOctopus(NPC, ref NPC.ai, Main.player[NPC.target].Center, 0.15f, 6f, 250f, 70f, FireMagic);
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;
            if (NPC.frameCounter >= 10)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += frameHeight;
                if (NPC.frame.Y > frameHeight * 5)
                {
                    NPC.frameCounter = 0;
                    NPC.frame.Y = 0;
                }
            }
        }

        public void FireMagic(NPC npc, Vector2 velocity)
        {
            Player player = Main.player[npc.target];
            BaseAI.ShootPeriodic(npc, player.position, player.width, player.height, ModContent.ProjectileType<TerraWizard_MagicBlast>(), ref shootAI[0], 5, (int)(npc.damage * (Main.expertMode ? 0.25f : 0.5f)), 12f, true, new Vector2(20f, 15f));
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                if (!Main.dedServ)
                {
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("TerraWizardGore1").Type, 1f);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("TerraWizardGore2").Type, 1f);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("TerraWizardGore3").Type, 1f);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("TerraWizardGore4").Type, 1f);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("TerraWizardGore5").Type, 1f);
                }
                NPC.position.X = NPC.position.X + NPC.width / 2;
                NPC.position.Y = NPC.position.Y + NPC.height / 2;
                NPC.width = 44;
                NPC.height = 78;
                NPC.position.X = NPC.position.X - NPC.width / 2;
                NPC.position.Y = NPC.position.Y - NPC.height / 2;
                int dust1 = ModContent.DustType<Dusts.TMagicDust>();
                int dust2 = ModContent.DustType<Dusts.TMagicDust>();
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

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            LeadingConditionRule notUnreleasedRule = new(new NotUnreleasedAndIsUnofficial());

            notUnreleasedRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<TerraPrism>(), 40));

            npcLoot.Add(notUnreleasedRule);

            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<TerraFocus>(), 20));
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(ModContent.BuffType<Terrablaze_Buff>(), 300);
        }
    }
}
