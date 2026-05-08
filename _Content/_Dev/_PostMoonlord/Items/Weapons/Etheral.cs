using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._Dev._PostMoonlord.Items.Weapons
{
    public class Etheral : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Etheral");
			// Tooltip.SetDefault(" \"If in the wrong hands, it can cause devastating damage, so don't give it to me\" \n-TheRedstoneBro");
		}


        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(159, 207, 190);
                }
            }
        }

        public override void SetDefaults()
		{
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 7;
            Item.useTime = 7;
            Item.mana = 10;
            Item.shootSpeed = 16f;
            Item.knockBack = 0f;
            Item.width = 122;
            Item.reuseDelay = 5;
            Item.height = 32;
            Item.damage = 270;
            Item.UseSound = SoundID.Item13;
            Item.channel = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Etheral_Proj>();
            Item.value = Item.sellPrice(0, 30, 0, 0);
            Item.noMelee = true;
            Item.DamageType = DamageClass.Magic;
            Item.autoReuse = true;
			Item.noUseGraphic = true;
            
		}
	}
}
