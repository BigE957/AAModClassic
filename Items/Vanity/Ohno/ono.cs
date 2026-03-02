using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Vanity.Ohno
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
		public override bool DrawHead()/* tModPorter Note: Removed. After registering this as EquipType.Head, use ArmorIDs.Head.Sets.DrawHead[slot] = false if you returned false */ 
		{
			return false;
		}
	}

	public class onoBody : EquipTexture
	{
		public override bool DrawBody()/* tModPorter Note: Removed. After registering this as EquipType.Body, use ArmorIDs.Body.Sets.HidesTopSkin[slot] = true if you returned false */ 
		{
			return false;
		}
	}

	public class onoLegs : EquipTexture
	{
		public override bool DrawLegs()/* tModPorter Note: Removed. After registering this as EquipType.Legs or Shoes, use ArmorIDs.Legs.Sets.HidesBottomSkin[slot] = true if you returned false for EquipType.Legs, and ArmorIDs.Shoe.Sets.OverridesLegs[slot] = true if you returned false for EquipType.Shoes */ 
		{
			return false;
		}
	}
}