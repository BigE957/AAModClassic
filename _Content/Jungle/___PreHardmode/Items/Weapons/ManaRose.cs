using AAModClassic._Content.Jungle.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Jungle.___PreHardmode.Items.Weapons
{
    public class ManaRose : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Mana Rose");
            /* Tooltip.SetDefault(@"Long and Magical
Right Clicking fires a piercing rose"); */
            Item.staff[Item.type] = true; //this makes the useStyle animate as a staff instead of as a gun
        }

        public override void SetDefaults()
        {
            Item.damage = 45;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 6;
            Item.width = 68;
            Item.height = 60;
            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true; //so the item's animation doesn't do damage
            Item.knockBack = 5;
            Item.value = 100000;
            Item.rare = ItemRarityID.LightRed;
            Item.UseSound = SoundID.Item20;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<ManaPetal>();
            Item.shootSpeed = 7f;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.shoot = ModContent.ProjectileType<ManaRose_Proj>();
                Item.damage = 20;
                Item.useTime = 40;
                Item.useAnimation = 40;
                Item.knockBack = 1;
            }
            else
            {
                Item.shoot = ModContent.ProjectileType<ManaPetal>();
                Item.damage = 45;
                Item.useTime = 18;
                Item.useAnimation = 18;
                Item.knockBack = 5;
            }
            return base.CanUseItem(player);
        }


        public override void AddRecipes()
        {
            Recipe recipe;
            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Vilethorn, 1);
            recipe.AddIngredient(ModContent.ItemType<MagicFlower>(), 1);
            recipe.AddIngredient(ItemID.MagicMissile, 1);
            recipe.AddIngredient(ItemID.FlowerofFire, 1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.CrimsonRod, 1);
            recipe.AddIngredient(ModContent.ItemType<MagicFlower>(), 1);
            recipe.AddIngredient(ItemID.MagicMissile, 1);
            recipe.AddIngredient(ItemID.FlowerofFire, 1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}