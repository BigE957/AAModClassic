using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Throwing
{
    public class HellBall : BaseAAItem
    {

        public override void SetDefaults()
        {
			Item.useTime = 25;
            Item.CloneDefaults(ItemID.LightDisc);
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.maxStack = 5;
            Item.damage = 42;                            
            Item.value = 6;
            Item.rare = ItemRarityID.Pink;
            Item.knockBack = 5;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 24;
            Item.useTime = 24;
            Item.shoot = ModContent.ProjectileType<HellBallP>();
			Item.width = 56;
            Item.height = 56;
            Item.noMelee = true;
        }

        public override void SetStaticDefaults()
        {
          // DisplayName.SetDefault("Scorched Saw");
          // Tooltip.SetDefault("");
        }

        public override bool CanUseItem(Player player)       //this make that you can shoot only 1 boomerang at once
        {
            int num = 0;
            for (int i = 0; i < 200; i++)
            {
                if (Main.projectile[i].active && Main.projectile[i].type == ModContent.ProjectileType<HellBallP>())
                {
                    num++;
                }
            }
            if (num > Item.stack)
            {
                return false;
            }
            return true;
        }

        public override void AddRecipes()
        {                                                   
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.HellstoneBar, 20);              //exeample of how to craft with a modded item
			recipe.AddIngredient(ItemID.SoulofNight, 5);
			recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
