using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._Dev.__Hardmode.Items.Armor.Vanity
{
	[AutoloadEquip(EquipType.Body)]
    public class CCChestplateS : ModItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Vanity.CC.Shiny";
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Draconian Cultist Robe");
			/* Tooltip.SetDefault(@"The hood of a crazy dragon enthusiast
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
			equipSlot = EquipLoader.GetEquipSlot(Mod, "ShinyCCRobe_Legs", EquipType.Legs);
		}

		//public override void DrawHands(ref bool drawHands, ref bool drawArms)/* tModPorter Note: _Unreleased. In SetStaticDefaults, use ArmorIDs.Body.Sets.HidesHands[Item.bodySlot] = false if you had drawHands set to true. If you had drawArms set to true, you don't need to do anything */ 
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
