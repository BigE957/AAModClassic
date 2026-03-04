using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.GameContent;


namespace AAModClassic.Projectiles.Greed
{
    public class OreConstruct : AAProjectile
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("OreConstruct");
		}
		
        public override void SetDefaults()
        {
            Projectile.width = 25;
            Projectile.height = 25;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 300;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = true;
            Projectile.penetrate = -1;
            Projectile.netImportant = true;	
        }

		public bool checkedMinPos = false;
		public float maxDistToAttack = 360f;
		public Entity target = null;

		public override void AI()
		{
			Player player = Main.player[Projectile.owner];
			int[] projs = BaseAI.GetProjectiles(player.Center, Projectile.type, player.whoAmI, -1);
			if(!checkedMinPos)
			{
				for (int m = 0; m < projs.Length; m++) { if (projs[m] == Projectile.identity) { Projectile.minionPos = m; } }
				if (Main.myPlayer == Projectile.owner) { Projectile.netUpdate = true; }
			}
			if (!Main.player[Projectile.owner].active || Main.player[Projectile.owner].dead || projs.Length > 2) { Projectile.Kill(); return; }
			Target();
            BaseAI.AIMinionFlier(Projectile, ref Projectile.ai, player, false, false, false, 40, 40, 400, 800, 1f, 10f, 10f, !CanShoot(target), false, (proj, owner) => { return target == player ? null : target; }, Shoot);
			Projectile.position -= player.oldPosition - player.position;
			if (CanShoot(target)) { Projectile.spriteDirection = Projectile.Center.X > target.Center.X ? -1 : 1; }
		}

		public bool CanShoot(Entity target)
		{
			return target != null && target is NPC && BaseUtility.CanHit(Projectile.Hitbox, new Rectangle((int)target.Center.X, (int)target.Center.Y, 1, 1)) && Vector2.Distance(Projectile.Center, target.Center) < 350;
		}

		public bool Shoot(Entity proj, Entity owner, Entity target)
		{
			if(CanShoot(target))
			{
				if (Main.myPlayer == Projectile.owner)
				{
					Projectile.localAI[0]--;
					if (Projectile.localAI[0] <= 0)
					{
						Projectile.localAI[0] = 30;
						Vector2 velocity = BaseUtility.RotateVector(default, new Vector2(5f, 0f), BaseUtility.RotationTo(Projectile.Center, target.Center));
						int projID = Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, 0f, 0f, Mod.ProjType("Gold"), Projectile.damage, 0f, Projectile.owner, 0, 1);
						((AAProjectile)Main.projectile[projID].ModProjectile).SetMaster(2, Projectile.identity, 1, 0f, 450f, false);	
						Main.projectile[projID].velocity = velocity;
						Main.projectile[projID].netUpdate = true;
					}
				}
				return true;
			}
			Projectile.localAI[0] = 0;
			return false;
		}

		public override bool OnTileCollide(Vector2 velocityChange) 
		{
			Projectile.velocity *= -1f;
			return false; 
		}

		public override void PostAI()
		{
			Projectile.rotation = Projectile.velocity.X * 0.1f;
            if (Projectile.frameCounter++ > 5)
            {
                Projectile.frame += 1;
                Projectile.frameCounter = 0;
            }
            if (Projectile.frame > 14)
            {
                Projectile.frame = 3;
            }
		}

        Color GlowColor = Color.Brown;

		public override bool PreDraw(ref Color lightColor)
        {
            Rectangle frame = BaseDrawing.GetFrame(frameCount, 60, 60, 0, 0);
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            Texture2D glowTex = Mod.GetTexture("Glowmasks/GreedMinion_Glow");
            Color lightColor = BaseDrawing.GetLightColor(Projectile.Center);
            BaseDrawing.DrawTexture(spriteBatch, tex, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, Projectile.rotation, Projectile.spriteDirection, 15, frame, lightColor);
            BaseDrawing.DrawTexture(spriteBatch, glowTex, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, Projectile.rotation, Projectile.spriteDirection, 15, frame, Color.Goldenrod);
			return false;
		}

        public void SetType()
        {
            switch (Projectile.ai[1])
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
            }
        }

		public void Target()
		{
			Vector2 startPos = Main.player[Projectile.owner].Center;
			if (target != null && target != Main.player[Projectile.owner] && !CanTarget(target, startPos))
			{
				target = null;
			}
			if (target == null || target == Main.player[Projectile.owner])
			{
				int[] npcs = BaseAI.GetNPCs(startPos, -1, default, maxDistToAttack);
				float prevDist = maxDistToAttack;
				foreach (int i in npcs)
				{
					NPC npc = Main.npc[i];
					float dist = Vector2.Distance(startPos, npc.Center);
					if (CanTarget(npc, startPos) && dist < prevDist) { target = npc; prevDist = dist; }
				}
			}
			if (target == null) { target = Main.player[Projectile.owner]; }
		}

		public bool CanTarget(Entity codable, Vector2 startPos)
		{
            if (codable is NPC npc)
            {
                return npc.active && npc.life > 0 && !npc.friendly && !npc.dontTakeDamage && npc.lifeMax > 5 && Vector2.Distance(startPos, npc.Center) < maxDistToAttack && BaseUtility.CanHit(Projectile.Hitbox, npc.Hitbox);
            }
            return false;
		}
	}
}



