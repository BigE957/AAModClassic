using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace AAMod.Items.Ranged
{
    public class Toxithrower : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Toxithrower");
			// Tooltip.SetDefault("Uses gel for ammo");
		}

        public override void SetDefaults()
        {
            Item.damage = 35;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 68;
            Item.height = 22;
            Item.useTime = 3;
            Item.useAnimation = 15;
            Item.useStyle = 5;
            Item.noMelee = true; //so the item's animation doesn't do damage
            Item.knockBack = 3.25f;
            Item.UseSound = SoundID.Item34;
            Item.value = 1000000;
            Item.rare = 4;
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("Toxifire").Type; //idk why but all the guns in the vanilla source have this
            Item.shootSpeed = 7.5f;
            Item.useAmmo = 23;
        }
		
		public override Vector2? HoldoutOffset()
        {
            return new Vector2(2, -3);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "HydraToxin", 5);
            recipe.AddIngredient(null, "AbyssiumBar", 10);
            recipe.AddIngredient(null, "SoulOfSpite", 3);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}