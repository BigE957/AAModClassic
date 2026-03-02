using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Melee   //where is located
{
    public class JungleReaper : BaseAAItem
    {
        public override void SetDefaults()
        {

            Item.damage = 13;            
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;            
            Item.width = 78;              
            Item.height = 60;             
            Item.useTime = 30;          
            Item.useAnimation = 30;     
            Item.useStyle = ItemUseStyleID.Swing;        
            Item.knockBack = 3;      
            Item.value = Terraria.Item.sellPrice(0, 0, 50, 0);
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item1;       
            Item.autoReuse = false;   
            Item.useTurn = false;
            Item.shoot = Mod.Find<ModProjectile>("JungleReaperP").Type;
            Item.shootSpeed = 8f;                                 
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Jungle Reaper");
      // Tooltip.SetDefault("It's a scythe. Calm down Welox.");
    }

        public override void AddRecipes()  //How to craft this sword
        {
            Recipe recipe = CreateRecipe();      
            recipe.AddRecipeGroup("AAMod:Gold", 15);
            recipe.AddTile(TileID.LivingLoom);   
            recipe.Register();

        }
    }
}
