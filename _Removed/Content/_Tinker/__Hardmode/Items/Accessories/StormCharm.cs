using AAModClassic._Content.Inferno.___PreHardmode.Items._BossBroodmother.Accessories;
using AAModClassic._Content.Mire.___PreHardmode.Items._BossHydra.Accessories;
using AAModClassic._Removed.Content._Tinker.___PreHardmode.Items.Accessories;
using AAModClassic._Removed.Content.Parthenan.__Hardmode.Items._BossOrthrusX.Accessories;
using AAModClassic._Removed.Content.Parthenan.__Hardmode.Items._BossRaiderUltima.Accessories;
using AAModClassic._Removed.Content.Parthenan.__Hardmode.Items._BossRetriever.Accessories;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Removed.Content._Tinker.__Hardmode.Items.Accessories
{
    public class StormCharm : EquipAbstract, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Accessories";
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Storm Charm");
        }
        public override void SetDefaults()
        {
            Item.width = 58;
            Item.height = 54;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.rare = ItemRarityID.Green;
            Item.accessory = true;
            Item.expert = true;
            Item.defense = 3;
        }

        
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<HoloCape>());
            recipe.AddIngredient(ModContent.ItemType<StormPendant>());
            recipe.AddIngredient(ModContent.ItemType<StormRiot>());
            recipe.AddIngredient(ModContent.ItemType<DragonSerpentNecklace>());
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }

        public override void RegisterEquipEffects()
        {
            damageMap.GetDamage(DamageClass.Generic) += .15f;
            damageMap.GetDamage(DamageClass.Default).Flat += 20;
            AddEffect(new EnduranceEffect(0.15f));
            AddEffect<StormClawEffect>();
            AddEffect<ShieldOfCthulhuDashEffect>();
        }

        public override bool CanEquipAccessory(Player player, int slot, bool modded)/* tModPorter Suggestion: Consider using new hook CanAccessoryBeEquippedWith */
        {
            if (slot < 10)
            {
                int maxAccessoryIndex = 5 + player.extraAccessorySlots;
                for (int i = 3; i < 3 + maxAccessoryIndex; i++)
                {
                    if (slot != i && player.armor[i].type == ModContent.ItemType<DragontamersCloak>())
                    {
                        return false;
                    }
                    if (slot != i && player.armor[i].type == ModContent.ItemType<HydraPendant>())
                    {
                        return false;
                    }
                    if (slot != i && player.armor[i].type == ModContent.ItemType<DragonSerpentNecklace>())
                    {
                        return false;
                    }
                    if (slot != i && player.armor[i].type == ModContent.ItemType<HoloCape>())
                    {
                        return false;
                    }
                    if (slot != i && player.armor[i].type == ModContent.ItemType<StormPendant>())
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
    
}