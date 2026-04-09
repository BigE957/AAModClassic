using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Items.Boss.Djinn
{
    public class SandstormMedallion : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Sandstorm Medallion");
            // Tooltip.SetDefault(@"Doubles your stats during a Sandstorm");
        }
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 50;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.accessory = true;
            Item.expert = true; Item.expertOnly = true;
        }

        public override void UpdateAccessory(Player p, bool hideVisual)
        {
			if(p.ZoneSandstorm)
			{
				p.GetDamage(DamageClass.Melee) += 1f;
				p.GetDamage(DamageClass.Ranged) += 1f;
				p.GetDamage(DamageClass.Magic) += 1f;
				p.GetDamage(DamageClass.Summon) += 1f;
				p.GetDamage(DamageClass.Throwing) += 1f;
				p.GetCritChance(DamageClass.Melee) *= 2;
				p.GetCritChance(DamageClass.Ranged) *= 2;
				p.GetCritChance(DamageClass.Magic) *= 2;
				p.GetCritChance(DamageClass.Throwing) *= 2;
			}
        }
        public override bool CanEquipAccessory(Player player, int slot, bool modded)/* tModPorter Suggestion: Consider using new hook CanAccessoryBeEquippedWith */
        {
            if (slot < 10)
            {
                int maxAccessoryIndex = 5 + player.extraAccessorySlots;
                for (int i = 3; i < 3 + maxAccessoryIndex; i++)
                {
                    if (slot != i && player.armor[i].type == Terraria.ModLoader.ModContent.ItemType<FireFrostMedallion>())
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}