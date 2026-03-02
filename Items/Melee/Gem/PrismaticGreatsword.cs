using AAModClassic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Melee.Gem   //where is located
{
    public class PrismaticGreatsword : BaseAAItem
    {
        public override void SetDefaults()
        {

            Item.damage = 48;            
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;            
            Item.width = 58;              
            Item.height = 60;             
            Item.useTime = 20;          
            Item.useAnimation = 20;     
            Item.useStyle = ItemUseStyleID.Swing;        
            Item.knockBack = 5;
            Item.value = 20000;        
            Item.rare = ItemRarityID.LightRed;
            Item.UseSound = new LegacySoundStyle(2, 8, Terraria.Audio.SoundType.Sound);
            Item.autoReuse = true;   
            Item.useTurn = true; 
			Item.shoot = Mod.Find<ModProjectile>("PrismBolt").Type;
			Item.shootSpeed = 13f;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Prismatic Greatsword");
            // Tooltip.SetDefault("");
        }

        static int shoot = 0;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            shoot++;
            if (shoot % 2 != 0) return false;

            shoot = 0;
            return true;
        }

        public override void AddRecipes()  //How to craft this sword
        {
            Recipe recipe = CreateRecipe();      
			recipe.AddIngredient(null, "Poppy", 1);
            recipe.AddIngredient(null, "AmethystGreatsword", 1);
            recipe.AddIngredient(null, "EmeraldGreatsword", 1);
            recipe.AddIngredient(null, "RubyGreatsword", 1);
            recipe.AddIngredient(null, "SapphireGreatsword", 1);
            recipe.AddIngredient(null, "TopazGreatsword", 1);
            recipe.AddIngredient(null, "AmberGreatsword", 1);
            recipe.AddIngredient(null, "DiamondGreatsword", 1);
            recipe.AddIngredient(ItemID.BeamSword);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();

        }
    }
}
