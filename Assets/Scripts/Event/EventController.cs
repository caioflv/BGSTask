using System;
using UnityEngine;

namespace BGSTask
{
    //All the most important game events can be executed by calling EventControler.EventName

    public class EventController : Controller
    {
        public static Action<bool, Vector2> InteractableElement { get; set; }
        public static Action<bool> TogglePlayerController { get; set; }
        public static Action<bool> ToggleShop { get; set; }
        public static Action<string, string> SetPlayerSkinPart { get; set; }

        public static Action<Item> TryBuyItem { get; set; }
        public static Action<Item> ItemBought { get; set; }
        public static Action<Item> ItemSold { get; set; }

        public static Action<int, Vector2> SpawnCollectableItem { get; set; }
        public static Action<Item> CollectedItem { get; set; }

        public void ToggleShopByButton(bool mode) => ToggleShop.Invoke(mode);
    }
}
