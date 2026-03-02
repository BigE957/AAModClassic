using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Greed
{
    public class GildedGlock : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Gilded Glock");
            // Tooltip.SetDefault("Uses Coins as Ammo");
        }
        public override void SetDefaults()
        {
            Item.width = 44;
            Item.height = 30;
            Item.rare = ItemRarityID.Yellow;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 28;
            Item.useTime = 28;
            Item.UseSound = SoundID.Item41;
            Item.damage = 70;
            Item.knockBack = 7;
            Item.DamageType = DamageClass.Ranged;
            Item.autoReuse = false;
            Item.noMelee = true;
            Item.shoot = ProjectileID.CopperCoin;
            Item.shootSpeed = 12;
            Item.useAmmo = AmmoID.Coin;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-2, 0);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.FlintlockPistol, 1);
            recipe.AddIngredient(null, "StoneShell", 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}