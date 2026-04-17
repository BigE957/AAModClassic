using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Localization;
using Terraria.ID;
using AAModClassic.Globals;
using AAModClassic.Buffs;
using AAModClassic.Tiles.Crafters;
using AAModClassic.___Content.Mire._PostMoonlord.Items.Armor;
using AAModClassic.___Content.Chaos._PostMoonlord.Items.Materials;

namespace AAModClassic.Items.Armor.PerfectChaos
{
    [AutoloadEquip(EquipType.Head)]
    public class PerfectChaosVisor : BaseAAItem
    {
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
            Item.rare = ItemRarityID.Cyan;
            AARarity = 14;
            Item.defense = 39;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Rarity14;
                }
            }
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<PerfectChaosPlate>() && legs.type == ModContent.ItemType<PerfectChaosGreaves>();
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
            recipe.AddTile(ModContent.TileType<ACS_Tile>());
            recipe.Register();
        }

        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D Glow = Mod.GetTexture("Glowmasks/" + GetType().Name + "_Glow");
            spriteBatch.Draw(Glow, position, null, AAColor.Shen3, 0, origin, scale, SpriteEffects.None, 0f);
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = Mod.GetTexture("Glowmasks/" + GetType().Name + "_Glow");
            spriteBatch.Draw
            (
                texture,
                new Vector2
                (
                    Item.position.X - Main.screenPosition.X + Item.width * 0.5f,
                    Item.position.Y - Main.screenPosition.Y + Item.height - texture.Height * 0.5f + 2f
                ),
                new Rectangle(0, 0, texture.Width, texture.Height),
                AAColor.Shen3,
                rotation,
                texture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f
            );
        }
    }
}