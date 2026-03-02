using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Anubis.Forsaken
{
    public class CursedFury : BaseAAItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Cursed Flamefury");
			// Tooltip.SetDefault("50% chance to not consume gel");
		}

	    public override void SetDefaults()
	    {
			Item.damage = 70;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 80;
			Item.height = 38;
			Item.useTime = 5;
			Item.useAnimation = 10;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 4f;
			Item.UseSound = SoundID.Item34;
            Item.value = Item.buyPrice(0, 1, 0, 0);
            Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("ForsakenFlame").Type;
			Item.shootSpeed = 10f;
			Item.useAmmo = 23;
            Item.rare = ItemRarityID.Cyan;
            AARarity = 12;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Rarity12;
                }
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	        for (int index = 0; index < 2; ++index)
	        {
	            float SpeedX = speedX + Main.rand.Next(-25, 26) * 0.05f;
	            float SpeedY = speedY + Main.rand.Next(-25, 26) * 0.05f;
                Projectile.NewProjectile(position.X, position.Y, SpeedX, SpeedY, type, damage, knockBack, player.whoAmI, 0.0f, 0.0f);
            }
	    	return false;
		}

	    public override bool CanConsumeAmmo(Item ammo, Player player)
	    {
	    	if (Main.rand.Next(0, 100) < 50)
	    		return false;
	    	return true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<SandstormThrower>(), 1);
			recipe.AddIngredient(null, "SoulFragment", 5);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.Register();
		}
	}
}
