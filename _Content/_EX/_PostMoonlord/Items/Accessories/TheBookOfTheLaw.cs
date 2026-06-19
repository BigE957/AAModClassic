using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using AAModClassic._Content._EX._PostMoonlord.Items.Materials;
using AAModClassic._Content._Dev.__Hardmode.Items.Accessories;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._Content._Dev.__Hardmode.Items.Armor.Vanity;

namespace AAModClassic._Content._EX._PostMoonlord.Items.Accessories
{
    [AutoloadEquip(EquipType.HandsOff)]
	public class TheBookOfTheLaw : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Accessories";
		public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("The Book of the Law");
            /* Tooltip.SetDefault(@"A Legendary Book of the Mega Therion.
30% increased minion damage
+2 minion slots
Includes the effects of all the pieces used to make this.
"); */
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            string text = "";
            text += Language.GetTextValue("Mods.AAModClassic.Common.InvokerBookEX1");
            
            if(!Main.player[Main.myPlayer].GetModPlayer<TheBookOfTheLaw_InvokerPlayer>().DarkCaligula)
            text += Language.GetTextValue("Mods.AAModClassic.Common.InvokerBookEX2");
            else
            text += Language.GetTextValue("Mods.AAModClassic.Common.InvokerBookEX3");

            TooltipLine line = new TooltipLine(Mod, "newtooltip", text);
            list.RemoveAt(2);
            list.Insert(2,line);

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
            Item.width = 18;
            Item.height = 20;
            Item.rare = ItemRarityID.Purple;
            Item.accessory = true;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTime = 1;
            Item.expertOnly = true;
            Item.useTime = 30;
            Item.useAnimation = 30;
        }
        
        public override void UpdateAccessory(Player player, bool hideVisual)
		{
            player.GetDamage(DamageClass.Summon) += .3f;
            player.maxMinions += 2;
            player.GetKnockback(DamageClass.Summon).Base += 2f;

			player.lifeRegen += 26;
            player.lifeRegenTime += 2;

            player.maxTurrets++;
            

            TheBookOfTheLaw_InvokerPlayer InvokerPlayer = TheBookOfTheLaw_InvokerPlayer.ModPlayer(player);
            //InvokerPlayer.BanishProjClear = true;  //This need change.
            InvokerPlayer.Thebookoflaw = true;
            InvokerPlayer.SpringInvoker = true;
            if(!hideVisual) 
                InvokerPlayer.InvokerShow = true;
            InvokerPlayer.BanishDamageMult += 4.5f;
            InvokerPlayer.BanishLimit += 5;
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<AleisterBook>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CerberusHelmet>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CerberusChestplate>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CerberusLeggings>(), 1);
			recipe.AddIngredient(ItemID.SquireGreatHelm, 1);
            recipe.AddIngredient(ItemID.SquireAltShirt, 1);
            recipe.AddIngredient(ItemID.ShinyStone, 1);
            recipe.AddIngredient(ItemID.FrozenShield, 1);
            recipe.AddIngredient(ItemID.SpectreBar, 60);
            recipe.AddIngredient(ModContent.ItemType<EXSoul>(), 1);
			recipe.AddTile(ModContent.TileType<AnyAncientCraftingStation_Tile>());
			recipe.Register();
		}
    }
}