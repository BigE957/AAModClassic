using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using AAModClassic._Removed.Content.Parthenan.__Hardmode.Items.Materials;

namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.Items.Weapons
{
    public class FulguriteTazerblaster : BaseAAItem
	{
        public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Fulgurite Tazerblaster");
            /* Tooltip.SetDefault(@"Rapidly fires taserblasts
Slim chance to fire 2 taserblasts at once"); */
            
        }

		public override void SetDefaults()
		{
			Item.damage = 45;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 52;
			Item.height = 18;
            Item.useAnimation = 12;
            Item.useTime = 12;
            Item.reuseDelay = 2;
            Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 2.5f;
            Item.value = Item.sellPrice(0, 5, 0, 0);
			Item.rare = ItemRarityID.Pink;
			Item.UseSound = SoundID.Item12;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<FulguriteTazerblaster_Taserblast>();
            Item.shootSpeed = 17f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<FulguriteBar>(), 20);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0, 0);
        }
    }
}