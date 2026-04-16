using Terraria;
using Terraria.ID;
using Terraria.GameContent.Events;
using Terraria.ModLoader;
using AAModClassic.Items.Boss.Anubis.Forsaken;
using AAModClassic.___Content.Inferno.__Hardmode.Items.Accessories;
using AAModClassic.Items.Accessories;

namespace AAModClassic.___Content.Tinkers._PostMoonlord.Items.Accessories
{
    [AutoloadEquip(EquipType.Face)]
    public class CursedEyeofSoulBinder : BaseAAItem
    {
        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 56;
            Item.value = Item.sellPrice(0, 30, 0, 0);
            Item.rare = ItemRarityID.Cyan;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Summon) += .21f;
            player.maxMinions += 1;
            player.maxTurrets ++;
            player.statLifeMax2 += 50;
            if(DD2Event.Ongoing) player.GetDamage(DamageClass.Summon) += .1f;
            player.GetModPlayer<AAPlayer>().OldOneCharm = true;
            player.GetModPlayer<AAPlayer>().CursedEyeofSoulBinder = true;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Cursed Eye of the Soul Binder");
            /* Tooltip.SetDefault(@"Pressing the accessory ability hotkey helps player skip the time between old one army two waves.
Increase 21% minion damage
Increase your max number of minions
Increase your max number of sentries
+50 Max Life
Your minions can strike the enemy's soul
While Old One's Army is on, increase 31% minion damage."); */
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SummonerEmblem, 1);
            recipe.AddIngredient(ModContent.ItemType<OldOneCharm>(), 1);
            recipe.AddIngredient(ItemID.PygmyNecklace, 1);
            recipe.AddIngredient(ModContent.ItemType<OrnateBand>(), 1);
            recipe.AddIngredient(ItemID.SpectreBar, 10);
            recipe.AddIngredient(ModContent.ItemType<SoulFragment>(), 10);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }

    }
}