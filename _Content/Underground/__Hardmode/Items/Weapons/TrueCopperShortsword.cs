using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content.Underground.__Hardmode.Items.Weapons
{
    public class TrueCopperShortsword : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Melee";
        
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("True Copper Shortsword");
			// Tooltip.SetDefault("Literally just did it for the memes");
        }
		public override void SetDefaults()
		{
            
			Item.damage = 300;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 36;
			Item.height = 36;
			Item.useTime = 40;
			Item.useAnimation = 40;
			Item.useStyle = ItemUseStyleID.Thrust;
			Item.knockBack =20;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.rare = ItemRarityID.Cyan;
			Item.expert = true;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = false;
            Item.shoot = ModContent.ProjectileType<TrueCopperShortsword_Proj>();
            Item.shootSpeed = 20f;
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.CopperShortsword, 1);
			recipe.AddIngredient(ItemID.BrokenHeroSword, 1);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}
