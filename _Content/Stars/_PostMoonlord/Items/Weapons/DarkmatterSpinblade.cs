using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System;
using AAModClassic.Globals;
using AAModClassic._Content.Stars._PostMoonlord.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Rarities;
using AAModClassic._Content.Stars._PostMoonlord.Items.Tiles.Functional;

namespace AAModClassic._Content.Stars._PostMoonlord.Items.Weapons
{
    public class DarkmatterSpinblade : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Weapons.Melee";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Darkmatter Spinblade");
        }
        public override void SetDefaults()
		{
            Item.damage = 65;            
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 30;
            Item.height = 30;
	        Item.useTime = 16;
	        Item.useAnimation = 16;
            Item.noUseGraphic = true;
            Item.useStyle = ItemUseStyleID.Swing;
	        Item.knockBack = 0;
	        Item.value = 100000;
	        Item.rare = ItemRarityID.Purple;
	        Item.shootSpeed = 12f;
	        Item.shoot = ModContent.ProjectileType<DarkmatterSpinblade_Proj>();
	        Item.UseSound = SoundID.Item1;
	        Item.autoReuse = true;
            Item.noMelee = true;
            Item.rare = ModContent.RarityType<PostEquinoxRarity>();
        }

        

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float spread = 50f * 0.0174f;
            float baseSpeed = (float)Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y);
            double startAngle = Math.Atan2(velocity.X, velocity.Y) - .1d;
            double deltaAngle = spread / 12f;
            double offsetAngle;
            for (int i = 0; i < 3; i++)
            {
                if(i == 1) continue;
                offsetAngle = startAngle + deltaAngle * i;
                int proj = Projectile.NewProjectile(source, position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), ModContent.ProjectileType<DarkmatterSpinblade_EnergyBlade>(), damage, knockback, Item.playerIndexTheItemIsReservedFor);
            }
            return true;
        }

        public override bool CanUseItem(Player player) 
        {
            foreach (Projectile p in Main.ActiveProjectiles)
            {
                if (p.owner == Main.myPlayer && p.type == Item.shoot)
                {
                    return false;
                }
            }
            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DarkEnergy>(), 5);
            recipe.AddIngredient(ModContent.ItemType<DarkmatterBar>(), 15);
            recipe.AddTile(ModContent.TileType<QuantumFusionAccelerator_Tile>());
            recipe.Register();
		}
    }
}
