using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.AH
{
    public class HarukaKunai : BaseAAItem
    {
		public override void SetDefaults()
		{
			Item.damage = 140;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 14;
			Item.height = 34;
			Item.noUseGraphic = true;
			Item.useTime = 8;
			Item.useAnimation = 8;
			Item.shoot = Mod.Find<ModProjectile>("HarukaKunaiF").Type;
			Item.shootSpeed = 15f;
			Item.useStyle = 1;
			Item.knockBack = 0;
			Item.value = Item.sellPrice(0, 30, 0, 0);
			Item.rare = 11;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
		}

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Abyssal Kunai");
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
            float spread = 25f * 0.0174f;
            float baseSpeed = (float)Math.Sqrt((speedX * speedX) + (speedY * speedY));
            double startAngle = Math.Atan2(speedX, speedY) - .1d;
            double deltaAngle = spread / 6f;
            double offsetAngle;
            for (int i = 0; i < 3; i++)
            {
                offsetAngle = startAngle + (deltaAngle * i);
                int proj = Projectile.NewProjectile(position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), type, damage, knockBack, Main.myPlayer);
            }
            return false;
        }
    }
}
