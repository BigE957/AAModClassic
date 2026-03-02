using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Zero
{
    public class BHB : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Black Hole Blaster");
            // Tooltip.SetDefault("Occasionally fires off a rocket that explodes into a vortex when it collides with a tile");
        }
        public override void SetDefaults()
        {
            Item.damage = 200;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 80;
            Item.height = 34;
            Item.useTime = 6;
            Item.useAnimation = 6;
            Item.useStyle = 5;
            Item.noMelee = true; //so the item's animation doesn't do damage
            Item.knockBack = 2.5f;
            Item.value = 4000000;
            Item.rare = 2;
            Item.UseSound = Mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Sounds/BHB");
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("RedBullet").Type; //idk why but all the guns in the vanilla source have this
            Item.shootSpeed = 18f;
            Item.crit = 45;
            Item.useAmmo = AmmoID.Bullet;
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
            return new Vector2(-15, -5);
        }

        public int cooldown;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            cooldown++;
            double rotationA = -0.15;
            for (int i = 0; i < Main.rand.Next(2, 4); i++)
            {
                Vector2 vector = new Vector2(speedX, speedY).RotatedBy(rotationA, default);
                Projectile.NewProjectile(position.X + (vector.X * 4.8f) - 0.2f * vector.Y, position.Y + (vector.Y * 4.8f) + 0.2f * vector.X, vector.X, vector.Y, Mod.Find<ModProjectile>("RedBullet").Type, damage, knockBack, player.whoAmI, 0f, 0f);
                rotationA += Main.rand.NextFloat(0.02f, 0.1f);
            }
            if (cooldown == 10)
            {
                Projectile.NewProjectile(position.X, position.Y, speedX * 0.5f, speedY / 2, Mod.Find<ModProjectile>("Rocket").Type, damage, knockBack, player.whoAmI, 0f, 0f);
                cooldown = 0;
            }
            if (Main.rand.Next(1, 25) == 1)
                Projectile.NewProjectile(position.X, position.Y, speedX * 0.5f, speedY / 2, Mod.Find<ModProjectile>("Black").Type, damage, knockBack, player.whoAmI, 0f, 0f);

            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.VortexBeater, 1);
            recipe.AddIngredient(null, "ApocalyptitePlate", 5);
            recipe.AddIngredient(null, "UnstableSingularity", 5);
            recipe.AddTile(null, "ACS");
            recipe.Register();
        }
    }
}