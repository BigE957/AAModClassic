using Terraria;
using Microsoft.Xna.Framework; 
using Microsoft.Xna.Framework.Graphics; 
using Terraria.ModLoader;
using AAModClassic;

namespace AAModClassic.Removed.Items.Boss.Infinity
{
    public class IZCache : ModItem
	{
        
        public override void SetStaticDefaults()
        {
            
            // DisplayName.SetDefault("Treasure Cache");
			// Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
		}

		public override void SetDefaults()
		{
			Item.maxStack = 999;
			Item.consumable = true;
			Item.width = 36;
			Item.height = 32;
			Item.expert = true;
            //TODOIZ
			//bossBagNPC = Mod.Find<ModNPC>("Infinity").Type;
		}


        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = Mod.GetTexture("Removed/Glowmasks/" + GetType().Name + "_Glow");
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

        public override bool CanRightClick()
		{
			return true;
		}

        //TODOIZ
        /*
		public override void OpenBossBag(Player player)
		{
            if (Main.rand.Next(7) == 0)
            {
                player.QuickSpawnItem(Item.GetSource_FromThis(), Mod.Find<ModItem>("ZeroMask").Type);
            }
            if (Main.rand.NextFloat() < 0.01f)
            {
                AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
                modPlayer.SADevArmor();
            }
            player.QuickSpawnItem(Item.GetSource_FromThis(), Mod.Find<ModItem>("Infinitium").Type, Main.rand.Next(30, 40));
            player.QuickSpawnItem(Item.GetSource_FromThis(), Mod.Find<ModItem>("EXSoul").Type);
            string[] lootTable = 
            {
                "Genocide",
                "Nova",
                "Sagittarius",
                "TotalDestruction",
                "Annihilator"
                //"RiftShredder",
                //"VoidStar",
            };
            int loot = Main.rand.Next(lootTable.Length);
            player.QuickSpawnItem(Item.GetSource_FromThis(), Mod.Find<ModItem>(lootTable[loot]).Type);
        }
        */
	}
}