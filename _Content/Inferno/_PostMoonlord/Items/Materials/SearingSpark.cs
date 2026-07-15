using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno._PostMoonlord.Items.Materials
{
    public class SearingSpark : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Materials";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Searing Spark");
            // ticksperframe, frameCount
            Main.RegisterItemAnimation(Type, new DrawAnimationVertical(6, 4));
            ItemID.Sets.ItemIconPulse[Item.type] = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            ItemID.Sets.AnimatesAsSoul[Type] = true;

            Item.ResearchUnlockCount = 15;
        }

        // TODO -- Velocity Y smaller, post NewItem?
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.maxStack = Item.CommonMaxStack;
            Item.value = 1000;
            Item.rare = ModContent.RarityType<PostEquinoxRarity>();
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, Color.Cyan.ToVector3() * 0.55f * Main.essScale);
        }
    }
}