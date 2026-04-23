using System;
using System.Collections.Generic;
using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;



namespace AAModClassic._Content.Snow.___PreHardmode.Items._BossSubzeroSerpent.Weapons
{
    public class SnakeStaff_SerpentHead : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;

            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.netImportant = true;
            Projectile.tileCollide = false;
            Projectile.minion = true;

            Projectile.penetrate = -1;
            Projectile.timeLeft = 18000;
            Projectile.timeLeft *= 5;
            Projectile.GetGlobalProjectile<AAGlobalProjectile>().LongMinion = true;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Snow Serpent");
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture2D13 = TextureAssets.Projectile[Projectile.type].Value;
            int num214 = TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type];
            int y6 = num214 * Projectile.frame;
            Main.spriteBatch.Draw(texture2D13, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Rectangle(0, y6, texture2D13.Width, num214),
                Projectile.GetAlpha(lightColor), Projectile.rotation, new Vector2(texture2D13.Width / 2f, num214 / 2f), Projectile.scale,
                Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            return false;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();

            if ((int)Main.time % 120 == 0) Projectile.netUpdate = true;
            if (!player.active)
            {
                Projectile.active = false;
                return;
            }
            if (player.dead) modPlayer.SnakeMinion = false;
            if (modPlayer.SnakeMinion) Projectile.timeLeft = 2;
            int num1038 = 30;

            Vector2 center = player.Center;
            float num1040 = 700f;
            float num1041 = 1000f;
            int num1042 = -1;
            if (Projectile.Distance(center) > 2000f)
            {
                Projectile.Center = center;
                Projectile.netUpdate = true;
            }

            bool flag66 = true;
            if (flag66)
            {
                NPC ownerMinionAttackTargetNPC5 = Projectile.OwnerMinionAttackTargetNPC;
                if (ownerMinionAttackTargetNPC5 != null && ownerMinionAttackTargetNPC5.CanBeChasedBy(Projectile, false))
                {
                    float num1043 = Projectile.Distance(ownerMinionAttackTargetNPC5.Center);
                    if (num1043 < num1040 * 2f)
                    {
                        num1042 = ownerMinionAttackTargetNPC5.whoAmI;
                    }
                }

                if (num1042 < 0)
                {
                    for (int num1044 = 0; num1044 < 200; num1044++)
                    {
                        NPC nPC13 = Main.npc[num1044];
                        if (nPC13.CanBeChasedBy(Projectile, false) && player.Distance(nPC13.Center) < num1041)
                        {
                            float num1045 = Projectile.Distance(nPC13.Center);
                            if (num1045 < num1040)
                            {
                                num1042 = num1044;
                            }
                        }
                    }
                }
            }

            if (num1042 != -1)
            {
                NPC nPC14 = Main.npc[num1042];
                Vector2 vector132 = nPC14.Center - Projectile.Center;
                (vector132.X > 0f).ToDirectionInt();
                (vector132.Y > 0f).ToDirectionInt();
                float scaleFactor15 = 0.4f;
                if (vector132.Length() < 600f) scaleFactor15 = 0.6f;
                if (vector132.Length() < 300f) scaleFactor15 = 0.8f;
                if (vector132.Length() > nPC14.Size.Length() * 0.75f)
                {
                    Projectile.velocity += Vector2.Normalize(vector132) * scaleFactor15 * 1.5f;
                    if (Vector2.Dot(Projectile.velocity, vector132) < 0.25f) Projectile.velocity *= 0.8f;
                }

                float num1046 = 30f;
                if (Projectile.velocity.Length() > num1046) Projectile.velocity = Vector2.Normalize(Projectile.velocity) * num1046;
            }
            else
            {
                float num1047 = 0.2f;
                Vector2 vector133 = center - Projectile.Center;
                if (vector133.Length() < 200f) num1047 = 0.12f;
                if (vector133.Length() < 140f) num1047 = 0.06f;
                if (vector133.Length() > 100f)
                {
                    if (Math.Abs(center.X - Projectile.Center.X) > 20f) Projectile.velocity.X = Projectile.velocity.X + num1047 * Math.Sign(center.X - Projectile.Center.X);
                    if (Math.Abs(center.Y - Projectile.Center.Y) > 10f) Projectile.velocity.Y = Projectile.velocity.Y + num1047 * Math.Sign(center.Y - Projectile.Center.Y);
                }
                else if (Projectile.velocity.Length() > 2f)
                {
                    Projectile.velocity *= 0.96f;
                }

                if (Math.Abs(Projectile.velocity.Y) < 1f) Projectile.velocity.Y = Projectile.velocity.Y - 0.1f;
                float num1048 = 15f;
                if (Projectile.velocity.Length() > num1048) Projectile.velocity = Vector2.Normalize(Projectile.velocity) * num1048;
            }

            Projectile.rotation = Projectile.velocity.ToRotation() + 1.57079637f;
            int direction = Projectile.direction;
            Projectile.direction = Projectile.spriteDirection = Projectile.velocity.X > 0f ? 1 : -1;
            if (direction != Projectile.direction) Projectile.netUpdate = true;
            float num1049 = MathHelper.Clamp(Projectile.localAI[0], 0f, 50f);
            Projectile.position = Projectile.Center;
            Projectile.scale = 1f + num1049 * 0.01f;
            Projectile.width = Projectile.height = (int)(num1038 * Projectile.scale);
            Projectile.Center = Projectile.position;
            if (Projectile.alpha > 0)
            {
                Projectile.alpha -= 42;
                if (Projectile.alpha < 0)
                {
                    Projectile.alpha = 0;
                }
            }

            float DamageBoost = Main.player[Projectile.owner].GetDamage(DamageClass.Summon).Flat;
            Projectile.damage = (int)(DamageBoost > 0f? (10 + (Projectile.localAI[0] > 10? 10 : Projectile.localAI[0] - 1)) * DamageBoost : 1);
        }
    }
}
