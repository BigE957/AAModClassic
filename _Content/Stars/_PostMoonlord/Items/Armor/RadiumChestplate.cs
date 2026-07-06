using AAModClassic._Content.Stars._PostMoonlord.Items.Materials;
using AAModClassic._Content.Stars._PostMoonlord.Items.Tiles.Functional;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using AAModClassic.Utilities.Attributes;
using Microsoft.Xna.Framework;
using System.Linq.Expressions;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Stars._PostMoonlord.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    [AutoloadEquipGlow(EquipType.Body)]
    public class RadiumChestplate : EquipAbstract, ILocalizedModType, ICustomEquipGlow
    {
        public new string LocalizationCategory => "Items.Armor.Radium";
        public Color Color => AAColor.Glow;

        public bool Condition(Player p) => Main.dayTime && p.GetModPlayer<AAPlayer>().Radium;

        public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			// DisplayName.SetDefault("Radium Platemail");
			// Tooltip.SetDefault("'Shines with the light of a starry night sky'");
		}

		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 20;
			Item.value = 300000;
			Item.defense = 28;
            Item.rare = ModContent.RarityType<PostEquinoxRarity>();
		}

        public override void RegisterEquipEffects()
        {
            damageMap.GetDamage(DamageClass.Generic) += .25f;
			AddEffect(new EmitLightFromPlayerEffect(1.0f, 1.0f, 1.0f));
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<RadiumBar>(), 30);
            recipe.AddIngredient(ModContent.ItemType<RadiantPhoton>(), 20);
            recipe.AddTile(ModContent.TileType<QuantumFusionAccelerator_Tile>());
			recipe.Register();
		}
	}
}