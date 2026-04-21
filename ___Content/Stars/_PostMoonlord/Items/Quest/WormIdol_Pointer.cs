using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Stars._PostMoonlord.Items.Quest
{
    public class WormIdol_Pointer : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Pointer");
        }

        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft *= 5;
            Projectile.ignoreWater = true;
            Projectile.minionSlots = 0;
        }

        public override void AI()
        {
            Vector2 AltarSpawn = new Vector2(Main.maxTilesX * 0.15f * 16, 100 * 16);
            Player player = Main.player[Projectile.owner];
            IdolPointer modPlayer = player.GetModPlayer<IdolPointer>();

            if (!modPlayer.effect)
            {
                Projectile.Kill();
                return;
            }

            Vector2 PlayerPoint = Vector2.Zero;

            PlayerPoint.X = player.Center.X - Projectile.width / 2;
            PlayerPoint.Y = player.Center.Y - Projectile.height / 2 + player.gfxOffY - 60f;

            Projectile.Center = PlayerPoint;

            AltarSpawn += new Vector2(37.5f * 16, 42 * 16);

            BaseAI.LookAt(AltarSpawn, Projectile, 2, 0, 0, true);

            Projectile.direction = 1;
        }

        public float auraPercent = 0f;
        public bool auraDirection = true;

        public override bool PreDraw(ref Color lightColor)
        {
            if (auraDirection) { auraPercent += 0.1f; auraDirection = auraPercent < 1f; }
            else { auraPercent -= 0.1f; auraDirection = auraPercent <= 0f; }

            Rectangle frame = BaseDrawing.GetFrame(0, 30, 30, 0, 0);

            BaseDrawing.DrawAura(Main.spriteBatch, TextureAssets.Projectile[Projectile.type].Value, 0, Projectile.position, Projectile.width, Projectile.height, auraPercent, 1.2f, Projectile.scale, Projectile.rotation, -1, 1, frame, 0, 0, Color.White);
            BaseDrawing.DrawTexture(Main.spriteBatch, TextureAssets.Projectile[Projectile.type].Value, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, Projectile.rotation, -1, 1, frame, Projectile.GetAlpha(ColorUtils.COLOR_GLOWPULSE));

            return false;
        }
    }
}