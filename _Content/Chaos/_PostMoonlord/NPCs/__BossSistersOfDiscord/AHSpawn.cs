using AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossSistersOfDiscord.Ashe;
using AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossSistersOfDiscord.Haruka;
using AAModClassic._Content.Inferno.___PreHardmode.NPCs.__BossBroodmother;
using AAModClassic._Content.Mire.___PreHardmode.NPCs.__BossHydra;
using AAModClassic.Base;
using AAModClassic.Music;
using AAModClassic.UI.Dialogue;
using AAModClassic.UI.Dialogue.DisplayEffects;
using AAModClassic.UI.World;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossSistersOfDiscord
{
    public class AHSpawn : ModNPC
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Sisters of Discord");
            NPCID.Sets.ShouldBeCountedAsBoss[NPC.type] = true;
            this.HideFromBestiary();
        }

        public override void SetDefaults()
        {
            NPC.dontTakeDamage = true;
            NPC.lifeMax = 1;
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
                return;
            }
            NPC.ai[1]++;

            NPC.Center = player.Center;
            NPC.netOffset = Vector2.Zero;

            if(WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                switch (NPC.ai[1])
                {
                    case 60:
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            if (NPCExtensions.BeenKilled<HydraBody>() && NPCExtensions.BeenKilled<Broodmother>())
                                WorldTextSystem.StartDialogue("Mods.AAModClassic.WorldText.Sisters.Intro.BothKilled", NPC, uptime: 30, effects: new NearbyBossText());
                            else if (NPCExtensions.BeenKilled<HydraBody>())
                                WorldTextSystem.StartDialogue("Mods.AAModClassic.WorldText.Sisters.Intro.HydraKilled", NPC, uptime: 30, effects: new NearbyBossText());
                            else if (NPCExtensions.BeenKilled<Broodmother>())
                                WorldTextSystem.StartDialogue("Mods.AAModClassic.WorldText.Sisters.Intro.BroodmotherKilled", NPC, uptime: 30, effects: new NearbyBossText());
                            else
                                WorldTextSystem.StartDialogue("Mods.AAModClassic.WorldText.Sisters.Intro.NoneKilled", NPC, uptime: 30, effects: new NearbyBossText());
                        }
                        Music = MusicManagementSystem.MusicSlots["Sisters_Intro"];
                        NPC.boss = true;
                        break;

                    case 420:
                        NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X - 200, (int)NPC.position.Y - 150, ModContent.NPCType<AsheSpawn>(), 0, NPC.whoAmI);
                        break;

                    case 600:
                        NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X + 200, (int)NPC.position.Y - 150, ModContent.NPCType<HarukaSpawn>(), 0, NPC.whoAmI);
                        break;

                    case 1140:
                        MusicUtils.InstantSwitchMusic(MusicManagementSystem.MusicSlots["Sisters"]);
                        NPC.boss = false;
                        Music = -1;
                        Main.npc[BaseAI.GetNPC(NPC.Center, ModContent.NPCType<AsheSpawn>(), -1)].Transform(ModContent.NPCType<Ashe.Ashe>());
                        SpawnBoss(player, "Ashe");
                        break;

                    case 1260:
                        Main.npc[BaseAI.GetNPC(NPC.Center, ModContent.NPCType<HarukaSpawn>(), -1)].Transform(ModContent.NPCType<Haruka.Haruka>());
                        SpawnBoss(player, "Haruka");
                        NPC.active = false;
                        break;
                }
            else
                switch (NPC.ai[1])
                {
                    case 60:
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SistersOfDiscord.Spawn.1"), new Color(102, 20, 48));
                        Music = MusicManagementSystem.MusicSlots["Sisters_Intro"];
                        NPC.boss = true;
                        break;
                    case 360:
                        if (NPCExtensions.BeenKilled<Broodmother>())
                        {
                            if (Main.netMode != NetmodeID.MultiplayerClient)
                                BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SistersOfDiscord.Spawn.2.Broodmother"), new Color(102, 20, 48));
                        }
                        else
                        {
                            if (Main.netMode != NetmodeID.MultiplayerClient)
                                BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SistersOfDiscord.Spawn.2.None"), new Color(102, 20, 48));
                        }
                        break;
                    case 600:
                        if (NPCExtensions.BeenKilled<HydraBody>())
                        {
                            if (NPCExtensions.BeenKilled<Broodmother>())
                            {
                                if (Main.netMode != NetmodeID.MultiplayerClient)
                                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SistersOfDiscord.Spawn.3.Both"), new Color(72, 78, 117));
                            }
                            else
                            {
                                if (Main.netMode != NetmodeID.MultiplayerClient)
                                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SistersOfDiscord.Spawn.3.Hydra"), new Color(72, 78, 117));
                            }
                        }
                        else
                        {
                            if (Main.netMode != NetmodeID.MultiplayerClient)
                                BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SistersOfDiscord.Spawn.3.None"), new Color(72, 78, 117));
                        }
                        break;
                    case 660:
                        NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X - 200, (int)NPC.position.Y - 150, ModContent.NPCType<AsheSpawn>(), 0, NPC.whoAmI);
                        NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X + 200, (int)NPC.position.Y - 150, ModContent.NPCType<HarukaSpawn>(), 0, NPC.whoAmI);
                        break;

                    case 900:
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SistersOfDiscord.Spawn.4"), new Color(102, 20, 48));
                        break;

                    case 1140:
                        MusicUtils.InstantSwitchMusic(MusicManagementSystem.MusicSlots["Sisters"]);
                        NPC.boss = false;
                        Music = -1;
                        Main.npc[BaseAI.GetNPC(NPC.Center, ModContent.NPCType<AsheSpawn>(), -1)].Transform(ModContent.NPCType<Ashe.Ashe>());
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SistersOfDiscord.Spawn.5"), new Color(102, 20, 48));
                        SpawnBoss(player, "Ashe");
                        break;

                    case 1260:
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SistersOfDiscord.Spawn.6"), new Color(72, 78, 117));
                        Main.npc[BaseAI.GetNPC(NPC.Center, ModContent.NPCType<HarukaSpawn>(), -1)].Transform(ModContent.NPCType<Haruka.Haruka>());
                        SpawnBoss(player, "Haruka");
                        NPC.active = false;
                        break;
                }
        }

        public void SpawnBoss(Player player, string name)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                int bossType = Mod.Find<ModNPC>(name).Type;
                if (NPC.AnyNPCs(bossType)) { return; } //don't spawn if there's already a boss!
                int npcID = NPC.NewNPC(NPC.GetSource_FromThis(), (int)player.Center.X, (int)player.Center.Y, bossType, 0);
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
                int npcID = NPC.NewNPC(NPC.GetSource_FromThis(), (int)player.Center.X, (int)player.Center.Y, bossType, 0);
                Main.npc[npcID].Center = player.Center - new Vector2(800f, 0);
                Main.npc[npcID].netUpdate2 = true; Main.npc[npcID].netUpdate = true;
            }
        }


    }
}