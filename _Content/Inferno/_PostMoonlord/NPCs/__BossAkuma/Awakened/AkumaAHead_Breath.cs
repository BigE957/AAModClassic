using AAModClassic._Content.Inferno.Buffs;
using AAModClassic.UI.World;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno._PostMoonlord.NPCs.__BossAkuma.Awakened
{
    public class AkumaAHead_Breath : FireProj
    {
        public override MulticolorShift ColorShift
        {
            get
            {
                Color color = new(95, 120, 255, 200);
                Color color2 = new(50, 180, 255, 70);
                Color color3 = Color.Lerp(new Color(95, 160, 255, 100), color2, 0.25f);
                Color color4 = new(33, 125, 202, 100);

                return new MulticolorShift
                ([
                    new(Color.Transparent, 0f,   0.1f),   // fade in
                    new(color,             0f,   0.1f),   // to color2
                    new(color2,            0.15f, 0.35f), // hold then to color3
                    new(color3,            0f,   0.15f),  // to color4
                    new(color4,            0f,   0.15f)   // to final
                ]);
            }
        }

        public override int DustType => DustID.IceTorch;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Blazing Fury");
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.damage = 30;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.penetrate = 1;
            Projectile.alpha = 60;
            Projectile.timeLeft = 60;
        }

        public override void AI()
        {
            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
            {
                base.AI();
                return;
            }

            if (Projectile.ai[0] > 7f)
            {
                float num296 = 1f;
                if (Projectile.ai[0] == 8f)
                {
                    num296 = 0.25f;
                }
                else if (Projectile.ai[0] == 9f)
                {
                    num296 = 0.5f;
                }
                else if (Projectile.ai[0] == 10f)
                {
                    num296 = 0.75f;
                }
                Projectile.ai[0] += 1f;
                int num297 = ModContent.DustType<Dusts.AkumaADust>();
                if (Main.rand.NextBool(2))
                {
                    for (int num298 = 0; num298 < 3; num298++)
                    {
                        int num299 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, num297, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100);
                        Main.dust[num299].noGravity = true;
                        if (Main.rand.NextBool(3))
                        {
                            Main.dust[num299].scale *= 2f;
                            Dust expr_DD5D_cp_0 = Main.dust[num299];
                            expr_DD5D_cp_0.velocity.X *= 2f;
                            Dust expr_DD7D_cp_0 = Main.dust[num299];
                            expr_DD7D_cp_0.velocity.Y *= 2f;
                        }
                        Dust expr_DDE2_cp_0 = Main.dust[num299];
                        expr_DDE2_cp_0.velocity.X *= 1.2f;
                        Dust expr_DE02_cp_0 = Main.dust[num299];
                        expr_DE02_cp_0.velocity.Y *= 1.2f;
                        Main.dust[num299].scale *= num296;
                        Main.dust[num299].velocity += Projectile.velocity;
                        if (!Main.dust[num299].noGravity)
                        {
                            Main.dust[num299].velocity *= 0.5f;
                        }
                    }
                }
            }
            else
            {
                Projectile.ai[0] += 1f;
            }
            Projectile.rotation += 0.3f * Projectile.direction;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(ModContent.BuffType<DragonFire_Buff>(), 600);
        }
    }
}