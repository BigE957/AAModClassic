using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Vanity.CC
{
	[AutoloadEquip(EquipType.Body)]
	internal class CCRobe : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Dread Cultist Robe");
			/* Tooltip.SetDefault(@"The hood of a crazy lizard enthusiast
'Great for impersonating Ancients Awakened Developers!'"); */
		}
		public override void SetDefaults() 
		{
			Item.width = 18;
			Item.height = 14;
			Item.rare = ItemRarityID.Blue;
			Item.vanity = true;
		}

		public override void SetMatch(bool male, ref int equipSlot, ref bool robes) 
		{
			robes = true;
			equipSlot = EquipLoader.GetEquipSlot(Mod, "CCRobe_Legs", EquipType.Legs);
		}

		//public override void DrawHands(ref bool drawHands, ref bool drawArms)/* tModPorter Note: Removed. In SetStaticDefaults, use ArmorIDs.Body.Sets.HidesHands[Item.bodySlot] = false if you had drawHands set to true. If you had drawArms set to true, you don't need to do anything */ 
		//{
		//	drawHands = false;
		//}

		public override void ModifyTooltips(List<TooltipLine> list)
		{
			foreach (TooltipLine line2 in list)
			{
				if (line2.Mod == "Terraria" && line2.Name == "ItemName")
				{
					line2.OverrideColor = new Color(92, 101, 150);
				}
			}
		}
	}
}
