using AAModClassic._Content.Bunny.__Hardmode.Items.Armor;
using AAModClassic._Content.Bunny._PostMoonlord.Items.Materials;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossSistersOfDiscord.Ashe;
using AAModClassic._Content.Mire._PostMoonlord.Items._BossYamata.BossStandard;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos._PostMoonlord.Items._BossSistersOfDiscord.Armor
{
    public class FuryWitchsHelmetSetHotkeyEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetModPlayer<FuryWitchsHelmetSetHotkeyPlayer>().effect = true;
        }
    }

    public class FuryWitchsHelmetSetHotkeyPlayer : EquipmentEffectPlayer
    {
        public override void PostUpdate()
        {
            if (effect && AAMod.ArmorAbilityKey.JustPressed && !Player.HasBuff<FuryWitchsHelmetSetHotkeyEffect_AsheFlameCooldown>())
            {
                SoundEngine.PlaySound(SoundID.Zombie104, Player.position);
                if (Player.inventory[Player.selectedItem].CountsAsClass(DamageClass.Magic) || Player.inventory[Player.selectedItem].CountsAsClass(DamageClass.Summon))
                {
                    for (int i = 0; i < 8; i++)
                    {
                        Vector2 shoot = new Vector2((float)Math.Sin(i * 0.25f * 3.1415926f), (float)Math.Cos(i * 0.25f * 3.1415926f));
                        shoot *= 8f;
                        int id = Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center.X, Player.Center.Y, shoot.X, shoot.Y, ModContent.ProjectileType<Ashe_FireBomb>(), Player.inventory[Player.selectedItem].damage, 5, Main.myPlayer, 0f, 1f);
                        Main.projectile[id].DamageType = DamageClass.Magic;
                        Main.projectile[id].hostile = false;
                        Main.projectile[id].friendly = true;
                    }
                }
                Player.AddBuff(ModContent.BuffType<FuryWitchsHelmetSetHotkeyEffect_AsheFlame>(), 900);
                Player.AddBuff(ModContent.BuffType<FuryWitchsHelmetSetHotkeyEffect_AsheFlameCooldown>(), 5400);
            }
        }
    }
}