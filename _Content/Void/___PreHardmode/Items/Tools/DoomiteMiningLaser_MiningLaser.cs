using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void.___PreHardmode.Items.Tools
{
    //ported from my tAPI mod because I don't want to make artwork
    public class DoomiteMiningLaser_MiningLaser : ModProjectile
	{
		public override void SetDefaults()
		{
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.hide = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = true;
            Projectile.ownerHitCheck = true;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            float num = 1.57079637f;
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            Projectile.localAI[0] += 1f;
            if (Projectile.localAI[0] >= 60f)
            {
                Projectile.localAI[0] = 0f;
            }
            if (Vector2.Distance(vector, Projectile.Center) >= 5f)
            {
                float num8 = Projectile.localAI[0] / 60f;
                if (num8 > 0.5f)
                {
                    num8 = 1f - num8;
                }
                Vector3 value4 = new Vector3(1f, 0f, 0f);
                Vector3 value5 = new Vector3(0.3f, 0.2f, 2f);
                Vector3 value6 = Vector3.Lerp(value4, value5, 1f - num8 * 2f) * 0.5f;
                if (Vector2.Distance(vector, Projectile.Center) >= 30f)
                {
                    Vector2 vector4 = Projectile.Center - vector;
                    vector4.Normalize();
                    vector4 *= Vector2.Distance(vector, Projectile.Center) - 30f;
                    DelegateMethods.v3_1 = value6 * 0.8f;
                    Utils.PlotTileLine(Projectile.Center - vector4, Projectile.Center, 8f, new Utils.TileActionAttempt(DelegateMethods.CastLightOpen));
                }
                Lighting.AddLight((int)Projectile.Center.X / 16, (int)Projectile.Center.Y / 16, value6.X, value6.Y, value6.Z);
            }
            if (Main.myPlayer == Projectile.owner)
            {
                if (Projectile.localAI[1] > 0f)
                {
                    Projectile.localAI[1] -= 1f;
                }
                if (!player.channel || player.noItems || player.CCed)
                {
                    Projectile.Kill();
                }
                else if (Projectile.localAI[1] == 0f)
                {
                    Vector2 value7 = vector;
                    Vector2 vector5 = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY) - value7;
                    if (player.gravDir == -1f)
                    {
                        vector5.Y = Main.screenHeight - Main.mouseY + Main.screenPosition.Y - value7.Y;
                    }
                    Tile tile = Main.tile[Player.tileTargetX, Player.tileTargetY];
                    if (tile.HasTile)
                    {
                        vector5 = new Vector2(Player.tileTargetX, Player.tileTargetY) * 16f + Vector2.One * 8f - value7;
                        Projectile.localAI[1] = 2f;
                    }
                    vector5 = Vector2.Lerp(vector5, Projectile.velocity, 0.7f);
                    if (float.IsNaN(vector5.X) || float.IsNaN(vector5.Y))
                    {
                        vector5 = -Vector2.UnitY;
                    }
                    float num9 = 30f;
                    if (vector5.Length() < num9)
                    {
                        vector5 = Vector2.Normalize(vector5) * num9;
                    }
                    int tileBoost = player.inventory[player.selectedItem].tileBoost;
                    int num10 = -Player.tileRangeX - tileBoost + 1;
                    int num11 = Player.tileRangeX + tileBoost - 1;
                    int num12 = -Player.tileRangeY - tileBoost;
                    int num13 = Player.tileRangeY + tileBoost - 1;
                    int num14 = 12;
                    bool flag3 = false;
                    if (vector5.X < num10 * 16 - num14)
                    {
                        flag3 = true;
                    }
                    if (vector5.Y < num12 * 16 - num14)
                    {
                        flag3 = true;
                    }
                    if (vector5.X > num11 * 16 + num14)
                    {
                        flag3 = true;
                    }
                    if (vector5.Y > num13 * 16 + num14)
                    {
                        flag3 = true;
                    }
                    if (flag3)
                    {
                        Vector2 value8 = Vector2.Normalize(vector5);
                        float num15 = -1f;
                        if (value8.X < 0f && ((num10 * 16 - num14) / value8.X < num15 || num15 == -1f))
                        {
                            num15 = (num10 * 16 - num14) / value8.X;
                        }
                        if (value8.X > 0f && ((num11 * 16 + num14) / value8.X < num15 || num15 == -1f))
                        {
                            num15 = (num11 * 16 + num14) / value8.X;
                        }
                        if (value8.Y < 0f && ((num12 * 16 - num14) / value8.Y < num15 || num15 == -1f))
                        {
                            num15 = (num12 * 16 - num14) / value8.Y;
                        }
                        if (value8.Y > 0f && ((num13 * 16 + num14) / value8.Y < num15 || num15 == -1f))
                        {
                            num15 = (num13 * 16 + num14) / value8.Y;
                        }
                        vector5 = value8 * num15;
                    }
                    if (vector5.X != Projectile.velocity.X || vector5.Y != Projectile.velocity.Y)
                    {
                        Projectile.netUpdate = true;
                    }
                    Projectile.velocity = vector5;
                }
            }
            Projectile.position = player.RotatedRelativePoint(player.MountedCenter, true) - Projectile.Size / 2f;
            Projectile.rotation = Projectile.velocity.ToRotation() + num;
            Projectile.spriteDirection = Projectile.direction;
            Projectile.timeLeft = 2;
            player.ChangeDir(Projectile.direction);
            player.heldProj = Projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;
            player.itemRotation = (float)Math.Atan2(Projectile.velocity.Y * Projectile.direction, Projectile.velocity.X * Projectile.direction);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            SpriteEffects spriteEffects = SpriteEffects.None;
            Color color25 = Lighting.GetColor((int)(Projectile.position.X + Projectile.width * 0.5) / 16, (int)((Projectile.position.Y + Projectile.height * 0.5) / 16.0));
            Vector2 mountedCenter = Main.player[Projectile.owner].MountedCenter;
            Vector2 vector45 = Projectile.position + new Vector2(Projectile.width, Projectile.height) / 2f + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition;
            Texture2D texture2D36 = TextureAssets.Projectile[Projectile.type].Value;
            Color alpha6 = Projectile.GetAlpha(color25);
            Vector2 vector46 = Main.player[Projectile.owner].RotatedRelativePoint(mountedCenter, true) + Vector2.UnitY * Main.player[Projectile.owner].gfxOffY;
            Vector2 vector47 = vector45 + Main.screenPosition - vector46;
            Vector2 value50 = Vector2.Normalize(vector47);
            float num296 = vector47.Length();
            float num297 = vector47.ToRotation() + 1.57079637f;
            float num298 = -5f;
            float num299 = num298 + 30f;
            new Vector2(2f, num296 - num299);
            Vector2 value51 = Vector2.Lerp(vector45 + Main.screenPosition, vector46 + value50 * num299, 0.5f);
            Vector2 vector48 = -Vector2.UnitY.RotatedBy(Projectile.localAI[0] / 60f * 3.14159274f, default);
            Vector2[] array7 = new Vector2[]
            {
                        vector48,
                        vector48.RotatedBy(1.5707963705062866, default),
                        vector48.RotatedBy(3.1415927410125732, default),
                        vector48.RotatedBy(4.71238911151886, default)
            };
            if (num296 > num299)
            {
                for (int num300 = 0; num300 < 2; num300++)
                {
                    Color color65;
                    if (num300 % 2 == 0)
                    {
                        color65 = Color.Red;
                        color65.A = 128;
                        color65 *= 0.5f;
                    }
                    else
                    {
                        color65 = Color.DimGray;
                        color65.A = 128;
                        color65 *= 0.5f;
                    }
                    Vector2 value52 = new Vector2(array7[num300].X, 0f).RotatedBy(num297, default) * 4f;
                    Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, value51 - Main.screenPosition + value52, new Rectangle?(new Rectangle(0, 0, 1, 1)), color65, num297, Vector2.One / 2f, new Vector2(2f, num296 - num299), spriteEffects, 0f);
                }
            }
            Texture2D texture2D37 = TextureAssets.Item[Main.player[Projectile.owner].inventory[Main.player[Projectile.owner].selectedItem].type].Value;
            Color color66 = Lighting.GetColor((int)vector46.X / 16, (int)vector46.Y / 16);
            Main.spriteBatch.Draw(texture2D37, vector46 - Main.screenPosition + value50 * num298, null, color66, Projectile.rotation + 1.57079637f + (spriteEffects == SpriteEffects.None ? 3.14159274f : 0f), new Vector2(spriteEffects == SpriteEffects.None ? 0 : texture2D37.Width, texture2D37.Height / 2f) + Vector2.UnitY * 1f, Main.player[Projectile.owner].inventory[Main.player[Projectile.owner].selectedItem].scale, spriteEffects, 0f);
            Main.spriteBatch.Draw(ModContent.Request<Texture2D>("AAModClassic/Glowmasks/DoomiteMiningLaser_Glow").Value, vector46 - Main.screenPosition + value50 * num298, null, new Color(255, 255, 255, 0), Projectile.rotation + 1.57079637f + (spriteEffects == SpriteEffects.None ? 3.14159274f : 0f), new Vector2(spriteEffects == SpriteEffects.None ? 0 : texture2D37.Width, texture2D37.Height / 2f) + Vector2.UnitY * 1f, Main.player[Projectile.owner].inventory[Main.player[Projectile.owner].selectedItem].scale, spriteEffects, 0f);
            if (num296 > num299)
            {
                for (int num301 = 2; num301 < 4; num301++)
                {
                    Color color67;
                    if (num301 % 2 == 0)
                    {
                        color67 = Color.Red;
                        color67.A = 128;
                        color67 *= 0.5f;
                    }
                    else
                    {
                        color67 = Color.DimGray;
                        color67.A = 128;
                        color67 *= 0.5f;
                    }
                    Vector2 value53 = new Vector2(array7[num301].X, 0f).RotatedBy(num297, default) * 4f;
                    Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, value51 - Main.screenPosition + value53, new Rectangle?(new Rectangle(0, 0, 1, 1)), color67, num297, Vector2.One / 2f, new Vector2(2f, num296 - num299), spriteEffects, 0f);
                }
            }
            float num302 = Projectile.localAI[0] / 60f;
            if (num302 > 0.5f)
            {
                num302 = 1f - num302;
            }
            Main.spriteBatch.Draw(texture2D36, vector45, null, alpha6 * num302 * 2f, Projectile.rotation, new Vector2(texture2D36.Width, texture2D36.Height) / 2f, Projectile.scale, spriteEffects, 0f);
            Main.spriteBatch.Draw(TextureAssets.Projectile[Projectile.type].Value, vector45, null, alpha6 * (0.5f - num302) * 2f, Projectile.rotation, new Vector2(texture2D36.Width, texture2D36.Height) / 2f, Projectile.scale, spriteEffects, 0f);
            return false;
        }
    }
}