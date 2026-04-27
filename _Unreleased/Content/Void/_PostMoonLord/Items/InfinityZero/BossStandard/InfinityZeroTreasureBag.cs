using AAModClassic.Items.Boss;
using AAModClassic.Items.Vanity.Mask;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.Void._PostMoonLord.Items.InfinityZero.BossStandard
{
    public class InfinityZeroTreasureBag : ModItem
	{
        public override void SetStaticDefaults()
        {

            // DisplayName.SetDefault("Treasure Cache");
            // Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");

            Item.ResearchUnlockCount = 3;
            ItemID.Sets.BossBag[Type] = true;
        }

		public override void SetDefaults()
		{
			Item.maxStack = Item.CommonMaxStack;
			Item.consumable = true;
			Item.width = 36;
			Item.height = 32;
            Item.expert = true;
		}

        public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
        {
            itemGroup = ContentSamples.CreativeHelper.ItemGroup.BossBags;
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
            if (Main.rand.NextBool(7))
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