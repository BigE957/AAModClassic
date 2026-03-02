
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.AH
{
    public class AHSpawn : ModNPC
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Sisters of Discord");
            Terraria.ID.NPCID.Sets.ShouldBeCountedAsBoss[NPC.type] = true;
        }

        public override void SetDefaults()
        {
            NPC.dontTakeDamage = true;
            NPC.lifeMax = 1;
            {
                NPC.width = 100;
                NPC.height = 100;
                NPC.friendly = false;
                NPC.lifeMax = 1;
                NPC.dontTakeDamage = true;
                NPC.noGravity = true;
                NPC.aiStyle = -1;
                NPC.timeLeft = 10;
                
                for (int k = 0; k < NPC.buffImmune.Length; k++)
                {
                    NPC.buffImmune[k] = true;
                }
            }
        }
        public bool ATransitionActive = false;
        public int RVal = 255;
        public int BVal = 0;

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(RVal, 125, BVal);
        }

        public override void AI()
        {
            NPC.TargetClosest();
            Player player = Main.player[NPC.target];
            if (AAConfigClient.Instance.NoBossDialogue)
            {
                SpawnBoss(player, "Ashe");
                SpawnBoss2(player, "Haruka");
                NPC.active = false;
            }
            NPC.ai[1]++;

            NPC.Center = player.Center;

            if (NPC.ai[1] == 60)          //if the timer has gotten to 7.5 seconds, this happens (60 = 1 second)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Lang.BossChat("AHSpawn1"), new Color(102, 20, 48));
                Music = Mod.GetSoundSlot(SoundType.Music, "Sounds/Music/ChaosSissy");
            }

            if (NPC.ai[1] == 300)
            {
                if (AAWorld.downedBrood)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Lang.BossChat("AHSpawn2"), new Color(102, 20, 48));
                }
                else
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Lang.BossChat("AHSpawn3"), new Color(102, 20, 48));
                }
            }

            if (NPC.ai[1] == 500)
            {
                if (AAWorld.downedHydra)
                {
                    if (AAWorld.downedBrood)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Lang.BossChat("AHSpawn4"), new Color(72, 78, 117));
                    }
                    else
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Lang.BossChat("AHSpawn5"), new Color(72, 78, 117));
                    }
                }
                else
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Lang.BossChat("AHSpawn6"), new Color(72, 78, 117));
                }
            }

            if (NPC.ai[1] == 550)
            {
                NPC.NewNPC((int)NPC.position.X - 200, (int)NPC.position.Y - 150, Mod.Find<ModNPC>("AsheSpawn").Type, 0, NPC.whoAmI);
            }

            if (NPC.ai[1] == 700)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Lang.BossChat("AHSpawn7"), new Color(102, 20, 48));
            }

            if (NPC.ai[1] == 550)
            {
                NPC.NewNPC((int)NPC.position.X + 200, (int)NPC.position.Y - 150, Mod.Find<ModNPC>("HarukaSpawn").Type, 0, NPC.whoAmI);
            }

            if (NPC.ai[1] == 820)
            {
                Music = Mod.GetSoundSlot(SoundType.Music, "Sounds/Music/AH");
                Main.npc[BaseAI.GetNPC(NPC.Center, Mod.Find<ModNPC>("AsheSpawn").Type, -1)].Transform(Mod.Find<ModNPC>("Ashe").Type);
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Lang.BossChat("AHSpawn8"), new Color(102, 20, 48));
                SpawnBoss(player, "Ashe");
            }

            if (NPC.ai[1] >= 960)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Lang.BossChat("AHSpawn9"), new Color(72, 78, 117));
                Main.npc[BaseAI.GetNPC(NPC.Center, Mod.Find<ModNPC>("HarukaSpawn").Type, -1)].Transform(Mod.Find<ModNPC>("Haruka").Type);
                SpawnBoss(player, "Haruka");
                NPC.active = false;
            }
        }

        public void SpawnBoss(Player player, string name)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                int bossType = Mod.Find<ModNPC>(name).Type;
                if (NPC.AnyNPCs(bossType)) { return; } //don't spawn if there's already a boss!
                int npcID = NPC.NewNPC((int)player.Center.X, (int)player.Center.Y, bossType, 0);
                Main.npc[npcID].Center = player.Center - new Vector2(MathHelper.Lerp(-100f, 100f, (float)Main.rand.NextDouble()), 800f);
                Main.npc[npcID].netUpdate2 = true; Main.npc[npcID].netUpdate = true;
            }
        }

        public void SpawnBoss2(Player player, string name)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                int bossType = Mod.Find<ModNPC>(name).Type;
                if (NPC.AnyNPCs(bossType)) { return; } //don't spawn if there's already a boss!
                int npcID = NPC.NewNPC((int)player.Center.X, (int)player.Center.Y, bossType, 0);
                Main.npc[npcID].Center = player.Center - new Vector2(800f, 0);
                Main.npc[npcID].netUpdate2 = true; Main.npc[npcID].netUpdate = true;
            }
        }


    }
}