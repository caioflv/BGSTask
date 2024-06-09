using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

namespace BGSTask
{
    //Responsible for loading all items from the .xlsx spreadsheet
    //The data is stored in a dictionary Key: Type, Value: List of items

    public partial class ItemController : Controller
    {
        [SerializeField] private Transform _collectablePrefab;

        //Type - Item
        public Dictionary<string, List<Item>> ItemDictionary { get; set; }

        public override void Init()
        {
            EventController.SpawnCollectableItem += SpawnRandomCollectableItems;

            ItemDictionary = new();

            DataTableCollection tables = new LoadSpreadsheet().Load(Application.dataPath + "/StreamingAssets/ItemDatabase.xlsx");
            foreach (DataTable table in tables)
            {
                for (int r = 1; r < table.Rows.Count; r++)
                {
                    string code = Convert.ToString(table.Rows[r][0]).Replace(" ", string.Empty);

                    if (string.IsNullOrEmpty(code))
                        continue;

                    Item auxItem = new()
                    {
                        Code = Convert.ToString(table.Rows[r][0]),
                        Name = Convert.ToString(table.Rows[r][1]),
                        Type = Convert.ToString(table.Rows[r][2]),
                        Price = Convert.ToInt32(table.Rows[r][3])
                    };

                    if (ItemDictionary.ContainsKey(auxItem.Type))
                    {
                        ItemDictionary[auxItem.Type].Add(auxItem);
                    }
                    else
                    {
                        ItemDictionary.TryAdd(auxItem.Type, new List<Item> { auxItem });
                    }
                }
            }
        }
        public void SpawnRandomCollectableItems(int amount, Vector2 position)
        {
            for (int i = 0; i < amount; i++)
            {
                Transform spawnedItem = Instantiate(_collectablePrefab, position, Quaternion.identity);

                spawnedItem.DOMove(position + UnityEngine.Random.insideUnitCircle * 5, 0.3f);

                spawnedItem.GetComponent<CollectableItem>().SetInfo(ItemDictionary["Collectable"][UnityEngine.Random.Range(0, ItemDictionary["Collectable"].Count)]);
            }
        }
    }
}
