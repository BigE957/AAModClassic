using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria;
using AAModClassic;

namespace AAModClassic.Items.Melee   //where is located
{
    public class Dragonkite : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dragonkite");
            // Tooltip.SetDefault("");
        }
        public override void SetDefaults()
        {
            Item.damage = 150;            
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;            
            Item.width = 176;              
            Item.height = 176;             
            Item.useTime = 45;          
            Item.useAnimation = 45;     
            Item.useStyle = ItemUseStyleID.Swing;        
            Item.knockBack = 4;      
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.rare = ItemRarityID.Cyan;
            Item.UseSound = SoundID.Item20;       
            Item.autoReuse = true;   
            Item.useTurn = true;

            glowmaskTexture = "Glowmasks/" + GetType().Name + "_Glow"; //the glowmask texture path.
            glowmaskDrawType = GLOWMASKTYPE_SWORD; //what type it is when drawn in the hand, _NONE == no draw, _SWORD == like a sword, _GUN == like a gun	
            glowmaskDrawColor = Color.White;  //glowmask draw color
        }

        public override void AddRecipes()  //How to craft this sword
        {
            Recipe recipe = CreateRecipe();      
            recipe.AddIngredient(null, "RadiantIncinerite", 10);
            recipe.AddIngredient(ItemID.Ectoplasm, 15); 
            recipe.AddTile(TileID.MythrilAnvil);   
            recipe.Register();
        }
    }
}
