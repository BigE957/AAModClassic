using System;
using AAModClassic;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;


namespace AAModClassic.Projectiles.Sag
{
    public class OrbiterMinion : AAProjectile
	{
		float rot = 0f;
		float rotInit = -1f;

        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.timeLeft = 320;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = false;
            Projectile.damage = 0;
            Projectile.penetrate = -1;
            Projectile.minion = true;
            Projectile.minionSlots = 1;
            Projectile.ignoreWater = true;		
        }

		public void SetRot()
		{
			float oldInit = rotInit;
			int[] projs = BaseAI.GetProjectiles(Main.player[Projectile.owner].Center, Projectile.type, Projectile.owner, 200f);
			rotInit = projs.Length == 0 ? 0f : ((float)Math.PI * 2f / projs.Length);

			if (rotInit != oldInit)
			{
				int projSlot = 0;
				for(int m = 0; m < projs.Length; m++)
				{
					if (projs[m] == Projectile.identity) { projSlot = m; }
				}
				rot = rotInit * (projSlot + 1f);
			}
		}

        public override void AI()
		{
			Player player = Main.player[Projectile.owner];
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
            if (player.dead || !player.HasBuff(Mod.Find<ModBuff>("SagOrbiter").Type)) Projectile.Kill();
            if (modPlayer.SagOrbiter)
            {
				Projectile.timeLeft = 2;
				player.AddBuff(Mod.Find<ModBuff>("SagOrbiter").Type, 2, true);
            }

            Projectile.ai[0] = 30 * player.GetDamage(DamageClass.Summon);

            Vector2 vector46 = Projectile.position;
            bool flag25 = false;
            float num633 = 700f;
            int Height = 0;
            int Width = 0;

            if (player.HasMinionAttackTargetNPC)
			{
				NPC nPC2 = Main.npc[player.MinionAttackTargetNPC];
                if (nPC2.CanBeChasedBy(Projectile, false))
                {
                    float num646 = Vector2.Distance(nPC2.Center, Projectile.Center);
                    if (((Vector2.Distance(Projectile.Center, vector46) > num646 && num646 < num633) || !flag25) && Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, nPC2.position, nPC2.width, nPC2.height))
                    {
                        num633 = num646;
                        vector46 = nPC2.position;
                        flag25 = true;
                        Height = nPC2.height;
                        Width = nPC2.width;
                    }
                }
			}
			else
			{
                for (int num645 = 0; num645 < 200; num645++)
                {
                    NPC nPC2 = Main.npc[num645];
                    if (nPC2.CanBeChasedBy(Projectile, false))
                    {
                        float num646 = Vector2.Distance(nPC2.Center, Projectile.Center);
                        if (((Vector2.Distance(Projectile.Center, vector46) > num646 && num646 < num633) || !flag25) && Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, nPC2.position, nPC2.width, nPC2.height))
                        {
                            num633 = num646;
                            vector46 = nPC2.position;
                            flag25 = true;
                            Height = nPC2.height;
                            Width = nPC2.width;
                        }
                    }
                }
            }
            if (flag25)
            {
                int id = BaseAI.ShootPeriodic(Projectile, vector46, Width, Height, Terraria.ModLoader.ModContent.ProjectileType<Darkray>(), ref Projectile.ai[1], 120, (int)Projectile.ai[0], 11, true);
                Main.projectile[id].ranged = false/* tModPorter Suggestion: Remove. See Item.DamageType */;
                Main.projectile[id].minion = true;
            }
			
            if (Projectile.active) { SetRot(); }
			BaseAI.AIRotate(Projectile, ref Projectile.rotation, ref rot, player.Center, true, 80f, 20f, 0.07f, true);
		}

        public override bool PreDraw(ref Color lightColor)
        {
            Rectangle frame = BaseDrawing.GetFrame(Projectile.frame, TextureAssets.Projectile[Projectile.type].Value.Width, TextureAssets.Projectile[Projectile.type].Value.Height, 0, 2);
            BaseDrawing.DrawTexture(spriteBatch, TextureAssets.Projectile[Projectile.type].Value, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, 0, 0, 4, frame, drawColor, true);
            return false;
        }

        public override void OnKill(int timeLeft)
		{
			int[] projs = BaseAI.GetProjectiles(Projectile.Center, Projectile.type, Projectile.owner, 200f);
		}
	}
}