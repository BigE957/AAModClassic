using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno.__Hardmode.Items.Weapons
{
    public class SunHalberd_Holdout : ModProjectile
	{
		public static Color lightColor = new Color(82, 138, 206);
		public static Vector2[] spearPos = new Vector2[]{ new Vector2(0, 0), new Vector2(50, -25), new Vector2(100, -50), new Vector2(100, 0), new Vector2(100, 50), new Vector2(50, 25), new Vector2(30, 0), new Vector2(120, 0), new Vector2(120, 0), new Vector2(30, 0) };
	
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Sun Halberd");
		}	

        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 600;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = false;
            Projectile.damage = 1;
            Projectile.penetrate = -1;
            Projectile.hide = true;
            Projectile.ownerHitCheck = true;
            Projectile.DamageType = DamageClass.Melee;
			Projectile.alpha = 254;
        }

		public override void AI()
		{
			AIArcStabSpear(Projectile, ref Projectile.ai, false);
			if (Main.rand.NextBool(3))
			{
				int dustID = Dust.NewDust(Projectile.Center, 0, 0, ModContent.DustType<Dusts.CthulhuDust>(), 0f, 0f, 0);
				Main.dust[dustID].noGravity = true;
			}			
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.immune[Projectile.owner] = 5;
		}

		public override bool PreDraw(ref Color lightColor)
		{
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            float offsetX = -texture.Width * 0.5f;
            Vector2 origin = new Vector2((float)texture.Width * 0.5f, (float)texture.Height * 0.5f);
            float offsetY = -Main.player[Projectile.owner].gfxOffY;
            Vector2 offset = BaseUtility.RotateVector(Projectile.Center, Projectile.Center + new Vector2(Projectile.direction == -1 ? offsetX : offsetY, Projectile.direction == 1 ? offsetX : offsetY), Projectile.rotation - 2.355f) - Projectile.Center;
            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition + offset, new Rectangle(0, 0, texture.Width, texture.Height), lightColor, Projectile.rotation, origin, Projectile.scale, Projectile.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
			return false;
		}

        public static void AIArcStabSpear(Projectile p, ref float[] ai, bool overrideKill = false)
        {
            Player plr = Main.player[p.owner];
            Item item = plr.inventory[plr.selectedItem];
            if (Main.myPlayer == p.owner && item != null && item.autoReuse && plr.itemAnimation == 1) { p.Kill(); return; } //prevents a bug with autoReuse and spears
            Main.player[p.owner].heldProj = p.whoAmI;
            Main.player[p.owner].itemTime = Main.player[p.owner].itemAnimation;
			Vector2 gfxOffset = new Vector2(0, plr.gfxOffY);
            AIArcStabSpear(p, ref ai, plr.Center + gfxOffset, BaseUtility.RotationTo(p.Center, p.Center + p.velocity), plr.direction, plr.itemAnimation, plr.itemAnimationMax, overrideKill, plr.frozen);
        }

        public static void AIArcStabSpear(Projectile p, ref float[] ai, Vector2 center, float itemRot, int ownerDirection, int itemAnimation, int itemAnimationMax, bool overrideKill = false, bool frozen = false)
        {
			if(p.timeLeft < 598) p.alpha -= 70; if(p.alpha < 0) p.alpha = 0;
            p.direction = ownerDirection;
			Vector2 oldCenter = p.Center;
            p.position.X = center.X - p.width * 0.5f;
            p.position.Y = center.Y - p.height * 0.5f;
			p.position += BaseUtility.RotateVector(default, BaseUtility.MultiLerpVector(1f - itemAnimation / (float)itemAnimationMax, spearPos), itemRot);		
            if (!overrideKill && Main.player[p.owner].itemAnimation == 0){ p.Kill(); }
            p.rotation = BaseUtility.RotationTo(center, oldCenter) + 2.355f;				
			if (p.direction == -1) { p.rotation -= 0f; }else
			if (p.direction == 1) { p.rotation -= 1.57f; }		
		}
	}
}