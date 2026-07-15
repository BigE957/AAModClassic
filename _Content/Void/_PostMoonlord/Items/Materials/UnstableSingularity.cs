using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void._PostMoonlord.Items.Materials
{
    public class UnstableSingularity : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Materials";
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Unstable Singularity");
            // Tooltip.SetDefault("Barely stable enough to hold");
            // ticksperframe, frameCount
            Main.RegisterItemAnimation(Type, new DrawAnimationVertical(5, 6));
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            ItemID.Sets.AnimatesAsSoul[Type] = true;

            Item.ResearchUnlockCount = 15;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return AAColor.COLOR_WHITEFADE1;
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 24;
            Item.maxStack = Item.CommonMaxStack;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = ModContent.RarityType<AncientsRarity>();
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, Color.Red.ToVector3() * 0.55f * Main.essScale);
        }
    }
}