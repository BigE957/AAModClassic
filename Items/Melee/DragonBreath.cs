using AAModClassic.___Content.Sky.__Hardmode.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Melee   //where is located
{
    public class DragonBreath : BaseAAItem
    {
        public override void SetDefaults()
        {

            Item.useTime = 25;
            Item.CloneDefaults(ItemID.Code2);

            Item.damage = 60;
            Item.value = 100000;
            Item.rare = ItemRarityID.Green;
            Item.knockBack = 1;
            Item.channel = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 18;
            Item.useTime = 18;
            Item.shoot = ModContent.ProjectileType<Projectiles.DragonBreathP>();
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("The Dragon's Breath");
            // Tooltip.SetDefault("It must need to brush it's teeth");
        }

        public override void AddRecipes()  //How to craft this sword
        {
            Recipe recipe = CreateRecipe();      
            recipe.AddIngredient(ModContent.ItemType<DragonSpirit>(), 20);		
            recipe.AddTile(TileID.MythrilAnvil);   
            recipe.Register();

        }
    }
}
