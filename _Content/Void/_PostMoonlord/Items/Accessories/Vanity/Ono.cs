using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void._PostMoonlord.Items.Accessories.Vanity
{
    public class Ono : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Vanity.Ohno";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("ono");
			// Tooltip.SetDefault("ono");
		}

		public override void SetDefaults() 
		{
			Item.width = 16;
			Item.height = 16;
			Item.accessory = true;
			Item.vanity = true;
			Item.value = 100;
			Item.rare = ItemRarityID.Gray;
		}

		public override void UpdateAccessory(Player player, bool hideVisual) 
		{
			ZAAPlayer p = player.GetModPlayer<ZAAPlayer>();
			p.ono = true;
			if (hideVisual) 
			{
				p.onoHideVanity = true;
			}
		}
	}

	public class OnoHead : EquipTexture
	{
        public override void PreUpdateVanitySet(Player player)
        {
			ArmorIDs.Head.Sets.DrawHead[Slot] = false;
        }
	}

	public class OnoBody : EquipTexture
	{
        public override void PreUpdateVanitySet(Player player)
        {
            ArmorIDs.Body.Sets.HidesTopSkin[Slot] = true;
        }
	}

	public class OnoLegs : EquipTexture
	{
        public override void PreUpdateVanitySet(Player player)
        {
            ArmorIDs.Legs.Sets.HidesBottomSkin[Slot] = true;
        }
	}
}