using AAModClassic;
using AAModClassic._Content.Acropolis.__Hardmode.Items._BossAthena.Accessories;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._Content.Void._PostMoonlord.Items.Armor;
using AAModClassic._Unreleased.Content.Void._PostMoonLord.Items._BossInfinityZero;
using AAModClassic._Unreleased.Content.Void.Buffs;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using AAModClassic.Utilities.Attributes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.Void._PostMoonLord.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    [AutoloadEquipGlow(EquipType.Head)]
	public class InfinitySlayerHelmet : EquipAbstract, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Armor.InfinitySlayer";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Infinity Slayer Visor");
			/* Tooltip.SetDefault(@" power and malice flows through this armor'"); */

		}

		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 22;
            Item.value = Item.sellPrice(3, 0, 0, 0);
            Item.defense = 40;
            Item.rare = ModContent.RarityType<SuperancientsRarity>();
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.IZ;
                }
            }
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<InfinitySlayerChestplate>() && legs.type == ModContent.ItemType<InfinitySlayerLeggings>();
		}

        public override void RegisterEquipEffects()
        {
            damageMap.GetDamage(DamageClass.Ranged) += 0.35f;
            AddEffect(new EnduranceEffect(0.12f));
            AddEffect<AmmoCost75Effect>();

            AddSetEffect<HunterEffect>();
            AddSetEffect<DangersenseEffect>();
            AddSetEffect(new AttacksInflictBuffEffect(null, (ModContent.BuffType<InfinityScorch_Buff>(), 300)));
            AddSetEffect<InfinitySlayerHelmetSetDescEffect>();
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DoomsdayHelmetMage>(), 1);
            recipe.AddIngredient(ModContent.ItemType<Infinitium>(), 12);
            recipe.AddTile(ModContent.TileType<AnyAncientCraftingStation_Tile>());
			recipe.Register();

            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(ModContent.ItemType<DoomsdayHelmetSummoner>(), 1);
            recipe2.AddIngredient(ModContent.ItemType<Infinitium>(), 12);
            recipe2.AddTile(ModContent.TileType<AnyAncientCraftingStation_Tile>());
            recipe2.Register();
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = ModContent.Request<Texture2D>(Texture + "_Glow").Value;
            spriteBatch.Draw
            (
                texture,
                new Vector2
                (
                    Item.position.X - Main.screenPosition.X + Item.width * 0.5f,
                    Item.position.Y - Main.screenPosition.Y + Item.height - texture.Height * 0.5f + 2f
                ),
                new Rectangle(0, 0, texture.Width, texture.Height),
                Color.White,
                rotation,
                texture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f
            );
        }
    }

    public class InfinitySlayerHelmetSetDescEffect : EquipmentEffectData
    {

    }
}