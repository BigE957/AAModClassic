using System.Collections.Generic;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic.Items.Dev
{
    public class CursedSickle : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Cursed Sickle");
            /* Tooltip.SetDefault(@"Spins a cursed scythe around you that shreds through enemies
Left click to swing the scythe"); */			
		}

		public override void SetDefaults()
		{
            Item.width = 40;
            Item.height = 40;
            Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.Cyan;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.UseSound = SoundID.Item71;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 25;
            Item.useTime = 25;
            Item.damage = 130;
            Item.knockBack = 4;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.CursedSickle>();
            Item.shootSpeed = 0.1f;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
		}

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(29, 109, 124);
                }
            }
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {

            if (player.altFunctionUse == 2)
            {
                Item.noMelee = false;
                Item.noUseGraphic = false;
                Item.shoot = ModContent.ProjectileType<Projectiles.CursedSickleProj>();
                Item.shootSpeed = 7f;
            }
            else
            {
                Item.noMelee = true;
                Item.noUseGraphic = true;
                Item.shoot = ModContent.ProjectileType<Projectiles.CursedSickle>();
                Item.shootSpeed = 0.1f;
            }
            return base.CanUseItem(player);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                return true;
            }
            for (int k = 0; k < 2; k++)
			{
				Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), player.Center.X, player.Center.Y, 0f, 0f, ModContent.ProjectileType<Projectiles.CursedSickleEffect>(), damage, knockback, player.whoAmI, k, 0f);
			}
			return true;
		}
    }
}