using AAModClassic._Unofficial.Content.Bunny.___PreHardmode.Items;
using AAModClassic.Utilities;
using System;
using Terraria.Audio;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unofficial.Content.Bunny.___PreHardmode.NPCs
{
    public class WoundedBunny : ModNPC
    {
        public override void SetStaticDefaults()
        {
            this.HideFromBestiary();
        }

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.Bunny);
            
            NPC.aiStyle = -1;
            AIType = -1;
            Banner = NPCID.Bunny;

            NPC.dontTakeDamageFromHostiles = true;
        }

        public override void AI()
        {
            if (NPC.velocity.X != 0)
            {
                if (MathF.Abs(NPC.velocity.X) <= 0.1f)
                    NPC.velocity.X = 0f;
                else
                    NPC.velocity.X -= 0.1f * MathF.Sign(NPC.velocity.X);
            }

            if (NPC.ai[0] > 1800)
            {
                NPC.ai[0] = 0;
                NPC.StrikeInstantKill();
                return;
            }
            NPC.ai[0]++;
        }

        public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
        {
            NPC.Transform(NPCID.Bunny);
        }

        public override bool PreHoverInteract(bool mouseIntersects)
        {
            if (!mouseIntersects)
                return true;

            Player player = Main.LocalPlayer;
            player.noThrow = 2;
            player.cursorItemIconEnabled = true;
            Item lowestHeal = GetLowestHealingPotion(player);

            if (lowestHeal == null)
            {
                player.cursorItemIconID = ItemID.LesserHealingPotion;
                return false;
            }

            player.cursorItemIconID = lowestHeal.type;

            if (!player.dead && Main.mouseRight)
            {
                if (PlayerInput.UsingGamepad)
                    player.releaseInventory = false;

                if (!player.tileInteractionHappened)
                {
                    if (--lowestHeal.stack <= 0)
                        lowestHeal.TurnToAir();

                    NPC.Transform(NPCID.Bunny);
                    NPC.life = NPC.lifeMax;
                    NPC.GetGlobalNPC<BunnyDeathGlobalNPC>().BeenHealed = true;
                    SoundEngine.PlaySound(SoundID.Item4);
                    player.ModifyRajahReputation(0.05f);
                }
            }

            return false;
        }

        private static Item GetLowestHealingPotion(Player p)
        {
            Item result = null;
            int lowestHeal = int.MaxValue;
            int num3 = 58;
            if (p.useVoidBag())
                num3 = 98;

            for (int i = 0; i < num3; i++)
            {
                Item item = (i >= 58) ? p.bank4.item[i - 58] : p.inventory[i];
                if (item.stack <= 0 || item.type <= ItemID.None || !item.potion || item.healLife <= 0)
                    continue;

                int heal = p.GetHealLife(item, true);
                if(heal >= 50 && lowestHeal > heal)
                    result = item;
            }

            return result;
        }

        public override void OnKill()
        {
            Main.NewText(NPC.type == NPCID.Bunny);
            //Main.BestiaryTracker.Kills.RegisterKill(ContentSamples.NpcsByNetId[NPCID.Bunny]);
        }
    }
}
