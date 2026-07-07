using AAModClassic._Content._EX._PostMoonlord.Items.Accessories;
using AAModClassic._Content.Acropolis.__Hardmode.Items._BossAthena.Accessories;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._Dev.__Hardmode.Items.Accessories
{
    public class APageOfTheRuneBook : EquipAbstract, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Accessories";
		public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("A Page of the Rune Book");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = Color.Gold;
                }
            }
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.value = 100000;
            Item.rare = ItemRarityID.Purple;
            Item.accessory = true;
        }

        public override void RegisterEquipEffects()
        {
            AddEffect<APageOfTheRuneBookEffect>();
        }
    }
}