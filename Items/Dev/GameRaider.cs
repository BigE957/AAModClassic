using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Dev
{
    public class GameRaider : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Game Raider");
            // Tooltip.SetDefault("Fires explosive Mooshroom Rockets");
        }

        public override void SetDefaults()
        {
            Item.damage = 150;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 66;
            Item.height = 28;
            Item.useTime = 35;
            Item.useAnimation = 35;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 7.5f;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.UseSound = SoundID.Item11;
            Item.autoReuse = true;
            Item.shootSpeed = 25f;
            Item.shoot = Mod.Find<ModProjectile>("GameRocket").Type;
            Item.useAmmo = AmmoID.Rocket;
            Item.rare = ItemRarityID.Cyan;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(140, 42, 42);
                }
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, Mod.Find<ModProjectile>("GameRocket").Type, damage, knockBack, player.whoAmI, 0.0f, 0.0f);
            return false;
        }
    }
}