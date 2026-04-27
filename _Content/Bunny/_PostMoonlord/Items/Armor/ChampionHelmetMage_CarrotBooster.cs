using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;

namespace AAModClassic._Content.Bunny._PostMoonlord.Items.Armor
{
    public class ChampionHelmetMage_CarrotBooster : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Carrot Booster");
            // Tooltip.SetDefault("Etheral, but crunchy.");
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 4));
            ItemID.Sets.AnimatesAsSoul[Item.type] = true;
            ItemID.Sets.ItemIconPulse[Item.type] = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = Item.CommonMaxStack;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return AAColor.Rainbow2;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, AAColor.Rainbow2.ToVector3() * 0.55f * Main.essScale);
        }

        public override void GrabRange(Player player, ref int grabRange)
        {
            grabRange += 100;
        }

        public override bool OnPickup(Player player)
        {
            SoundEngine.PlaySound(SoundID.Grab, player.position);
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                player.GetModPlayer<AAPlayer>().CarrotLevelup();
            }
            Item.TurnToAir();
            return true;
        }
    }
}