using System;

public class Shop
{
    private List<Item> shopItems;

    public Shop()
    {
        shopItems = new List<Item>
        {
            new Item("수련자 갑옷", 0, 3, "수련에 도움을 주는 갑옷", 900, ItemType.Armor),
            new Item("무쇠갑옷", 0, 5, "적당히 튼튼한 철갑옷", 1100, ItemType.Armor),
            new Item("공격의 건틀릿", 2, 0, "가볍지만 묵직한 한방을 줄 수 있다", 800, ItemType.Weapon),
            new Item("날카로운 단검", 5, 0, "작고 빠른 단검. 치명타에 유리하다", 900, ItemType.Weapon),
            new Item("불타는 검", 10, 0, "불꽃이 타오르는 강력한 검", 2000, ItemType.Weapon),
            new Item("마법사의 망토", 0, 8, "마법 저항력이 깃든 망토", 1500, ItemType.Armor),
            new Item("강철 방패", 0, 10, "모든 공격을 막아낼 수 있을 것 같다", 2000, ItemType.Armor)
        };

    }

    public Shop(List<Item> newShopItems)
    {
        shopItems = newShopItems;
    }

    public void ShowShop(Status player)
    {
        Console.WriteLine("\n**상점**");
        Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
        Console.WriteLine("\n[보유 골드]");
        Console.WriteLine($"{player.Gold} G");
        Console.WriteLine("\n[아이템 목록]");
        if (shopItems.Count == 0)
        {
            Console.WriteLine("아이템이 없습니다.");
        }
        else
        {
            foreach (var item in shopItems)
            {
                item.PrintInfoInShop();
            }
        }
        Console.WriteLine("");
    }

    public void ShowPurchaseShop(Status player)
    {
        Console.WriteLine("\n**상점** - 아이템 구매");
        Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
        Console.WriteLine("\n[보유 골드]");
        Console.WriteLine($"{player.Gold} G");
        Console.WriteLine("\n[아이템 목록]");
        if (shopItems.Count == 0)
        {
            Console.WriteLine("아이템이 없습니다.");
        }
        else
        {
            int cnt = 1;
            foreach (var item in shopItems)
            {
                item.PrintInfoInShop(cnt);
                cnt++;
            }
        }
        Console.WriteLine("");
    }

    

    public List<Item> GetAllItems() => shopItems;

}

