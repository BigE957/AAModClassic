using AAModClassic._Content.Acropolis.__Hardmode.Items._BossAthena.Accessories;
using AAModClassic._Content.Inferno.Buffs;
using AAModClassic._Content.Mire.___PreHardmode.Items.Materials;
using AAModClassic._Content.Mire.Buffs;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire.___PreHardmode.Items.Accessories
{
    public class AshProofVest3 : EquipAbstract, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Accessories";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ash-Proof Vest");
            // Tooltip.SetDefault(@"Lingering in the firestorm for too long will degrade this accessory and cause it to break...");
        }

        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 36;
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
            base.UpdateAccessory(player, hideVisual);

            if (Item.type == ModContent.ItemType<AshProofVest0>())
            {
                if (Main.itemAnimations[Item.type].Frame == 5)
                {
                    Item.TurnToAir();
                }
            }

            if (player.GetModPlayer<AAPlayer>().ZoneInferno && !Main.dayTime && !AAWorld.downedAkuma)
            {
                if (Main.rand.NextBool(3600))
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

        public override void RegisterEquipEffects()
        {
            AddEffect<AshProofVestEffect>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<HydraClaw_Item>(), 15);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}