using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.NPCs.Enemies.Terrarium.PostPlant
{
    public class TerraWizard : ModNPC
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
            Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("TerraWizardBanner").Type;
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
            
            NPC.frameCounter++;
            if (NPC.frameCounter >= 10)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += 58;
                if (NPC.frame.Y > (58 * 5))
                {
                    NPC.frameCounter = 0;
                    NPC.frame.Y = 0;
                }
            }
        }

        public void FireMagic(NPC npc, Vector2 velocity)
        {
            Player player = Main.player[npc.target];
            BaseAI.ShootPeriodic(npc, player.position, player.width, player.height, Mod.ProjType("MagicBlast"), ref shootAI[0], 5, (int)(npc.damage * (Main.expertMode ? 0.25f : 0.5f)), 12f, true, new Vector2(20f, 15f));
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/TerraWizardGore1"), 1f);
                Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/TerraWizardGore2"), 1f);
                Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/TerraWizardGore3"), 1f);
                Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/TerraWizardGore4"), 1f);
                Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/TerraWizardGore5"), 1f);
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

        public override void OnKill()
        {
            if (Main.rand.Next(40) == 0)
            {
                Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<Items.Materials.TerraCrystal>());
            }
            if (Main.rand.Next(20) == 0)
            {
                Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<Items.Magic.TerraFocus>());
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(ModContent.BuffType<Buffs.Terrablaze>(), 300);
        }
    }
}
