using Terraria;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic._Content.Void.Projectiles;

namespace AAModClassic._Content.Void.___PreHardmode.Items._BossSagittarius.Weapons
{
    class StallionsStar_Proj : ModProjectile
	{
        public override void SetDefaults()
        {
            Projectile.aiStyle = -1;
            Projectile.width = 32;
	        Projectile.height = 32;
	        Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = false;
	        Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Melee;
        }
        public float[] internalAI = new float[1];
        public float[] shootAI = new float[1];

        public override void AI()
        {
            Player p = Main.player[Projectile.owner];
            BaseAI.AIBoomerang(Projectile, ref Projectile.ai, p.position, p.width, p.height, true, 20f, 30, 20f, 0.6f, true);
            int Target = BaseAI.GetNPC(Projectile.Center, -1, 500);
            if (Target != -1 && !Main.npc[Target].friendly)
            {
                NPC target = Main.npc[Target];
                int id = BaseAI.ShootPeriodic(Projectile, target.position, 14, 14, ModContent.ProjectileType<Darkray>(), ref internalAI[0], 30, Projectile.damage, 7, true);
                //Main.projectile[id].DamageType = DamageClass.Melee;
            }
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            width = 8;
            height = 8;
            return true;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D Glow = ModContent.Request<Texture2D>(Texture + "_Glow").Value;
            BaseDrawing.DrawTexture(Main.spriteBatch, ModContent.Request<Texture2D>("AAModClassic/Projectiles/Sag/ZeroStarP").Value, 0, Projectile, lightColor, true);
            BaseDrawing.DrawTexture(Main.spriteBatch, Glow, 0, Projectile, AAColor.COLOR_WHITEFADE1, true);
            return false;
        }
    }
}