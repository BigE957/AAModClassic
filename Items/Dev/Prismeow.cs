using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Dev
{
    public class Prismeow : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Prismeow");
            /* Tooltip.SetDefault(@"Fires rainbow cats
'Godly'
-Hallam"); */
			Item.staff[Item.type] = true; //this makes the useStyle animate as a staff instead of as a gun
		}

		public override void SetDefaults()
		{
			Item.damage = 180;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 6;
			Item.width = 58;
			Item.height = 58;
			Item.useTime = 18;
			Item.useAnimation = 18;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 5;
			Item.value = 1000000;
			Item.rare = ItemRarityID.Purple;
			Item.UseSound = SoundID.Item20;
			Item.autoReuse = true;
			Item.shoot = ProjectileID.Meowmere;
			Item.shootSpeed = 10f;
		}


        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float spread = Main.rand.Next(20, 30) * 0.0174f;
            float baseSpeed = (float)Math.Sqrt((speedX * speedX) + (speedY * speedY));
            double startAngle = Math.Atan2(speedX, speedY) - .1d;
            double deltaAngle = spread / 6f;
            double offsetAngle;
            for (int i = 0; i < 3; i++)
            {
                float randomSpeed = baseSpeed + Main.rand.NextFloat() * 1.5f;
                offsetAngle = startAngle + (deltaAngle * i);
                int shoot = Projectile.NewProjectile(position.X, position.Y, randomSpeed * (float)Math.Sin(offsetAngle), randomSpeed * (float)Math.Cos(offsetAngle), type, damage, knockBack, Main.myPlayer);
                Main.projectile[shoot].melee = false/* tModPorter Suggestion: Remove. See Item.DamageType */;
                Main.projectile[shoot].DamageType = DamageClass.Magic;
            }
            return false;
        }
		
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(255, 8, 251);
                }
            }
        }
	}
}