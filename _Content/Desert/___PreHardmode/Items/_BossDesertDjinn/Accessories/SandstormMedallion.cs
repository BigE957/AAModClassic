using AAModClassic._Content._Tinker.___PreHardmode.Items.Accessories;
using AAModClassic._Content.Chaos._PostMoonlord.Items._BossSistersOfDiscord.Accessories;
using AAModClassic.Dusts;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using System;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Desert.___PreHardmode.Items._BossDesertDjinn.Accessories
{
    public class SandstormMedallion : EquipAbstract, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Accessories";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Sandstorm Medallion");
        }
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 50;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.accessory = true;
            Item.expert = true;
        }

        public override void RegisterEquipEffects()
        {
            AddEffect<SandstormMedallionEffect>();
        }

        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            if (equippedItem.type == ModContent.ItemType<FireFrostMedallion>())
                return false;

            return true;
        }
    }
}