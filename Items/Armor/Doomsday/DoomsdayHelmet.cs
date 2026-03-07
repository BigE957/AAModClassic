using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.Localization;
using AAModClassic;
using AAModClassic.Globals;


namespace AAModClassic.Items.Armor.Doomsday
{
    [AutoloadEquip(EquipType.Head)]
	public class DoomsdayHelmet : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Doomsday Assault Visor");
			/* Tooltip.SetDefault(@"25% increased magic damage
18% increased magic critical strike chance
The power to destroy entire planets rests in this armor"); */
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
			Item.width = 18;
			Item.height = 18;
			Item.value = 3000000;
			Item.defense = 32;
            Item.rare = ItemRarityID.Cyan;
            AARarity = 13;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Rarity13;
                }
            }
        }

        public override void UpdateEquip(Player player)
		{
            player.GetDamage(DamageClass.Magic) += .25f;
            player.GetCritChance(DamageClass.Magic) += 18;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == Mod.Find<ModItem>("DoomsdayChestplate").Type && legs.type == Mod.Find<ModItem>("DoomsdayLeggings").Type;
		}

		public override void UpdateArmorSet(Player player)
		{
			
			player.setBonus = Language.GetTextValue("Mods.AAModClassic.Common.DoomsdayHelmetBonus");

            player.manaCost *= .7f;
            player.AddBuff(BuffID.Hunter, 2);
            player.AddBuff(BuffID.NightOwl, 2);
            player.GetModPlayer<AAPlayer>().zeroSet = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "ApocalyptitePlate", 15);
            recipe.AddIngredient(null, "UnstableSingularity", 5);
            recipe.AddTile(null, "ACS");
			recipe.Register();
		}
	}
}