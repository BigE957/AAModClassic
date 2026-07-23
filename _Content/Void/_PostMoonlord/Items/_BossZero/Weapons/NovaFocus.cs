using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using AAModClassic.Globals;
using AAModClassic._Content.Void._PostMoonlord.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Rarities;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic.UI.World;

namespace AAModClassic._Content.Void._PostMoonlord.Items._BossZero.Weapons
{
    public class NovaFocus : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Magic";
        
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Nova Focus");
            // Tooltip.SetDefault("Fires an insanely powerful death laser");
        }

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.mana = 10;
            Item.shootSpeed = 16f;
            Item.knockBack = 0f;
            Item.width = 122;
            Item.reuseDelay = 5;
            Item.height = 32;
            Item.damage = 483;
            Item.channel = true;
            Item.rare = ItemRarityID.Pink;
            Item.useTime = 20;
            Item.UseSound = SoundID.Item13;
            Item.useAnimation = 20;
            Item.shoot = ModContent.ProjectileType<NovaFocus_DoomRay>();
            Item.value = Item.sellPrice(0, 30, 0, 0);
            Item.noMelee = true;
            Item.DamageType = DamageClass.Magic;
            Item.autoReuse = true;
            Item.rare = ModContent.RarityType<AncientsRarity>();

            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
            {
                Item.knockBack = 0.25f;
            }
        }

        

        public override Vector2? HoldoutOffset()
		{
			return new Vector2(-45, -3);
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<ApocalyptitePlate>(), 5);
			recipe.AddIngredient(ModContent.ItemType<UnstableSingularity>(), 5);
			recipe.AddIngredient(ItemID.ChargedBlasterCannon);
	        recipe.AddTile(ModContent.TileType<AnyAncientCraftingStation_Tile>());
	        recipe.Register();
		}
	}
}
