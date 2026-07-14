using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Map;
using Terraria.ModLoader;
using Terraria.UI;

namespace AAModClassic._Content._EX._PostMoonlord.Items.Materials
{
    public class EXSoul : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Materials";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("EX Soul");
            // Tooltip.SetDefault("Essence of ancient, arcane magic");
            // ticksperframe, frameCount
            Main.RegisterItemAnimation(Type, new DrawAnimationVertical(4, 4));
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            ItemID.Sets.AnimatesAsSoul[Type] = true;
        }

        // TODO -- Velocity Y smaller, post NewItem?
        // this is an alphakip todo i think
        public override void SetDefaults()
        {
            Item refItem = new Item();
            refItem.SetDefaults(ItemID.SoulofSight);
            Item.width = refItem.width;
            Item.height = refItem.height;
            Item.maxStack = Item.CommonMaxStack;
            Item.value = 1000000;
            Item.rare = ItemRarityID.Purple;
            Item.expert = true;
            
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Main.DiscoColor; //GConstants.COLOR_RARITYN1;
        }


        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, Main.DiscoColor.ToVector3() * 0.55f * Main.essScale);
        }
    }

    public class EXSoulMapLayer : ModMapLayer
    {
        public override Position GetDefaultPosition() => new After(IMapLayer.Pings);

        public override void Draw(ref MapOverlayDrawContext context, ref string text)
        {
            foreach (Item item in Main.ActiveItems)
            {
                if (item.type != ModContent.ItemType<EXSoul>())
                    continue;

                Texture2D tex = TextureAssets.Item[ModContent.ItemType<EXSoul>()].Value;

                Vector2 tilePosition = item.position / 16f;

                int animTime = (int)(Main.GlobalTimeWrappedHourly * 15);

                var v = context.Draw(tex, tilePosition, Main.DiscoColor, new SpriteFrame(1, 4, 0, (byte)(animTime % 4)), 1.25f, 1.25f, Alignment.Center);
                if(v.IsMouseOver)
                    text = item.Name;
            }
        }
    }
}