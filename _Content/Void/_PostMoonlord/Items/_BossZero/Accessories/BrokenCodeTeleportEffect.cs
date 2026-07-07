using AAModClassic._Content.Terra.__Hardmode.Items.Armor;
using AAModClassic._Unreleased.Content.Void.Dusts;
using AAModClassic.Dusts;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.UI.World;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Humanizer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.Audio;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void._PostMoonlord.Items._BossZero.Accessories
{
    public class BrokenCodeTeleportEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetModPlayer<BrokenCodeTeleportPlayer>().effect = true;
        }
    }

    public class BrokenCodeTeleportPlayer : EquipmentEffectPlayer
    {
        public int CodeCD = 0;
        public bool on = true;

        public override void UpdateEquips()
        {
            if (effect)
            {
                if (Player.controlHook && CodeCD == 0 && Main.myPlayer == Player.whoAmI)
                {
                    Vector2 vector32;
                    vector32.X = Main.mouseX + Main.screenPosition.X;
                    if (Player.gravDir == 1f)
                    {
                        vector32.Y = Main.mouseY + Main.screenPosition.Y - Player.height;
                    }
                    else
                    {
                        vector32.Y = Main.screenPosition.Y + Main.screenHeight - Main.mouseY;
                    }
                    vector32.X -= Player.width / 2;
                    if (vector32.X > 50f && vector32.X < Main.maxTilesX * 16 - 50 && vector32.Y > 50f && vector32.Y < Main.maxTilesY * 16 - 50)
                    {
                        int num246 = (int)(vector32.X / 16f);
                        int num247 = (int)(vector32.Y / 16f);
                        if ((Main.tile[num246, num247].WallType != WallID.LihzahrdBrickUnsafe || num247 <= Main.worldSurface || NPC.downedPlantBoss) && !Collision.SolidCollision(vector32, Player.width, Player.height))
                        {
                            Player.Teleport(vector32, 1, 0);
                            NetMessage.SendData(MessageID.TeleportEntity, -1, -1, null, 0, Player.whoAmI, vector32.X, vector32.Y, 1, 0, 0);
                            SoundEngine.PlaySound(new SoundStyle("AAModClassic/Sounds/Glitch"));
                            CodeCD = 600;
                            Player.AddBuff(ModContent.BuffType<BrokenCode_Glitched>(), 300);
                        }
                    }
                }
                if (CodeCD > 300)
                {
                    if (CodeCD > 450)
                    {
                        Player.immuneNoBlink = true;
                    }
                    else
                    {
                        Player.immuneNoBlink = false;
                    }
                    if (on)
                    {
                        on = false;
                        Player.moveSpeed += 5f;
                        Player.headPosition.Y -= 20f;
                        Player.headPosition.X += 15f;
                        Player.bodyPosition.Y += 37f;
                        Player.bodyPosition.X -= 23f;
                        Player.legPosition.Y += 20f;
                        Player.legPosition.X -= 12f;
                    }
                }
                else
                {
                    if (!on)
                    {
                        on = true;
                        Player.moveSpeed -= 5f;
                        Player.headPosition.Y += 20f;
                        Player.headPosition.X -= 15f;
                        Player.bodyPosition.Y -= 37f;
                        Player.bodyPosition.X += 23f;
                        Player.legPosition.Y -= 20f;
                        Player.legPosition.X += 12f;
                    }
                }
                if (CodeCD > 0)
                {
                    CodeCD--;
                }
            }
        }
    }
}