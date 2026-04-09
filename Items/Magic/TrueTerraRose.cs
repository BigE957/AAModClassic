using AAModClassic.Items.Boss;
using AAModClassic.Tiles.Crafters;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Magic
{
    public class TrueTerraRose : BaseAAItem
	{
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("True Terra Rose");
            /* Tooltip.SetDefault(@"Some say this staff was used by the legendary hero themselves
Projectiles explode on hit
Projectiles go through walls
Right Clicking fires a piercing rose
Terra Rose EX"); */
			Item.staff[Item.type] = true; //this makes the useStyle animate as a staff instead of as a gun
		}

		public override void SetDefaults()
		{
			Item.damage = 500;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 15;
			Item.width = 68;
			Item.height = 60;
			Item.useTime = 10;
			Item.useAnimation = 10;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 6;
			Item.value = 500000;
			Item.rare = ItemRarityID.Purple;
			Item.UseSound = SoundID.Item20;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<Projectiles.TerraRoseShotEX>();
			Item.shootSpeed = 20f;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.shoot = ModContent.ProjectileType<Projectiles.TrueTerraRose>();
                Item.damage = 70;
                Item.useTime = 30;
                Item.useAnimation = 30;
                Item.knockBack = 2;
            }
            else
            {
                Item.shoot = ModContent.ProjectileType<Projectiles.TerraRoseShotEX>();
                Item.damage = 500;
                Item.useTime = 10;
                Item.useAnimation = 10;
                Item.knockBack = 6;
            }
            return base.CanUseItem(player);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();      
            recipe.AddIngredient(ModContent.ItemType<TerraRose>());
			recipe.AddIngredient(ModContent.ItemType<EXSoul>());
            recipe.AddTile(ModContent.TileType<QuantumFusionAccelerator_Tile>());
            recipe.Register();
        }
    }
}