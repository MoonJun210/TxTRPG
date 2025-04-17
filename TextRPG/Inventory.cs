using System;
using System.Collections.Generic;

public class Inventory
{
    private List<Item> items = new List<Item>();
    private Dictionary<ItemType, Item> equippedItems = new Dictionary<ItemType, Item>();

    public Inventory()
    {

    }

    public Inventory(List<Item> newItems) 
    {
        items = newItems;
    }

    // 아이템 추가
    public void AddItem(Item item)
    {
        items.Add(item);
        Console.WriteLine($"{item.Name}을(를) 인벤토리에 추가했습니다.");
    }

    public void EquipItem(int index, Status player)
    {
        if (index < 0 || index >= items.Count)
        {
            Console.WriteLine("해당 번호의 아이템이 없습니다.");
            return;
        }

        Item item = items[index];

        // 이미 장착된 상태라면 해제
        if (item.IsEquipped)
        {
            item.IsEquipped = false;
            equippedItems.Remove(item.Type);
        }
        else
        {
            // 같은 타입 장비 해제
            if (equippedItems.ContainsKey(item.Type))
            {
                Item oldItem = equippedItems[item.Type];
                oldItem.IsEquipped = false;
            }

            // 장착
            item.IsEquipped = true;
            equippedItems[item.Type] = item;
            Console.WriteLine($"{item.Name}을(를) 장착했습니다.");
        }

        // 장착/해제 후 스탯 재계산
        player.RecalculateStats();
    }


    public void ShowInventory()
    {
        ShowItemList("인벤토리", "보유 중인 아이템을 관리할 수 있습니다.\n", items, (item, index) => item.PrintInfo());
    }

    public void ShowEquipManageInventory()
    {
        ShowItemList("인벤토리 - 장비 관리", "보유 중인 아이템을 관리할 수 있습니다.\n", items, (item, index) => item.PrintInfo(index));
    }

    public void ShowSellingShop(Status player)
    {
        ShowItemList("상점 - 아이템 판매", "필요한 아이템을 얻을 수 있는 상점입니다.\n", items, (item, index) => item.PrintInfoInShopSelling(index), player);
    }

    // 공통 출력 메서드
    private void ShowItemList(string title, string subTitle, List<Item> items, Action<Item, int> printMethod, Status player = null)
    {
        Console.WriteLine($"\n**{title}**");
        Console.WriteLine(subTitle);

        if (player != null)
        {
            Console.WriteLine("\n[보유 골드]");
            Console.WriteLine($"{player.Gold} G");
        }

        Console.WriteLine("\n[아이템 목록]");
        if (items.Count == 0)
        {
            Console.WriteLine("아이템이 없습니다.");
        }
        else
        {
            for (int i = 0; i < items.Count; i++)
            {
                printMethod(items[i], i + 1);
            }
        }
        Console.WriteLine("");
    }

  
    // 인벤토리에 있는 모든 아이템 리스트 반환 (필요 시)
    public List<Item> GetAllItems() => items;

    public void RemoveItem(Item item)
    {
        items.Remove(item);
    }

    public bool HasItem(string itemName)
    {
        return items.Exists(i => i.Name == itemName);
    }

    public void UnequipItem(Item item)
    {
        if (item == null || !item.IsEquipped) return;

        if (equippedItems.ContainsKey(item.Type) && equippedItems[item.Type] == item)
        {
            equippedItems.Remove(item.Type);
        }

        item.IsEquipped = false;
    }
}
