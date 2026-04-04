using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using System;
using Terraria.ModLoader;

namespace AAModClassic.Items.Summoning
{
    public class DemonStaff : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Demon Staff");
            // Tooltip.SetDefault(@"Summons a demon to fight with you");
        }

        public override void SetDefaults()
        {
            Item.damage = 35;
            Item.DamageType = DamageClass.Summon;
            Item.mana = 10;
            Item.width = 26;
            Item.height = 28;
            Item.useTime = 36;
            Item.useAnimation = 36;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true;
            Item.knockBack = 3;
            Item.rare = ItemRarityID.LightRed;
            Item.UseSound = SoundID.Item44;
            Item.shoot = ModContent.ProjectileType<DemonMinion>();
            Item.shootSpeed = 10f;
            Item.buffType = ModContent.BuffType<DemonMinion>();
            Item.autoReuse = true;
            Item.value = Item.sellPrice(0, 1, 0, 0);
        }
		
		public override void UseStyle(Player player, Rectangle heldItemFrame)
		{
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
			{
				player.AddBuff(Item.buffType, 3600, true);
			}
		}

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int i = Main.myPlayer;
            float num72 = Item.shootSpeed;
            int num73 = damage;
            float num74 = knockback;
            num74 = player.GetWeaponKnockback(Item, num74);
            player.itemTime = Item.useTime;
            Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
            float num78 = Main.mouseX + Main.screenPosition.X - vector2.X;
            float num79 = Main.mouseY + Main.screenPosition.Y - vector2.Y;
            if (player.gravDir == -1f)
            {
                num79 = Main.screenPosition.Y + Main.screenHeight - Main.mouseY - vector2.Y;
            }
            float num80 = (float)Math.Sqrt(num78 * num78 + num79 * num79);
            float num81 = num80;
            if ((float.IsNaN(num78) && float.IsNaN(num79)) || (num78 == 0f && num79 == 0f))
            {
                num78 = player.direction;
                num79 = 0f;
                num80 = num72;
            }
            else
            {
                num80 = num72 / num80;
            }
            num78 = 0f;
            num79 = 0f;
            vector2.X = Main.mouseX + Main.screenPosition.X;
            vector2.Y = Main.mouseY + Main.screenPosition.Y;
            Projectile.NewProjectile(source, vector2.X, vector2.Y, num78, num79, ModContent.ProjectileType<DemonMinion>(), num73, num74, i, 0f, 0f);
            return false;
        }

        public override void AddRecipes()  //How to craft this sword
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.ImpStaff, 1);
            recipe.AddIngredient(ItemID.HornetStaff, 1);
            recipe.AddIngredient(null, "SkullStaff", 1);
            recipe.AddRecipeGroup("AAModClassic:EvilStaff", 1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}