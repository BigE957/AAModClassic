using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Dev
{
    public class PoniumStaff : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Ponium Staff");
            /* Tooltip.SetDefault(@"'That's a f***ing REEEEEEEEE if I've ever seen one'
-Beg"); */
			Item.staff[Item.type] = true; //this makes the useStyle animate as a staff instead of as a gun
		}

		public override void SetDefaults()
		{
			Item.damage = 170;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 7;
			Item.width = 88;
			Item.height = 88;
			Item.useTime = 15;
			Item.useAnimation = 15;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 3;
			Item.value = 1000000;
			Item.rare = ItemRarityID.Purple;
			Item.UseSound = SoundID.Item20;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<Projectiles.PonyShot>();
			Item.shootSpeed = 9f;
            Item.expert = true; Item.expertOnly = true;
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
                AAColor.Hallow,
                rotation,
                texture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f
            );
        }

        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D texture = Mod.GetTexture("Glowmasks/" + GetType().Name + "_Glow");
            spriteBatch.Draw(texture, position, null, AAColor.Hallow, 0, origin, scale, SpriteEffects.None, 0f);
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(0, 130, 150);
                }
            }
        }
    }
}