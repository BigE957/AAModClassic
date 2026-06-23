using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic._Removed.Content.Parthenan.__Hardmode.Items._BossRaiderUltima.Accessories;
using AAModClassic._Content.Inferno.___PreHardmode.Items._BossBroodmother.Accessories;
using AAModClassic._Content.Mire.___PreHardmode.Items._BossHydra.Accessories;
using AAModClassic._Removed.Content.Parthenan.__Hardmode.Items._BossRetriever.Accessories;
using AAModClassic._Removed.Content.Parthenan.__Hardmode.Items._BossOrthrusX.Accessories;
using AAModClassic._Removed.Content._Tinker.___PreHardmode.Items.Accessories;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Removed.Content._Tinker.__Hardmode.Items.Accessories
{
    public class StormCharm : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Accessories";
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Storm Charm");
            /* Tooltip.SetDefault(@"15% increased damage and damage resistance
10% Increased melee speed
All attacks deal 20 True damage (damage unaffected by class)
Grants the ability to dash."); */
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

        public override void UpdateEquip(Player player)
        {
            player.endurance += .15f;
            player.GetDamage(DamageClass.Melee) += .15f;
            player.GetDamage(DamageClass.Ranged) += .15f;
            player.GetDamage(DamageClass.Magic) += .15f;
            player.GetDamage(DamageClass.Summon) += .15f;
            player.GetDamage(DamageClass.Throwing) += .15f;
            player.GetModPlayer<StormClawPlayer>().StormClaw = true;
            player.dash = DashID.TabiAndMasterNinjaGear;
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