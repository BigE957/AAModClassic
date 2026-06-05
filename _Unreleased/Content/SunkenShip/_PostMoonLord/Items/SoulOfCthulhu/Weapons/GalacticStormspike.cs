using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Tiles.Crafters;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.Items.SoulOfCthulhu.Weapons
{
	public class GalacticStormspike : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            Item.staff[Item.type] = true;
            // DisplayName.SetDefault("Galactic Stormspike");
            //Tooltip.SetDefault("Shoots a branching ray of dark electricity");
        }
        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 25;
            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.sellPrice(0, 35, 55, 20);

            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.UseSound = SoundID.Item15;
            Item.damage = 190;
            Item.knockBack = 4;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 10;
            Item.autoReuse = true;
            Item.noMelee = true;	
            Item.shoot = ModContent.ProjectileType<GalacticStormspike_Stormray>();
            Item.shootSpeed = 4;
            AARarity = 14;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			int pID = Projectile.NewProjectile(Item.GetSource_FromThis(), position.X, position.Y, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI);
			return false;
		}

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<RealityBar>(), 5);
            recipe.AddIngredient(ItemID.ShadowbeamStaff, 1);
            recipe.AddTile(ModContent.TileType<ACS_Tile>());
            recipe.Register();
        }
    }
}