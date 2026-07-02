using AAModClassic._Content.Void._PostMoonlord.Items._BossZero.Accessories;
using AAModClassic.Dusts;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Acropolis.__Hardmode.Items._BossAthena.Accessories
{
    public class OlympianWingsEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetModPlayer<OlympianWingsPlayer>().effect = true;
        }
    }

    public class OlympianWingsPlayer : EquipmentEffectPlayer
    {
        public int DashTimer;

        public override void PostUpdateBuffs()
        {
            if (Player.mount.Active || Player.mount.Cart)
            {
                Player.dashDelay = 60;
                effect = false;
            }
        }

        public override void PostUpdateEquips()
        {
            if (Player.mount.Active || Player.mount.Cart)
            {
                Player.dashDelay = 60;
                effect = false;
            }
        }

        public override void PostUpdateRunSpeeds()
        {
            if (Player.pulley && effect)
            {
                AADashMovement();
            }
            else if (Player.grappling[0] == -1 && !Player.tongued)
            {
                AAHorizontalMovement();
                if (effect)
                {
                    AADashMovement();
                }
            }
        }

        public void AAHorizontalMovement()
        {
            float runSpeed = (Player.accRunSpeed + Player.maxRunSpeed) / 2f;
            if (Player.controlLeft && Player.velocity.X > -Player.accRunSpeed && Player.dashDelay >= 0)
            {
                if (Player.velocity.X < -runSpeed && Player.velocity.Y == 0f && !Player.mount.Active)
                {
                    if (effect && Main.rand.NextBool(50))
                    {
                        int dust = Dust.NewDust(new Vector2(Player.position.X - 4f, Player.position.Y), Player.width + 8, 4, ModContent.DustType<FeatherDust>(), -Player.velocity.X * 0.5f, Player.velocity.Y * 0.5f, 50, default, 1.5f);
                        Main.dust[dust].velocity.X = Main.dust[dust].velocity.X * 0.2f;
                        Main.dust[dust].velocity.Y = Main.dust[dust].velocity.Y * 0.2f;
                        Main.dust[dust].shader = GameShaders.Armor.GetSecondaryShader(Player.cWings, Player);
                    }
                }
            }
            else if (Player.controlRight && Player.velocity.X < Player.accRunSpeed && Player.dashDelay >= 0)
            {
                if (Player.velocity.X > runSpeed && Player.velocity.Y == 0f && !Player.mount.Active)
                {
                    if (effect && Main.rand.NextBool(50))
                    {
                        int dust = Dust.NewDust(new Vector2(Player.position.X - 4f, Player.position.Y), Player.width + 8, 4, ModContent.DustType<FeatherDust>(), -Player.velocity.X * 0.5f, Player.velocity.Y * 0.5f, 50, default, 1.5f);
                        Main.dust[dust].velocity.X = Main.dust[dust].velocity.X * 0.2f;
                        Main.dust[dust].velocity.Y = Main.dust[dust].velocity.Y * 0.2f;
                        Main.dust[dust].shader = GameShaders.Armor.GetSecondaryShader(Player.cWings, Player);
                    }
                }
            }
        }

        public void AADashMovement()
        {
            if (Player.dashDelay > 0)
            {
                return;
            }
            if (Player.dashDelay < 0)
            {
                float num7 = 12f;
                float num8 = 0.985f;
                float num9 = Math.Max(Player.accRunSpeed, Player.maxRunSpeed);
                float num10 = 0.94f;
                int num11 = 20;
                if (effect)
                {
                    for (int k = 0; k < 2; k++)
                    {
                        int num12;
                        if (Player.velocity.Y == 0f)
                        {
                            num12 = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y + Player.height - 4f), Player.width, 8, ModContent.DustType<FeatherDust>(), 0f, 0f, 100, default, 1);
                        }
                        else
                        {
                            num12 = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y + Player.height / 2 - 8f), Player.width, 16, ModContent.DustType<FeatherDust>(), 0f, 0f, 100, default, 1);
                        }
                        Main.dust[num12].velocity *= 0.1f;
                        Main.dust[num12].scale *= 1f + Main.rand.Next(20) * 0.01f;
                        Main.dust[num12].shader = GameShaders.Armor.GetSecondaryShader(Player.cWings, Player);
                    }
                }

                Player.vortexStealthActive = false;
                if (Player.velocity.X > num7 || Player.velocity.X < -num7)
                {
                    Player.velocity.X = Player.velocity.X * num8;
                    return;
                }
                if (Player.velocity.X > num9 || Player.velocity.X < -num9)
                {
                    Player.velocity.X = Player.velocity.X * num10;
                    return;
                }
                Player.dashDelay = num11;
                if (Player.velocity.X < 0f)
                {
                    Player.velocity.X = -num9;
                    return;
                }
                if (Player.velocity.X > 0f)
                {
                    Player.velocity.X = num9;
                    return;
                }
            }
            else if (effect && !Player.mount.Active)
            {
                int direction = 0;
                bool DashAttempt = false;
                if (DashTimer > 0)
                {
                    DashTimer--;
                }
                if (DashTimer < 0)
                {
                    DashTimer++;
                }
                if (Player.controlRight && Player.releaseRight && Player.velocity.Y != 0)
                {
                    if (DashTimer > 0)
                    {
                        direction = 1;
                        DashAttempt = true;
                        DashTimer = 0;
                    }
                    else
                    {
                        DashTimer = 15;
                    }
                }
                else if (Player.controlLeft && Player.releaseLeft && Player.velocity.Y != 0)
                {
                    if (DashTimer < 0)
                    {
                        direction = -1;
                        DashAttempt = true;
                        DashTimer = 0;
                    }
                    else
                    {
                        DashTimer = -15;
                    }
                }
                if (DashAttempt)
                {
                    Player.velocity.X = 14.5f * direction;
                    Point point = (Player.Center + new Vector2(direction * Player.width / 2 + 2, Player.gravDir * -Player.height / 2f + Player.gravDir * 2f)).ToTileCoordinates();
                    Point point2 = (Player.Center + new Vector2(direction * Player.width / 2 + 2, 0f)).ToTileCoordinates();
                    if (WorldGen.SolidOrSlopedTile(point.X, point.Y) || WorldGen.SolidOrSlopedTile(point2.X, point2.Y))
                    {
                        Player.velocity.X = Player.velocity.X / 2f;
                    }
                    Player.dashDelay = -1;
                    for (int num17 = 0; num17 < 2; num17++)
                    {
                        int num18 = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y), Player.width, Player.height, ModContent.DustType<FeatherDust>(), 0f, 0f, 100, default, 1);
                        Dust expr_CDB_cp_0 = Main.dust[num18];
                        expr_CDB_cp_0.position.X += Main.rand.Next(-5, 6);
                        Dust expr_D02_cp_0 = Main.dust[num18];
                        expr_D02_cp_0.position.Y += Main.rand.Next(-5, 6);
                        Main.dust[num18].velocity *= 0.2f;
                        Main.dust[num18].scale *= .1f + Main.rand.Next(20) * 0.01f;
                        Main.dust[num18].shader = GameShaders.Armor.GetSecondaryShader(Player.cWings, Player);
                    }
                    return;
                }
            }
        }
    }
}