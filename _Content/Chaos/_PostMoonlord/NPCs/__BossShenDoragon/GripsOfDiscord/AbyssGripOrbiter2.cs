using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;

using Terraria.ModLoader;
using Terraria.Graphics.Shaders;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic._Content.Mire.Buffs;

namespace AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon.GripsOfDiscord
{
    public class AbyssGripOrbiter2 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Abyss Grip Orbiter");
            Main.projFrames[Projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            Projectile.width = 40;
            Projectile.height = 40;
            Projectile.penetrate = -1;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 1200;
            Projectile.alpha = 255;
        }

        public override void PostAI()
        {
            if (Projectile.frameCounter++ > 5)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame > 3)
                {
                    Projectile.frame = 0;
                }
            }
        }

        public override void OnKill(int timeLeft)
        {
            Projectile.timeLeft = 0;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(ModContent.BuffType<HydraToxin_Buff>(), 200);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Color Alpha = lightColor;
            int blue = GameShaders.Armor.GetShaderIdFromItemId(ItemID.LivingOceanDye);
            Rectangle frame = BaseDrawing.GetFrame(Projectile.frame, TextureAssets.Projectile[Projectile.type].Value.Width, TextureAssets.Projectile[Projectile.type].Value.Height / 4, 0, 0);
            BaseDrawing.DrawTexture(Main.spriteBatch, TextureAssets.Projectile[Projectile.type].Value, blue, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, Projectile.rotation, 0, 4, frame, Color.White, true);
            return false;
        }
    }
}
