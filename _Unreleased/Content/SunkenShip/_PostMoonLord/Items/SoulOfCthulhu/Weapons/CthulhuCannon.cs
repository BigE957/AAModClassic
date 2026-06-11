using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Tiles.Crafters;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.Items.SoulOfCthulhu.Weapons
{
    public class CthulhuCannon : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Cthulhu Cannon");
            //Tooltip.SetDefault(@"Uses cannonballs for ammo\Fires reality-breaking bombs that fragment into powerful dark rifts on impact");
        }

        public override void SetDefaults()
        {
            Item.damage = 400;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 98;
            Item.height = 32;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 0f;
            Item.value = 5000000;
            Item.UseSound = SoundID.Item11;
            Item.useAmmo = ItemID.Cannonball;
            Item.autoReuse = true;
            Item.shootSpeed = 14f;
            Item.shoot = ModContent.ProjectileType<CthulhuCannon_CthulhuBomb>();
            Item.rare = ModContent.RarityType<SuperancientsRarity>();
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Cthulhu;
                }
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(Item.GetSource_FromThis(), position + velocity.SafeNormalize(Vector2.UnitX * player.direction) * 64, velocity, ModContent.ProjectileType<CthulhuCannon_CthulhuBomb>(), damage, knockback, player.whoAmI, 0.0f, 0.0f);
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<RealityBar>(), 5);
            recipe.AddIngredient(ItemID.Cannon, 1);
            recipe.AddTile(ModContent.TileType<ACS_Tile>());
            recipe.Register();
        }
    }
}