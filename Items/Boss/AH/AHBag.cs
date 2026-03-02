using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Boss.AH
{
    public class AHBag : BaseAAItem
	{
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Treasure Bag");
            // Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
            Item.ResearchUnlockCount = 3;
            ItemID.Sets.BossBag[Type] = true;
        }

		public override void SetDefaults()
		{
			Item.maxStack = 999;
			Item.consumable = true;
			Item.width = 36;
			Item.height = 32;
            Item.expert = true; Item.expertOnly = true;
		}

        public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
        {
            itemGroup = ContentSamples.CreativeHelper.ItemGroup.BossBags;
        }

        public override bool CanRightClick() => true;

        public override Color? GetAlpha(Color lightColor) => Color.Lerp(lightColor, Color.White, 0.4f);

        public override void PostUpdate()
        {
            // Spawn some light and dust when dropped in the world
            Lighting.AddLight(Item.Center, Color.White.ToVector3() * 0.4f);

            if (Item.timeSinceItemSpawned % 12 == 0)
            {
                Vector2 center = Item.Center + new Vector2(0f, Item.height * -0.1f);

                // This creates a randomly rotated vector of length 1, which gets it's components multiplied by the parameters
                Vector2 direction = Main.rand.NextVector2CircularEdge(Item.width * 0.6f, Item.height * 0.6f);
                float distance = 0.3f + Main.rand.NextFloat() * 0.5f;
                Vector2 velocity = new Vector2(0f, -Main.rand.NextFloat() * 0.3f - 1.5f);

                Dust dust = Dust.NewDustPerfect(center + direction * distance, DustID.SilverFlame, velocity);
                dust.scale = 0.5f;
                dust.fadeIn = 1.1f;
                dust.noGravity = true;
                dust.noLight = true;
                dust.alpha = 0;
            }
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Texture2D texture = TextureAssets.Item[Item.type].Value;
            Rectangle frame = texture.Frame();

            // Use special item animations if applicable.
            if (Main.itemAnimations[Item.type] != null)
                frame = Main.itemAnimations[Item.type].GetFrame(texture, Main.itemFrameCounter[whoAmI]);

            Vector2 frameOrigin = frame.Size() * 0.5f;
            Vector2 offset = new Vector2(Item.width / 2 - frameOrigin.X, Item.height - frame.Height);
            Vector2 drawPos = Item.position - Main.screenPosition + frameOrigin + offset;

            float localTime = Item.timeSinceItemSpawned / 240f + Main.GlobalTimeWrappedHourly * 0.04f;

            // Transform the global time value's incremental form into a unit-interval triangle wave.
            float time = Main.GlobalTimeWrappedHourly % 4f / 2f;
            if (time >= 1f)
                time = 2f - time;
            time = time * 0.5f + 0.5f;

            // Draw the outer pulse effect.
            for (int i = 0; i < 4; i++)
            {
                Vector2 pulseOffset = Vector2.UnitY.RotatedBy((i / 4f + localTime) * MathHelper.TwoPi) * time * 8f;
                spriteBatch.Draw(texture, drawPos + pulseOffset, frame, new Color(90, 70, 255, 50), rotation, frameOrigin, scale, 0, 0);
            }

            // Draw the inner pulse effect.
            for (int i = 0; i < 3; i++)
            {
                Vector2 pulseOffset = Vector2.UnitY.RotatedBy((i / 3f + localTime) * MathHelper.TwoPi) * time * 4f;
                spriteBatch.Draw(texture, drawPos + pulseOffset, frame, new Color(140, 120, 255, 77), rotation, frameOrigin, scale, 0, 0);
            }

            return true;
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            if (Main.rand.Next(10) == 0)
            {
                AAPlayer modPlayer = Main.LocalPlayer.GetModPlayer<AAPlayer>();
                modPlayer.PMLDevArmor();
            }

            string[] lootTableA = { "AshRain", "FuryFlame", "FireSpiritStaff", "AsheSatchel" };
            int lootA = Main.rand.Next(lootTableA.Length);
            Main.LocalPlayer.QuickSpawnItem(Item.GetSource_Loot(), Mod.Find<ModItem>(lootTableA[lootA]).Type);

            string[] lootTableH = { "HarukaKunai", "Masamune", "MizuArashi", "HarukaBox" };
            int lootH = Main.rand.Next(lootTableH.Length);
            Main.LocalPlayer.QuickSpawnItem(Item.GetSource_Loot(), Mod.Find<ModItem>(lootTableH[lootH]).Type);


            Main.LocalPlayer.QuickSpawnItem(Item.GetSource_Loot(), Mod.Find<ModItem>("HeartOfPassion").Type);
            Main.LocalPlayer.QuickSpawnItem(Item.GetSource_Loot(), Mod.Find<ModItem>("HeartOfSorrow").Type);
        }
	}
}