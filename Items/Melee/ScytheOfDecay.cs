using AAModClassic;
using AAModClassic.Items.Boss;
using AAModClassic.Tiles.Crafters;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Melee     //We need player to basically indicate the folder where it is to be read from, so you the texture will load correctly
{
    public class ScytheOfDecay : BaseAAItem
    {

        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Scythe of Evil");
            /* Tooltip.SetDefault(@"The scythe of the lord of death himself
Inflicts Ichor and Cursed Inferno
Death Sickle EX"); */
        }

        public override void SetDefaults()
        {
            Item.damage = 1250;  
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */; 
            Item.width = 80;    
            Item.height = 72; 

            Item.useTime = 6; 
            Item.useAnimation = 6;
            Item.channel = true;
            Item.useStyle = 100;  
            Item.knockBack = 2f; 
            Item.value = Item.sellPrice(0, 30, 0, 0); 
            Item.rare = ItemRarityID.Cyan;
            Item.expert = true; Item.expertOnly = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.DecayScythe>(); 
            Item.noUseGraphic = true;
            Item.noMelee = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.DeathSickle);
            recipe.AddIngredient(ModContent.ItemType<EXSoul>());
            recipe.AddTile(ModContent.TileType<QuantumFusionAccelerator_Tile>());
            recipe.Register();
        }
 
        public override void UseItemFrame(Player player)  
        {
            player.bodyFrame.Y = 3 * player.bodyFrame.Height;
        }
    }
}
