using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using AAModClassic._Content.Mire.__Hardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content.Mire.__Hardmode.Items.Weapons
{
    public class GuardianOfTheDepths : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Melee";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Guardian of the Depths");
            // Tooltip.SetDefault("");
        }
        public override void SetDefaults()
        {

            Item.damage = 174;            
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;            
            Item.width = 78;              
            Item.height = 78;             
            Item.useTime = 26;          
            Item.useAnimation = 26;     
            Item.useStyle = ItemUseStyleID.Swing;        
            Item.knockBack = 4;      
            Item.value = 20;        
            Item.rare = ItemRarityID.Lime;
            Item.UseSound = SoundID.Item1;       
            Item.autoReuse = true;   
            Item.useTurn = true;
        }

        public override void AddRecipes()  //How to craft this sword
        {
            Recipe recipe = CreateRecipe();      
			recipe.AddIngredient(ModContent.ItemType<DeepAbyssiumBar>(), 10);
			recipe.AddIngredient(ItemID.Ectoplasm, 15);
            recipe.AddTile(TileID.MythrilAnvil);   
            recipe.Register();

        }
    }
}
