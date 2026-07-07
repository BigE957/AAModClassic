using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._Dev._PostMoonlord.Items.Weapons
{
    public class GibsFemur : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Ranged";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Angry Femur");
            // Tooltip.SetDefault("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
        }

        public override void SetDefaults()
        {
			Item.useTime = 25;
            Item.CloneDefaults(ItemID.Bone);
            Item.maxStack = Item.CommonMaxStack;
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 120;                            
            Item.value = 600000;
            Item.rare = ItemRarityID.Purple;
            Item.knockBack = 5;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 24;
            Item.useTime = 24;
            Item.shoot = ModContent.ProjectileType<GibsFemur_AngryFemur>();
			Item.width = 32;
            Item.height = 32;
            Item.noMelee = true;
            Item.shootSpeed = 10f;
            Item.consumable = false;
            Item.autoReuse = true;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(255, 128, 0);
                }
            }
        }
    }
}
