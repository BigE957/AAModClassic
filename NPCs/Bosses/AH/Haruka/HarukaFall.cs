using AAModClassic.Items.Boss.AH;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Bosses.AH.Haruka
{
    public class HarukaFall : ModNPC
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Haruka Yamata");
            Main.npcFrameCount[NPC.type] = 8;
        }

        public override void SetDefaults()
        {
            NPC.npcSlots = 1000;
            NPC.aiStyle = -1;
            NPC.defense = 1;
            NPC.knockBackResist = 0f;
            NPC.noGravity = false;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.boss = true;
            NPC.lifeMax = 1;
            NPC.dontTakeDamage = true;
            NPC.damage = 0;
            NPC.value = 0;
            Music = Mod.GetSoundSlot(SoundType.Music, "Sounds/Music/silence");
        }

        public override void AI()
        {
            NPC.dontTakeDamage = true;

            if (NPC.collideY)
            {
                NPC.ai[0]++;
                if (NPC.frame.Y < 78 * 4)
                {
                    NPC.frameCounter = 0;
                    NPC.frame.Y = 78 * 4;
                }

                if (NPC.frame.Y < 78 * 6)
                {
                    if (NPC.frameCounter++ > 5)
                    {
                        NPC.frame.Y += 78;
                        NPC.frameCounter = 0;
                    }
                }

                if (NPC.ai[0] == 60)
                {
                    CombatText.NewText(NPC.Hitbox, new Color(72, 78, 117), "..?");
                }

                if (NPC.ai[0] == 120)
                {
                    NPC.frame.Y = 78 * 7;
                }
                if (NPC.ai[0] == 180)
                {
                    CombatText.NewText(NPC.Hitbox, new Color(72, 78, 117), "...Ashe?");
                    NPC.frame.Y = 78 * 6;
                }
                if (NPC.ai[0] == 240)
                {
                    NPC.frame.Y = 78 * 7;
                }

                if (NPC.ai[0] == 360)
                {
                    CombatText.NewText(NPC.Hitbox, new Color(72, 78, 117), "...thanks for shutting her up.");

                    if (Main.expertMode)
                    {
                        Item.NewItem(NPC.GetSource_Loot(), NPC.Hitbox, ModContent.ItemType<AHBag>());
                    }

                    if (!Main.expertMode)
                    {
                        string[] lootTableH = { "HarukaKunai", "Masamune", "MizuArashi", "HarukaBox" };
                        int lootH = Main.rand.Next(lootTableH.Length);
                        NPC.DropLoot(Mod.Find<ModItem>(lootTableH[lootH]).Type);
                    }

                    if (Main.rand.Next(10) == 0)
                    {
                        Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<HarukaTrophy>());
                    }

                    NPC.NewNPC(NPC.GetSource_Death(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<HarukaVanish>(), 0, 0, 4);
                    NPC.active = false;
                    NPC.netUpdate = true;
                }
            }
            else
            {
                if (NPC.frameCounter++ > 6)
                {
                    NPC.frameCounter = 0;
                    NPC.frame.Y += 78;
                    if (NPC.frame.Y > 78 * 3)
                    {
                        NPC.frame.Y = 0;
                    }
                }
            }
        }
    }
}