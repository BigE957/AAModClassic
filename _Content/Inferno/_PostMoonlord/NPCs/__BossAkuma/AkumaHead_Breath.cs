using AAModClassic._Content.Inferno.Buffs;
using AAModClassic.UI.WorldGen;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno._PostMoonlord.NPCs.__BossAkuma
{
    internal class AkumaHead_Breath : FireProj
    {
        public override MulticolorShift ColorShift 
        { get {
            Color color = new(176, 7, 65, 200);
            Color color2 = new(255, 205, 20, 70);
            Color color3 = Color.Lerp(new Color(176, 7, 65, 100), color2, 0.25f);
            Color color4 = new(80, 80, 80, 100);

            return new MulticolorShift
            ([
                new(Color.Transparent, 0f,   0.1f),   // fade in
                new(color,             0f,   0.1f),   // to color2
                new(color2,            0.15f, 0.35f), // hold then to color3
                new(color3,            0f,   0.15f),  // to color4
                new(color4,            0f,   0.15f)   // to final
            ]);
        }} 

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Blazing Fury");
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.hostile = true;
            Projectile.friendly = false;
            Projectile.damage = 25;
            Projectile.ignoreWater = true;
            Projectile.penetrate = 1;
            Projectile.alpha = 60;
            Projectile.timeLeft = 60;
        }

        public override void AI()
        {
            if(WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
            {
                base.AI();
                return;
            }

            if (Projectile.ai[0] > 7f)
            {
                float scale = 1f;
                if (Projectile.ai[0] == 8f)
                {
                    scale = 0.25f;
                }
                else if (Projectile.ai[0] == 9f)
                {
                    scale = 0.5f;
                }
                else if (Projectile.ai[0] == 10f)
                {
                    scale = 0.75f;
                }
                Projectile.ai[0] += 1f;
                if (Main.rand.NextBool(2))
                {
                    for (int num298 = 0; num298 < 4; num298++)
                    {
                        int d = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<Dusts.AkumaDust>(), Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100);
                        Main.dust[d].noGravity = true;
                        if (Main.rand.NextBool(3))
                        {
                            Main.dust[d].scale *= 2f;
                            Main.dust[d].velocity.X *= 2f;
                            Main.dust[d].velocity.Y *= 2f;
                        }
                        Main.dust[d].velocity.X *= 1.2f;
                        Main.dust[d].velocity.Y *= 1.2f;
                        Main.dust[d].scale *= scale;
                        Main.dust[d].velocity += Projectile.velocity;
                        if (!Main.dust[d].noGravity)
                        {
                            Main.dust[d].velocity *= 0.5f;
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