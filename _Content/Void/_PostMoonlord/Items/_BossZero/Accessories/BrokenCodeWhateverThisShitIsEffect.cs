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
    public class BrokenCodeWhateverThisShitIsEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetModPlayer<BrokenCodeWhateverThisShitIsPlayer>().effect = true;
        }
    }

    public class BrokenCodeWhateverThisShitIsPlayer : EquipmentEffectPlayer
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