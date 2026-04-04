using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles.Akuma   //The directory for your .cs and .png; Example: TutorialMOD/Projectiles
{
    public class Solar : ModProjectile   //make sure the sprite file is named like the class name (CustomYoyoProjectile)
    {
        public short customGlowMask = 0;
        public override void SetStaticDefaults()
        {
            if (Main.netMode != NetmodeID.Server)
            {
                Asset<Texture2D>[] glowMasks = new Asset<Texture2D>[TextureAssets.GlowMask.Length + 1];
                for (int i = 0; i < TextureAssets.GlowMask.Length; i++)
                {
                    glowMasks[i] = TextureAssets.GlowMask[i];
                }
                glowMasks[glowMasks.Length - 1] = ModContent.Request<Texture2D>("AAModClassic/Glowmasks/" + GetType().Name + "_Glow");
                customGlowMask = (short)(glowMasks.Length - 1);
                TextureAssets.GlowMask = glowMasks;
            }
            Projectile.glowMask = customGlowMask;
            // DisplayName.SetDefault("Solar");
        }

        public override void SetDefaults()
        {
            Projectile.extraUpdates = 2;
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.aiStyle = ProjAIStyleID.Yoyo;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Melee;
            ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = 60f;
            ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 1000f;
            ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 17f;
        }

        public override void AI()
        {
            Dust dust1;
            Dust dust2;
            Vector2 position = Projectile.position;
            dust1 = Main.dust[Dust.NewDust(position, Projectile.width, Projectile.height, ModContent.DustType<Dusts.AkumaDust>(), 0f, 0f, 0, default, 1f)];
            dust2 = Main.dust[Dust.NewDust(position, Projectile.width, Projectile.height, ModContent.DustType<Dusts.AkumaADust>(), 0f, 0f, 0, default, 1f)];
            dust1.noGravity = true;
            dust2.noGravity = true;

            Player player = Main.player[Projectile.owner];
            if (Main.myPlayer == Projectile.owner)
            {
                if (Main.rand.Next(35) == 0)
                {
                    float num78 = Main.mouseX + Main.screenPosition.X - Projectile.Center.X;
                    float num79 = Main.mouseY + Main.screenPosition.Y - Projectile.Center.Y;
                    Vector2 value2 = new Vector2(num78, num79);
                    value2.Normalize();
                    Vector2 value3 = new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
                    value3.Normalize();
                    value2 = value2 * 6f + value3;
                    value2.Normalize();
                    value2 *= 10f;
                    float num91 = Main.rand.Next(10, 50) * 0.001f;
                    if (Main.rand.Next(2) == 0)
                    {
                        num91 *= -1f;
                    }
                    float num92 = Main.rand.Next(10, 50) * 0.001f;
                    if (Main.rand.Next(2) == 0)
                    {
                        num92 *= -1f;
                    }
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, value2.X, value2.Y, ModContent.ProjectileType<FireTentacle>(), Projectile.damage * (int)1.25f, Projectile.knockBack, player.whoAmI, num92, num91);
                }
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Daybreak, 300);
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, Projectile.velocity, ModContent.ProjectileType<AkumaExp>(), Projectile.damage, Projectile.knockBack, Projectile.owner, Projectile.whoAmI);
        }
    }
}
