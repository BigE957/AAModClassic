using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Armor;
using AAModClassic._Content.Inferno._PostMoonlord.Items.Materials;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
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
    public class DraconianSunLeggings : EquipAbstract, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Armor.DraconianSun";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Draconian Sun Greaves");
			/* Tooltip.SetDefault(@"'The blazing fury of the Inferno rests in this armor'"); */

		}

        public override void SetDefaults()
		{
			Item.width = 26;
			Item.height = 18;
			Item.value = 3000000;
			Item.defense = 32;
            Item.rare = ModContent.RarityType<AncientsRarity>();
        }

        public override void RegisterEquipEffects()
        {
            damageMap.GetAttackSpeed(DamageClass.Melee) += 0.15f;
            AddEffect(new MovementSpeedEffect(0.16f));
            AddEffect(new MaxRunSpeedEffect(0.16f));
            AddEffect(new EnduranceEffect(0.03f));
            AddEffect(new MaxLifeEffect(25));
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DaybreakIncineriteBar>(), 18);
            recipe.AddIngredient(ModContent.ItemType<CrucibleScale>(), 5);
            recipe.AddIngredient(ModContent.ItemType<KindledLeggings>(), 1);
            recipe.AddTile(ModContent.TileType<AnyAncientCraftingStation_Tile>());
            recipe.Register();
        }
    }
}