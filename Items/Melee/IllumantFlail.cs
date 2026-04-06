using AAModClassic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Melee   //where is located
{
    public class IllumantFlail : BaseAAItem
    {
        public override void SetDefaults()
        {
			Item.CloneDefaults(ItemID.SolarEruption);

            Item.damage = 52;            
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;            
            Item.width = 56;              
            Item.height = 56;             

            Item.knockBack = 6;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = ItemRarityID.Lime;
            Item.autoReuse = true;   
            Item.useTurn = false;
            Item.shoot = ModContent.ProjectileType<Projectiles.IllumantBall>();
            Item.UseSound = SoundID.Item1;
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Illuminant Flail");
    }

        public override void AddRecipes()  //How to craft this sword
        {
            Recipe recipe = CreateRecipe();      
            recipe.AddIngredient(ItemID.CrystalShard, 20);   
			recipe.AddIngredient(ItemID.BlueMoon, 1);
			recipe.AddIngredient(ItemID.SoulofLight, 10);
            recipe.AddTile(TileID.MythrilAnvil);   
            recipe.Register();

        }
    }
}
