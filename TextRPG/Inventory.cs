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

    // 아이템 출력
    public void ShowInventory()
    {
        Console.WriteLine("\n**인벤토리**");
        Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
        Console.WriteLine("\n[아이템 목록]");
        if (items.Count == 0)
        {
            Console.WriteLine("아이템이 없습니다.");
        }
        else
        {
            foreach (var item in items)
            {
                item.PrintInfo();
            }
        }
        Console.WriteLine("");
    }

    public void ShowEquipManageInventory()
    {
        Console.WriteLine("\n**인벤토리** - 장비 관리");
        Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
        Console.WriteLine("\n[아이템 목록]");
        if (items.Count == 0)
        {
            Console.WriteLine("아이템이 없습니다.");
        }
        else
        {
            int cnt = 1;
            foreach (var item in items)
            {
                item.PrintInfo(cnt);
                cnt++;
            }
        }
        Console.WriteLine("");
    }



    public void EquipItem(int index)
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
            return;
        }

        // 같은 타입의 장비가 이미 장착되어 있다면 해제
        if (equippedItems.ContainsKey(item.Type))
        {
            Item oldItem = equippedItems[item.Type];
            oldItem.IsEquipped = false;
        }

        // 새 아이템 장착
        item.IsEquipped = true;
        equippedItems[item.Type] = item;
        Console.WriteLine($"{item.Name}을(를) 장착했습니다.");
    }

    public void ShowSellingShop(Status player)
    {
        Console.WriteLine("\n**상점** - 아이템 판매");
        Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
        Console.WriteLine("\n[보유 골드]");
        Console.WriteLine($"{player.Gold} G");
        Console.WriteLine("\n[아이템 목록]");
        if (items.Count == 0)
        {
            Console.WriteLine("아이템이 없습니다.");
        }
        else
        {
            int cnt = 1;
            foreach (var item in items)
            {
                item.PrintInfoInShopSelling(cnt);
                cnt++;
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
