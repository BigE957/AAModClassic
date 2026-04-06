using AAModClassic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Summoning
{
    public class DevilStaff : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Devil Staff");
            // Tooltip.SetDefault(@"Summons a devil to fight with you");
        }

        public override void SetDefaults()
        {
            Item.damage = 90;
            Item.DamageType = DamageClass.Summon;
            Item.mana = 10;
            Item.width = 26;
            Item.height = 28;
            Item.useTime = 36;
            Item.useAnimation = 36;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true;
            Item.knockBack = 3;
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item44;
            Item.shoot = ModContent.ProjectileType<Projectiles.DevilMinion>();
            Item.shootSpeed = 10f;
            Item.buffType = ModContent.BuffType<DevilMinion_Buff>();
            Item.autoReuse = true;
            Item.value = Item.sellPrice(0, 10, 0, 0);
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
            int num73 = damage;
            float num74 = knockback;
            num74 = player.GetWeaponKnockback(Item, num74);
            player.itemTime = Item.useTime;
            Vector2 vector2;
            vector2.X = Main.mouseX + Main.screenPosition.X;
            vector2.Y = Main.mouseY + Main.screenPosition.Y;
            Projectile.NewProjectile(source, vector2.X, vector2.Y, 0, 0, ModContent.ProjectileType<DevilMinion>(), num73, num74, Main.myPlayer, 0f, 0f);
            return false;
        }

        public override void AddRecipes()  //How to craft this sword
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "DemonStaff", 1);
            recipe.AddIngredient(null, "PureEvil", 3);
            recipe.AddIngredient(null, "HeroShards", 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}