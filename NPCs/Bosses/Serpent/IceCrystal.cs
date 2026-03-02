using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Bosses.Serpent
{
    public class IceCrystal : ModNPC
    {
        public override void SetDefaults()
        {
            NPC.life = 200;
            NPC.defense = 10;
            NPC.width = 30;
            NPC.height = 30;
            NPC.aiStyle = -1;
            NPC.alpha = 255;
            NPC.value = 0;
            NPC.noGravity = true;
        }

		public override void SetStaticDefaults()
		{
		    // DisplayName.SetDefault("Ice Crystal");
		}

        public override void AI()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.ai[2]++;
            }
            if (NPC.alpha > 40)
            {
                NPC.alpha -= 3;
            }
            else
            {
                BaseAI.ShootPeriodic(NPC, NPC.position, NPC.width, NPC.height, ModContent.ProjectileType<IceSpike>(), ref NPC.ai[0], 180, NPC.damage / 2, 7, true);
                NPC.alpha = 40;
            }
        }

        public override void OnKill()
        {
            SoundEngine.PlaySound(SoundID.Item50, NPC.position);
            int pieCut = 20;
            for (int m = 0; m < pieCut; m++)
            {
                int dustID = Dust.NewDust(new Vector2(NPC.Center.X - 1, NPC.Center.Y - 1), 2, 2, ModContent.DustType<SnowDust>(), 0f, 0f, 100, Color.White, 1.6f);
                Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(6f, 0f), m / pieCut * 6.28f);
            }
            for (int m = 0; m < pieCut; m++)
            {
                int dustID = Dust.NewDust(new Vector2(NPC.Center.X - 1, NPC.Center.Y - 1), 2, 2, ModContent.DustType<SnowDust>(), 0f, 0f, 100, Color.White, 2f);
                Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(9f, 0f), m /pieCut * 6.28f);
                Main.dust[dustID].noLight = false;
            }
        }
    }
}
