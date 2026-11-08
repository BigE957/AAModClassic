namespace AAModClassic.Base
{
    public class BasePlayer
    {
        //------------------------------------------------------//
        //------------------BASE PLAYER CLASS-------------------//
        //------------------------------------------------------//
        // Contains methods relating to players.                //
        //------------------------------------------------------//
        //  Author(s): Grox the Great                           //
        //------------------------------------------------------//

        public static bool HasHelmet(Player player, int itemType, bool vanity = true) { return HasArmor(player, itemType, 0, vanity); }
        public static bool HasChestplate(Player player, int itemType, bool vanity = true) { return HasArmor(player, itemType, 1, vanity); }
        public static bool HasLeggings(Player player, int itemType, bool vanity = true) { return HasArmor(player, itemType, 2, vanity); }

        /*
         * Returns true if the player is wearing the given armor
         * armorType : 0 == helmet, 1 == chestplate, 2 == leggings.
         * vanity : If true, include vanity slots.
         */
        public static bool HasArmor(Player player, int itemType, int armorType, bool vanity = true)
        {
            if (vanity)
            {
                if (armorType == 0)
                    return player.armor[10] != null && player.armor[10].type == itemType || player.armor[0] != null && player.armor[0].type == itemType;
                if (armorType == 1)
                    return player.armor[11] != null && player.armor[11].type == itemType || player.armor[1] != null && player.armor[1].type == itemType;
                if (armorType == 2)
                    return player.armor[12] != null && player.armor[12].type == itemType || player.armor[2] != null && player.armor[2].type == itemType;
            }
            else
            {
                if (armorType == 0)
                    return player.armor[0] != null && player.armor[0].type == itemType;
                if (armorType == 1)
                    return player.armor[1] != null && player.armor[1].type == itemType;
                if (armorType == 2)
                    return player.armor[2] != null && player.armor[2].type == itemType;
            }
            return false;
        }

        public static bool HasAccessory(Player player, int type, bool normal, bool vanity)
        {
            int dummy = 0; bool dummeh = false;
            return HasAccessory(player, type, normal, vanity, ref dummeh, ref dummy);
        }

        /**
         * Returns true if the given player has the given accessory equipped.
         */
        public static bool HasAccessory(Player player, int type, bool normal, bool vanity, ref bool social, ref int index)
        {
            if (vanity)
            {
                for (int m = 13; m < 18 + player.GetAmountOfExtraAccessorySlotsToShow(); m++)
                {
                    Item item = player.armor[m];
                    if (item is { IsAir: false } && item.type == type) { index = m; social = true; return true; }
                }
            }
            if (normal)
            {
                for (int m = 3; m < 8 + player.GetAmountOfExtraAccessorySlotsToShow(); m++)
                {
                    Item item = player.armor[m];
                    if (item is { IsAir: false } && item.type == type) { index = m; social = false; return true; }
                }
            }
            return false;
        }

        public static bool HasEquipment(Player player, int type, bool normal, bool vanity)
        {
            if (HasAccessory(player, type, normal, vanity) || HasHelmet(player, type, vanity) || HasChestplate(player, type, vanity) || HasLeggings(player, type, vanity))
                return true;
            return false;
        }
    }
}


