using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Globals
{
    public class AAGlobalProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity => true;

        public static int CountProjectiles(int type)
        {
            int num = 0;
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                if (Main.projectile[i].active && Main.projectile[i].type == type)
                {
                    num++;
                }
            }

            return num;
        }

        public static bool AnyProjectiles(int type)
        {
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                if (Main.projectile[i].active && Main.projectile[i].type == type)
                {
                    return true;
                }
            }

            return false;
        }

        public static float GetSyncedItemAnimation(Projectile projectile, Player player)
        {
            float itemAnimation = player.itemAnimation;

            if (Main.netMode != NetmodeID.SinglePlayer && Main.myPlayer == projectile.owner)
            {
                if (projectile.ai[1] != itemAnimation)
                {
                    projectile.ai[1] = itemAnimation;
                    projectile.netUpdate = true;
                }
            }

            if (Main.netMode == NetmodeID.SinglePlayer || Main.myPlayer == projectile.owner)
                return itemAnimation;

            if (projectile.ai[1] > 0f)
                projectile.localAI[1] = 1f;

            if (projectile.localAI[1] == 1f)
                return projectile.ai[1];

            return Math.Max(1f, player.itemAnimationMax);
        }

        public override void PostAI(Projectile projectile)
        {
            if (isReflecting && projectile.hostile && !projectile.friendly)
            {
                oldvelocity = projectile.velocity;
                projectile.velocity = reflectvelocity;
                projectile.rotation += projectile.velocity.ToRotation() - oldvelocity.ToRotation();
            }
            if (!projectile.minion && projectile.type > ProjectileID.None && !projectile.CountsAsClass(DamageClass.Melee) && !projectile.CountsAsClass(DamageClass.Magic) && !projectile.CountsAsClass(DamageClass.Ranged))
            {
                foreach(Projectile p in Main.ActiveProjectiles)
                {
                    if (p.sentry && p.type + 1 == projectile.type)
                    {
                        projectile.minion = true;
                        break;
                    }
                }
            }
            if ((projectile.minion || projectile.sentry) && !ProjectileID.Sets.StardustDragon[projectile.type] && !LongMinion)
			{
				if (setDefMinionDamage)
				{
					DefMinionDamageMultiply = Main.player[projectile.owner].GetDamage(DamageClass.Summon).Multiplicative;
					DefMinionDamage = (int)(projectile.damage / DefMinionDamageMultiply);
					setDefMinionDamage = false;
				}
				if (Main.player[projectile.owner].GetDamage(DamageClass.Summon).Flat != DefMinionDamageMultiply)
				{
					int damage = (int)(Main.player[projectile.owner].GetDamage(DamageClass.Summon)).ApplyTo(DefMinionDamage);
                    if(damage <= 0) damage = 1;
					projectile.damage = damage;
				}
			}
        }

        public Vector2 reflectvelocity = Vector2.Zero;

        private Vector2 oldvelocity = Vector2.Zero;

        public bool isReflecting = false;

        private bool setDefMinionDamage = true;

        public bool LongMinion = false;

        public float DefMinionDamageMultiply = 1f;

		public int DefMinionDamage;
    }
}
