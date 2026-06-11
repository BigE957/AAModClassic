using AAModClassic._Content.Chaos._PostMoonlord.Items.Materials;
using AAModClassic._Content.Inferno._PostMoonlord.Items.Armor;
using AAModClassic._Content.Mire._PostMoonlord.Items.Armor;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Tiles.Crafters;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos._PostMoonlord.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
	public class ChaosSlayerChestplate : BaseAAItem
	{
        public override Color GlowmaskDrawColor => AAColor.Shen3;

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Chaos Slayer Plate");
            /* Tooltip.SetDefault(@"4% increased damage resistance
+75 Max Life
The power of discordian rage radiates from this armor"); */
        }


        public override void SetDefaults()
		{
			Item.width = 26;
			Item.height = 20;
			Item.value = Item.sellPrice(3, 0, 0, 0);
            Item.rare = ModContent.RarityType<SuperancientsRarity>();
            Item.defense = 60;
        }

        

        public override void UpdateEquip(Player player)
		{
            player.endurance += .04f;
            player.GetAttackSpeed(DamageClass.Melee) += .15f;
            player.statLifeMax2 += 75;
        }
		
		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DraconianSunChestplate>(), 1);
            recipe.AddIngredient(ModContent.ItemType<DreadMoonChestplate>(), 1);
            recipe.AddIngredient(ModContent.ItemType<DiscordiumBar>(), 10);
            recipe.AddIngredient(ModContent.ItemType<ChaosScale>(), 10);
            recipe.AddTile(ModContent.TileType<ACS_Tile>());
            recipe.Register();
        }
    }
}