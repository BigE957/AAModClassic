using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.ModLoader.IO;
using Terraria.Utilities;

namespace AAModClassic.UI.Dialogue
{
    public class DialoguePool<TContext>
    {
        private const string Path = "Dialogue.Mods.AAModClassic.DialogueTrees.";

        public readonly List<(string TreeKey, Func<TContext, bool> Requirement, byte Priority, bool Repeatable)> Dialogues;
        public List<(string TreeKey, Func<TContext, bool> Requirement, byte Priority)> CirculatingDialogues = [];

        public DialoguePool(params (string TreeKey, Func<TContext, bool> Requirement, byte Priority, bool Repeatable)[] dialogues)
        {
            Dialogues = dialogues.ToList();
            ResetCirculatingDialogues();
        }

        public void ResetCirculatingDialogues()
        {
            CirculatingDialogues.Clear();

            foreach (var (TreeKey, Requirement, Priority, Repeatable) in Dialogues)
            {
                (string TreeKey, Func<TContext, bool> Requirement, byte Priority) myDialogue = new(TreeKey, Requirement, Priority);
                CirculatingDialogues.Add(myDialogue);
            }
        }

        public string GetTree(TContext context)
        {
            short globalTopPriority = -1;
            foreach (var (TreeKey, Requirement, Priority, Repeatable) in Dialogues)
            {
                if (Requirement.Invoke(context) && Priority > globalTopPriority)
                    globalTopPriority = Priority;
            }

            if (globalTopPriority == -1)
                return null;

            bool hasAvailableInTopTier = CirculatingDialogues.Any(d => d.Priority == globalTopPriority && d.Requirement.Invoke(context));

            if (!hasAvailableInTopTier)
            {
                foreach (var (TreeKey, Requirement, Priority, Repeatable) in Dialogues)
                {
                    if (Priority == globalTopPriority && Repeatable && !CirculatingDialogues.Any(d => d.TreeKey == TreeKey))
                        CirculatingDialogues.Add((TreeKey, Requirement, Priority));
                }
            }

            WeightedRandom<string> keys = new();
            foreach (var (TreeKey, Requirement, Priority) in CirculatingDialogues)
            {
                if (Priority == globalTopPriority && Requirement.Invoke(context))
                    keys.Add(TreeKey);
            }

            string selectedKey = keys.Get();

            CirculatingDialogues.RemoveAll(d => d.TreeKey == selectedKey);

            return Path + selectedKey;
        }

        public void SaveData(string tagKey, TagCompound tag)
        {
            List<string> circulatingTrees = [];
            for (int i = 0; i < CirculatingDialogues.Count; i++)
                circulatingTrees.Add(CirculatingDialogues[i].TreeKey);

            tag[tagKey] = circulatingTrees;
        }

        public void LoadData(string tagKey, TagCompound tag)
        {
            if (!tag.ContainsKey(tagKey))
                return;

            CirculatingDialogues.Clear();
            List<string> circulatingTrees = (List<string>)tag.GetList<string>(tagKey);
            for (int i = 0; i < circulatingTrees.Count; i++)
            {
                var (TreeKey, Requirement, Priority, Repeatable) = Dialogues.First(d => d.TreeKey == circulatingTrees[i]);
                CirculatingDialogues.Add(new(TreeKey, Requirement, Priority));
            }
        }
    }

    public static class DialoguePoolExtensions
    {
        public static string GetTree<T1, T2>(this DialoguePool<(T1, T2)> pool, T1 arg1, T2 arg2) =>  pool.GetTree((arg1, arg2));

        public static string GetTree<T1, T2, T3>(this DialoguePool<(T1, T2, T3)> pool, T1 arg1, T2 arg2, T3 arg3) => pool.GetTree((arg1, arg2, arg3));

        public static string GetTree<T1, T2, T3, T4>(this DialoguePool<(T1, T2, T3, T4)> pool, T1 arg1, T2 arg2, T3 arg3, T4 arg4) =>  pool.GetTree((arg1, arg2, arg3, arg4));
    }
}