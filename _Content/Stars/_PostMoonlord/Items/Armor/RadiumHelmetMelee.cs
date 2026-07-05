using AAModClassic._Content.Stars._PostMoonlord.Items.Materials;
using AAModClassic._Content.Stars._PostMoonlord.Items.Tiles.Functional;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using AAModClassic.Utilities.Attributes;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Stars._PostMoonlord.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    [AutoloadEquipGlow(EquipType.Head)]
    public class RadiumHelmetMelee : EquipAbstract, ILocalizedModType, ICustomEquipGlow
    {
        public new string LocalizationCategory => "Items.Armor.Radium";
        public Color Color => AAColor.Glow;

        public bool Condition(Player p) => Main.dayTime && p.GetModPlayer<AAPlayer>().Radium;

        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Radium Helmet");
			/* Tooltip.SetDefault(@"'Shines with the light of a starry night sky'"); */
        }

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 20;
			Item.value = 300000;
			Item.defense = 30;
            Item.rare = ModContent.RarityType<PostEquinoxRarity>();
        }

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
            return body.type == ModContent.ItemType<RadiumChestplate>() && legs.type == ModContent.ItemType<RadiumLeggings>();
        }

        public override void RegisterEquipStats()
        {
            damageMap.GetDamage(DamageClass.Melee) += 0.15f;

            AddSetEffect<RadiumHelmetMeleeSetEffect>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<RadiumBar>(), 25);
            recipe.AddIngredient(ModContent.ItemType<RadiantPhoton>(), 10);
            recipe.AddTile(ModContent.TileType<QuantumFusionAccelerator_Tile>());
            recipe.Register();
        }
	}
    
}