using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles     //We need this to basically indicate the folder where it is to be read from, so you the texture will load correctly
{
    public class DecayScythe : ModProjectile
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


        }

        public override void SetDefaults()
        {
            Projectile.width = 140;
            Projectile.height = 140;
            Projectile.friendly = true;
            Projectile.penetrate = -1; 
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;  
            Projectile.DamageType = DamageClass.Melee;
            
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            Color color = BaseUtility.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, AAColor.CursedInferno, AAColor.Ichor);
            if (Main.myPlayer == Projectile.owner)
            {
                if (!player.channel || player.noItems || player.CCed)
                {
                    Projectile.Kill();
                }
            }
            Lighting.AddLight(Projectile.Center, color.R / 255, color.G / 255, color.B / 255);     //this is the projectile light color R, G, B (Red, Green, Blue)
            Projectile.Center = player.MountedCenter;
            Projectile.position.X += player.width / 2 * player.direction;  //this is the projectile width sptrite direction from the playr
            Projectile.spriteDirection = player.direction;
            Projectile.rotation += .5f * player.direction; //this is the projectile rotation/spinning speed
            if (Projectile.rotation > MathHelper.TwoPi)
            {
                Projectile.rotation -= MathHelper.TwoPi;
            }
            else if (Projectile.rotation < 0)
            {
                Projectile.rotation += MathHelper.TwoPi;
            }
            player.heldProj = Projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;
            player.itemRotation = Projectile.rotation;
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                Projectile.ai[1]++;
                if (Projectile.ai[1] > 20)
                {
                    Projectile.ai[1] = 0;
                    Vector2 vector = new Vector2(player.position.X + player.width * 0.5f, player.position.Y + player.height * 0.5f);
                    float num22 = Main.mouseX + Main.screenPosition.X - vector.X;
                    float num23 = Main.mouseY + Main.screenPosition.Y - vector.Y;
                    if (player.gravDir == -1f)
                    {
                        num23 = Main.screenPosition.Y + Main.screenHeight - Main.mouseY - vector.Y;
                    }
                    float num24 = (float)Math.Sqrt(num22 * num22 + num23 * num23);
                    if ((float.IsNaN(num22) && float.IsNaN(num23)) || (num22 == 0f && num23 == 0f))
                    {
                        num22 = player.direction;
                        num23 = 0f;
                        num24 = 10;
                    }
                    else
                    {
                        num24 = 10 / num24;
                    }
                    num22 *= num24;
                    num23 *= num24;
                    int a = Projectile.NewProjectile(Projectile.GetSource_FromThis(), vector.X, vector.Y, num22, num23, ModContent.ProjectileType<DecayScytheProj>(), Projectile.damage, Projectile.knockBack, player.whoAmI, 0f, 0f);
                    Main.projectile[a].netUpdate = true;
                    SoundEngine.PlaySound(SoundID.Item71, Projectile.Center);
                }
            }
            
 
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Ichor, 1000);
            target.AddBuff(BuffID.CursedInferno, 1000);
        }

        public override bool PreDraw(ref Color lightColor)  //this make the projectile sprite rotate perfectaly around the player
        {
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, null, Color.White, Projectile.rotation, new Vector2(texture.Width / 2, texture.Height / 2), 1f, Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            return false;
        }

    }
}
