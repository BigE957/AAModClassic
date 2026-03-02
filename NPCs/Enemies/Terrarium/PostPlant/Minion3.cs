
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Enemies.Terrarium.PostPlant
{
    public class Minion3 : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Terra Sphere");
		}

		public override void SetDefaults()
		{
            NPC.width = 26;
            NPC.height = 26;
            NPC.lifeMax =  350;
            NPC.defense = 20;
            NPC.damage = 10;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.4f;
            NPC.alpha = 255;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("TerraSphereBanner").Type;
        }


        public float[] shootAI = new float[4];

        public override void AI()
        {
            Player player = Main.player[NPC.target];

            BaseAI.AISkull(NPC, ref NPC.ai, false, 4, 300, .011f, .020f);
            BaseAI.ShootPeriodic(NPC, player.position, player.width, player.height, Mod.ProjType("SummonBlast"), ref shootAI[0], 120, (int)(NPC.damage * (Main.expertMode ? 0.25f : 0.5f)), 3f, true, new Vector2(20f, 15f));

            if (NPC.alpha != 0)
            {
                for (int spawnDust = 0; spawnDust < 2; spawnDust++)
                {
                    int num935 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, Mod.Find<ModDust>("SummonDust").Type, 0f, 0f, 100, default, 2f);
                    Main.dust[num935].noGravity = true;
                    Main.dust[num935].noLight = true;
                }
            }
            NPC.alpha -= 12;
            if (NPC.alpha < 0)
            {
                NPC.alpha = 0;
            }
        }

        public void FireMagic(NPC npc, Vector2 velocity)
        {
            Player player = Main.player[npc.target];
            BaseAI.ShootPeriodic(npc, player.position, player.width, player.height, Mod.ProjType("SummonBlast"), ref shootAI[0], 5, (int)(npc.damage * (Main.expertMode ? 0.25f : 0.5f)), 24f, true, new Vector2(20f, 15f));
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

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(ModContent.BuffType<Buffs.Terrablaze>(), 300);
        }
    }
}
