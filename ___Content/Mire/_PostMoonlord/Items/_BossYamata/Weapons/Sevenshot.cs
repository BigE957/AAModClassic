using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using AAModClassic.Globals;

namespace AAModClassic.___Content.Mire._PostMoonlord.Items._BossYamata.Weapons
{
    public class Sevenshot : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Hydra Sevenshot");
			/* Tooltip.SetDefault(@"Fires 6 bullets and a destructive moon blast
50% chance to not consume ammo"); */
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 130;
	        Item.DamageType = DamageClass.Ranged;
	        Item.width = 60;
	        Item.height = 26;
	        Item.useTime = 16;
	        Item.useAnimation = 16;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.noMelee = true;
	        Item.knockBack = 4.5f;
	        Item.value = Item.sellPrice(0, 30, 0, 0);
	        Item.UseSound = SoundID.Item36;
	        Item.autoReuse = true;
	        Item.shoot = ProjectileID.PurificationPowder;
	        Item.shootSpeed = 20f;
	        Item.useAmmo = 97;
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
			return new Vector2(-5, 0);
		}
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
		    float SpeedX = velocity.X + Main.rand.Next(-25, 26) * 0.05f;
		    float SpeedY = velocity.Y + Main.rand.Next(-25, 26) * 0.05f;
		    Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), position, velocity, ModContent.ProjectileType<Darksprayer_Moonblow>(), (int)(damage * 1.5f), knockback, player.whoAmI, 0.0f, 0.0f);
		    for (int i = 0; i <= 6; i++)
		    {
		    	float SpeedNewX = velocity.X + Main.rand.Next(-45, 46) * 0.05f;
		    	float SpeedNewY = velocity.Y + Main.rand.Next(-45, 46) * 0.05f;
		    	Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), position.X, position.Y, SpeedNewX, SpeedNewY, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
		    }
		    return false;
		}
	    
	    public override bool CanConsumeAmmo(Item ammo, Player player)
	    {
	    	if (Main.rand.Next(0, 100) <= 50)
	    		return false;
	    	return true;
	    }
	
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "EventideAbyssium", 5);
	        recipe.AddIngredient(null, "DreadScale", 5);
            recipe.AddIngredient(null, "HydraTrishot");
            recipe.AddIngredient(ItemID.OnyxBlaster);
            recipe.AddTile(TileID.LunarCraftingStation);
	        recipe.Register();
	    }
	}
}