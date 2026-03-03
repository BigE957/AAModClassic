using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles
{
    public class RainbowCatPro : ModProjectile
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
                        int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, -16 + Main.rand.Next(0, 33), -16 + Main.rand.Next(0, 33), Shoot, Projectile.damage, 3, Main.myPlayer);
						Main.projectile[proj].usesLocalNPCImmunity = true;
						Main.projectile[proj].localNPCHitCooldown = 4;
						//Main.projectile[proj].melee = false/* tModPorter Suggestion: Remove. See Item.DamageType */;
						Main.projectile[proj].DamageType = DamageClass.Magic;
                        int proj1 = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, -16 + Main.rand.Next(0, 33), -16 + Main.rand.Next(0, 33), Shoot, Projectile.damage, 3, Main.myPlayer);
						Main.projectile[proj1].usesLocalNPCImmunity = true;
						Main.projectile[proj1].localNPCHitCooldown = 4;
						//Main.projectile[proj1].melee = false/* tModPorter Suggestion: Remove. See Item.DamageType */;
						Main.projectile[proj1].DamageType = DamageClass.Magic;
                    }
                    if (Main.rand.Next(50) == 0)
                    {
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, -16 + Main.rand.Next(0, 33), -16 + Main.rand.Next(0, 33), ProjectileID.RainbowRodBullet, Projectile.damage, 3, Main.myPlayer);
                    }
                }
            }
            Projectile.velocity.X *= 0.00f;
            Projectile.velocity.Y += 0.00f;
            Projectile.localAI[0] += 1f;
            if (Projectile.localAI[0] > 300f) //projectile time left before disappears
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Enchanted_Pink, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, 20, default, 2f);
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.BlueTorch, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, 20, default, 2f);
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.RedTorch, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, 20, default, 2f);
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.GreenTorch, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, 20, default, 2f);
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.PurpleTorch, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, 20, default, 2f);
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.YellowTorch, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, 20, default, 2f);
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.DemonTorch, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, 20, default, 2f);
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Enchanted_Pink, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, 20, default, 2f);
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.BlueTorch, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, 20, default, 2f);
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.RedTorch, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, 20, default, 2f);
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.GreenTorch, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, 20, default, 2f);
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.PurpleTorch, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, 20, default, 2f);
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.YellowTorch, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, 20, default, 2f);
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.DemonTorch, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, 20, default, 2f);
                Projectile.Kill();
            }
        }
    }
}