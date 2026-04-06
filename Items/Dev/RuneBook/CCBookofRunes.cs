using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using AAModClassic.Buffs;
using AAModClassic;

namespace AAModClassic.Items.Dev.RuneBook
{
    public class CCBookofRunes : BaseAAItem
	{
		public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("The Book of Runes");
            /* Tooltip.SetDefault(@"Summons runes according to how many minion slots you have left
When player has 1 minion slot it summons terra rune.
When player has 2 minion slots it summons terra and chaos rune.
When player has 3 minion slots it summons terra, chaos and void rune."); */
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = Color.Gold;
                }
            }
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.value = 100000;
            Item.rare = ItemRarityID.Purple;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
            modPlayer.CCBookEX = true;
            if(hideVisual)
            {
                modPlayer.CCBookEX = false;
                player.ClearBuff(ModContent.BuffType<Buffs.CCRune_Buff>());
            }
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod, "CCRuneBookPage", 1);
			recipe.AddIngredient(ItemID.Book, 1);
            recipe.AddIngredient(Mod, "DreadScale", 15);
            recipe.AddIngredient(Mod, "EXSoul", 1);
			recipe.AddTile(Mod, "ACS");
			recipe.Register();
		}
    }
}