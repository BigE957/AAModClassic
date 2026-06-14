using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic._Content.Desert.___PreHardmode.Items._BossDesertDjinn.Accessories;
using AAModClassic._Content.Snow.___PreHardmode.Items._BossSubzeroSerpent.Accessories;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content._Tinker.___PreHardmode.Items.Accessories
{
    public class FireFrostMedallion : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Accessories";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Fire Frost Medallion");
            // Tooltip.SetDefault(@"Doubles your stats during a Blizzard or Sandstorm");
        }
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 50;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.accessory = true;
            Item.expert = true; Item.expertOnly = true;
        }

        public override void UpdateAccessory(Player p, bool hideVisual)
        {
			if(p.ZoneSandstorm || p.ZoneRain && p.ZoneSnow)
			{
				p.GetDamage(DamageClass.Melee) *= 2f;
				p.GetDamage(DamageClass.Ranged) *= 2f;
				p.GetDamage(DamageClass.Magic) *= 2f;
				p.GetDamage(DamageClass.Summon) *= 2f;
				p.GetDamage(DamageClass.Throwing) *= 2f;
				p.GetCritChance(DamageClass.Melee) *= 2;
				p.GetCritChance(DamageClass.Ranged) *= 2;
				p.GetCritChance(DamageClass.Magic) += 2;
				p.GetCritChance(DamageClass.Throwing) *= 2;
			}
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ArcticMedallion>(), 1);
            recipe.AddIngredient(ModContent.ItemType<SandstormMedallion>(), 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }
        public override bool CanEquipAccessory(Player player, int slot, bool modded)/* tModPorter Suggestion: Consider using new hook CanAccessoryBeEquippedWith */
        {
            if (slot < 10)
            {
                int maxAccessoryIndex = 5 + player.extraAccessorySlots;
                for (int i = 3; i < 3 + maxAccessoryIndex; i++)
                {
                    if (slot != i && player.armor[i].type == ModContent.ItemType<SandstormMedallion>())
                    {
                        return false;
                    }
                    if (slot != i && player.armor[i].type == ModContent.ItemType<ArcticMedallion>())
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}