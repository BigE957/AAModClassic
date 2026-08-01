using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using System;

using Terraria.Graphics.Shaders;
using AAModClassic.Globals;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic._Content.Inferno.Buffs;

namespace AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon.GripsOfDiscord
{
    public class BlazeGrip_Clone : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Shen Blaze Claw");
        }

        public override void SetDefaults()
        {
            Projectile.width = 66;
            Projectile.height = 60;
            Projectile.penetrate = -1;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 600;
            Projectile.scale = 1.5f;
            Projectile.alpha = 255;
        }

        public int timecount = 0;

        public override Color? GetAlpha(Color lightColor)
        {
            return AAColor.Glow;
        }

        public override void AI()
        {
            Player targetPlayer = Main.player[Main.npc[(int)Projectile.ai[0]].target];

            timecount++;

            if(timecount < 100)
            {
                Projectile.position = Main.npc[(int)Projectile.ai[0]].Center + 100f * Vector2.Normalize(Main.npc[(int)Projectile.ai[0]].DirectionTo(targetPlayer.Center)) + 200f * Projectile.ai[1] * Vector2.Normalize(Main.npc[(int)Projectile.ai[0]].DirectionTo(targetPlayer.Center).RotatedBy(3.1415926f / 2));
            }
            else if(timecount == 100)
            {
                Projectile.position = Main.npc[(int)Projectile.ai[0]].Center + 100f * Vector2.Normalize(Main.npc[(int)Projectile.ai[0]].DirectionTo(targetPlayer.Center)) + 200f * Projectile.ai[1] * Vector2.Normalize(Main.npc[(int)Projectile.ai[0]].DirectionTo(targetPlayer.Center).RotatedBy(3.1415926f / 2));
                Projectile.velocity = 24f * Vector2.Normalize(Main.npc[(int)Projectile.ai[0]].DirectionTo(targetPlayer.Center));
            }
            else
            {
                Projectile.velocity = Projectile.oldVelocity;
            }

            foreach (Projectile p in Main.ActiveProjectiles)
            {
                if (p.friendly && !p.minion && Main.player[p.owner].heldProj != p.whoAmI && p.damage > 0 && p.Hitbox.Intersects(Projectile.Hitbox))
                    p.Kill();
            }
        }

        public override void OnKill(int timeLeft)
        {
            Projectile.timeLeft = 0;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(ModContent.BuffType<DragonFire_Buff>(), 200);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Player targetPlayer = Main.player[Main.npc[(int)Projectile.ai[0]].target];
            Color Alpha = lightColor;
            if(timecount < 10)
            {
                Alpha.R = (byte)0f;
                Alpha.G = (byte)0f;
                Alpha.B = (byte)0f;
                Alpha.A = (byte)0f;
                Projectile.rotation = Main.npc[(int)Projectile.ai[0]].DirectionTo(targetPlayer.Center).ToRotation() + (Main.npc[(int)Projectile.ai[0]].position.X < targetPlayer.position.X ? 0 : (float)Math.PI);
                Projectile.direction = Projectile.spriteDirection = Main.npc[(int)Projectile.ai[0]].position.X < targetPlayer.position.X ? -1 : 1;
            }
            else if(timecount < 100)
            {
                Alpha.R = (byte)(float)(timecount * 2);
                Alpha.G = (byte)(float)(timecount * 2);
                Alpha.B = (byte)(float)(timecount * 2);
                Alpha.A = (byte)(float)(timecount * 2);
                Projectile.rotation = Main.npc[(int)Projectile.ai[0]].DirectionTo(targetPlayer.Center).ToRotation() + (Main.npc[(int)Projectile.ai[0]].position.X < targetPlayer.position.X ? 0 : (float)Math.PI);
                Projectile.direction = Projectile.spriteDirection = Main.npc[(int)Projectile.ai[0]].position.X < targetPlayer.position.X ? -1 : 1;
            }
            else
            {
                Alpha.R = (byte)200f;
                Alpha.G = (byte)200f;
                Alpha.B = (byte)200f;
                Alpha.A = (byte)200f;
                Projectile.rotation = Projectile.velocity.ToRotation() + (Projectile.velocity.X > 0 ? 0 : (float)Math.PI);
                Projectile.direction = Projectile.velocity.X > 0 ? -1 : 1;
            }
            int red = GameShaders.Armor.GetShaderIdFromItemId(ItemID.LivingFlameDye);
            Rectangle frame = BaseDrawing.GetFrame(Projectile.frame, TextureAssets.Projectile[Projectile.type].Width(), TextureAssets.Projectile[Projectile.type].Height(), 0, 2);
            BaseDrawing.DrawTexture(Main.spriteBatch, TextureAssets.Projectile[Projectile.type].Value, red, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, Projectile.rotation, Projectile.direction, 1, frame, Alpha, true);
            return false;
        }

    }
}
