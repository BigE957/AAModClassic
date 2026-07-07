using AAModClassic._Content.Void.___PreHardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void.___PreHardmode.Items.Tools
{
    public class DoomiteJacksaw : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Tools";
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Doomite Jacksaw");
            // Tooltip.SetDefault("Engineered for ultimate tree and wall breaking action!");
		}

		public override void SetDefaults()
		{
			Item.damage = 18;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 50;
			Item.height = 18;
			Item.channel = true;
			Item.noUseGraphic = true;
			Item.noMelee = true;
			Item.useAnimation = 15;
			Item.useTime = 12;
			Item.hammer = 70;
			Item.axe = 30;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 0;
			Item.value = 15000;
			Item.rare = ItemRarityID.LightRed;
			Item.UseSound = SoundID.Item23;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<DoomiteJacksaw_Holdout>();
			Item.shootSpeed = 40f;
		}

        public override void AddRecipes()  
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DoomiteBar>(), 15);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}