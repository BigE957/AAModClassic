using AAModClassic._Content.Stars._PostMoonlord.Items.Materials;
using AAModClassic.Globals;
using AAModClassic.Tiles.Crafters;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Terra.__Hardmode.Items.Armor
{
    public class TerraHelmetRangedPlayer : EquipEffectAbstract
    {
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(target, hit, damageDone);

            if (effect && hit.DamageType.CountsAsClass(DamageClass.Ranged) && Main.rand.NextBool(3))
            {
                float screenX;
                float screenY;
                if (Main.rand.NextBool(2))
                {
                    screenX = Main.screenPosition.X;
                    if (Main.rand.NextBool(2))
                    {
                        screenX += Main.screenWidth;
                    }
                    screenY = Main.screenPosition.Y;
                    screenY += Main.rand.Next(Main.screenHeight);
                }
                else
                {
                    screenY = Main.screenPosition.Y;
                    if (Main.rand.NextBool(2))
                    {
                        screenY += Main.screenHeight;
                    }
                    screenX = Main.screenPosition.X;
                    screenX += Main.rand.Next(Main.screenWidth);
                }
                Vector2 vector = new Vector2(screenX, screenY);
                float velocityX = target.Center.X - vector.X;
                float velocityY = target.Center.Y - vector.Y;
                velocityX += Main.rand.Next(-50, 51) * 0.1f;
                velocityY += Main.rand.Next(-50, 51) * 0.1f;
                float num6 = 24 / (float)Math.Sqrt(velocityX * velocityX + velocityY * velocityY);
                velocityX *= num6;
                velocityY *= num6;
                Projectile p = Projectile.NewProjectileDirect(Player.GetSource_OnHit(target), new Vector2(screenX, screenY), new Vector2(velocityX, velocityY), ModContent.ProjectileType<TerraHelmetRangedPlayer_TerraBullet>(), damageDone / 3, 0f, Player.whoAmI);
                p.tileCollide = false;
            }
        }
    }
}