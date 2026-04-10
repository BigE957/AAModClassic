using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using AAModClassic.Items.Boss;
using AAModClassic.Items.Vanity.Mask;

namespace AAModClassic._Unreleased.Content.Void._PostMoonLord.Items.InfinityZero.BossStandard
{
    public class InfinityZeroTreasureBag : ModItem
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

        public override bool CanRightClick()
		{
			return true;
		}

		public override void RightClick(Player player)
		{
            if (Main.rand.Next(7) == 0)
            {
                //TODOIZ erm, maskless bozo alert
                player.QuickSpawnItem(Item.GetSource_FromThis(), ModContent.ItemType<ZeroMask>());
            }
            if (Main.rand.NextFloat() < 0.01f)
            {
                AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
                modPlayer.SADevArmor();
            }
            player.QuickSpawnItem(Item.GetSource_FromThis(), ModContent.ItemType<Infinitium>(), Main.rand.Next(30, 40));
            player.QuickSpawnItem(Item.GetSource_FromThis(), ModContent.ItemType<EXSoul>());
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
	}
}