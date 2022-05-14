using System;
using UnityEngine;

namespace NMaterial
{
    public enum Materials
    {
        WOOD, STONE, AXE
    }

    public static class Mat
    {
        private static int itemIds = Enum.GetValues(typeof(Materials)).Length; // Index of all items to register and call back from resources
        static InventoryItemData[] _prefabs;

        public static void LoadPrefabs()
        {
            _prefabs = new InventoryItemData[itemIds];

            for (int i = 0; i < itemIds; i++)
                _prefabs[i] = (InventoryItemData) Resources.Load("Scriptable Objects/" + i, typeof(InventoryItemData));

        }

        public static InventoryItemData getIID(Materials material)
        {
            if (_prefabs.Length == 0) return new InventoryItemData();
            return _prefabs[(int)material];
        }
    }
}
