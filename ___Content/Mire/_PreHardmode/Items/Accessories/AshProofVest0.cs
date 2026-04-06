using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Mire._PreHardmode.Items.Accessories
{
    public class AshProofVest0 : BaseAAItem
    {
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 24;
            Item.value = Item.sellPrice(0, 8, 0, 0);
            Item.rare = ItemRarityID.Orange;
            Item.accessory = true;
        }

        public override void UpdateInventory(Player player)
        {
            if (Item.type == ModContent.ItemType<AshProofVest0>())
            {
                if (Main.itemAnimations[Item.type].Frame == 5)
                {
                    Item.TurnToAir();
                }
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (Item.type == ModContent.ItemType<AshProofVest0>())
            {
                if (Main.itemAnimations[Item.type].Frame == 5)
                {
                    Item.TurnToAir();
                }
            }
            if (Item.accessory)
            {
                player.buffImmune[ModContent.BuffType<BurningAsh>()] = true;
                if (player.GetModPlayer<AAPlayer>().ZoneInferno && !Main.dayTime && !AAWorld.downedAkuma)
                {
                    if (Main.rand.Next(3600) == 0)
                    {
                        if (Item.type == ModContent.ItemType<AshProofVest3>())
                        {
                            SoundEngine.PlaySound(SoundID.Item34);
                            Item.type = ModContent.ItemType<AshProofVest2>();
                            Item.CloneDefaults(ModContent.ItemType<AshProofVest2>());
                            Item.stack++;
                            Item.stack--;
                        }
                        else if (Item.type == ModContent.ItemType<AshProofVest2>())
                        {
                            SoundEngine.PlaySound(SoundID.Item34);
                            Item.type = ModContent.ItemType<AshProofVest1>();
                            Item.CloneDefaults(ModContent.ItemType<AshProofVest1>());
                            Item.stack++;
                            Item.stack--;
                        }
                        else
                        {
                            SoundEngine.PlaySound(SoundID.Item34);
                            Item.type = ModContent.ItemType<AshProofVest0>();
                            Item.CloneDefaults(ModContent.ItemType<AshProofVest0>());
                            Item.stack++;
                            Item.stack--;
                        }
                    }
                }
            }
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ash-Proof Vest");
            // Tooltip.SetDefault(@"Temporary accessory to completly remove Ash Rain");
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 6));
        }
    }
}