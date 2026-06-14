using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using AAModClassic._Removed.Content.Parthenan.__Hardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.Items.Weapons
{
    public class StormJavelin : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Melee";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Storm Javelin");
            // Tooltip.SetDefault("");
        }

        public override void SetDefaults()
        {
            Item.damage = 70;           
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;             
            Item.noMelee = true;
            Item.width = 30;
            Item.height = 30;
            Item.useTime = 18;       
            Item.useAnimation = 18;   
            Item.useStyle = ItemUseStyleID.Swing;      
            Item.knockBack = 6;
            Item.value = 1;
            Item.rare = ItemRarityID.Yellow;
            Item.reuseDelay = 0;    
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;       
            Item.shoot = ModContent.ProjectileType<StormJavelin_Proj>();  
            Item.shootSpeed = 15f;     
            Item.useTurn = true;
            Item.maxStack = 999;       
            Item.consumable = true;  
            Item.noUseGraphic = true;
            Item.noMelee = true;
        }

        public override void AddRecipes()  
        {
            Recipe recipe = CreateRecipe(50);
            recipe.AddIngredient(ModContent.ItemType<FulguriteBar>());
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
