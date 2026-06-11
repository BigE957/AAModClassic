using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno._PostMoonlord.Items._BossAkuma.Weapons
{
    public class AncientLungStaff_LungBody : ModProjectile
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
            Projectile.minionSlots = .5f;
            Projectile.GetGlobalProjectile<AAGlobalProjectile>().LongMinion = true;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ancient Lung");
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

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.immune[Projectile.owner] = 6;
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

            int num1038 = 10;
            if (player.dead) modPlayer.LungMinion = false;
            if (modPlayer.LungMinion) Projectile.timeLeft = 2;
            num1038 = 30;

            //D U S T
            /*if (Main.rand.Next(30) == 0)
            {
                int num1039 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 135, 0f, 0f, 0, default, 2f);
                Main.dust[num1039].noGravity = true;
                Main.dust[num1039].fadeIn = 2f;
                Point point4 = Main.dust[num1039].position.ToTileCoordinates();
                if (WorldGen.InWorld(point4.X, point4.Y, 5) && WorldGen.SolidTile(point4.X, point4.Y))
                {
                    Main.dust[num1039].noLight = true;
                }
            }*/

            bool flag67 = false;
            Vector2 value67 = Vector2.Zero;
            Vector2 arg_2D865_0 = Vector2.Zero;
            float num1052 = 0f;
            float scaleFactor16 = 0f;
            float scaleFactor17 = 1f;
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
                Vector2 arg_2D957_0 = Main.projectile[byUUID].velocity;
                num1052 = Main.projectile[byUUID].rotation;
                float num1053 = MathHelper.Clamp(Main.projectile[byUUID].scale, 0f, 50f);
                scaleFactor17 = num1053;
                scaleFactor16 = 16f;
                int arg_2D9AD_0 = Main.projectile[byUUID].alpha;
                Main.projectile[byUUID].localAI[0] = Projectile.localAI[0] + 1f;
                if (Main.projectile[byUUID].type != ModContent.ProjectileType<AncientLungStaff_LungHead>()) Main.projectile[byUUID].localAI[1] = Projectile.whoAmI;
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
            Projectile.scale = scaleFactor17;
            Projectile.width = Projectile.height = (int)(num1038 * Projectile.scale);
            Projectile.Center = Projectile.position;
            if (vector134 != Vector2.Zero) Projectile.Center = value67 - Vector2.Normalize(vector134) * scaleFactor16 * scaleFactor17;
            Projectile.spriteDirection = vector134.X > 0f ? 1 : -1;

            Projectile.damage = Main.projectile[byUUID].damage;
        }

        public override void OnKill(int timeLeft)
        {
            Player player = Main.player[Projectile.owner];
            if (player.slotsMinions + Projectile.minionSlots > player.maxMinions && Projectile.owner == Main.myPlayer)
            {
                int byUUID = Projectile.GetByUUID(Projectile.owner, Projectile.ai[0]);
                if (byUUID != -1)
                {
                    Projectile projectile1 = Main.projectile[byUUID];
                    if (projectile1.type != ModContent.ProjectileType<AncientLungStaff_LungHead>()) projectile1.localAI[1] = Projectile.localAI[1];
                    projectile1 = Main.projectile[(int)Projectile.localAI[1]];
                    projectile1.ai[0] = Projectile.ai[0];
                    projectile1.ai[1] = 1f;
                    projectile1.netUpdate = true;
                }
            }
        }
    }
}