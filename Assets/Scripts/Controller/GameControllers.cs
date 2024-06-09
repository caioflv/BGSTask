using UnityEngine;

namespace BGSTask
{
    //Manager of all controllers. Ordering the initialization of each controller
    public class GameControllers : MonoBehaviour
    {
        public static GameControllers Instance;

        [SerializeField] private UIController _uiController;
        [SerializeField] private ShopController _shopController;
        [SerializeField] private EventController _eventController;
        public ItemController ItemController;
        [SerializeField] private PlayerController _playerController;
        public InventoryController InventoryController;

        private void Awake()
        {
            Instance = this;

            ItemController.Init();
            _eventController.Init();
            InventoryController.Init();
            _uiController.Init();
            _shopController.Init();
            _playerController.Init();
        }
    }
}

