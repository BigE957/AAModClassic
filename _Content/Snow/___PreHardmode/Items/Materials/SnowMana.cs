using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Snow.___PreHardmode.Items.Materials
{
    public class SnowMana : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Materials";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Snow Mana");
            // ticksperframe, frameCount
            Main.RegisterItemAnimation(Type, new DrawAnimationVertical(5, 7));
            ItemID.Sets.ItemIconPulse[Item.type] = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            ItemID.Sets.AnimatesAsSoul[Type] = true;

            Item.ResearchUnlockCount = 25;
        }

        // TODO -- Velocity Y smaller, post NewItem?
        public override void SetDefaults()
        {
            Item refItem = new Item();
            refItem.SetDefaults(ItemID.SoulofSight);
            Item.width = refItem.width;
            Item.height = refItem.height;
            Item.maxStack = Item.CommonMaxStack;
            Item.value = 1000;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, Color.Cyan.ToVector3() * 0.55f * Main.essScale);
        }
    }
}