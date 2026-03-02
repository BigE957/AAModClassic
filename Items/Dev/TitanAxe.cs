using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Dev
{
    public class TitanAxe : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Titan Axe");
            // Tooltip.SetDefault("Right clicking throws the axe. \n" + "Left clicking swings the axe. \n" + "'Oof this isn't google' \n'" + "-Welox");
		}

		public override void SetDefaults()
		{
			Item.damage = 200;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 72;
			Item.height = 72;
			Item.useTime = 26;
			Item.useAnimation = 26;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = 100000;
			Item.rare = ItemRarityID.Purple;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
            Item.shootSpeed = 12f;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(40, 255, 40);
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
                Item.shoot = Mod.Find<ModProjectile>("TitanAxe").Type;
                Item.noMelee = true;
                Item.noUseGraphic = true;
            }
            else
            {
                Item.shoot = ProjectileID.None;
                Item.noMelee = false;
                Item.noUseGraphic = false;
            }
            return base.CanUseItem(player);
		}
	}
}