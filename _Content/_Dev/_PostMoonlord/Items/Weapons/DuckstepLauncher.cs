using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._Dev._PostMoonlord.Items.Weapons
{
    public class DuckstepLauncher : BaseAAItem
    {
        
        public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Duckstep Launcher");
            /* Tooltip.SetDefault(@"Quack.
-Aves"); */
        }

		public override void SetDefaults()
		{
            
			Item.damage = 130;
			Item.DamageType = DamageClass.Magic;
            Item.mana = 9;
            Item.width = 74;
			Item.height = 36;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 4;
			Item.value = 1000000;
			Item.rare = ItemRarityID.Green;
            Item.expert = true; Item.expertOnly = true;
			Item.UseSound = SoundID.Zombie10;
            Item.autoReuse = true;
			Item.shoot = ProjectileID.PurificationPowder; //idk why but all the guns in the vanilla source have this
			Item.shootSpeed = 12f;
            Item.shoot = ModContent.ProjectileType<DuckstepLauncher_Duck>();
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(158, 255, 61);
                }
            }
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0, 0);
        }
    }
}
