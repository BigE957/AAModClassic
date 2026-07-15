using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire._PostMoonlord.Items.Materials
{
    public class TerrorSoul : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Materials";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Terror Soul");
            Main.RegisterItemAnimation(Type, new DrawAnimationVertical(6, 3));
            ItemID.Sets.ItemIconPulse[Type] = true;
            ItemID.Sets.ItemNoGravity[Type] = true;
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
            Lighting.AddLight(Item.Center, Color.PaleVioletRed.ToVector3() * 0.55f * Main.essScale);
        }
    }
}