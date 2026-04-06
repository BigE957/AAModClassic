using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Enemies.Terrarium.PreHM
{
    public class PuritySphere : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Purity Sphere");
		}

		public override void SetDefaults()
		{
            NPC.width = 26;
            NPC.height = 26;
            NPC.lifeMax =  60;
            NPC.defense = 5;
            NPC.damage = 10;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.4f;
            NPC.alpha = 255;
            NPC.noGravity = true;
            NPC.noTileCollide = false;
            Banner = NPC.type;
			BannerItem = ModContent.ItemType<Items.Banners.PuritySphereBanner>();
        }

        public override Color? GetAlpha(Color drawColor)
        {
            return Color.White;
        }

        public float[] shootAI = new float[4];

        public override void AI()
        {
            Player player = Main.player[NPC.target];
            BaseAI.AISkull(NPC, ref NPC.ai, true, 4, 300, .011f, .020f);
            BaseAI.ShootPeriodic(NPC, player.position, player.width, player.height, Mod.ProjType("PurityShot")/*TerraShot*/, ref shootAI[0], 120, (int)(NPC.damage * (Main.expertMode ? 0.25f : 0.5f)), 3f, true, new Vector2(20f, 15f));
            if (NPC.alpha != 0)
            {
                for (int spawnDust = 0; spawnDust < 2; spawnDust++)
                {
                    int num935 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.Terra, 0f, 0f, 100, default, .8f);
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

        public override void OnKill()
        {
            if (Main.rand.Next(4) == 0)
            {
                Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<TerraShard>());
            }
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.Terra, 0f, 0f, 0);
                }
            }
        }
    }
}
