using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.PumpkinMoon.__Hardmode.Items.Weapons
{
    public class SpookySawblade : BaseAAItem
    {

        public override void SetDefaults()
        {
			Item.useTime = 25;
            Item.CloneDefaults(ItemID.PossessedHatchet);
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.damage = 140;                            
            Item.value = 20;
            Item.rare = ItemRarityID.Orange;
            Item.knockBack = 2;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 19;
            Item.useTime = 19;
            Item.shoot = ModContent.ProjectileType<SpookySawblade_Proj>();
			Item.width = 54;
            Item.height = 54;
            Item.noMelee = true;
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Spooky Sawblade");
      // Tooltip.SetDefault("A posessed chakram than homes in on enemies because it's possessed by a spooky ghost");
    }


        public override void AddRecipes()
        {                                                   
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.ChlorophyteBar, 10);              //exeample of how to craft with a modded item
			recipe.AddIngredient(ItemID.SpookyWood, 50);
			recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
