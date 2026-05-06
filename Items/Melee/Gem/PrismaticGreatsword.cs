using AAModClassic.Projectiles.GemShot;
using Microsoft.Xna.Framework;
using Terraria;
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
            Item.UseSound = SoundID.Item8;
            Item.autoReuse = true;   
            Item.useTurn = true; 
			Item.shoot = ModContent.ProjectileType<PrismBolt>();
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
			recipe.AddIngredient(ModContent.ItemType<Poppy>(), 1);
            recipe.AddIngredient(ModContent.ItemType<AmethystGreatsword>(), 1);
            recipe.AddIngredient(ModContent.ItemType<TopazGreatsword>(), 1);
            recipe.AddIngredient(ModContent.ItemType<SapphireGreatsword>(), 1);
            recipe.AddIngredient(ModContent.ItemType<EmeraldGreatsword>(), 1);
            recipe.AddIngredient(ModContent.ItemType<RubyGreatsword>(), 1);
            recipe.AddIngredient(ModContent.ItemType<AmberGreatsword>(), 1);
            recipe.AddIngredient(ModContent.ItemType<DiamondGreatsword>(), 1);
            recipe.AddIngredient(ItemID.BeamSword);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();

        }
    }
}
