using AAModClassic.___Content.Mire._PreHardmode.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Mire._PreHardmode.Items.Weapons
{
    public class HydraGlove : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Hydra Glove");
		}

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 7;
            Item.useTime = 7;
            Item.width = 28;
            Item.height = 24;
            Item.damage = 19;
            Item.knockBack = 4f;
            Item.UseSound = SoundID.Item1;
            Item.scale = 1.35f;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.rare = ItemRarityID.Orange;
            Item.value = 50000;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<AbyssiumBar>(), 10);
            recipe.AddIngredient(ModContent.ItemType<HydraClaw>(), 5);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
	}
}
