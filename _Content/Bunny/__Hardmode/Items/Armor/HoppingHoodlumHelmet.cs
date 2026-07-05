using AAModClassic._Content.Acropolis.__Hardmode.Items._BossAthena.Accessories;
using AAModClassic._Content.Bunny.__Hardmode.Items.Materials;
using AAModClassic.Globals;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using AAModClassic.Utilities.Attributes;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Bunny.__Hardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    [AutoloadEquipGlow(EquipType.Head)]
    public class HoppingHoodlumHelmet : EquipAbstract, ILocalizedModType, ICustomEquipGlow
    {
        public new string LocalizationCategory => "Items.Armor.HoppingHoodlum";
        public Color Color => AAColor.COLOR_WHITEFADE1;

        public bool Condition(Player p) => p.statLife < (p.statLifeMax2 / 2);

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Hopping Hoodlum Hood");
            /* Tooltip.SetDefault(@"'Hopping Mad'"); */
        }

        public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 24;
			Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.Yellow;
            Item.defense = 13;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<HoppingHoodlumChestplate>() && legs.type == ModContent.ItemType<HoppingHoodlumLeggings>();
		}

        public override void RegisterEquipStats()
        {
            damageMap.GetDamage(DamageClass.Melee) += .18f;
            damageMap.GetDamage(DamageClass.Summon) += .18f;
            AddEffect(new AggroEffect(2));

            AddSetEffect<HoppingHoodlumHelmetSetEffect>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<RajahPelt>(), 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}