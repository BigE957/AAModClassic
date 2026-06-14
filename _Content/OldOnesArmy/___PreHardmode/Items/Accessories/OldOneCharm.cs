using Terraria.ModLoader;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.ID;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content.OldOnesArmy.___PreHardmode.Items.Accessories
{
    [AutoloadEquip(EquipType.Neck)]
    public class OldOneCharm : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Accessories";
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 24;
			Item.value = Item.sellPrice(1, 0, 0, 0);
			Item.rare = ItemRarityID.Yellow;
			Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
		{
            player.GetDamage(DamageClass.Summon) += .12f;
            player.maxTurrets ++;
            if(DD2Event.Ongoing) player.GetDamage(DamageClass.Summon) += .1f;
		}
		
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Old One Charm");
			/* Tooltip.SetDefault(@"Increase 12% minion damage
Increases your max number of sentries
While Old One's Army is on, increase 22% minion damage."); */
			
		}
	}
}
