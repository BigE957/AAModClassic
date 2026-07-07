using Terraria.DataStructures;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Linq;
using System;
using Terraria.Audio;

namespace AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items
{
    public abstract class TransformationAccessory : ModItem
    {
        public bool IsForced = false;

        public abstract string AssetPath { get; }
        
        public virtual (EquipType Type, string AssetName, string EquipName)[] EquipSlots => [];

        public virtual float Priority => 1;

        public virtual Func<Player, bool> ShouldTransform => (player) => true;

        public virtual bool ShouldHideAccessories => false;

        public virtual (SoundStyle sound, int delay)? HurtSound(Player p) => null;

        public virtual void ModifyDrawInfo(ref PlayerDrawSet drawInfo) { }

        public virtual void ArmorIDSets()
        {
            if (EquipSlots.Any(s => s.Type == EquipType.Head))
            {
                string name = EquipSlots.First(s => s.Type == EquipType.Head).EquipName;
                int equipSlotHead = EquipLoader.GetEquipSlot(Mod, name ?? Name, EquipType.Head);
                ArmorIDs.Head.Sets.DrawHead[equipSlotHead] = false;
            }

            if (EquipSlots.Any(s => s.Type == EquipType.Body))
            {
                string name = EquipSlots.First(s => s.Type == EquipType.Body).EquipName;
                int equipSlotBody = EquipLoader.GetEquipSlot(Mod, name ?? Name, EquipType.Body);
                ArmorIDs.Body.Sets.HidesTopSkin[equipSlotBody] = true;
                ArmorIDs.Body.Sets.HidesArms[equipSlotBody] = true;
            }

            if (EquipSlots.Any(s => s.Type == EquipType.Legs))
            {
                string name = EquipSlots.First(s => s.Type == EquipType.Legs).EquipName;
                int equipSlotLegs = EquipLoader.GetEquipSlot(Mod, name ?? Name, EquipType.Legs);
                ArmorIDs.Legs.Sets.HidesBottomSkin[equipSlotLegs] = true;
            }
        }

        public virtual bool CustomSetEquipType(Player player, EquipType type, Mod mod, string name) => false;

        public virtual void TransformFrameEffects(Player player) { }

        public virtual void TransformPostUpdate(Player player) { }

        public override void Load()
        {
            if (Main.netMode != NetmodeID.Server)
            {
                foreach (var v in EquipSlots)
                {
                    if (v.AssetName == null)
                        continue;
                    EquipLoader.AddEquipTexture(Mod, AssetPath + v.AssetName + "_" + v.Type.ToString(), v.Type, this, v.EquipName);
                }
            }
        }

        public override void SetStaticDefaults()
        {
            if (Main.netMode == NetmodeID.Server)
                return;

            ArmorIDSets();
        }
    }

    public class TransformationPlayer : ModPlayer
    {
        public override void Load()
        {
            On_Player.ResetVisibleAccessories += ResetTransformation;
            On_Player.UpdateVisibleAccessory += SetTransformationItem;
        }

        internal TransformationAccessory currentTransformation = null;
        internal TransformationAccessory previousTransformation = null;
        internal int previousHighest = -1;
        internal int currentHighest = -1;

        public int Type 
        { 
            get => currentTransformation == null ? -1 : currentTransformation.Type;

            set
            {
                ModItem item = ModContent.GetModItem(value);
                if (item != null && item is TransformationAccessory trans)
                {
                    currentTransformation = trans;
                    currentTransformation.IsForced = true;
                }
                else
                    currentTransformation = null;
            }
        }

        private void ResetTransformation(On_Player.orig_ResetVisibleAccessories orig, Player self)
        {
            orig(self);

            if (!self.dead)
                Reset(self);
            else
            {
                TransformationAccessory trans = self.Transformation().currentTransformation = self.Transformation().previousTransformation;
                if (trans is not null)
                    EquipTransformation(trans, self, Mod);
            }
        }

        private static void Reset(Player p)
        {
            p.Transformation().previousHighest = p.Transformation().currentHighest;
            p.Transformation().currentHighest = -1;

            if (p.Transformation().currentTransformation != null && !p.Transformation().currentTransformation.IsForced)
            {
                p.Transformation().previousTransformation = p.Transformation().currentTransformation;
                p.Transformation().currentTransformation = null;               
            }
        }

        private void SetTransformationItem(On_Player.orig_UpdateVisibleAccessory orig, Player self, int itemSlot, Item item, bool modded)
        {
            orig(self, itemSlot, item, modded);

            if (self.Transformation().currentTransformation == null || !self.Transformation().currentTransformation.IsForced)
            {
                if (item.ModItem is TransformationAccessory transformation && transformation.ShouldTransform.Invoke(self) && (currentTransformation == null || transformation.Priority >= currentTransformation.Priority))
                    self.Transformation().currentTransformation = transformation;
            }

            if (self.Transformation().currentHighest < itemSlot)
                self.Transformation().currentHighest = itemSlot;

            if (self.Transformation().currentTransformation != null && itemSlot == self.Transformation().previousHighest) // last item slot, can set equip slots to our transformation if one is equipped
            {
                TransformationAccessory trans = self.Transformation().currentTransformation;
                EquipTransformation(trans, self, Mod);
            }
        }

