using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Summoning.Minions.Terra
{
    public class Minion2 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Terra Crawler");
			Main.projFrames[Projectile.type] = 5;
		}		
		
        public override void SetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 18;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 300;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = false;
			Projectile.damage = 1;
            Projectile.penetrate = -1;
            Projectile.netImportant = true;
            Projectile.minionSlots = 1f;
            Projectile.minion = true;
			ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;		
        }

        public static int frameWidth = 26, frameHeight = 18;
        public float frameSubCounter = 0f;
        public int frameCount = 0, textureAlt = -1;
        public Rectangle frame;
        public bool syncSpawn = false;

        public Entity target = null;

		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
		{
			fallThrough = false;
			return true;
		}
		
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
			if (modPlayer.TerraSummon)
			{
				Projectile.timeLeft = 2;
			}
            if (player.dead)
            {
                modPlayer.TerraSummon = false;
            }
            if (!modPlayer.TerraSummon)
            {
                Projectile.active = false;
            }
            if (Main.netMode == NetmodeID.MultiplayerClient && Main.myPlayer == Projectile.owner && !syncSpawn) { syncSpawn = Projectile.netUpdate2 = true; }
            if (!player.active || player.dead) { Projectile.Kill(); return; }
            Target();
            bool playerTarget = target != null && target.Equals(player);
            int maxDistBeforeReturn = playerTarget ? 950 : 1100;
            BaseAI.AIMinionFighter(Projectile, ref Projectile.ai, Main.player[Projectile.owner], false, 14, 20, 20, 900, maxDistBeforeReturn, target == player ? -1f : .2f, target == player ? -1f : 12, 10, (proj, owner) => { return target == player ? null : target; });
        }

        public override bool OnTileCollide(Vector2 value2)
        {
            return false;
        }

        public override void PostAI()
        {
            if (Projectile.velocity.X != 0 && Projectile.velocity.Y == 0)
            {
                if (Projectile.frameCounter++ > 5)
                {
                    Projectile.frame++;
                    Projectile.frameCounter = 0;
                }
                if (Projectile.frame > 4)
                {
                    Projectile.frame = 0;
                }
            }
            else
            {
                Projectile.frame = 0;
            }
        }

        public void Target()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 startPos = Main.player[Projectile.owner].Center;
            if (target != null && target != Main.player[Projectile.owner] && !CanTarget(target, startPos))
            {
                target = null;
            }
            if (player.HasMinionAttackTargetNPC)
			{
				NPC targetNPC = Main.npc[player.MinionAttackTargetNPC];
                float prevDist = 900;
                float dist = Vector2.Distance(startPos, targetNPC.Center);
                if (CanTarget(targetNPC, startPos) && dist < prevDist) { target = targetNPC; prevDist = dist; }
			}
            else if (target == null || target == Main.player[Projectile.owner])
            {
                int[] npcs = BaseAI.GetNPCs(startPos, -1, default, 900);
                float prevDist = 900;
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
                return npc.active && npc.life > 0 && !npc.friendly && !npc.dontTakeDamage && npc.lifeMax > 5 && Vector2.Distance(startPos, npc.Center) < 900 && Math.Abs(npc.Center.Y - startPos.Y) < (16f * (20 - 1)) && (BaseUtility.CanHit(Projectile.Hitbox, npc.Hitbox) || BaseUtility.CanHit(Main.player[Projectile.owner].Hitbox, npc.Hitbox));
            }
            return false;
        }
    }
}



