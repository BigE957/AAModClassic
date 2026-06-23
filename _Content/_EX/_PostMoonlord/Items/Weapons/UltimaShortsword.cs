using AAModClassic._Content._EX._PostMoonlord.Items.Materials;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._Content.Stars._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._Content.Underground.__Hardmode.Items.Weapons;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._EX._PostMoonlord.Items.Weapons
{
    public class UltimaShortsword : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Melee";
        
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Ultima Shortsword");
			// Tooltip.SetDefault("Copper Shortsword EX");
        }
		public override void SetDefaults()
		{
            
			Item.damage = 1000;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 64;
			Item.height = 64;
			Item.useTime = 20;
			Item.useAnimation = 10;
			Item.useStyle = ItemUseStyleID.Thrust;
			Item.knockBack =20;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.rare = ItemRarityID.Cyan;
			Item.expert = true;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<UltimaShortsword_UltimaShot>();
            Item.shootSpeed = 20f;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int proj = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, Main.myPlayer);
            Main.projectile[proj].usesLocalNPCImmunity = true;
            Main.projectile[proj].localNPCHitCooldown = 6;
            return false;
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<TrueCopperShortsword>(), 1);
			recipe.AddIngredient(ModContent.ItemType<EXSoul>(), 1);
            recipe.AddTile(ModContent.TileType<AnyAncientCraftingStation_Tile>());
            recipe.Register();
		}
	}
}
