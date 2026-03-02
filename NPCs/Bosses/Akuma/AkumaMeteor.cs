using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Akuma
{
    public class AkumaMeteor : ModProjectile
    {
    	
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Dayfire");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.penetrate = 1;
            Projectile.extraUpdates = 1;
        }

        public override void AI()
        {
            Projectile.velocity.Y += .01f;
            if (Projectile.position.Y > Main.player[Projectile.owner].position.Y - 300f)
            {
                Projectile.tileCollide = true;
            }
            if (Projectile.position.Y < Main.worldSurface * 16.0)
            {
                Projectile.tileCollide = true;
            }
            Projectile.scale = Projectile.ai[1];
            Projectile.rotation = Projectile.velocity.ToRotation() - 1.57079637f;
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 124, Terraria.Audio.SoundType.Sound));
            float spread = 45f * 0.0174f;
            double startAngle = Math.Atan2(Projectile.velocity.X, Projectile.velocity.Y) - (spread / 2);
            double deltaAngle = spread / 8f;
            if (Main.netMode != 1)
            {
                int proj = Projectile.NewProjectile(Projectile.position.X, Projectile.position.Y, Projectile.velocity.X, Projectile.velocity.Y, Mod.Find<ModProjectile>("AkumaBoom").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
                Main.projectile[proj].netUpdate = true;
            }
            for (int num468 = 0; num468 < 20; num468++)
            {
                int num469 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, ModContent.DustType<Dusts.AkumaDust>(), -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 0);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, ModContent.DustType<Dusts.AkumaDust>(), -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 0);
                Main.dust[num469].velocity *= 2f;
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(ModContent.BuffType<Buffs.DragonFire>(), 200);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            int shader = GameShaders.Armor.GetShaderIdFromItemId(ItemID.LivingFlameDye);
            Vector2 Drawpos = Projectile.Center - Main.screenPosition + new Vector2(0, Projectile.gfxOffY);

            BaseDrawing.DrawTexture(spriteBatch, TextureAssets.Projectile[Projectile.type].Value, shader, Projectile, Color.White, true);
            return false;
        }
    }
}