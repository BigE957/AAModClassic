using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class RainbowCatPro : ModProjectile
    {
        public short customGlowMask = 0;
        public override void SetStaticDefaults()
        {
            if (Main.netMode != 2)
            {
                Texture2D[] glowMasks = new Texture2D[TextureAssets.GlowMask.Value.Length + 1];
                for (int i = 0; i < TextureAssets.GlowMask.Value.Length; i++)
                {
                    glowMasks[i] = TextureAssets.GlowMask[i].Value;
                }
                glowMasks[glowMasks.Length - 1] = Mod.GetTexture("Glowmasks/" + GetType().Name + "_Glow");
                customGlowMask = (short)(glowMasks.Length - 1);
                TextureAssets.GlowMask.Value = glowMasks;
            }
            Projectile.glowMask = customGlowMask;

            // DisplayName.SetDefault("Legendary Rainbow Cat");
            Main.projFrames[Projectile.type] = 17;
        }
        public override void SetDefaults()
        {
            Projectile.width = 42;
            Projectile.height = 46;
            Projectile.penetrate = -1;
            Projectile.hostile = false;
            Projectile.friendly = false;
            Projectile.alpha = 20;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.timeLeft = 300;
        }
        public override void AI()
        {
            if (++Projectile.frameCounter >= 3)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 17)
                {
                    Projectile.frame = 7;
                }
            }
            if (Projectile.localAI[0] > 21f) //projectile time left before disappears
            {
                int Shoot = ProjectileID.Meowmere;
                if (Main.myPlayer == Projectile.owner)
                {
                    if (Main.rand.Next(3) == 0)
                    {
                        int proj = Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, -16 + Main.rand.Next(0, 33), -16 + Main.rand.Next(0, 33), Shoot, Projectile.damage, 3, Main.myPlayer);
						Main.projectile[proj].usesLocalNPCImmunity = true;
						Main.projectile[proj].localNPCHitCooldown = 4;
						Main.projectile[proj].melee = false/* tModPorter Suggestion: Remove. See Item.DamageType */;
						Main.projectile[proj].DamageType = DamageClass.Magic;
                        int proj1 = Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, -16 + Main.rand.Next(0, 33), -16 + Main.rand.Next(0, 33), Shoot, Projectile.damage, 3, Main.myPlayer);
						Main.projectile[proj1].usesLocalNPCImmunity = true;
						Main.projectile[proj1].localNPCHitCooldown = 4;
						Main.projectile[proj1].melee = false/* tModPorter Suggestion: Remove. See Item.DamageType */;
						Main.projectile[proj1].DamageType = DamageClass.Magic;
                    }
                    if (Main.rand.Next(50) == 0)
                    {
                        Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, -16 + Main.rand.Next(0, 33), -16 + Main.rand.Next(0, 33), ProjectileID.RainbowRodBullet, Projectile.damage, 3, Main.myPlayer);
                    }
                }
            }
            Projectile.velocity.X *= 0.00f;
            Projectile.velocity.Y += 0.00f;
            Projectile.localAI[0] += 1f;
            if (Projectile.localAI[0] > 300f) //projectile time left before disappears
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 58, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, 20, default, 2f);
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 59, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, 20, default, 2f);
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 60, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, 20, default, 2f);
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 61, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, 20, default, 2f);
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 62, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, 20, default, 2f);
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 64, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, 20, default, 2f);
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 65, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, 20, default, 2f);
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 58, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, 20, default, 2f);
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 59, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, 20, default, 2f);
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 60, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, 20, default, 2f);
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 61, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, 20, default, 2f);
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 62, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, 20, default, 2f);
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 64, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, 20, default, 2f);
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 65, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, 20, default, 2f);
                Projectile.Kill();
            }
        }
    }
}