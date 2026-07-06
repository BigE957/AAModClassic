using AAModClassic._Content.Chaos._PostMoonlord.Items.Materials;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._Content.Chaos.Buffs;
using AAModClassic._Content.Inferno._PostMoonlord.Items.Armor;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using AAModClassic.Utilities.Attributes;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos._PostMoonlord.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    [AutoloadEquipGlow(EquipType.Head)]
    public class ChaosSlayerHelmetMelee : EquipAbstract, ILocalizedModType, ICustomEquipGlow
    {
        public new string LocalizationCategory => "Items.Armor.ChaosSlayer";
        public Color Color => AAColor.Shen3;

        public override Color GlowmaskDrawColor => AAColor.Shen3;

        public override void Load()
        {
            EquipLoader.AddEquipTexture(Mod, Texture + "_Head_Alt", EquipType.Head, item: this, name: $"{Name}_Head_Alt");
            AAPlayer.ModifyDrawInfoEvent += ModifyDrawInfo;
        }

        private void ModifyDrawInfo(Player player)
        {
            int red = EquipLoader.GetEquipSlot(Mod, Name, EquipType.Head);
            int blue = EquipLoader.GetEquipSlot(Mod, Name + "_Head_Alt", EquipType.Head);

            if (player.head == blue && player.direction == -1)
                player.head = red;
            else if (player.head == red && player.direction == 1)
                player.head = blue;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Chaos Slayer Kabuto");
            /* Tooltip.SetDefault(@"30% increased Melee damage & critical strike chance
5% increased damage resistance
15% increased melee speed
+25 Max Life
The power of discordian rage radiates from this armor"); */
        }

        public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 24;
			Item.value = Item.sellPrice(3, 0, 0, 0);
            Item.rare = ModContent.RarityType<SuperancientsRarity>();
            Item.defense = 44;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<ChaosSlayerChestplate>() && legs.type == ModContent.ItemType<ChaosSlayerLeggings>();
		}

        public override void RegisterEquipEffects()
        {
            damageMap.GetDamage(DamageClass.Melee) += .3f;
            damageMap.GetCritChance(DamageClass.Melee) += 30;
            damageMap.GetAttackSpeed(DamageClass.Melee) += .15f;
            AddEffect(new EnduranceEffect(0.05f));
            AddEffect(new MaxLifeEffect(25));

            AddSetEffect(new AttacksInflictBuffEffect(DamageClass.Melee, (ModContent.BuffType<DiscordianInferno_Buff>(), 300)));
            AddSetEffect<ChaosSlayerHelmetMeleeSetStatScalingEffect>();
            AddSetEffect<ChaosSlayerHelmetSetDescEffect>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DraconianSunHelmet>(), 1);
            recipe.AddIngredient(ModContent.ItemType<DiscordiumBar>(), 6);
            recipe.AddIngredient(ModContent.ItemType<ChaosScale>(), 6);
            recipe.AddTile(ModContent.TileType<AnyAncientCraftingStation_Tile>());
            recipe.Register();
        }
    }
}