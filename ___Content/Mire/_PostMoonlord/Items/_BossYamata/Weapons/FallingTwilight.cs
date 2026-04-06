using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;
using AAModClassic.Globals;

namespace AAModClassic.___Content.Mire._PostMoonlord.Items._BossYamata.Weapons
{
    public class FallingTwilight : BaseAAItem
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Falling Twilight");
        }

        public override void SetDefaults()
        {
            Item.damage = 170;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 44;
            Item.height = 76;
            Item.useAnimation = 18;
            Item.useTime = 18;
            Item.reuseDelay = 0;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 2.5f;
            Item.value = Item.sellPrice(0, 30, 0, 0);
            Item.UseSound = SoundID.Item5;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.WoodenArrowFriendly;
            Item.shootSpeed = 20f;
            Item.useAmmo = AmmoID.Arrow;
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
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float numberProjectiles = 4;
            float rotation = MathHelper.ToRadians(4);
            position += Vector2.Normalize(velocity) * -45f;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 projectileOffset = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) *5f;
                projectileOffset.X *= MathHelper.Lerp(0.8f, 1.2f, (float)Main.rand.NextDouble());
                projectileOffset.Y *= MathHelper.Lerp(0.8f, 1.2f, (float)Main.rand.NextDouble());
                Vector2 newSpeed = velocity * MathHelper.Lerp(0.8f, 1.2f, (float)Main.rand.NextDouble());
                Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), position.X + projectileOffset.X, position.Y + projectileOffset.Y, newSpeed.X, newSpeed.Y, ModContent.ProjectileType<NightSoul>(), damage, knockback, player.whoAmI);
            }
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "EventideAbyssium", 5);
            recipe.AddIngredient(null, "DreadScale", 5);
            recipe.AddIngredient(ItemID.Tsunami);
            recipe.AddTile(null, "ACS");
            recipe.Register();
        }
    }
}
