using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Yamata
{
    public class Darksprayer : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Darksprayer");
            /* Tooltip.SetDefault(@"'Spouts of dark, leaves its mark'
Inflicts Moonrazed"); */           
        }

        public override void SetDefaults()
        {
            Item.damage = 425;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 44;
            Item.height = 34;
            Item.useTime = 19;
            Item.useAnimation = 19;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAmmo = AmmoID.Rocket;
            Item.knockBack = 8f;
            Item.value = Item.sellPrice(0, 30, 0, 0);
            Item.UseSound = SoundID.Item38;      
            Item.autoReuse = true;   
            Item.shootSpeed = 20f;
            Item.shoot = Mod.Find<ModProjectile>("Moonblow").Type;
            Item.rare = ItemRarityID.Cyan; AARarity = 13;
            Item.noMelee = true;
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
            return new Vector2(-12, 0);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, Mod.Find<ModProjectile>("Moonblow").Type, damage, knockBack, player.whoAmI, 0, 1);
            return false;
        }
	
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "EventideAbyssium", 5);
            recipe.AddIngredient(null, "DreadScale", 5);
            recipe.AddIngredient(ItemID.SnowmanCannon);
            recipe.AddTile(null, "ACS");
            recipe.Register();
        }
    }
}
