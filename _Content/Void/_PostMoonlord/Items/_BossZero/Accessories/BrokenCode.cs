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
    public class BrokenCode : EquipAbstract, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Accessories";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Broken Code");
            /* Tooltip.SetDefault(@"
            'You don't look so good'"); */
            ItemID.Sets.ItemNoGravity[Item.type] = true;
        }

        // TODO -- Velocity Y smaller, post NewItem?
        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 52;
            Item.maxStack = Item.CommonMaxStack;
            Item.value = Item.sellPrice(3, 0, 0, 0);
            Item.expert = true; Item.expertOnly = true;
            Item.accessory = true;
            Item.rare = ModContent.RarityType<AncientsRarity>();
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return AAColor.COLOR_WHITEFADE1;
        }

        public override void RegisterEquipStats()
        {
            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
            {
                AddEffect<BrokenCodeTeleportUnofficial>();
            }
            else
            {
                AddEffect<BrokenCodeWhateverThisShitIs>();
                AddEffect<BrokenCodeTeleport>();
            }
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, Color.Red.ToVector3() * 0.55f * Main.essScale);
        }
    }

    public class BrokenCodeTeleport : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetModPlayer<BrokenCodeTeleportPlayer>().effect = true;
        }
    }

    public class BrokenCodeTeleportPlayer : EquipEffectAbstract
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

    public class BrokenCodeTeleportUnofficial : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetModPlayer<BrokenCodeTeleportUnofficialPlayer>().effect = true;
        }
    }

    public class BrokenCodeTeleportUnofficialPlayer : EquipEffectAbstract
    {
        public int secretInvulTimer = 0;

        private const int FREEZEDURATION = 150;
        private const int SECRETINVULDURATION = 30;
        private const int FREEZECOOLDOWNDURATION = 1200;

        public override void UpdateEquips()
        {
            if (effect)
            {
                if (Player.controlHook && !Player.HasBuff<BrokenCode_FreezeCooldown>() && !Player.HasBuff<BrokenCode_Freeze>() && Main.myPlayer == Player.whoAmI)
                {
                    Teleport();
                    Player.AddBuff(ModContent.BuffType<BrokenCode_FreezeCooldown>(), FREEZECOOLDOWNDURATION);
                    //Player.AddBuff(ModContent.BuffType<BrokenCode_FreezeCooldown>(), 60);
                    Player.AddBuff(ModContent.BuffType<BrokenCode_Freeze>(), FREEZEDURATION);
                    //Player.AddBuff(ModContent.BuffType<BrokenCode_Freeze>(), 20);
                    secretInvulTimer = SECRETINVULDURATION;
                }

                if (secretInvulTimer > 0)
                {
                    Player.immuneNoBlink = true;
                }
            }
        }

        public void Teleport()
        {
            Vector2 teleportPos = Main.MouseWorld;
            TeleportEffect(Player.getRect(), 1, teleportPos);
            Player.Teleport(teleportPos, TeleportationStyleID.DebugTeleport, 0);
            NetMessage.SendData(MessageID.TeleportEntity, -1, -1, null, 0, Player.whoAmI, teleportPos.X, teleportPos.Y, 1, 0, 0);
            TeleportEffect(Player.getRect(), 1, Player.position);
            SoundEngine.PlaySound(new SoundStyle("AAModClassic/Sounds/Glitch") { PitchVariance = 0.8f });
        }

        // shamelessly stolen from the queen slime hook teleport effect
        public void TeleportEffect(Rectangle effectRect, float dustCountMult = 1f, Vector2 otherPosition = default)
        {
            effectRect.Inflate(15, 15);
            int num7 = (int)(60f * dustCountMult);
            Vector2 vector = otherPosition - effectRect.TopLeft();
            for (int n = 0; n < num7; n++)
            {
                float fadeIn = 0.4f + Main.rand.NextFloat();
                float scale = 0.4f + Main.rand.NextFloat();
                Color newColor = Main.hslToRgb(0.66f + Main.rand.NextFloat() * 0.24f, 1f, 0.5f);
                Dust dust = Dust.NewDustDirect(effectRect.TopLeft(), effectRect.Width, effectRect.Height, ModContent.DustType<VoidDust_Unreleased>(), 0f, 0f, 127, newColor);
                dust.scale = (float)Main.rand.Next(20, 70) * 0.01f;
                if (n < 10)
                    dust.scale += 0.25f;

                if (n < 5)
                    dust.scale += 0.25f;

                if ((float)n < (float)num7 * 0.8f)
                    dust.velocity += vector * 0.1f * Main.rand.NextFloat();

                dust.noGravity = true;
                dust.noLight = true;
                dust.scale = scale;
                dust.fadeIn = fadeIn;
                if (dust.dustIndex != 6000)
                {
                    Dust obj2 = Dust.CloneDust(dust);
                    obj2.scale *= 0.65f;
                    obj2.fadeIn *= 0.65f;
                    obj2.color = new Color(255, 255, 255, 255);
                }
            }
        }
    }

    // oughh thanks diamondwalker this thing is awesome
    public class BrokenCodeTeleportUnofficialEdits
    {
        public static void ApplyEdits()
        {
            On_Player.Update += UpdatePlayer;
            On_NPC.UpdateNPC += UpdateNPC;
            On_Projectile.Update += UpdateProj;
            On_Dust.UpdateDust += UpdateDust;

            On_Player.QuickGrapple_GetItemToUse += QuickGrapple_GetItemToUse;
        }

        private static void UpdatePlayer(On_Player.orig_Update orig, Player p, int i)
        {
            int buff = ModContent.BuffType<BrokenCode_Freeze>();

            if (p.active && p.HasBuff(buff))
            {
                // make sure the glitched buff is still counting down even though the others aren't
                int index = p.FindBuffIndex(buff);
                p.buffTime[index]--;
                p.GetModPlayer<BrokenCodeTeleportUnofficialPlayer>().secretInvulTimer--;

                // the teleport
                // we check if player is pressing grapple a diff way bcuz since player logic is paused we cant know if theyre still pressing
                // it or not the safe normal human way
                if (PlayerInput.Triggers.Current.Grapple && p.GetModPlayer<BrokenCodeTeleportUnofficialPlayer>().secretInvulTimer <= 0 && Main.myPlayer == p.whoAmI)
                {
                    p.GetModPlayer<BrokenCodeTeleportUnofficialPlayer>().Teleport();
                    p.ClearBuff(ModContent.BuffType<BrokenCode_Freeze>());
                    p.GetModPlayer<BrokenCodeTeleportUnofficialPlayer>().secretInvulTimer = 60;
                }

                return;
            }

            // if any other players have the buff, we still freeze
            foreach (Player player in Main.player)
            {
                if (player != null && player != p && player.active && player.HasBuff(buff))
                {
                    return;
                }
            }

            orig(p, i);
        }

        private static void UpdateNPC(On_NPC.orig_UpdateNPC orig, NPC npc, int i)
        {
            int buff = ModContent.BuffType<BrokenCode_Freeze>();
            foreach (Player player in Main.player)
            {
                if (player != null && player.active && player.HasBuff(buff))
                {
                    return;
                }
            }

            orig(npc, i);
        }

        private static void UpdateProj(On_Projectile.orig_Update orig, Projectile proj, int i)
        {
            int buff = ModContent.BuffType<BrokenCode_Freeze>();
            foreach (Player player in Main.player)
            {
                if (player != null && player.active && player.HasBuff(buff))
                {
                    return;
                }
            }

            orig(proj, i);
        }

        private static void UpdateDust(On_Dust.orig_UpdateDust orig)
        {
            int buff = ModContent.BuffType<BrokenCode_Freeze>();
            foreach (Player player in Main.player)
            {
                if (player != null && player.active && player.HasBuff(buff))
                {
                    return;
                }
            }

            orig();
        }

        private static Item QuickGrapple_GetItemToUse(On_Player.orig_QuickGrapple_GetItemToUse orig, Player self)
        {
            if (self.GetModPlayer<BrokenCodeTeleportUnofficialPlayer>().effect && self.HasBuff<BrokenCode_FreezeCooldown>() && Main.myPlayer == self.whoAmI)
                return null;

            return orig(self);
        }
    }

    public class BrokenCodeWhateverThisShitIs : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetModPlayer<BrokenCodeWhateverThisShitIsPlayer>().effect = true;
        }
    }

    public class BrokenCodeWhateverThisShitIsPlayer : EquipEffectAbstract
    {
        public override void OnHitByAnything(Player.HurtInfo hurtInfo, NPC npc = null, Projectile proj = null)
        {
            if (effect)
            {
                Player.AddBuff(BuffID.Panic, 180);
                Player.immuneTime = Player.longInvince ? 180 : 120;
            }
            ;
        }
    }
}