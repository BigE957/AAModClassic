using AAModClassic.Dusts;
using AAModClassic.UI.World;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu._DeityEye
{
    internal class DeityEye_DeityFlames : FireProj
    {
        public override MulticolorShift ColorShift
        {
            get
            {
                Color color = Color.Cyan;
                Color color2 = Color.Aqua;
                Color color3 = Color.Lerp(Color.Lime, color2, 0.25f);
                Color color4 = Color.Green;

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

        public override int DustType => -1;// ModContent.DustType<CthulhuDust>();

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Flames of Agony");
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.hostile = true;
            Projectile.friendly = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = 1;
            //Projectile.alpha = 255;
            Projectile.alpha = 60;
            Projectile.timeLeft = 100;
            Projectile.aiStyle = -1;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                return base.PreDraw(ref lightColor);
            return false;
        }

        public override void AI()
        {
            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
            {
                base.AI();
                return;
            }

            if (Projectile.timeLeft > 60)
            {
                Projectile.timeLeft = 60;
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
                int num297 = ModContent.DustType<Dusts.CthulhuDust>();
                if (Main.rand.NextBool(2))
                {
                    for (int num298 = 0; num298 < 3; num298++)
                    {
                        int num299 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, num297, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, default, 1f);
                        if (Main.rand.NextBool(3))
                        {
                            Main.dust[num299].noGravity = true;
                            Main.dust[num299].scale *= 3f;
                            Dust expr_DD5D_cp_0 = Main.dust[num299];
                            expr_DD5D_cp_0.velocity.X = expr_DD5D_cp_0.velocity.X * 2f;
                            Dust expr_DD7D_cp_0 = Main.dust[num299];
                            expr_DD7D_cp_0.velocity.Y = expr_DD7D_cp_0.velocity.Y * 2f;
                        }
                        Main.dust[num299].scale *= 1f;
                        Dust expr_DDE2_cp_0 = Main.dust[num299];
                        expr_DDE2_cp_0.velocity.X = expr_DDE2_cp_0.velocity.X * 1.2f;
                        Dust expr_DE02_cp_0 = Main.dust[num299];
                        expr_DE02_cp_0.velocity.Y = expr_DE02_cp_0.velocity.Y * 1.2f;
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
            target.AddBuff(ModContent.BuffType<RealityBent_Buff>(), 200);
        }
    }
}