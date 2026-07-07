using AAModClassic._Content.Bunny.__Hardmode.Items.Armor;
using AAModClassic._Content.Bunny._PostMoonlord.Items.Materials;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._Content.Mire._PostMoonlord.Items._BossYamata.BossStandard;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Bunny._PostMoonlord.Items.Armor
{
    public class ChampionHelmetRangedSetEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetModPlayer<ChampionHelmetRangedSetPlayer>().effect = true;
        }
    }

    public class ChampionHelmetRangedSetPlayer : EquipmentEffectPlayer
    {
        public override void PostUpdate()
        {
            if (effect && AAMod.ArmorAbilityKey.JustPressed && !Player.HasBuff(ModContent.BuffType<ChampionHelmetRangedSetEffect_RABITUnitReloadProtocol>()) && !AAGlobalProjectile.AnyProjectiles(ModContent.ProjectileType<ChampionHelmetRangedSetEffect_RABITDrone>()))
            {
                Vector2 vector2;
                vector2.X = Main.mouseX + Main.screenPosition.X;
                vector2.Y = Main.mouseY + Main.screenPosition.Y;
                Projectile.NewProjectile(Player.GetSource_FromThis(), vector2.X, vector2.Y, 0, 0, ModContent.ProjectileType<ChampionHelmetRangedSetEffect_RABITDrone>(), (int)(Player.GetDamage(DamageClass.Ranged)).ApplyTo(100), 2, Main.myPlayer, 0f, 0f);
            }
        }
    }
}