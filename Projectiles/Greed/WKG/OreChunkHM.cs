using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using AAModClassic.Dusts;
using AAModClassic.Buffs;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.___Content.Mire.Buffs;


namespace AAModClassic.Projectiles.Greed.WKG
{
    public class OreChunkHM : ModProjectile
    {
        public override string Texture => "AAModClassic/Projectiles/Greed/WKG/OreChunkM";
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
			Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = 6;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ignoreWater = true;
        }

		public override void SetStaticDefaults()
		{
		    // DisplayName.SetDefault("Ore");
            Main.projFrames[Projectile.type] = 28;
		}

        public override void AI()
        {
            OreEffect();
            if (Projectile.velocity.X > 0)
            {
                Projectile.direction = 1;
            }
            else
            {
                Projectile.direction = -1;
            }
            Projectile.rotation += .2f * Projectile.direction;
            for (int m = Projectile.oldPos.Length - 1; m > 0; m--)
            {
                Projectile.oldPos[m] = Projectile.oldPos[m - 1];
            }
            Projectile.oldPos[0] = Projectile.position;
        }

        public override void PostAI()
        {
            Projectile.frame = (int)Projectile.ai[1];
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Rectangle frame = BaseDrawing.GetFrame(Projectile.frame, TextureAssets.Projectile[Projectile.type].Value.Width, TextureAssets.Projectile[Projectile.type].Value.Height / 28, 0, 0);
            if (Projectile.ai[1] == 9 || Projectile.ai[1] == 11 || Projectile.ai[1] == 22 || Projectile.ai[1] == 26)
            {
                 BaseDrawing.DrawAfterimage(Main.spriteBatch, TextureAssets.Projectile[Projectile.type].Value, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.oldPos, 1, Projectile.rotation, Projectile.direction, 28, frame, .8f, 1, 4, true, 0, 0, lightColor);
            }
            BaseDrawing.DrawTexture(Main.spriteBatch, TextureAssets.Projectile[Projectile.type].Value, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, Projectile.rotation, 0, 28, frame, lightColor, true);
            return false;
        }

