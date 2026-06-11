using AAModClassic._Content.Terra.Projectiles;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Terra.__Hardmode.Items.Armor
{
    public class TerraHelmetMeleePlayer : EquipEffectAbstract
    {
        public int AARegenCount = 0;

        public override void OnHitByAnything(Player.HurtInfo hurtInfo, NPC npc = null, Projectile proj = null)
        {
            base.OnHitByAnything(hurtInfo, npc, proj);

            if (effect)
            {
                int p = Projectile.NewProjectile(Player.GetSource_OnHurt(hurtInfo.DamageSource), Player.Center, Vector2.One * hurtInfo.HitDirection * 10, ModContent.ProjectileType<TerraSphere>(), 30, 4, Main.myPlayer);
                Main.projectile[p].DamageType = DamageClass.Melee;
            }
        }

        public override void UpdateLifeRegen()
        {
            base.UpdateLifeRegen();

            if (effect)
            {
                AARegenCount++;
                while (AARegenCount >= 100)
                {
                    AARegenCount -= 100;
                    if (Player.statLife < Player.statLifeMax2)
                    {
                        Player.statLife += 2;
                        for (int i = 0; i < 10; i++)
                        {
                            int num6 = Dust.NewDust(Player.position, Player.width, Player.height, DustID.Terra, 0f, 0f, 175, default, 1.75f);
                            Main.dust[num6].noGravity = true;
                            Main.dust[num6].velocity *= 0.75f;
                            int num7 = Main.rand.Next(-40, 41);
                            int num8 = Main.rand.Next(-40, 41);
                            Dust expr_7EE_cp_0 = Main.dust[num6];
                            expr_7EE_cp_0.position.X = expr_7EE_cp_0.position.X + num7;
                            Dust expr_80A_cp_0 = Main.dust[num6];
                            expr_80A_cp_0.position.Y = expr_80A_cp_0.position.Y + num8;
                            Main.dust[num6].velocity.X = -num7 * 0.075f;
                            Main.dust[num6].velocity.Y = -num8 * 0.075f;
                        }
                    }
                }
            }
        }
    }
}