using AAModClassic._Content.Inferno.Buffs;
using AAModClassic._Content.Mire.Buffs;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Bosses.Shen
{
    public class DiscordianInferno : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Discordian Inferno");
        }

        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.aiStyle = ProjAIStyleID.Arrow;
            Projectile.penetrate = -1;
            Projectile.friendly = false;
            Projectile.hostile = true;
			Projectile.extraUpdates = 1;
        }


        public override void AI()
        {
            int dustType = Projectile.ai[0] == 1 ? ModContent.DustType<Dusts.AkumaADust>() : ModContent.DustType<Dusts.YamataADust>();
            if (Projectile.localAI[0] == 0f)
            {
                Projectile.localAI[0] = 1f;
                SoundEngine.PlaySound(SoundID.DD2_BetsyFireballShot, Projectile.Center);
            }
            if (InternalAI[0] >= 2f)
            {
                Projectile.alpha -= 30;
                if (Projectile.alpha < 0)
                {
                    Projectile.alpha = 0;
                }
            }
			if(Main.rand.NextBool(3))
			{
				for(int m = 0; m < 3; m++)
				{
					int dustID = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, dustType, 0f, 0f, 100, Color.White, 1.6f);
					Main.dust[dustID].velocity = -Projectile.velocity * 0.5f;
					Main.dust[dustID].noLight = false;
					Main.dust[dustID].noGravity = true;
				}
				int dustID2 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, dustType, 0f, 0f, 100, Color.Purple, 2f);
				Main.dust[dustID2].velocity = -Projectile.velocity * 0.5f;
				Main.dust[dustID2].noLight = false;
				Main.dust[dustID2].noGravity = true;
			}
        }

        public override void OnKill(int timeLeft)
        {
            int dustType = Projectile.ai[0] == 1 ? ModContent.DustType<Dusts.AkumaADust>() : ModContent.DustType<Dusts.YamataADust>();
            int pieCut = 20;
			for(int m = 0; m < pieCut; m++)
			{
				int dustID = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, dustType, 0f, 0f, 100, Color.White, 1.6f);
				Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(6f, 0f), m / (float)pieCut * 6.28f);
				Main.dust[dustID].noLight = false;
				Main.dust[dustID].noGravity = true;
			}
			for(int m = 0; m < pieCut; m++)
			{
				int dustID = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, dustType, 0f, 0f, 100, Color.White, 2f);
				Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(9f, 0f), m / (float)pieCut * 6.28f);
				Main.dust[dustID].noLight = false;
				Main.dust[dustID].noGravity = true;
			}
			for(int m = 0; m < 15; m++)
			{
				int dustID = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, dustType, 0f, 0f, 100, Color.White, 1.2f);
				Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(8f + Main.rand.Next(6), 0f), MathHelper.Lerp((float)Main.rand.NextDouble(), 0f, 6.28f));
				Main.dust[dustID].noLight = false;
				Main.dust[dustID].noGravity = true;
			}
            SoundEngine.PlaySound(SoundID.Item62, Projectile.position);
        }


        public override Color? GetAlpha(Color lightColor)
        {
            Color color = Projectile.ai[0] == 1 ? AAColor.AkumaA : AAColor.YamataA ;
            return new Color(color.R, color.G, color.B, 200);
        }

        public float[] InternalAI = new float[1];
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == NetmodeID.Server || Main.dedServ)
            {
                writer.Write(InternalAI[0]);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                InternalAI[0] = reader.ReadSingle();
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(Projectile.ai[0] == 1 ? ModContent.BuffType<DragonFire_Buff>() : ModContent.BuffType<HydraToxin_Buff>(), 200);
        }
    }
}