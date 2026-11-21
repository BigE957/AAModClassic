using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.ModLoader.IO;
using Terraria.Utilities;

namespace AAModClassic.UI.Dialogue
{
    public class DialoguePool<TContext>
    {
        public readonly Dictionary<string, (Func<TContext, bool> Requirement, byte Priority, bool Repeatable)> Dialogues;
        private readonly HashSet<string> CirculatingDialogues = [];

        public DialoguePool(params (string TreeKey, Func<TContext, bool> Requirement, byte Priority, bool Repeatable)[] dialogues)
        {
            Dialogues = [];
            foreach (var (TreeKey, Requirement, Priority, Repeatable) in dialogues)
            {
                Dialogues.Add(TreeKey, (Requirement, Priority, Repeatable));
            }
            ResetCirculatingDialogues();
        }

        public void ResetCirculatingDialogues()
        {
            CirculatingDialogues.Clear();

            foreach (var key in Dialogues.Keys)
            {
                CirculatingDialogues.Add(key);
            }
        }

        public bool IsTreeInCirculation(string key) => CirculatingDialogues.Contains(key);

        public bool AllTreesInCirculation() => CirculatingDialogues.Count == Dialogues.Keys.Count;

        public string GetTree(TContext context)
        {
            List<(string Key, byte Priority)> passing = [];
            int topPriority = -1;

            foreach (var (key, (requirement, priority, repeatable)) in Dialogues)
            {
                if (!repeatable && !CirculatingDialogues.Contains(key))
                    continue;
                if (priority < topPriority)
                    continue;
                if (!requirement(context))
                    continue;

                passing.Add((key, priority));
                if (priority > topPriority)
                    topPriority = priority;
            }

            if (passing.Count == 0)
                return null;

            bool hasAvailableInTopTier = false;
            foreach (var (key, priority) in passing)
            {
                if (priority == topPriority && CirculatingDialogues.Contains(key))
                {
                    hasAvailableInTopTier = true;
                    break;
                }
            }

            if (!hasAvailableInTopTier)
            {
                foreach (var (key, entry) in Dialogues)
                {
                    if (entry.Priority == topPriority && entry.Repeatable)
                        CirculatingDialogues.Add(key);
                }
            }

            WeightedRandom<string> keys = new();
            foreach (var (key, priority) in passing)
            {
                if (priority == topPriority && CirculatingDialogues.Contains(key))
                    keys.Add(key);
            }

            string selectedKey = keys.Get();
            CirculatingDialogues.Remove(selectedKey);
            return selectedKey;
        }

        public void SaveData(string tagKey, TagCompound tag)
        {
            List<string> usedKeys = Dialogues.Keys.Where(key => !CirculatingDialogues.Contains(key)).ToList();
            tag[tagKey + "_Used"] = usedKeys;
        }

        public void LoadData(string tagKey, TagCompound tag)
        {
            ResetCirculatingDialogues();

            if (!tag.ContainsKey(tagKey + "_Used"))
                return;

            foreach (var key in tag.GetList<string>(tagKey + "_Used"))
                CirculatingDialogues.Remove(key);
        }
    }

    public static class DialoguePoolExtensions
    {
        public static string GetTree<T1, T2>(this DialoguePool<(T1, T2)> pool, T1 arg1, T2 arg2) =>  pool.GetTree((arg1, arg2));

        public static string GetTree<T1, T2, T3>(this DialoguePool<(T1, T2, T3)> pool, T1 arg1, T2 arg2, T3 arg3) => pool.GetTree((arg1, arg2, arg3));

        public static string GetTree<T1, T2, T3, T4>(this DialoguePool<(T1, T2, T3, T4)> pool, T1 arg1, T2 arg2, T3 arg3, T4 arg4) =>  pool.GetTree((arg1, arg2, arg3, arg4));
    }
}