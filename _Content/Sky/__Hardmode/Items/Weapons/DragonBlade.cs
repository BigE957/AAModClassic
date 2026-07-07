using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria;
using AAModClassic._Content.Sky.__Hardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content.Sky.__Hardmode.Items.Weapons   //where is located
{
    public class DragonBlade : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Melee";
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dragon Blade");
            // Tooltip.SetDefault("Shoots tiny swords!");
        }
        public override void SetDefaults()
        {

            Item.damage = 54;            
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;            
            Item.width = 60;              
            Item.height = 60;             
            
            Item.useTime = 25;          
            Item.useAnimation = 25;     
            Item.useStyle = ItemUseStyleID.Swing;        
            Item.knockBack = 4;
            Item.value = Item.sellPrice(0, 2, 0, 0);
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = SoundID.Item1;       
            Item.autoReuse = true;   
            Item.useTurn = false;
			Item.shoot = ModContent.ProjectileType<DragonBlade_TinySword>();
			Item.shootSpeed = 14f;
        }

        public override void AddRecipes()  //How to craft this sword
        {
            Recipe recipe = CreateRecipe();      
            recipe.AddIngredient(ModContent.ItemType<DragonSpirit>(), 30);   
            recipe.AddTile(TileID.MythrilAnvil);   
            recipe.Register();

        }
    }
}
