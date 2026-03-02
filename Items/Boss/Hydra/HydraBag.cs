using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Hydra
{
    public class HydraBag : BaseAAItem
	{
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Treasure Bag");
			// Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
		}

		public override void SetDefaults()
		{
			Item.maxStack = 999;
			Item.consumable = true;
			Item.width = 36;
			Item.height = 32;
			Item.expert = true; Item.expertOnly = true;
		}

        public override int BossBagNPC => Mod.Find<ModNPC>("Hydra").Type;

        public override bool CanRightClick()
		{
			return true;
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

        public override void OpenBossBag(Player player)
		{
            if (Main.rand.Next(7) == 0)
            {
                player.QuickSpawnItem(Mod.Find<ModItem>("HydraMask1").Type);
            }
            else if (Main.rand.Next(7) == 1)
            {
                player.QuickSpawnItem(Mod.Find<ModItem>("HydraMask2").Type);
            }
            else if(Main.rand.Next(7) == 2)
            {
                player.QuickSpawnItem(Mod.Find<ModItem>("HydraMask3").Type);
            }
            if (Main.rand.Next(10) == 0)
            {
                AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
                modPlayer.PHMDevArmor();
            }
            player.QuickSpawnItem(Mod.Find<ModItem>("Abyssium").Type, Main.rand.Next(75, 125));
            player.QuickSpawnItem(Mod.Find<ModItem>("HydraHide").Type, Main.rand.Next(50, 100));
            player.QuickSpawnItem(Mod.Find<ModItem>("HydraPendant").Type);
        }
	}
}