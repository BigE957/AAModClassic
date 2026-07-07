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

namespace AAModClassic._Content.Ocean.___PreHardmode.Items.Armor
{
    public class AtlanteanHelmetSetEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            if (player.wet)
            {
                player.AddBuff(ModContent.BuffType<Atlantean_Buff>(), 2);
            }
        }
    }
}