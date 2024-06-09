using System.Collections.Generic;

namespace BGSTask
{
    //The Inventory class stores all the items that the player owns. Both those in the wardrobe and items collected from chests

    public class InventoryController : Controller
    {
        private int _coin;
        public Dictionary<string, List<Item>> Items; //type - List<Item>

        public override void Init()
        {
            Items = new();

            EventController.ItemBought += OnItemBought;
            EventController.ItemSold += OnItemSold;
            EventController.CollectedItem += AddItem;

            //Adding some items on player inventory
            Items.Add(CharacterSkinPart.Hood.ToString(), new List<Item> { GameControllers.Instance.ItemController.ItemDictionary[CharacterSkinPart.Hood.ToString()][0] });
            Items.Add(CharacterSkinPart.Face.ToString(), new List<Item> { GameControllers.Instance.ItemController.ItemDictionary[CharacterSkinPart.Face.ToString()][0] });
            Items.Add(CharacterSkinPart.Shoulder.ToString(), new List<Item> { GameControllers.Instance.ItemController.ItemDictionary[CharacterSkinPart.Shoulder.ToString()][0] });
            Items.Add(CharacterSkinPart.Torso.ToString(), new List<Item> { GameControllers.Instance.ItemController.ItemDictionary[CharacterSkinPart.Torso.ToString()][0] });
            Items.Add(CharacterSkinPart.Leg.ToString(), new List<Item> { GameControllers.Instance.ItemController.ItemDictionary[CharacterSkinPart.Leg.ToString()][0] });
            Items.Add(CharacterSkinPart.Boot.ToString(), new List<Item> { GameControllers.Instance.ItemController.ItemDictionary[CharacterSkinPart.Boot.ToString()][0] });
        }

        public int GetTotalCoin()
        {
            return _coin;
        }

        public int GetTotalCollectableItems()
        {
            return Items.ContainsKey("Collectable")? Items["Collectable"].Count : 0;
        }

        public bool ContainsItem(Item item)
        {
            return Items.ContainsKey(item.Type) && Items[item.Type].Contains(item);
        }

        
        private void OnItemBought(Item item)
        {
            _coin -= item.Price;
            AddItem(item);
        }

        private void AddItem(Item item)
        {
            if (Items.ContainsKey(item.Type))
            {
                Items[item.Type].Add(item);
            }
            else
            {
                Items.Add(item.Type, new List<Item> { item });
            }
        }

        private void OnItemSold(Item item)
        {
            Items[item.Type].Remove(item);
            _coin += item.Price;
        }
    }
}


