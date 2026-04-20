using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using System;

using Terraria;
using Terraria.ModLoader;


namespace AAModClassic.___Content.Chaos._PostMoonlord.Items._BossSistersOfDiscord.Weapons
{
    public class FlameVortexStaff_FireOrbiter : AAProjectile
	{
		float rot = 0f;
		float rotInit = -1f;
		
		public override void SetStaticDefaults()
		{
			Main.projFrames[Projectile.type] = 4;
		}

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
			rotInit = projs.Length == 0 ? 0f : (float)Math.PI * 2f / projs.Length;

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
			Projectile.frameCounter++;
            if (Projectile.frameCounter >= 8)
            {
                Projectile.frameCounter = 0;
                Projectile.frame += 1;
            }
            if (Projectile.frame > 3)
            {
                Projectile.frame = 0;
            }
			
			Player player = Main.player[Projectile.owner];
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
            if (player.dead || !player.HasBuff(ModContent.BuffType<FlameVortexStaff_Buff>())) Projectile.Kill();
            if (modPlayer.Orbiters)
            {
				Projectile.timeLeft = 2;
				player.AddBuff(ModContent.BuffType<FlameVortexStaff_Buff>(), 2, true);
            }
			
            if (Projectile.active) { SetRot(); }
			BaseAI.AIRotate(Projectile, ref Projectile.rotation, ref rot, player.Center, true, 40f, 20f, 0.07f, true);
		}

		public override void OnKill(int timeLeft)
		{
			int[] projs = BaseAI.GetProjectiles(Projectile.Center, Projectile.type, Projectile.owner, 200f);
		}
	}
}