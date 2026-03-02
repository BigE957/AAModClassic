using AAModClassic;
using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Boss.Zero
{

    public class GenocideCannon : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Genocide Cannon");
            /* Tooltip.SetDefault(@"Fires highly explosive Rockets
25% chance to fire a glitched rocket that explodes into frag rockets"); */
        }

        public override void SetDefaults()
        {
            Item.damage = 400;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 66;
            Item.height = 28;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true; //so the item's animation doesn't do damage
            Item.knockBack = 10f;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.UseSound = SoundID.Item11;
            Item.autoReuse = true;
            Item.shootSpeed = 24f;
            Item.shoot = Mod.Find<ModProjectile>("GRocket").Type;
            Item.useAmmo = AmmoID.Rocket;
            Item.rare = ItemRarityID.Cyan;
            AARarity = 13;
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

        public override Vector2? HoldoutOffset()
		{
			return new Vector2(-6, 0);
		}
		
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            type = Main.rand.Next(4) == 0 ? Mod.Find<ModProjectile>("GRocket2").Type : Mod.Find<ModProjectile>("GRocket").Type;
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 0.0f, 0.0f);
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "ApocalyptitePlate", 5);
            recipe.AddIngredient(null, "UnstableSingularity", 5);
            recipe.AddIngredient(ItemID.RocketLauncher, 1);
            recipe.AddTile(null, "ACS");
            recipe.Register();
        }
    }
}