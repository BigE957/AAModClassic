using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Buffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles
{
    public class DMC : ModProjectile
	{

          public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
          {
              target.AddBuff(ModContent.BuffType<Electrified_Buff>(), 500);
          }

        public override void SetDefaults()
        {
	    Projectile.aiStyle = -1;
            Projectile.width = 26;
            Projectile.height = 26;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 6;
            Projectile.extraUpdates = 3;
        }

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			BaseAI.TileCollideBoomerang(Projectile, ref Projectile.velocity, true);
			return false;
		}

        public override void AI()
        {
            Player p = Main.player[Projectile.owner];
            BaseAI.AIBoomerang(Projectile, ref Projectile.ai, p.position, p.width, p.height, true, 24f, 45, 1.2f, .5f, false);
        }
		
		public override bool PreDraw(ref Color lightColor)
        {
            Rectangle frame = BaseDrawing.GetFrame(Projectile.frame, TextureAssets.Projectile[Projectile.type].Value.Width, TextureAssets.Projectile[Projectile.type].Value.Height, 0, 2);
            BaseDrawing.DrawTexture(Main.spriteBatch, TextureAssets.Projectile[Projectile.type].Value, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, Projectile.rotation, 0, 1, frame, lightColor, true);
            return false;
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            target.AddBuff(ModContent.BuffType<Buffs.Electrified_Buff>(), 90);
        }
    }
}
