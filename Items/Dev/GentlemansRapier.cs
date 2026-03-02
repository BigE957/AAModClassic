using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Dev
{
    public class GentlemansRapier : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Gentleman's Rapier");
            /* Tooltip.SetDefault(@"Shoots spooky dapper top hats
Right clicking thrusts the blade forward
Left clicking swings the blade
'Spoopy'
-Tied"); */
		}

		public override void SetDefaults()
		{
			Item.damage = 200;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 64;
			Item.height = 66;
			Item.useTime = 10;
			Item.useAnimation = 10;
			Item.useStyle = 1;
			Item.knockBack = 3;
			Item.value = 100000;
			Item.rare = 11;
            Item.shoot = Mod.Find<ModProjectile>("TopHat").Type;
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
                    line2.OverrideColor = new Color(0, 105, 0);
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
                Item.useStyle = 3;
            }
            else
            {
                Item.useStyle = 1;
            }
            return base.CanUseItem(player);
		}
	}
}