using AAModClassic.___Content.Inferno._PostMoonlord.Items.Materials;
using AAModClassic.Items.Vanity.Mask;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Boss.Akuma
{
    public class AkumaBag : BaseAAItem
    {
        public static Asset<Texture2D> Glowmask;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Treasure Bag");
            // Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");

            Glowmask = ModContent.Request<Texture2D>("AAModClassic/Glowmasks/" + GetType().Name + "_Glow");

            Item.ResearchUnlockCount = 3;
            ItemID.Sets.BossBag[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.width = 32;
            Item.height = 32;
            Item.expert = true; Item.expertOnly = true;
            Item.rare = ItemRarityID.Red;
        }

        public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
        {
            itemGroup = ContentSamples.CreativeHelper.ItemGroup.BossBags;
        }
        //public override int BossBagNPC => ModContent.NPCType<AkumaA>();

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

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            if (Main.rand.NextBool(10))
            {
                AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
                modPlayer.PMLDevArmor();
            }
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<AkumaMask>(), 7));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<CrucibleScale>(), 1, 30, 40));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<TaiyangBaolei>()));

            int[] lootTable = { ModContent.ItemType<AkumaTerratool>(), ModContent.ItemType<Daystorm>(), ModContent.ItemType<LungStaff>(), ModContent.ItemType<MorningGlory>(), ModContent.ItemType<RadiantDawn>(), ModContent.ItemType<Solar>(), ModContent.ItemType<SunSpear>(), ModContent.ItemType<ReignOfFire>(), ModContent.ItemType<DaybreakArrow>(), ModContent.ItemType<Daycrusher>(), ModContent.ItemType<Dawnstrike>(), ModContent.ItemType<SunStorm>(), ModContent.ItemType<SunStaff>(), ModContent.ItemType<DragonSlasher>() };

            itemLoot.Add(ItemDropRule.OneFromOptions(1, lootTable));
        }
    }
}