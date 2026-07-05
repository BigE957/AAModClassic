using AAModClassic._Content.Acropolis.__Hardmode.Items._BossAthena.Accessories;
using AAModClassic._Content.Desert._PostMoonlord.Items.Materials;
using AAModClassic._Content.Inferno.__Hardmode.Items.Accessories;
using AAModClassic._Content.OldOnesArmy.___PreHardmode.Items.Accessories;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._Tinker._PostMoonlord.Items.Accessories
{
    [AutoloadEquip(EquipType.Face)]
    public class CursedEyeOfTheSoulBinder : EquipAbstract, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Accessories";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Cursed Eye of the Soul Binder");
            /* Tooltip.SetDefault(@"Increase 21% minion damage
Increase your max number of minions
Increase your max number of sentries
+50 Max Life
Your minions can strike the enemy's soul
While Old One's Army is on, increase 31% minion damage."); */
        }

        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 56;
            Item.value = Item.sellPrice(0, 30, 0, 0);
            Item.rare = ItemRarityID.Cyan;
            Item.accessory = true;
        }

        public override void RegisterEquipStats()
        {
            damageMap.GetDamage(DamageClass.Summon) += .21f;
            AddEffect(new MaxMinionSlotEffect(1));
            AddEffect(new MaxSentrySlotEffect(1));
            AddEffect(new MaxLifeEffect(50));
            AddEffect(new OldOneCharmEffect(0.31f));

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