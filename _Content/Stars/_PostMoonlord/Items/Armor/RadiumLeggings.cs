using AAModClassic._Content.Stars._PostMoonlord.Items.Materials;
using AAModClassic._Content.Stars._PostMoonlord.Items.Tiles.Functional;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using AAModClassic.Utilities.Attributes;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Stars._PostMoonlord.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    [AutoloadEquipGlow(EquipType.Legs)]
    public class RadiumLeggings : EquipAbstract, ILocalizedModType, ICustomEquipGlow
	{
        public new string LocalizationCategory => "Items.Armor.Radium";
        public Color Color => AAColor.Glow;

        public bool Condition(Player p) => Main.dayTime && p.GetModPlayer<ZAAPlayer>().Radium;

        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Radium Cuisses");
			/* Tooltip.SetDefault(@"'Shines with the light of a starry night sky'"); */
		}

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 18;
			Item.value = 300000;
			Item.defense = 20;
            Item.rare = ModContent.RarityType<PostEquinoxRarity>();
        }

        public override void RegisterEquipEffects()
        {
            AddEffect(new MovementSpeedEffect(0.30f));
            AddEffect(new MaxRunSpeedEffect(0.30f));
            AddEffect(new EmitLightFromPlayerEffect(1.0f, 1.0f, 1.0f));
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<RadiumBar>(), 27);
            recipe.AddIngredient(ModContent.ItemType<RadiantPhoton>(), 15);
            recipe.AddTile(ModContent.TileType<QuantumFusionAccelerator_Tile>());
            recipe.Register();
        }
	}
}