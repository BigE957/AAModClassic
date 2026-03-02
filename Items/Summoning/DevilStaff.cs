using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Summoning
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
            Item.shoot = Mod.Find<ModProjectile>("DevilMinion").Type;
            Item.shootSpeed = 10f;
            Item.buffType = Mod.Find<ModBuff>("DevilMinion").Type;
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
            float num74 = knockBack;
            num74 = player.GetWeaponKnockback(Item, num74);
            player.itemTime = Item.useTime;
            Vector2 vector2;
            vector2.X = Main.mouseX + Main.screenPosition.X;
            vector2.Y = Main.mouseY + Main.screenPosition.Y;
            Projectile.NewProjectile(vector2.X, vector2.Y, 0, 0, Mod.Find<ModProjectile>("DevilMinion").Type, num73, num74, Main.myPlayer, 0f, 0f);
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