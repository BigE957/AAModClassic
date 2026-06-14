using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Hallow.__Hardmode.Items.Weapons   //where is located
{
    public class IlluminantFlail : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Melee";
        public override void SetDefaults()
        {
			Item.CloneDefaults(ItemID.SolarEruption);

            Item.damage = 26;            
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;            
            Item.width = 56;              
            Item.height = 56;             

            Item.knockBack = 6;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = ItemRarityID.Lime;
            Item.autoReuse = true;   
            Item.useTurn = false;
            Item.shoot = ModContent.ProjectileType<IlluminantFlail_Holdout>();
            Item.UseSound = SoundID.Item1;
            Item.channel = true;
        }

        public override void SetStaticDefaults()
        {
          // DisplayName.SetDefault("Illuminant Flail");
          ItemID.Sets.ToolTipDamageMultiplier[Type] = 2f;
        }

        public override void AddRecipes()  //How to craft this sword
        {
            Recipe recipe = CreateRecipe();      
            recipe.AddIngredient(ItemID.CrystalShard, 20);   
			recipe.AddIngredient(ItemID.BlueMoon, 1);
			recipe.AddIngredient(ItemID.SoulofLight, 10);
            recipe.AddTile(TileID.MythrilAnvil);   
            recipe.Register();

        }
    }
}
