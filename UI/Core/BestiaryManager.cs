using AAModClassic.Utilities.Interfaces;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.UI.Core
{
    public class BestiaryManager : ModSystem
    {
        private static FieldInfo _currentDatabaseField;
        private static MethodInfo _getExclusionsMethod;
        private static MethodInfo _registerMethod;
        private static FieldInfo _byNpcIdField;
        private static FieldInfo _wasSeenNearPlayerByNetIdField;
        private static FieldInfo _playerHitboxesForBestiaryField;

        public override void OnModLoad()
        {
            _currentDatabaseField = typeof(BestiaryDatabaseNPCsPopulator).GetField("_currentDatabase", BindingFlags.NonPublic | BindingFlags.Static);

            _getExclusionsMethod = typeof(BestiaryDatabaseNPCsPopulator).GetMethod("GetExclusions", BindingFlags.NonPublic | BindingFlags.Static);

            _registerMethod = typeof(BestiaryDatabaseNPCsPopulator).GetMethod("Register", BindingFlags.NonPublic | BindingFlags.Instance, null, new[] { typeof(BestiaryEntry) }, null);

            _byNpcIdField = typeof(BestiaryDatabase).GetField("_byNpcId", BindingFlags.NonPublic | BindingFlags.Instance);

            _wasSeenNearPlayerByNetIdField = typeof(NPCWasNearPlayerTracker).GetField("_wasSeenNearPlayerByNetId", BindingFlags.NonPublic | BindingFlags.Instance);

            _playerHitboxesForBestiaryField = typeof(NPCWasNearPlayerTracker).GetField("_playerHitboxesForBestiary", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        public override void Unload()
        {
            _currentDatabaseField = null;
            _getExclusionsMethod = null;
            _registerMethod = null;
            _byNpcIdField = null;
            _wasSeenNearPlayerByNetIdField = null;
            _playerHitboxesForBestiaryField = null;
        }

        public override void PostSetupContent()
        {
            On_BestiaryDatabaseNPCsPopulator.AddEmptyEntries_CrittersAndEnemies_Automated += ForciblyAddEmptyEntriesForCritters;
            On_NPCWasNearPlayerTracker.ScanWorldForFinds += ForciblySetWasSeenByPlayer;
        }

        private void ForciblyAddEmptyEntriesForCritters(On_BestiaryDatabaseNPCsPopulator.orig_AddEmptyEntries_CrittersAndEnemies_Automated orig, BestiaryDatabaseNPCsPopulator self)
        {
            orig(self);

            // Run through all entries again and remove the empty Enemy entries that are added by tMod itself.
            // Afterwards, manually add empty Critter entries for all NPCs within the ID set.
            HashSet<int> exclusions = (HashSet<int>)_getExclusionsMethod.Invoke(null, null);
            BestiaryDatabase currentDatabase = (BestiaryDatabase)_currentDatabaseField.GetValue(null);
            Dictionary<int, BestiaryEntry> byNpcId = (Dictionary<int, BestiaryEntry>)_byNpcIdField.GetValue(currentDatabase);

            foreach (KeyValuePair<int, NPC> pair in ContentSamples.NpcsByNetId)
            {
                if (!exclusions.Contains(pair.Key))
                {
                    if (pair.Value.ModNPC is IBestiaryCritterNPC)
                    {
                        if (byNpcId.TryGetValue(pair.Value.netID, out BestiaryEntry enemyEntry) && enemyEntry.UIInfoProvider is not CritterUICollectionInfoProvider)
                            currentDatabase.Entries.Remove(enemyEntry);

                        BestiaryEntry registeredEntry = (BestiaryEntry)_registerMethod.Invoke(self, new object[] { BestiaryEntry.Critter(pair.Key) });
                        NPCLoader.SetBestiary(pair.Value, currentDatabase, registeredEntry);
                    }
                }
            }
        }

        private void ForciblySetWasSeenByPlayer(On_NPCWasNearPlayerTracker.orig_ScanWorldForFinds orig, NPCWasNearPlayerTracker self)
        {
            orig(self);

            // Allow NPCs with manually added empty critter entries to be registered by player proximity.
            List<int> wasSeenNearPlayerByNetId = (List<int>)_wasSeenNearPlayerByNetIdField.GetValue(self);
            List<Rectangle> playerHitboxesForBestiary = (List<Rectangle>)_playerHitboxesForBestiaryField.GetValue(self);

            foreach (NPC npc in Main.ActiveNPCs)
            {
                if (npc.ModNPC is not IBestiaryCritterNPC critterNPC || !critterNPC.UnlockWhenNearby || wasSeenNearPlayerByNetId.Contains(npc.netID))
                    continue;

                for (int i = 0; i < playerHitboxesForBestiary.Count; i++)
                {
                    if (npc.Hitbox.Intersects(playerHitboxesForBestiary[i]))
                    {
                        if (critterNPC.CountAsType == -1)
                        {
                            wasSeenNearPlayerByNetId.Add(npc.netID);
                            self.RegisterWasNearby(npc);
                        }
                        else
                        {
                            NPC sample = ContentSamples.NpcsByNetId[critterNPC.CountAsType];
                            wasSeenNearPlayerByNetId.Add(sample.netID);
                            self.RegisterWasNearby(sample);
                        }
                    }
                }
            }
        }
    }
}