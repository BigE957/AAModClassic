using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles     //We need this to basically indicate the folder where it is to be read from, so you the texture will load correctly
{
    public class DarkShredders : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 58;
            Projectile.height = 90;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.scale = 4f;
            
        }
        public override void AI()
        {
            Projectile.soundDelay--;
            if (Projectile.soundDelay <= 0)
            {
                SoundEngine.PlaySound(SoundID.Item15, Projectile.Center);
                Projectile.soundDelay = 45;
            }
            Player player = Main.player[Projectile.owner];
            if (Main.myPlayer == Projectile.owner)
            {
                if (!player.channel || player.noItems || player.CCed)
                {
                    Projectile.Kill();
                }
            }
            Lighting.AddLight(Projectile.Center, 0f, .1f, .5f); 
            Projectile.Center = player.MountedCenter;
            Projectile.position.X += player.width / 2 * player.direction;
            Projectile.spriteDirection = player.direction;
            Projectile.rotation += .3f * player.direction;
            if (Projectile.rotation > MathHelper.TwoPi)
            {
                Projectile.rotation -= MathHelper.TwoPi;
            }
            else if (Projectile.rotation < 0)
            {
                Projectile.rotation += MathHelper.TwoPi;
            }
            player.heldProj = Projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;
            player.itemRotation = Projectile.rotation;
            if (Main.rand.Next(4) == 0)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Electric, 0, 0, 0, default, .1f);
                Main.dust[dust].velocity /= 2f;
            }
 
        }


        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<Electrified>(), 500);
        }

        public override bool PreDraw(ref Color lightColor)  //this make the projectile sprite rotate perfectaly around the player
        {
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, null, lightColor, Projectile.rotation, new Vector2(texture.Width / 2, texture.Height / 2), 1f, Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            Main.spriteBatch.Draw(Mod.GetTexture("Glowmasks/" + GetType().Name + "_Glow"), Projectile.Center - Main.screenPosition, null, Color.White, Projectile.rotation, new Vector2(texture.Width / 2, texture.Height / 2), 1f, Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            return false;
        }

    }
}