        public override void UpdateAutopause()
        {
            if (currentTransformation != null)
            {
                if (!currentTransformation.CustomSetEquipType(Player, EquipType.Head, Mod, currentTransformation.Name))
                {
                    var (Type, AssetName, EquipName) = currentTransformation.EquipSlots.First(v => v.Type == EquipType.Head);
                    string name = (EquipName == null && AssetName == null) ? null : EquipName ?? currentTransformation.Name;
                    SetEquipSlot(Player, EquipType.Head, Mod, name);
                }
            }

            Reset(Player);
        }

        public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
        {
            if (currentTransformation != null)
            {
                currentTransformation.ModifyDrawInfo(ref drawInfo);

                // CIT 24FEB2026: Previously, this hook also reset glowmasks and offsets on the head, body, and legs.
                // This has been removed, as it resulted in index OOB crashes due to the original armor being drawn for one frame.
            }
        }

        public override void FrameEffects()
        {
            if (currentTransformation != null)
            {
                currentTransformation.TransformFrameEffects(Player);

                if (currentTransformation.ShouldHideAccessories)
                    Player.HideAccessories();
            }
        }

        public override void PostUpdate()
        {
            currentTransformation?.TransformPostUpdate(Player);
        }

        public static void SetEquipSlot(Player player, EquipType type, Mod mod, string name)
        {
            switch (type)
            {
                case EquipType.Head:
                    player.head = name == null ? -1 : EquipLoader.GetEquipSlot(mod, name, type);
                    break;
                case EquipType.Body:
                    player.body = name == null ? -1 : EquipLoader.GetEquipSlot(mod, name, type);
                    break;
                case EquipType.Legs:
                    player.legs = name == null ? -1 : EquipLoader.GetEquipSlot(mod, name, type);
                    break;
                case EquipType.HandsOn:
                    player.handon = name == null ? -1 : EquipLoader.GetEquipSlot(mod, name, type);
                    break;
                case EquipType.HandsOff:
                    player.handoff = name == null ? -1 : EquipLoader.GetEquipSlot(mod, name, type);
                    break;
                case EquipType.Back:
                    player.back = name == null ? -1 : EquipLoader.GetEquipSlot(mod, name, type);
                    break;
                case EquipType.Front:
                    player.front = name == null ? -1 : EquipLoader.GetEquipSlot(mod, name, type);
                    break;
                case EquipType.Shoes:
                    player.shoe = name == null ? -1 : EquipLoader.GetEquipSlot(mod, name, type);
                    break;
                case EquipType.Waist:
                    player.waist = name == null ? -1 : EquipLoader.GetEquipSlot(mod, name, type);
                    break;
                case EquipType.Wings:
                    player.wings = name == null ? -1 : EquipLoader.GetEquipSlot(mod, name, type);
                    break;
                case EquipType.Shield:
                    player.shield = name == null ? -1 : EquipLoader.GetEquipSlot(mod, name, type);
                    break;
                case EquipType.Neck:
                    player.neck = name == null ? -1 : EquipLoader.GetEquipSlot(mod, name, type);
                    break;
                case EquipType.Face:
                    player.face = name == null ? -1 : EquipLoader.GetEquipSlot(mod, name, type);
                    break;
                case EquipType.Balloon:
                    player.balloon = name == null ? -1 : EquipLoader.GetEquipSlot(mod, name, type);
                    break;
                case EquipType.Beard:
                    player.beard = name == null ? -1 : EquipLoader.GetEquipSlot(mod, name, type);
                    break;
            }
        }
    
        public static int GetEquipSlot(Player player, EquipType type) => type switch
        {
            EquipType.Head => player.head,
            EquipType.Body => player.body,
            EquipType.Legs => player.legs,
            EquipType.HandsOn => player.handon,
            EquipType.HandsOff => player.handoff,
            EquipType.Back => player.back,
            EquipType.Front => player.front,
            EquipType.Shoes => player.shoe,
            EquipType.Waist => player.waist,
            EquipType.Wings => player.wings,
            EquipType.Shield => player.shield,
            EquipType.Neck => player.neck,
            EquipType.Face => player.face,
            EquipType.Balloon => player.balloon,
            EquipType.Beard => player.beard,
            _ => -1,
        };

        public static void EquipTransformation(TransformationAccessory trans, Player self, Mod mod)
        {
            bool[] list = new bool[15];
            foreach (var (Type, AssetName, EquipName) in trans.EquipSlots)
            {
                string name = AssetName == null ? null : EquipName ?? trans.Name;
                if (!trans.CustomSetEquipType(self, Type, mod, name) && !list[(int)Type])
                {
                    SetEquipSlot(self, Type, mod, name);
                    list[(int)Type] = true;
                }
            }
            if (trans.ShouldHideAccessories)
                self.HideAccessories();
        }

        public static EquipTexture GetEquipTexture(Player p, EquipType type) => EquipLoader.GetEquipTexture(type, GetEquipSlot(p, type));
    }
}
