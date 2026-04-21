using System;
using System.Collections.Generic;
using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;



namespace AAModClassic.___Content.Snow.___PreHardmode.Items._BossSubzeroSerpent.Weapons
{
    public class SnakeStaff_SerpentTail : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;

            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.netImportant = true;
            Projectile.tileCollide = false;
            Projectile.minion = true;

            Projectile.penetrate = -1;
            Projectile.timeLeft = 18000;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            Projectile.timeLeft *= 5;
            Projectile.GetGlobalProjectile<AAGlobalProjectile>().LongMinion = true;
            Projectile.minionSlots = 0;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ancient Serpent");
        }
        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            behindProjectiles.Add(index);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture2D13 = TextureAssets.Projectile[Projectile.type].Value;
            int num214 = TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type];
            int y6 = num214 * Projectile.frame;
            Main.spriteBatch.Draw(texture2D13, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Rectangle(0, y6, texture2D13.Width, num214),
                Projectile.GetAlpha(Color.White), Projectile.rotation, new Vector2(texture2D13.Width / 2f, num214 / 2f), Projectile.scale,
                Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            return false;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();

            if ((int)Main.time % 120 == 0) Projectile.netUpdate = true;
            if (!player.active)
            {
                Projectile.active = false;
                return;
            }


            if (player.dead) modPlayer.SnakeMinion = false;
            if (modPlayer.SnakeMinion) Projectile.timeLeft = 2;
            int num1038 = 30;

            bool flag67 = false;
            Vector2 value67 = Vector2.Zero;
            float num1052 = 0f;
            if (Projectile.ai[1] == 1f)
            {
                Projectile.ai[1] = 0f;
                Projectile.netUpdate = true;
            }

            int byUUID = Projectile.GetByUUID(Projectile.owner, (int)Projectile.ai[0]);
            if (byUUID >= 0 && Main.projectile[byUUID].active)
            {
                flag67 = true;
                value67 = Main.projectile[byUUID].Center;
                num1052 = Main.projectile[byUUID].rotation;
                Main.projectile[byUUID].localAI[0] = Projectile.localAI[0] + 1f;
                if (Main.projectile[byUUID].type != ModContent.ProjectileType<SnakeStaff_SerpentHead>()) Main.projectile[byUUID].localAI[1] = Projectile.whoAmI;
                if (Projectile.owner == player.whoAmI && Main.projectile[byUUID].type == ModContent.ProjectileType<SnakeStaff_SerpentHead>())
                {
                    Main.projectile[byUUID].Kill();
                    Projectile.Kill();
                    return;
                }
            }

            if (!flag67) return;
            if (Projectile.alpha > 0)
                for (int num1054 = 0; num1054 < 2; num1054++)
                {
                    int num1055 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.IceTorch, 0f, 0f, 100, default, 2f);
                    Main.dust[num1055].noGravity = true;
                    Main.dust[num1055].noLight = true;
                }

            Projectile.alpha -= 42;
            if (Projectile.alpha < 0) Projectile.alpha = 0;
            Projectile.velocity = Vector2.Zero;
            Vector2 vector134 = value67 - Projectile.Center;
            if (num1052 != Projectile.rotation)
            {
                float num1056 = MathHelper.WrapAngle(num1052 - Projectile.rotation);
                vector134 = vector134.RotatedBy(num1056 * 0.1f, default);
            }

            Projectile.rotation = vector134.ToRotation() + 1.57079637f;
            Projectile.position = Projectile.Center;
            Projectile.width = Projectile.height = (int)(num1038 * Projectile.scale);
            Projectile.Center = Projectile.position;
            if (vector134 != Vector2.Zero) Projectile.Center = value67 - Vector2.Normalize(vector134) * 31;
            Projectile.spriteDirection = vector134.X > 0f ? 1 : -1;
        }
    }
}
