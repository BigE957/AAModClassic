using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Sky.___PreHardmode.Items.Weapons   //where is located
{
    public class CloudEdge : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Melee";
        public override void SetDefaults()
        {

            Item.damage = 20;            
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;            
            Item.width = 32;              
            Item.height = 32;             
            Item.useTime = 45;          
            Item.useAnimation = 45;     
            Item.useStyle = ItemUseStyleID.Swing;        
            Item.knockBack = 1;      
            Item.value = 5000;        
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item1;       
            Item.autoReuse = true;   
            Item.useTurn = true;
            Item.shoot = ModContent.ProjectileType<CloudEdge_Cloud>();
            Item.shootSpeed = 12f;                                 
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Cloud Edge");
      // Tooltip.SetDefault("Shoots cloud projectiles");
    }

        public override void AddRecipes()  //How to craft this sword
        {
            Recipe recipe = CreateRecipe();      
            recipe.AddIngredient(ItemID.FallenStar, 5);   
			recipe.AddIngredient(ItemID.Cloud, 200);
            recipe.AddTile(TileID.WorkBenches);   
            recipe.Register();

        }
    }
}
