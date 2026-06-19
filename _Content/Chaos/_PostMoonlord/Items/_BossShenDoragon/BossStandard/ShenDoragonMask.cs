using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos._PostMoonlord.Items._BossShenDoragon.BossStandard
{
    [AutoloadEquip(EquipType.Head)]
	public class ShenDoragonMask : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Vanity.Masks";
        public override void Load()
        {
            EquipLoader.AddEquipTexture(Mod, Texture + "_Head_Alt", EquipType.Head, item: this, name: $"{Name}_Head_Alt");
            AAPlayer.ModifyDrawInfoEvent += ModifyDrawInfo;
        }

        private void ModifyDrawInfo(Player player)
        {
            int blue = EquipLoader.GetEquipSlot(Mod, Name, EquipType.Head);
            int red = EquipLoader.GetEquipSlot(Mod, Name + "_Head_Alt", EquipType.Head);

            if (player.head == blue && player.direction == -1)
                player.head = red;
            else if (player.head == red && player.direction == 1)
                player.head = blue;
        }

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Shen Doragon Mask");
		}

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 26;
            Item.rare = ModContent.RarityType<SuperancientsRarity>();
            Item.vanity = true;
        }
    }
}