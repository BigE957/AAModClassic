using AAModClassic;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Dev
{
    public class SkrallStaff : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Skrall Staff");
            /* Tooltip.SetDefault(@"A skraltopian Diamond wrapped in a stick 
It's the stick that's magic. The diamond is just for show
-Kingskrall"); */
			Item.staff[Item.type] = true; //this makes the useStyle animate as a staff instead of as a gun
		}

		public override void SetDefaults()
		{
			Item.damage = 170;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 6;
			Item.width = 58;
			Item.height = 58;
			Item.useTime = 5;
			Item.useAnimation = 5;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 5;
			Item.value = 1000000;
			Item.rare = ItemRarityID.Purple;
			Item.UseSound = SoundID.Item20;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("Crystal").Type;
			Item.shootSpeed = 20f;
		}

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(54, 69, 79);
                }
            }
        }

        public override bool CanUseItem(Player player)
        {

            if (player.altFunctionUse == 2)
            {
                Item.mana = 6;
                Item.useTime = 30;
                Item.useAnimation = 30;
                Item.useStyle = ItemUseStyleID.Shoot;
                Item.noMelee = true; //so the item's animation doesn't do damage
                Item.damage = 270;
                Item.shoot = Mod.Find<ModProjectile>("BigCrystal").Type;
                Item.shootSpeed = 15f;
            }
            else
            {
                Item.mana = 6;
                Item.useStyle = ItemUseStyleID.Shoot;
                Item.useTime = 5;
                Item.useAnimation = 5;
                Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
                Item.shoot = Mod.Find<ModProjectile>("Crystal").Type;
                Item.damage = 170;
                Item.noMelee = false;
                Item.shootSpeed = 20f;
            }
            return base.CanUseItem(player);
        }
	}
}