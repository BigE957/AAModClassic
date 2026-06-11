using AAModClassic._Content.Inferno.___PreHardmode.Items.Armor;
using AAModClassic._Content.Inferno._PostMoonlord.Items.Materials;
using AAModClassic.Globals;
using AAModClassic.Tiles.Crafters;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.Attributes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno._PostMoonlord.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    [AutoloadEquipGlow(EquipType.Legs)]
    public class DraconianSunLeggings : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Draconian Sun Greaves");
			/* Tooltip.SetDefault(@"16% increased movement speed
15% increased melee speed
3% increased damage resistance
+25 Max Life
The blazing fury of the Inferno rests in this armor"); */

		}

        public override void SetDefaults()
		{
			Item.width = 26;
			Item.height = 18;
			Item.value = 3000000;
			Item.defense = 32;
            Item.rare = ItemRarityID.Cyan;
            AARarity = 13;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Rarity13;
                }
            }
        }

        public override void UpdateEquip(Player player)
		{
			player.moveSpeed += 0.16f;
			player.GetAttackSpeed(DamageClass.Melee) += 0.15f;
			player.endurance += .03f;
            player.GetModPlayer<AAPlayer>().MaxMovespeedboost += .16f;
            player.statLifeMax2 += 25;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DaybreakIncineriteBar>(), 18);
            recipe.AddIngredient(ModContent.ItemType<CrucibleScale>(), 5);
            recipe.AddIngredient(ModContent.ItemType<KindledLeggings>(), 1);
            recipe.AddTile(ModContent.TileType<ACS_Tile>());
            recipe.Register();
        }
    }
}