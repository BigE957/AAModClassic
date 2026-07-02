using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.MartianMadness.__Hardmode.Items.Accessories
{
    public class EnergyConduit : EquipAbstract, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Accessories";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Energy Conduit");
        }

        public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 24;
			Item.value = Item.sellPrice(0, 6, 0, 0);
			Item.rare = ItemRarityID.Yellow;
			Item.accessory = true;
            
        }

        public override void RegisterEquipStats()
        {
            AddEffect(new MovementSpeedEffect(0.5f));
            AddEffect(new MaxRunSpeedEffect(0.5f));
        }
    }
}
