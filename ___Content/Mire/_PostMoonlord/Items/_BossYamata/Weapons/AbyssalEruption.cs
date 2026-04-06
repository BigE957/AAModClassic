using System.Collections.Generic;
using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Mire._PostMoonlord.Items._BossYamata.Weapons
{
    public class AE : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Abyssal Eruption");
			// Tooltip.SetDefault(@"spews out abyssal acid that will cause enamys to explode of killed by its visious acid, also uses gel");
		}

	    public override void SetDefaults()
	    {
            Item.damage = 350;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 76;
            Item.height = 34;
            Item.useTime = 2;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true; //so the item's animation doesn't do damage
            Item.knockBack = 3.25f;
            Item.UseSound = SoundID.Item34;
            Item.value = 1000000;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<AcidFlame>(); //idk why but all the guns in the vanilla source have this
            Item.shootSpeed = 20f;
            Item.useAmmo = 23;
            Item.rare = ItemRarityID.Cyan; AARarity = 13;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Rarity13;
                }
            }
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-3, 0);
        }

        int shoot = 0;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int i = 0; i < 3; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(10));
                Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<AcidFlame>(), damage, knockback, player.whoAmI);
            }
            shoot++;

            if (shoot % 6 != 0) return false;

            if (shoot >= 6)
            {
                Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), position, velocity, ModContent.ProjectileType<AcidFlame>(), damage * 2, knockback, player.whoAmI);
                shoot = 0;
            }
            shoot = 0;
            return false;
        }

	
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "EventideAbyssium", 5);
	        recipe.AddIngredient(null, "DreadScale", 5);
            recipe.AddIngredient(null, "Toxithrower");
            recipe.AddTile(TileID.LunarCraftingStation);
	        recipe.Register();
	    }
	}
}
