using AAModClassic._Content._Tinker.__Hardmode.Items.Accessories;
using AAModClassic._Content.Bunny._PostMoonlord.Items._BossRajahRabbitA.Accessories;
using AAModClassic._Content.Stars._PostMoonlord.Items.Materials;
using AAModClassic._Content.Stars._PostMoonlord.Items.Tiles.Functional;
using AAModClassic.UI.World;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Removed.Content._Tinker._PostMoonlord.Items.Accessories
{
    [AutoloadEquip(EquipType.HandsOn)]
    public class DwarvenGauntlet : EquipAbstract, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Accessories";
        public override void SetStaticDefaults()
        {
            /*DisplayName.SetDefault("Dwarven Gauntlet");
            Tooltip.SetDefault(
@"Enemies are much more likely to target you
18% Increased Melee Damage and 
12% increased melee speed
Increased Melee Knockback
Having this gauntlet allows you to handle the infinity stones without overloading
'Fine. I'll do it myself.'");*/
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 44;
            Item.value = Item.sellPrice(0, 30, 0, 0);
            Item.rare = ItemRarityID.Purple;
            Item.accessory = true;
            Item.defense = 12;
        }

        public override void RegisterEquipEffects()
        {
            damageMap.GetDamage(DamageClass.Melee) += 0.18f;
            damageMap.GetAttackSpeed(DamageClass.Melee) += 0.12f;
            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                AddEffect(new AggroEffect(800));
            else
                AddEffect(new AggroEffect(8));
            AddEffect<DwarvenGauntletEffect>();
        }

        public override void AddRecipes()
        {
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient<DemonGauntlet>();
                recipe.AddIngredient(ItemID.FragmentNebula, 5);
                recipe.AddIngredient(ItemID.FragmentSolar, 5);
                recipe.AddIngredient(ItemID.FragmentVortex, 5);
                recipe.AddIngredient(ItemID.FragmentStardust, 5);
                recipe.AddIngredient<DarkmatterBar>(10);
                recipe.AddIngredient<RadiumBar>(10);
                recipe.AddTile<QuantumFusionAccelerator_Tile>();
                recipe.Register();
            }
        }

    }
}