        public override void OnKill(int timeLeft)
        {
            int DustType = DType();
            if (Projectile.ai[1] == 8)
            {
                for (int num291 = 0; num291 < 5; num291++)
                {
                    int num292 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default, 2.1f);
                    Main.dust[num292].velocity *= 2f;
                    Main.dust[num292].noGravity = true;
                };
            }
            if (Projectile.ai[1] == 21)
            {
                for (int s = 0; s < 3; s++)
                {
                    Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.position, Vector2.Zero, ModContent.ProjectileType<OreSpores>(), Projectile.damage, Projectile.knockBack, Main.myPlayer, 0, s);
                }
            }
            if (Projectile.ai[1] == 22)
            {
                Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<LuminiteBlast>(), Projectile.damage, Projectile.knockBack, Main.myPlayer, 0, 0);
            }
            if (Projectile.ai[1] == 25)
            {
                Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<DaybreakBlast>(), Projectile.damage, Projectile.knockBack * 3, Main.myPlayer, 0, 0);
            }
            if (Projectile.ai[1] == 27)
            {
                for (int v = 0; v < 4; v++)
                {
                    int x = Main.rand.Next(-6, 6);
                    int y = -Main.rand.Next(3, 5);
                    int p = Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.position, new Vector2(x, y), ModContent.ProjectileType<AFrag>(), Projectile.damage, 0, Main.myPlayer, 0, Main.rand.Next(23));
                    Main.projectile[p].Center = Projectile.Center;
                }
            }
            for (int num468 = 0; num468 < 5; num468++)
            {
                float VelX = -Projectile.velocity.X * 0.2f;
                float VelY = -Projectile.velocity.Y * 0.2f;
                num468 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, DustType, VelX, VelY);
            }
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.FlatBonusDamage += Damage();
            switch ((int)Projectile.ai[1])
            {
                case 6:
                case 7:
                    target.AddBuff(BuffID.Midas, 180); break;

                case 12:
                case 13:
                    target.AddBuff(BuffID.OnFire, 180); break;

                case 23: target.AddBuff(ModContent.BuffType<Electrified_Buff>(), 180); break;
                case 25: target.AddBuff(BuffID.Daybreak, 180); break;
                case 26: target.AddBuff(ModContent.BuffType<Moonraze_Buff>(), 180); break;
            }
        }

        public void OreEffect()
        {
            switch ((int)Projectile.ai[1])
            {
                 case 9:
                 case 11:
                 case 24: Projectile.extraUpdates = 1;
                     break;
                 case 13:
                 case 14:
                     for (int num291 = 0; num291 < 5; num291++)
                     {
                         int num292 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100);
                         Main.dust[num292].velocity *= 2f;
                         Main.dust[num292].noGravity = true;
                     };
                     break;
                 case 25: Projectile.penetrate = 1;
                     break;
                 case 22:
                     Projectile.penetrate = 1; 
                     Projectile.extraUpdates = 2;
                     break;
                 case 26: Projectile.extraUpdates = 2;
                     break;
            }
        }

        public int Damage()
        {
            switch ((int)Projectile.ai[1])
            {
                case 0:
                    return 8;
                case 1:
                    return 9;
                case 2:
                    return 10;
                case 3:
                case 4:
                    return 11;
                case 5:
                    return 12;
                case 6:
                    return 13;
                case 7:
                    return 15;
                case 8:
                    return 21;
                case 9:
                    return 19;
                case 10:
                    return 22;
                case 11:
                    return 14;
                case 12:
                    return 26;
                case 13:
                    return 36;
                case 14:
                    return 39;
                case 15:
                    return 41;
                case 16:
                    return 44;
                case 17:
                    return 47;
                case 18:
                    return 50;
                case 19:
                    return 52;
                case 20:
                    return 57;
                case 21:
                    return 75;
                case 22:
                    return 110;
                case 23:
                    return 130;
                case 24:
                    return 170;
                case 25:
                    return 160;
                case 26:
                    return 130;
                case 27:
                    return 150;
                default:
                    goto case 0;
            }
        }

        public int DType()
        {
            switch ((int)Projectile.ai[1])
            {
                case 0:
                    return DustID.Copper;
                case 1:
                    return DustID.Tin;
                case 2:
                    return DustID.Iron;
                case 3:
                    return DustID.Lead;
                case 4:
                    return DustID.Silver;
                case 5:
                    return DustID.Tungsten;
                case 6:
                    return DustID.Gold;
                case 7:
                    return DustID.Platinum;
                case 8:
                    return DustID.t_Meteor;
                case 9:
                    return 14;
                case 10:
                    return 117;
                case 11:
                    return ModContent.DustType<Dusts.IncineriteDust>();
                case 12:
                    return ModContent.DustType<Dusts.AbyssiumDust>();
                case 13:
                    return DustID.Torch;
                case 14:
                    return 48;
                case 15:
                    return 144;
                case 16:
                    return 49;
                case 17:
                    return 145;
                case 18:
                    return 50;
                case 19:
                    return 146;
                case 20:
                    return DustID.Gold;
                case 21:
                    return 128;
                case 22:
                    return ModContent.DustType<Dusts.LuminiteDust>();
                case 23:
                    return ModContent.DustType<Dusts.DarkmatterDust>();
                case 24:
                    return ModContent.DustType<Dusts.RadiumDust>();
                case 25:
                    return ModContent.DustType<Dusts.DaybreakIncineriteDust>();
                case 26:
                    return ModContent.DustType<Dusts.YamataDust>();
                case 27:
                    return ModContent.DustType<Dusts.VoidDust>();
                default:
                    goto case 0;
            }
        }
    }
}
