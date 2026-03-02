using System;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles
{
    public class ChaosYariShot : AAProjectile
    {
        public bool spineEnd = false;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Chaos Yari");
        }

        public override void SetDefaults()
        {
            Projectile.width = 28;
            Projectile.height = 28;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 320;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.damage = 1;
            Projectile.penetrate = -1;
            Projectile.alpha = 255;
            Projectile.DamageType = DamageClass.Melee;
        }

        public override void AI()
        {
            BaseAI.AIVilethorn(Projectile, 80, 4, 20);
        }

        public override void PostAI()
        {
            if (Main.netMode != NetmodeID.Server && Projectile.alpha < 170 && Projectile.alpha + 5 >= 170)
            {
                for (int j = 0; j < 4; j++)
                {
                    Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, Main.rand.Next(2) == 0 ? Terraria.ModLoader.ModContent.DustType<Dusts.AkumaDust>() : Terraria.ModLoader.ModContent.DustType<Dusts.YamataAuraDust>(), Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
                }
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Color newLightColor = new Color(Math.Max(0, Color.Orange.R + Math.Min(0, -Projectile.alpha + 20)), Math.Max(0, Color.Orange.G + Math.Min(0, -Projectile.alpha + 20)), Math.Max(0, Color.Orange.B + Math.Min(0, -Projectile.alpha + 20)));
            Color newLightColor2 = new Color(Math.Max(0, Color.Indigo.R + Math.Min(0, -Projectile.alpha + 20)), Math.Max(0, Color.Indigo.G + Math.Min(0, -Projectile.alpha + 20)), Math.Max(0, Color.Indigo.B + Math.Min(0, -Projectile.alpha + 20)));
            BaseDrawing.AddLight(Projectile.Center, newLightColor);
            BaseDrawing.AddLight(Projectile.Center, newLightColor2);
            BaseDrawing.DrawTexture(sb, TextureAssets.Projectile[Projectile.type].Value, 0, Projectile);
            return false;
        }
    }
}