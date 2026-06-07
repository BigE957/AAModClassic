using AAModClassic._Content.Mire.___PreHardmode.Items.Tools;
using AAModClassic._Content.Terra.__Hardmode.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire.__Hardmode.Items.Tools
{
    public class PerfectShadowDrill : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Perfect Shadow Drill");
            // Tooltip.SetDefault("Now that's more like it.");
		}

		public override void SetDefaults()
		{
			Item.damage = 50;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 50;
			Item.height = 18;
			Item.useTime = 6;
			Item.useAnimation = 15;
			Item.channel = true;
			Item.noUseGraphic = true;
			Item.noMelee = true;
			Item.pick = 205;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 0;
            Item.value = Item.sellPrice(0, 10);
            Item.rare = ItemRarityID.Lime;
			Item.UseSound = SoundID.Item23;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<PerfectShadowDrill_Holdout>();
			Item.shootSpeed = 40f;
		}

        public override void AddRecipes()  
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ShadowDrill>());
            recipe.AddIngredient(ItemID.SoulofSight, 20);
            recipe.AddIngredient(ItemID.SoulofMight, 20);
            recipe.AddIngredient(ItemID.SoulofFright, 20);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}