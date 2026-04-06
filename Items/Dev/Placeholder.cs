using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;
using AAModClassic;

namespace AAModClassic.Items.Dev
{
    public class Placeholder : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Placeholder");
			/* Tooltip.SetDefault(@"'They will die SoonTM'
-Fargo"); */
		}
		public override void SetDefaults()
		{
			Item.damage = 220;
            Item.useStyle = ItemUseStyleID.Swing;
			Item.DamageType = DamageClass.Magic;
            Item.mana = 5;
            Item.useAnimation = 27;
            Item.useTime = 27;
            Item.knockBack = 7f;
            Item.width = 60;
            Item.height = 56;
            Item.scale = 1.15f;
            Item.UseSound = SoundID.Item1;
            Item.rare = ItemRarityID.Purple;
            Item.shootSpeed = 9f;
            Item.value = 500000;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.SoonTM>();
		}

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(189, 76, 15);
                }
            }
        }
	}
}
