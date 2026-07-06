using AAModClassic._Content.Acropolis.__Hardmode.Items._BossAthena.Accessories;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Materials;
using AAModClassic._Content.Mire.Buffs;
using AAModClassic.Globals;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno.___PreHardmode.Items.Accessories
{
    public class Lantern : EquipAbstract, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Accessories";
        public static Asset<Texture2D> Glowmask;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Lantern");

            Glowmask = ModContent.Request<Texture2D>(Texture + "_Glow");
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 32;
            Item.value = Item.sellPrice(0, 8, 0, 0);
            Item.rare = ItemRarityID.Orange;
            Item.accessory = true;
        }

        public override void RegisterEquipEffects()
        {
            AddEffect<LanternEffect>();
            AddEffect(new EmitLightFromPlayerEffect(AAColor.Lantern.R / 255, AAColor.Lantern.G / 255 * 0.95f, AAColor.Lantern.B / 255 * 0.8f));
        }

        //TODO: cant this be removed
        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D texture = Glowmask.Value;
            Texture2D texture2 = TextureAssets.Item[Item.type].Value;
            spriteBatch.Draw(texture2, position, null, drawColor, 0, origin, scale, SpriteEffects.None, 0f);
            for (int i = 0; i < 4; i++)
            {
                //Vector2 offsetPositon = Vector2.UnitY.RotatedBy(MathHelper.PiOver2 * i) * 2;
                spriteBatch.Draw(texture, position, null, AAColor.Lantern, 0, origin, scale, SpriteEffects.None, 0f);

            }

            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DragonClaw_Item>(), 15);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}