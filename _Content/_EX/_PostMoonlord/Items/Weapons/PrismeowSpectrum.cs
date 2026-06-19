using AAModClassic._Content._Dev._PostMoonlord.Items.Weapons;
using AAModClassic._Content._EX._PostMoonlord.Items.Materials;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._EX._PostMoonlord.Items.Weapons
{
    public class PrismeowSpectrum : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Weapons.Magic";
        
        public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Prismeow Spectrum");
            /* Tooltip.SetDefault(@"Summons a Legendary Rainbow Cat at cursor point
Shoots Rainbow Bolts that move in the direction of your cursor
Warning: Using this WILL lag your game!
Prismeow EX"); */
            Item.staff[Item.type] = true;
        }

		public override void SetDefaults()
		{
            
			Item.damage = 50;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 200;
			Item.width = 52;
            Item.height = 52;
			Item.useTime = 60;
			Item.useAnimation = 60;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 3;
			Item.value = Item.sellPrice(0, 30, 0, 0);
			Item.rare = ItemRarityID.Purple;
			Item.UseSound = SoundID.Item44;
			Item.autoReuse = false;
			Item.shoot = ModContent.ProjectileType<PrismeowSpectrum_LegendaryRainbowCat>();
			Item.shootSpeed = 0f;
            Item.expert = true; Item.expertOnly = true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            position = Main.MouseWorld;
            return true;
        }
        

        public override void AddRecipes()
        {
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(ModContent.ItemType<Prismeow>());
                recipe.AddIngredient(ModContent.ItemType<EXSoul>());
                recipe.AddTile(ModContent.TileType<AnyAncientCraftingStation_Tile>());
                recipe.Register();
            }
        }
    }
}