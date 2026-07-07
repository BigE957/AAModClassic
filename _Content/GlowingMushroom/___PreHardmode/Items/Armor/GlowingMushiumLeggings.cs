using AAModClassic._Content.GlowingMushroom.___PreHardmode.Items.Materials;
using AAModClassic.Globals;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using AAModClassic.Utilities.Attributes;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic._Content.GlowingMushroom.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    [AutoloadEquipGlow(EquipType.Legs)]
    public class GlowingMushiumLeggings : EquipAbstract, ILocalizedModType, ICustomEquipGlow
    {
        public new string LocalizationCategory => "Items.Armor.GlowingMushium";
        public Color Color => AAColor.Glow;

        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Glowing Mushium Pants");

        }

		public override void SetDefaults()
		{
            Item.width = 22;
			Item.height = 18;
			Item.value = 50;
			Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(0, 0, 25, 0);
            Item.defense = 2;
		}

        public override void RegisterEquipEffects()
        {
            AddEffect(new ManaRegenEffect(2));
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<GlowingMushiumBar>(), 5);
            recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}