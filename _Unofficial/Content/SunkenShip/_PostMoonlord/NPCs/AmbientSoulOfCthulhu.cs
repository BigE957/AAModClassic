using AAModClassic._Unreleased;
using AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.Items.SoulOfCthulhu;
using AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu;
using AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu._Cthulhu;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.UI.WorldGen;
using AAModClassic.Utilities;
using AAModClassic.Utilities.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace AAModClassic._Unofficial.Content.SunkenShip._PostMoonlord.NPCs
{
    public class AmbientSoulOfCthulhu : ModNPC
    {
        public override string Texture => "AAModClassic/_Unreleased/Content/SunkenShip/_PostMoonLord/NPCs/SoulOfCthulhu/SoulOfCthulhu";

        public override void SetStaticDefaults()
        {
            this.HideFromBestiary();
        }

        public override void SetDefaults()
        {
            NPC.width = 54;
            NPC.height = 54;
            NPC.aiStyle = -1;
            NPC.damage = 100;
            NPC.defense = 150;
            NPC.friendly = true;
            NPC.life = NPC.lifeMax = 1000000;
            NPC.value = 0f;
            NPC.DeathSound = SoundID.Item88;// new LegacySoundStyle(2, 88, Terraria.Audio.SoundType.Sound);
            NPC.knockBackResist = 0f;
            NPC.dontTakeDamage = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.netAlways = true;
        }

        private VerletObject BigVine = null;
        private VerletObject[] SmallVines = [];
        private VerletObject[] BackVines = [];
        internal VerletObject[] Ropes = [];
        internal VerletObject Body = null;
        private float[] BackVineAngleOffsets = [];
        private bool initializedVerlets = false;

        public override void AI()
        {
            bool anyCthulhus = false;
            if(!AAWorld.downedEquinox && !AAWorld.downedAllAncients)
                foreach (NPC npc in Main.ActiveNPCs)
                    if (npc.type == ModContent.NPCType<UDUNFUKED>() || npc.type == ModContent.NPCType<SoulOfCthulhu>() || npc.type == ModContent.NPCType<Cthulhu>() || npc.type == ModContent.NPCType<CthulhuSpawn>() || npc.type == ModContent.NPCType<CthulhuPortal>())
                        anyCthulhus = true;

            if (AAWorld.downedEquinox || AAWorld.downedAllAncients || anyCthulhus)
            {
                NPC.active = false;
                if (Body != null)
                {
                    SunkenShipSystem.Ropes = new VerletObject[4];
                    for (int i = 0; i < 4; i++)
                    {
                        VerletIntegration.BreakVerletConnection(Body[i], Ropes[i][0]);
                        SunkenShipSystem.Ropes[i] = Ropes[i];
                    }
                }
                return;
            }

            if (NPC.FindClosestPlayer(out float dist) == -1 || dist > 1000)
                return;

            if (!initializedVerlets)
                InitializeVerlets();

            bool notableMove = VerletIntegration.AffectVerletObject(Body, 0.5f, 5f);
            if (notableMove)
                Main.BestiaryTracker.Sights.RegisterWasNearby(ContentSamples.NpcsByNetId[ModContent.NPCType<SoulOfCthulhu>()]);

            foreach (var rope in Ropes)
                VerletIntegration.AffectVerletObject(rope, 0.5f, 5f);

            VerletIntegration.VerletSimulation(Body, 10, gravity: 0.1f, windAffected: false);
            foreach(var rope in Ropes)
                VerletIntegration.VerletSimulation(rope, 10, gravity: 0.05f, windAffected: false);

            NPC.Center = (Body[0].Position + Body[2].Position) / 2f;
            NPC.rotation = (Body[2].Position - Body[0].Position).ToRotation();

            if (!Main.dedServ)
                UpdateVerlets();
        }

        public override bool CheckActive() => false;

        private void InitializeVerlets()
        {
            Body = VerletIntegration.CreateVerletBox(NPC.Hitbox);
            Ropes = new VerletObject[4];
            for (int i = 0; i < Ropes.Length; i++)
            {
                Vector2 dir = (-(MathHelper.PiOver2 + MathHelper.PiOver4) + (MathHelper.TwoPi / 4 * i)).ToRotationVector2();
                float randAngle = Main.rand.NextFloat(-MathHelper.Pi / 9f, MathHelper.Pi / 5f) * Math.Sign(dir.X);
                dir = dir.RotatedBy(randAngle);
                Vector2? tileWorld = CollisionUtils.RayCast(Body[i].Position, dir, 1000, out _);
                Vector2 pos = tileWorld ?? dir * 128;
                Ropes[i] = VerletIntegration.CreateVerletChain(pos, Body[i].Position, pos.Y < NPC.Center.Y ? 14 : 18, 8);
                VerletIntegration.ConnectVerlets(Ropes[i][^1], Body[i], 4);
            }

            SunkenShipSystem.RopeEnds = [Ropes[0].Positions[0], Ropes[1].Positions[0], Ropes[2].Positions[0], Ropes[3].Positions[0]];

            if (!Main.dedServ)
            {
                Vector2 start = NPC.Center + new Vector2(14, 35).RotatedBy(NPC.rotation);
                int count = 12;
                BigVine = VerletIntegration.CreateVerletChain(start, start + Vector2.UnitY * count * 6, count, 6);

                start = NPC.Center + new Vector2(-16, 36).RotatedBy(NPC.rotation);
                SmallVines = new VerletObject[5];
                for (int i = 0; i < SmallVines.Length; i++)
                {
                    count = Main.rand.Next(5, 8);
                    Vector2 myStart = start + Vector2.One * -4 * i;
                    SmallVines[i] = VerletIntegration.CreateVerletChain(myStart, myStart + Vector2.UnitY * count * 6, count, 6);
                }

                BackVines = new VerletObject[8];
                BackVineAngleOffsets = new float[8];

                for (int i = 0; i < BackVines.Length; i++)
                {
                    count = Main.rand.Next(4, 7);
                    BackVineAngleOffsets[i] = Main.rand.NextFloat(MathHelper.Pi / 16f - 0.05f, MathHelper.Pi / 16f + 0.05f);
                    Vector2 myStart = NPC.Center + Vector2.UnitX.RotatedBy(NPC.rotation + (MathHelper.TwoPi / BackVines.Length * i) + BackVineAngleOffsets[i]) * 44;
                    BackVines[i] = VerletIntegration.CreateVerletChain(myStart, myStart + Vector2.UnitY * count * 6, count, 6);
                }
            }
            initializedVerlets = true;
        }

        private void UpdateVerlets()
        {
            Vector2 start = NPC.Center + new Vector2(16, 36).RotatedBy(NPC.rotation);
            BigVine.Points[0].Position = start;
            VerletIntegration.VerletSimulation(BigVine, 10, gravity: 0.05f, windAffected: false);

            start = NPC.Center + new Vector2(-18, 34).RotatedBy(NPC.rotation);
            for (int i = 0; i < SmallVines.Length; i++)
            {
                Vector2 myStart = start + Vector2.One.RotatedBy(NPC.rotation) * -4 * i;
                if (i % 2 != 0)
                    myStart += Vector2.One.RotatedBy(NPC.rotation + MathHelper.PiOver2) * 4;
                SmallVines[i].Points[0].Position = myStart;
                VerletIntegration.VerletSimulation(SmallVines[i], 10, gravity: 0.05f, windAffected: false);
            }

            for (int i = 0; i < BackVines.Length; i++)
            {
                Vector2 myStart = NPC.Center + Vector2.UnitX.RotatedBy(NPC.rotation + (MathHelper.TwoPi / BackVines.Length * i) + BackVineAngleOffsets[i]) * 42;
                BackVines[i].Points[0].Position = myStart;
                VerletIntegration.VerletSimulation(BackVines[i], 10, gravity: 0.05f, windAffected: false);
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture2D13 = TextureAssets.Npc[NPC.type].Value;
            Texture2D WheelTex = ModContent.Request<Texture2D>(Texture + "_Wheel_Unofficial").Value;
            Texture2D Rift = ModContent.Request<Texture2D>("AAModClassic/_Unreleased/Content/SunkenShip/_PostMoonLord/NPCs/SoulOfCthulhu/UDUNFUKED_Rift").Value;
            Texture2D GlowTex = ModContent.Request<Texture2D>(Texture + "_Glow").Value;
            Texture2D RopeTex = ModContent.Request<Texture2D>("AAModClassic/_Unofficial/Content/SunkenShip/_PostMoonlord/NPCs/Rope").Value;

            Texture2D fuckedTex = TextureAssets.Npc[ModContent.NPCType<UDUNFUKED>()].Value;
            Texture2D fuckedWheelTex = ModContent.Request<Texture2D>("AAModClassic/_Unreleased/Content/SunkenShip/_PostMoonLord/NPCs/SoulOfCthulhu/UDUNFUKED_Wheel").Value; ;

            if (Ropes != null)
            {
                foreach (var rope in Ropes)
                {
                    if (rope == null)
                        continue;

                    spriteBatch.Draw(RopeTex, rope.Positions[0] - Main.screenPosition, new Rectangle(0, 18, 10, 8), Lighting.GetColor(rope.Positions[0].ToTileCoordinates()), rope.Positions[0].AngleFrom(rope.Positions[1]) - MathHelper.PiOver2, new Vector2(5, 2f), 1, 0, 0);
                    for (int i = 0; i < rope.Count - 1; i++)
                    {
                        Vector2 start = rope.Positions[i];
                        Vector2 end = rope.Positions[i + 1];
                        Vector2 dir = start.DirectionTo(end);

                        Rectangle frame;
                        if(i % 2 == 0)
                            frame = new Rectangle(0, 0, 10, 8);
                        else
                            frame = new Rectangle(0, 8, 10, 8);

                        float stretch = Vector2.Distance(start, end) / frame.Height;
                        stretch += 0.1f;
                        spriteBatch.Draw(RopeTex, start - Main.screenPosition, frame, Lighting.GetColor(((start + end) / 2f).ToTileCoordinates()), dir.ToRotation() - MathHelper.PiOver2, new Vector2(frame.Width / 2f, 2f), new Vector2(1, stretch), 0, 0);
                    }
                }
            }

            float riftOpacity = 1 - MathHelper.Clamp((SunkenShipSystem.CthulhuCountdown - 5050) / 120f, 0f, 1f);
            BaseDrawing.DrawTexture(spriteBatch, Rift, 0, NPC.position, NPC.width, NPC.height, 1.5f, Main.GlobalTimeWrappedHourly, 0, 1, new Rectangle(0, 0, Rift.Width, Rift.Height), AAColor.Cthulhu * riftOpacity, true);

            DrawBackVines(spriteBatch, drawColor);

            float fuckedUpRatio = 1 - (SunkenShipSystem.CthulhuCountdown / 1200f);

            BaseDrawing.DrawTexture(spriteBatch, WheelTex, 0, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, 0, 1, new Rectangle(0, 0, WheelTex.Width, WheelTex.Height), drawColor, true);
            BaseDrawing.DrawTexture(spriteBatch, fuckedWheelTex, 0, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, 0, 1, new Rectangle(0, 0, WheelTex.Width, WheelTex.Height), drawColor * fuckedUpRatio, true);

            BaseDrawing.DrawTexture(spriteBatch, texture2D13, 0, NPC.position, NPC.width, NPC.height, NPC.scale, 0, 0, 1, new Rectangle(0, 0, texture2D13.Width, texture2D13.Height), drawColor, true);
            BaseDrawing.DrawTexture(spriteBatch, fuckedTex, 0, NPC.position, NPC.width, NPC.height, NPC.scale, 0, 0, 1, new Rectangle(0, 0, texture2D13.Width, texture2D13.Height), drawColor * fuckedUpRatio, true);

            float glowOpacity = 1 - MathHelper.Clamp((SunkenShipSystem.CthulhuCountdown - 7050) / 30f, 0f, 1f);
            BaseDrawing.DrawTexture(spriteBatch, GlowTex, 0, NPC.position, NPC.width, NPC.height, NPC.scale, 0, 0, 1, new Rectangle(0, 0, GlowTex.Width, GlowTex.Height), Color.White * glowOpacity, true);
            //BaseDrawing.DrawAfterimage(spriteBatch, GlowTex, 0, NPC, 0.8f, 1f, 6, false, 0f, 0f, AAColor.Cthulhu2 * glowOpacity);

            DrawVines(spriteBatch, drawColor);

            return false;
        }

        private void DrawVines(SpriteBatch spriteBatch, Color drawColor)
        {
            if (!WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                return;

            Texture2D vinesAtlas = ModContent.Request<Texture2D>(Texture + "_Vines").Value;
            foreach (var vine in SmallVines)
            {
                for (int i = 0; i < vine.Count - 1; i++)
                {
                    Vector2 start = vine.Positions[i];
                    Vector2 end = vine.Positions[i + 1];
                    Vector2 dir = start.DirectionTo(end);

                    Rectangle frame = vinesAtlas.Frame(4, 4, 0, (i == vine.Count - 2 ? 3 : i % 3));
                    if (i != vine.Count - 2)
                        frame.Height -= 2;
                    float stretch = Vector2.Distance(start, end) / frame.Height;
                    stretch += 0.1f;
                    spriteBatch.Draw(vinesAtlas, start - Main.screenPosition, frame, drawColor, dir.ToRotation() - MathHelper.PiOver2, new Vector2(frame.Width / 2f, 2f), new Vector2(1, stretch), 0, 0);
                }
            }

            if (BigVine != null)
            {
                for (int i = 0; i < BigVine.Count - 1; i++)
                {
                    Vector2 start = BigVine.Positions[i];
                    Vector2 end = BigVine.Positions[i + 1];
                    Vector2 dir = start.DirectionTo(end);

                    Rectangle frame = vinesAtlas.Frame(4, 4, 0, (i == BigVine.Count - 2 ? 3 : i % 3));
                    if (i != BigVine.Count - 2)
                        frame.Height -= 2;
                    float stretch = Vector2.Distance(start, end) / frame.Height;
                    stretch += 0.1f;
                    spriteBatch.Draw(vinesAtlas, start - Main.screenPosition, frame, drawColor, dir.ToRotation() - MathHelper.PiOver2, new Vector2(frame.Width / 2f, 2f), new Vector2(1, stretch), 0, 0);
                }
            }
        }

        private void DrawBackVines(SpriteBatch spriteBatch, Color drawColor)
        {
            if (!WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                return;

            Texture2D vinesAtlas = ModContent.Request<Texture2D>(Texture + "_Vines").Value;

            foreach (var vine in BackVines)
            {
                for (int i = 0; i < vine.Count - 1; i++)
                {
                    Vector2 start = vine.Positions[i];
                    Vector2 end = vine.Positions[i + 1];
                    Vector2 dir = start.DirectionTo(end);

                    Rectangle frame = vinesAtlas.Frame(4, 4, (i % 3 + 1), (i == vine.Count - 2 ? 3 : 2));
                    if (i != vine.Count - 2)
                        frame.Height -= 2;
                    float stretch = Vector2.Distance(start, end) / frame.Height;
                    stretch += 0.1f;
                    spriteBatch.Draw(vinesAtlas, start - Main.screenPosition, frame, drawColor, dir.ToRotation() - MathHelper.PiOver2, new Vector2(frame.Width / 2f, 2f), new Vector2(1, stretch), 0, 0);
                }
            }
        }

    }

    public class SunkenShipSystem : ModSystem
    {
        public static int CthulhuCountdown = 10800;
        public static bool Leave = false;
        internal static Vector2[] RopeEnds = [];
        internal static VerletObject[] Ropes = [];

        public override void Load()
        {
            On_Main.DoDraw_DrawNPCsOverTiles += DrawRopes;
        }

        public override void SaveWorldData(TagCompound tag)
        {
            if(RopeEnds.Length > 0)
                tag.Add("RopeEnds", RopeEnds);
        }

        public override void LoadWorldData(TagCompound tag)
        {
            if (!tag.TryGet("RopeEnds", out RopeEnds))
                RopeEnds = [];
        }

        public override void PreUpdateNPCs()
        {
            //if(Main.LocalPlayer.miscCounter % 30 == 0)
            //    Main.NewText(Main.MouseWorld.ToTileCoordinates() - AAWorld_Unreleased.shipPos);
            bool anyCthulhus = false;
            if (!AAWorld.downedEquinox)
                foreach (NPC npc in Main.ActiveNPCs)
                    if (npc.type == ModContent.NPCType<UDUNFUKED>() || npc.type == ModContent.NPCType<SoulOfCthulhu>() || npc.type == ModContent.NPCType<Cthulhu>() || npc.type == ModContent.NPCType<CthulhuSpawn>() || npc.type == ModContent.NPCType<CthulhuPortal>())
                        anyCthulhus = true;

            if (!AAWorld.downedEquinox && !AAWorld.downedAllAncients)
            {
                if (!NPC.AnyNPCs(ModContent.NPCType<AmbientSoulOfCthulhu>()) && !anyCthulhus)
                {
                    Vector2 spawnPos = (AAWorld_Unreleased.shipPos + new Point(141, 41)).ToWorldCoordinates();
                    NPC.NewNPC(NPC.GetSource_NaturalSpawn(), (int)spawnPos.X, (int)spawnPos.Y, ModContent.NPCType<AmbientSoulOfCthulhu>());
                    Ropes = [];
                    CthulhuCountdown = 10800;
                }

                if (!anyCthulhus)
                {
                    int firstInShip = -1;
                    int firstWithCompass = -1;

                    foreach (Player p in Main.ActivePlayers)
                    {
                        if (firstInShip == -1 && p.GetModPlayer<AAPlayer_Unreleased>().ZoneShip)
                        {
                            firstInShip = p.whoAmI;
                        }
                        if (p.inventory.Any(i => i.type == ModContent.ItemType<CursedCompass>() && i.stack > 0))
                        {
                            firstWithCompass = p.whoAmI;
                            break;
                        }
                    }

                    if (!AAWorld_Unreleased.Compass && firstWithCompass != -1)
                    {
                        AAWorld_Unreleased.Compass = true;
                        Leave = false;
                        Player thief = Main.player[firstWithCompass];
                        if (thief.GetModPlayer<AAPlayer_Unreleased>().ZoneShip)
                        {
                            Vector2 spawnPos = thief.Center + (Vector2.UnitY.RotatedBy(Main.rand.NextFloat(-MathHelper.PiOver2, MathHelper.PiOver2)) * 800);
                            int n = NPC.NewNPC(NPC.GetSource_NaturalSpawn(), (int)spawnPos.X, (int)spawnPos.Y, ModContent.NPCType<UDUNFUKED>());
                            Main.npc[n].target = firstWithCompass;
                            if (Main.netMode != NetmodeID.MultiplayerClient)
                            {
                                BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SoulOfCthulhu.PreFight.CompassSteal"), Color.Cyan);
                            }
                        }
                    }

                    if (firstInShip != -1 && !Leave)
                    {
                        CthulhuCountdown--;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            switch (CthulhuCountdown)
                            {
                                case 9500:
                                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SoulOfCthulhu.PreFight.SunkenShipWarning.1"), Color.Blue);
                                    break;
                                case 7050:
                                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SoulOfCthulhu.PreFight.SunkenShipWarning.2"), Color.DarkCyan);
                                    break;
                                case 5050:
                                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SoulOfCthulhu.PreFight.SunkenShipWarning.3"), Color.Cyan);
                                    break;
                                case 3000:
                                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SoulOfCthulhu.PreFight.SunkenShipWarning.4"), Color.Cyan);
                                    break;
                                case 1200:
                                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SoulOfCthulhu.PreFight.SunkenShipWarning.5"), Color.DarkCyan);
                                    break;
                            }
                        }

                        if (CthulhuCountdown == 0)
                        {
                            Player tresspasser = Main.player[firstInShip];
                            Leave = false;
                            int n;
                            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                            {
                                n = NPC.FindFirstNPC(ModContent.NPCType<AmbientSoulOfCthulhu>());

                                if (Main.npc[n].ModNPC is AmbientSoulOfCthulhu ambient)
                                {
                                    Ropes = new VerletObject[4];
                                    for (int i = 0; i < 4; i++)
                                    {
                                        VerletIntegration.BreakVerletConnection(ambient.Body[i], ambient.Ropes[i][0]);
                                        Ropes[i] = ambient.Ropes[i];
                                    }
                                }

                                Main.npc[n].Transform(ModContent.NPCType<UDUNFUKED>());
                            }
                            else
                            {
                                Vector2 spawnPos = tresspasser.Center + (Vector2.UnitY.RotatedBy(Main.rand.NextFloat(-MathHelper.PiOver2, MathHelper.PiOver2)) * 800);
                                n = NPC.NewNPC(NPC.GetSource_NaturalSpawn(), (int)spawnPos.X, (int)spawnPos.Y, ModContent.NPCType<UDUNFUKED>());
                            }
                            Main.npc[n].target = firstInShip;

                            if (Main.netMode != NetmodeID.MultiplayerClient)
                                BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SoulOfCthulhu.PreFight.SunkenShipWarning.6"), Color.Cyan);
                        }
                    }
                    if (firstInShip == -1)
                    {
                        CthulhuCountdown = 10800;
                    }
                    if (firstInShip == -1 && Leave == true)
                    {
                        Leave = false;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SoulOfCthulhu.PreFight.SunkenShipWarning.Escape"), Color.DarkCyan);
                    }
                }
                else
                    CthulhuCountdown = 10800;
            }

            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial) && ((AAWorld.downedEquinox || AAWorld.downedAllAncients) || anyCthulhus))
            {
                Vector2 originalCenter = (AAWorld_Unreleased.shipPos + new Point(141, 41)).ToWorldCoordinates();
                if (Ropes.Length == 0)
                {
                    Ropes = new VerletObject[4];
                    for (int i = 0; i < 4; i++)
                        Ropes[i] = VerletIntegration.CreateVerletChain(RopeEnds[i], originalCenter, RopeEnds[i].Y < originalCenter.Y ? 8 : 10, 16);
                }

                foreach (var rope in Ropes)
                    VerletIntegration.AffectVerletObject(rope, 0.5f, 5f);

                foreach (var rope in Ropes)
                    VerletIntegration.VerletSimulation(rope, 10, gravity: rope.Positions[0].Y < originalCenter.Y ? 0.05f : -0.05f, windAffected: false);
            }
        }

        private void DrawRopes(On_Main.orig_DoDraw_DrawNPCsOverTiles orig, Main self)
        {
            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial) && Ropes.Length > 0)
            {
                Texture2D RopeTex = ModContent.Request<Texture2D>("AAModClassic/_Unofficial/Content/SunkenShip/_PostMoonlord/NPCs/Rope").Value;
                
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.TransformationMatrix);

                foreach (var rope in Ropes)
                {
                    if (rope == null)
                        continue;

                    Main.spriteBatch.Draw(RopeTex, rope.Positions[0] - Main.screenPosition, new Rectangle(0, 18, 10, 8), Lighting.GetColor(rope.Positions[0].ToTileCoordinates()), rope.Positions[0].AngleFrom(rope.Positions[1]) - MathHelper.PiOver2, new Vector2(5, 2f), 1, 0, 0);
                    for (int i = 0; i < rope.Count - 1; i++)
                    {
                        Vector2 start = rope.Positions[i];
                        Vector2 end = rope.Positions[i + 1];
                        Vector2 dir = start.DirectionTo(end);

                        Rectangle frame;
                        if (i % 2 == 0)
                            frame = new Rectangle(0, 0, 10, 8);
                        else
                            frame = new Rectangle(0, 8, 10, 8);

                        float stretch = Vector2.Distance(start, end) / frame.Height;
                        stretch += 0.1f;
                        Main.spriteBatch.Draw(RopeTex, start - Main.screenPosition, frame, Lighting.GetColor(((start + end) / 2f).ToTileCoordinates()), dir.ToRotation() - MathHelper.PiOver2, new Vector2(frame.Width / 2f, 2f), new Vector2(1, stretch), 0, 0);
                    }
                }

                Main.spriteBatch.End();
            }

            orig(self);
        }
    }
}
