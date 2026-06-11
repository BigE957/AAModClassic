using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using AAModClassic._Content.Mire.__Hardmode.Items.Materials;
using AAModClassic._Content.Mire.___PreHardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content.Mire.__Hardmode.Items.Weapons
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
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true; //so the item's animation doesn't do damage
            Item.knockBack = 3.25f;
            Item.UseSound = SoundID.Item34;
            Item.value = 1000000;
            Item.rare = ItemRarityID.LightRed;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Toxifire>(); //idk why but all the guns in the vanilla source have this
            Item.shootSpeed = 7.5f;
            Item.useAmmo = AmmoID.Gel;
            Item.consumeAmmoOnFirstShotOnly = true;
        }
		
		public override Vector2? HoldoutOffset()
        {
            return new Vector2(2, -3);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Bogtoxin>(), 5);
            recipe.AddIngredient(ModContent.ItemType<AbyssiumBar>(), 10);
            recipe.AddIngredient(ModContent.ItemType<SoulOfSpite>(), 3);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}