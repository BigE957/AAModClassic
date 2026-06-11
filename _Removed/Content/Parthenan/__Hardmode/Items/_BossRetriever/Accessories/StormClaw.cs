using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using AAModClassic._Content.Chaos.___PreHardmode.Items._BossGripsOfChaos.Accessories;
using Terraria.ID;
using AAModClassic._Removed.Content._Tinker.__Hardmode.Items.Accessories;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.Items._BossRetriever.Accessories
{
    [AutoloadEquip(EquipType.HandsOn)]
    public class StormClaw : BaseAAItem
    {
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 30;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.Expert;
            Item.expert = true; Item.expertOnly = true;
            Item.accessory = true;
        }
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Storm Claw");
            /* Tooltip.SetDefault(
@"For every hit you land on an enemy, 20 true damage (damage unassigned to any class) is dealt
Your non-autoswinging weapons are lightning fast"); */
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
			player.GetModPlayer<StormClawPlayer>().StormClaw = true;
        }

        public override bool CanEquipAccessory(Player player, int slot, bool modded)/* tModPorter Suggestion: Consider using new hook CanAccessoryBeEquippedWith */
        {
            if (slot < 10)
            {
                int maxAccessoryIndex = 5 + player.extraAccessorySlots;
                for (int i = 3; i < 3 + maxAccessoryIndex; i++)
                {
                    if (slot != i && player.armor[i].type == ModContent.ItemType<StormRiot>())
                    {
                        return false;
                    }
                    if (slot != i && player.armor[i].type == ModContent.ItemType<ClawOfChaos>())
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }

    public class StormClawPlayer : ModPlayer
    {
        public bool StormClaw;

        public override void ResetEffects()
        {
            StormClaw = false;
        }

        public virtual float UseTimeMultiplier(Item item, Player player)
        {
            float multiplier = 1f;

            int useTime = item.useTime;

            int useAnimate = item.useAnimation;
            if (StormClaw)
            {
                if (item.autoReuse == false)
                {
                    multiplier *= 2f;
                }
            }

            while (useTime / multiplier < 1)
            {
                multiplier -= .1f;
            }

            while (useAnimate / multiplier < 2)
            {
                multiplier -= .1f;
            }

            return multiplier;
        }

        //TODO: this should be reworked into like a BonusDamage stat on player that these accs add to
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (StormClaw)
                Player.ApplyDamageToNPC(target, 20, 0, 0, false);
        }

        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (StormClaw)
                Player.ApplyDamageToNPC(target, 20, 0, 0, false);
        }
    }
}