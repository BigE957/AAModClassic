using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Steamworks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Stars._PostMoonlord.Items._BossEquinoxWorms.Accessories
{
    [AutoloadEquip(EquipType.HandsOn)]
    public class RadiantStar : EquipAbstract, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Accessories";
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.rare = ItemRarityID.Purple;
            Item.accessory = true;
            Item.expert = true;
        }
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Radiant Star");
            /* Tooltip.SetDefault(@"'It's Shiny'"); */
        }

        public override void RegisterEquipStats()
        {
            AddEffect(new EquinoxDayNightStatBoostsEffect());
            AddEffect(new EmitLightFromPlayerEffect(1f, 0.95f, 0.8f));
        }
    }
}