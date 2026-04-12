using AAModClassic.___Content.Inferno._PreHardmode.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Magic
{
    public class DragonsBreath : BaseAAItem
    {
        public override void SetDefaults()
        {

            Item.damage = 30;                        
            Item.DamageType = DamageClass.Magic;                     
            Item.width = 24;
            Item.height = 28;
            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.useStyle = ItemUseStyleID.Shoot;        
            Item.noMelee = true;
            Item.knockBack = 4;
            Item.value = Item.sellPrice(0, 0, 20, 0);
            Item.rare = ItemRarityID.Blue;
            Item.mana = 5;             
            Item.UseSound = SoundID.Item21;            
            Item.autoReuse = true;
            Item.shoot = ProjectileID.DD2FlameBurstTowerT1Shot;  
            Item.shootSpeed = 11f;     
        }   

        public override void SetStaticDefaults()
        {
          // DisplayName.SetDefault("Dragon's Breath");
          // Tooltip.SetDefault("");
        }

		public override void AddRecipes()  
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Book, 1);
            recipe.AddIngredient(ModContent.ItemType<IncineriteBar>(), 10);
            recipe.AddTile(TileID.Bookcases);   
            recipe.Register();
        }
    }
}
