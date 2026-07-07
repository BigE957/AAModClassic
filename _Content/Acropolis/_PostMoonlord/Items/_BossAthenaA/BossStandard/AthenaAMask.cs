using AAModClassic.Globals;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.Attributes;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Acropolis._PostMoonlord.Items._BossAthenaA.BossStandard
{
    [AutoloadEquip(EquipType.Head)]
    [AutoloadEquipGlow(EquipType.Head)]
    public class AthenaAMask : BaseAAItem, ILocalizedModType, ICustomEquipGlow
    {
        public new string LocalizationCategory => "Items.Vanity.Masks";
        public Color Color => AAColor.Flash;

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Olympian Athena Mask");
		}

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 26;
            Item.rare = ItemRarityID.Green;
            Item.vanity = true;
        }
    }
}