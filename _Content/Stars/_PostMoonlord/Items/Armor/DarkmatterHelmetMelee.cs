using AAModClassic._Content.Stars._PostMoonlord.Items.Materials;
using AAModClassic._Content.Stars._PostMoonlord.Items.Tiles.Functional;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using AAModClassic.Utilities.Attributes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Stars._PostMoonlord.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    [AutoloadEquipGlow(EquipType.Head)]
    public class DarkmatterHelmetMelee : EquipAbstract, ILocalizedModType, ICustomEquipGlow
    {
        public new string LocalizationCategory => "Items.Armor.Darkmatter";
        public Color Color => AAColor.Nightcrawler;

        public bool Condition(Player p) => !Main.dayTime && p.GetModPlayer<AAPlayer>().DarkmatterSet;

        public static Asset<Texture2D> Glowmask;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Darkmatter Helmet");
            /* Tooltip.SetDefault(@"10% increased melee damage
Dark, yet still barely visible"); */

            Glowmask = ModContent.Request<Texture2D>(Texture + "_Glow");
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = Glowmask.Value;
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

        public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 20;
			Item.value = 300000;
			Item.defense = 34;
            Item.rare = ModContent.RarityType<PostEquinoxRarity>();
        }

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<DarkmatterChestplate>() && legs.type == ModContent.ItemType<DarkmatterLeggings>();
		}

        public override void RegisterEquipStats()
        {
            damageMap.GetDamage(DamageClass.Melee) += 0.10f;

            AddSetEffect<DarkmatterHelmetMeleeSetEffect>();
            AddSetEffect<DrawShadowLokisEffect>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DarkmatterBar>(), 25);
            recipe.AddIngredient(ModContent.ItemType<DarkEnergy>(), 10);
            recipe.AddTile(ModContent.TileType<QuantumFusionAccelerator_Tile>());
            recipe.Register();
        }
	}
    
}