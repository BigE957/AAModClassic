using AAModClassic._Content.Chaos.___PreHardmode.Items._BossGripsOfChaos.Accessories;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._Tinker.___PreHardmode.Items.Accessories
{
    [AutoloadEquip(EquipType.Shield)]
    public class BulwarkOfChaos : EquipAbstract, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Accessories";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Bulwark Of Chaos");
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 32;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.Green;
            Item.expert = true;
            Item.accessory = true;
            Item.defense = 3;
        }

        public override void RegisterEquipEffects()
        {
            damageMap.GetDamage(DamageClass.Generic).Flat += 5;
            AddEffect<ShieldOfCthulhuDashEffect>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ClawOfChaos>(), 1);
            recipe.AddIngredient(ItemID.EoCShield, 1);
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
                    if (slot != i && player.armor[i].type == ModContent.ItemType<ClawOfChaos>())
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}