using Microsoft.Xna.Framework;
using Terraria;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic.Globals;
using Terraria.Audio;
using AAModClassic.Tiles.Crafters;
using AAModClassic._Content.Void._PostMoonlord.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content.Void._PostMoonlord.Items._BossZero.Weapons
{
    public class BrokenZeroWeapon : BaseAAItem
    {
        
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Broken Zero Weapon");
            // Tooltip.SetDefault("Violently attempting to zap you");
        }

        public override void SetDefaults()
        {
            Item.DamageType = DamageClass.Ranged;
            Item.useTime = 4;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 2.5f;
            Item.autoReuse = true;
            Item.reuseDelay = 15;
            Item.useAnimation = 12;
            Item.shootSpeed = 16f;
            Item.width = 36;
            Item.height = 42;
            Item.damage = 240;
            Item.UseSound = new SoundStyle("AAModClassic/Sounds/Glitch");
            Item.value = Item.sellPrice(0, 30, 0, 0);
            Item.shoot = ModContent.ProjectileType<BrokenZeroWeapon_TeslaShock>();
            Item.rare = ItemRarityID.Cyan; AARarity = 13;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Rarity13;
                }
            }
        }

        // How can I make the shots appear out of the muzzle exactly?
        // Also, when I do this, how do I prevent shooting through tiles?
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 muzzleOffset = Vector2.Normalize(velocity) * 25f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}
			return true;
		}

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<ApocalyptitePlate>(), 5);
			recipe.AddIngredient(ModContent.ItemType<UnstableSingularity>(), 5);
	        recipe.AddTile(ModContent.TileType<ACS_Tile>());
	        recipe.Register();
		}
	}
}
