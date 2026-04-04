using AAModClassic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Melee   //where is located
{
    public class IceLongsword : BaseAAItem
    {
        public override void SetDefaults()
        {

            Item.damage = 26;          
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;            
            Item.width = 62;             
            Item.height = 64;             
            Item.useTime = 23;         
            Item.useAnimation = 23;     
            Item.useStyle = ItemUseStyleID.Swing;        
            Item.knockBack = 2;     
            Item.value = 8000;        
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item1;      
            Item.autoReuse = true;   
            Item.useTurn = false;
            Item.shoot = ModContent.ProjectileType<IceChunk>();
            Item.shootSpeed = 14f;                        
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Ice Longsword");
      // Tooltip.SetDefault("Chuck literal ice at your foes instead of that wimpy little snow bolt");
    }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();      
            recipe.AddIngredient(ItemID.IceBlade, 1);  
			recipe.AddIngredient(ItemID.IceBlock, 50);
			recipe.AddIngredient(ItemID.SnowBlock, 100);
            recipe.AddIngredient(null, "SnowMana", 3);
            recipe.AddTile(TileID.Anvils); 
            recipe.Register();

        }
    }
}
