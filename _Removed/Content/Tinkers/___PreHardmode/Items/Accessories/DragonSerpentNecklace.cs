using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic.Items.Boss.Broodmother;
using AAModClassic.___Content.Mire.___PreHardmode.Items._BossHydra.Accessories;

namespace AAModClassic._Removed.Content.Tinkers.___PreHardmode.Items.Accessories
{
    public class DragonSerpentNecklace : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dragon Serpent Necklace");
            /* Tooltip.SetDefault(@"7% increased damage and 3% increased damage resistance
Ignores 5 Enemy defense"); */
        }
        public override void SetDefaults()
        {
            Item.width = 58;
            Item.height = 54;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.rare = ItemRarityID.Green;
            Item.accessory = true;
            Item.expert = true; Item.expertOnly = true;
            Item.defense = 3;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DragonCape>(), 1);
            recipe.AddIngredient(ModContent.ItemType<HydraPendant>(), 1);
            recipe.AddIngredient(ItemID.SharkToothNecklace, 1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.endurance += .03f;
            player.GetDamage(DamageClass.Generic) += .07f;
            player.GetModPlayer<AAPlayer>().clawsOfChaos = true;
        }

        public override bool CanEquipAccessory(Player player, int slot, bool modded)/* tModPorter Suggestion: Consider using new hook CanAccessoryBeEquippedWith */
        {
            if (slot < 10)
            {
                int maxAccessoryIndex = 5 + player.extraAccessorySlots;
                for (int i = 3; i < 3 + maxAccessoryIndex; i++)
                {
                    if (slot != i && player.armor[i].type == ModContent.ItemType<DragonCape>())
                    {
                        return false;
                    }
                    if (slot != i && player.armor[i].type == ModContent.ItemType<HydraPendant>())
                    {
                        return false;
                    }
                    if (slot != i && player.armor[i].type == ItemID.WormScarf)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
    
}