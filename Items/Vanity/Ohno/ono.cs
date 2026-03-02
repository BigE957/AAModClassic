using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Vanity.Ohno
{
    public class ono : BaseAAItem
	{
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
			Item.value = 100;
			Item.rare = ItemRarityID.Gray;
		}

		public override void UpdateAccessory(Player player, bool hideVisual) 
		{
			AAPlayer p = player.GetModPlayer<AAPlayer>();
			p.ono = true;
			if (hideVisual) 
			{
				p.onoHideVanity = true;
			}
		}
	}

	public class onoHead : EquipTexture
	{
        public override void PreUpdateVanitySet(Player player)
        {
			ArmorIDs.Head.Sets.DrawHead[Slot] = false;
        }
	}

	public class onoBody : EquipTexture
	{
        public override void PreUpdateVanitySet(Player player)
        {
            ArmorIDs.Body.Sets.HidesTopSkin[Slot] = true;
        }
	}

	public class onoLegs : EquipTexture
	{
        public override void PreUpdateVanitySet(Player player)
        {
            ArmorIDs.Legs.Sets.HidesBottomSkin[Slot] = true;
        }
	}
}