using AAModClassic._Content.Void._PostMoonlord.Items._BossZero.Accessories;
using AAModClassic.Dusts;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Acropolis.__Hardmode.Items._BossAthena.Accessories
{
    [AutoloadEquip(EquipType.Wings)]
	public class OlympianWings : EquipAbstract, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Accessories";
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Olympian Wings");

            ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(170, 8, 2f);
        }

		public override void SetDefaults()
		{
			Item.width = 26;
			Item.height = 30;
            Item.value = Item.sellPrice(0, 8, 0, 0);
            Item.rare = ItemRarityID.Yellow;
			Item.accessory = true;
        }

        public override void RegisterEquipEffects()
        {
            AddEffect(new WingTimeMaxEffect(170));
            AddEffect<OlympianWingsEffect>();
        }

        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = 0.85f;
            ascentWhenRising = 0.15f;
            maxCanAscendMultiplier = 1f;
            maxAscentMultiplier = 2.1f;
            constantAscend = 0.135f;
        }
	}
}