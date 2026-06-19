using AAModClassic._Content.Chaos._PostMoonlord.Items.Materials;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._Content.Mire._PostMoonlord.Items.Armor;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.Attributes;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos._PostMoonlord.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    [AutoloadEquipGlow(EquipType.Head)]
    public class ChaosSlayerHelmetRanged : BaseAAItem, ILocalizedModType, ICustomEquipGlow
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
            // DisplayName.SetDefault("Chaos Slayer Visor");
            /* Tooltip.SetDefault(@"45% increased ranged damage
38% increased ranged critical strike chance
3% increased damage resistance
25% reduced ammo consumption
+15 Max Life
The power of discordian rage radiates from this hood"); */
        }

        public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 24;
			Item.value = Item.sellPrice(3, 0, 0, 0);
            Item.rare = ModContent.RarityType<SuperancientsRarity>();
            Item.defense = 39;
        }
        
        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<ChaosSlayerChestplate>() && legs.type == ModContent.ItemType<ChaosSlayerLeggings>();
		}

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = Language.GetTextValue("Mods.AAModClassic.Common.PerfectChaosVisorBonus");
            player.GetModPlayer<AAPlayer>().perfectChaosRa = true;
            player.AddBuff(ModContent.BuffType<ChaosWrath_Buff>(), 2);
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Ranged) += .45f;
            player.GetCritChance(DamageClass.Ranged) += 38;
            player.endurance += .03f;
            player.ammoCost75 = true;
            player.statLifeMax2 += 15;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DreadMoonHelmet>(), 1);
            recipe.AddIngredient(ModContent.ItemType<DiscordiumBar>(), 6);
            recipe.AddIngredient(ModContent.ItemType<ChaosScale>(), 6);
            recipe.AddTile(ModContent.TileType<AnyAncientCraftingStation_Tile>());
            recipe.Register();
        }
    }
}