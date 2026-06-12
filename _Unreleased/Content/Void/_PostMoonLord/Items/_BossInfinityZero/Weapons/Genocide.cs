using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.Void._PostMoonLord.Items._BossInfinityZero.Weapons
{
    public class Genocide : ModItem
    {
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Genocide");
            /* Tooltip.SetDefault(@"Fires a spread of infinitely piercing lasers that ignores tiles and home in on enemies
Gets stronger the more the laser pierces
Doesn't require ammo"); */
        }

        public override void SetDefaults()
        {          
            Item.damage = 500;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 74;
            Item.height = 24;
            Item.useTime = 20;
            Item.useAnimation = 20; 
            Item.useStyle = ItemUseStyleID.Shoot; 
            Item.shoot = ModContent.ProjectileType<Genocide_Antimatter>();
            Item.knockBack = 12;
            Item.value = Item.sellPrice(1, 0, 0, 0);
            Item.rare = ModContent.RarityType<SuperancientsRarity>();
            Item.UseSound = SoundID.Item75;
            Item.autoReuse = true;
            Item.shootSpeed = 8f;
            Item.crit = 5; 
        }


        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.IZ;
                }
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float spread = 45f * 0.0174f;
            float baseSpeed = (float)Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y);
            double startAngle = Math.Atan2(velocity.X, velocity.Y) - .1d;
            double deltaAngle = spread / 6f;
            double offsetAngle;
            for (int i = 0; i < 3; i++)
            {
                offsetAngle = startAngle + deltaAngle * i;
                Projectile.NewProjectile(Item.GetSource_FromThis(), position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), Item.shoot, damage, knockback, Item.playerIndexTheItemIsReservedFor);
            }
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-1, 0);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            //TODOIZ this item doesnt exist anymore
            //recipe.AddIngredient(ModContent.ItemType<AntimatterRifle>(), 1);
            recipe.AddIngredient(ModContent.ItemType<Infinitium>(), 12);
            recipe.AddTile(ModContent.TileType<ACS_Tile>());
            recipe.Register();
        }
    }
}