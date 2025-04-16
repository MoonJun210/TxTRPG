using System;


public enum ItemType
{
    Weapon,
    Armor
}

public class Item
{
    public string Name { get; set; }          // 아이템 이름
    public int Attack { get; set; }           // 공격력 보너스
    public int Defense { get; set; }          // 방어력 보너스
    public string Description { get; set; }   // 아이템 설명
    public bool IsEquipped { get; set; }      // 장착 여부
    public int Price { get; } // 가격 추가
    public bool IsPurchased { get; set; } // 구매 여부
    public ItemType Type { get; set; }

    // 생성자
    public Item(string name, int attack, int defense, string description, int price, ItemType type)
    {
        Name = name;
        Attack = attack;
        Defense = defense;
        Description = description;
        IsEquipped = false;
        Price = price;
        IsPurchased = false;
        Type = type;
    }

    // 아이템 정보 출력
    public void PrintInfo()
    {
        string equipMark = IsEquipped ? "[E]" : "   ";
        string statInfo = "";

        if (Attack > 0) statInfo += $"공격력 +{Attack}";
        if (Attack > 0 && Defense > 0) statInfo += " | ";
        if (Defense > 0) statInfo += $"방어력 +{Defense}";

        Console.WriteLine($"- {equipMark}{Name,-12}\t| {statInfo,-12}\t| {Description}");
    }

    // 아이템 정보 출력
    public void PrintInfo(int num)
    {
        string equipMark = IsEquipped ? "[E]" : "   ";
        string statInfo = "";

        if (Attack > 0) statInfo += $"공격력 +{Attack}";
        if (Attack > 0 && Defense > 0) statInfo += " | ";
        if (Defense > 0) statInfo += $"방어력 +{Defense}";

        Console.WriteLine($"- {num}. {equipMark}{Name,-10}\t| {statInfo,-12}\t| {Description}");
    }

    public void PrintInfoInShop()
    {
        string purchasedMark = IsPurchased ? "구매 완료" : $"{Price} G";
        string statInfo = "";

        if (Attack > 0) statInfo += $"공격력 +{Attack}";
        if (Attack > 0 && Defense > 0) statInfo += " | ";
        if (Defense > 0) statInfo += $"방어력 +{Defense}";

        Console.WriteLine($"- {Name,-10}\t| {statInfo,-12}\t| {Description, -30}\t| {purchasedMark}");
    }

    public void PrintInfoInShop(int num)
    {
        string purchasedMark = IsPurchased ? "구매 완료" : $"{Price} G";
        string statInfo = "";

        if (Attack > 0) statInfo += $"공격력 +{Attack}";
        if (Attack > 0 && Defense > 0) statInfo += " | ";
        if (Defense > 0) statInfo += $"방어력 +{Defense}";

        Console.WriteLine($"- {num}. {Name,-10}\t| {statInfo,-12}\t| {Description ,-30}\t| {purchasedMark}");
    }

    public void PrintInfoInShopSelling(int num)
    {
        //string purchasedMark = IsPurchased ? "구매 완료" : $"{Price} G";
        string statInfo = "";

        if (Attack > 0) statInfo += $"공격력 +{Attack}";
        if (Attack > 0 && Defense > 0) statInfo += " | ";
        if (Defense > 0) statInfo += $"방어력 +{Defense}";

        Console.WriteLine($"- {num}. {Name,-10}\t| {statInfo,-12}\t| {Description, -30}\t| {Price * 85/100} G");
    }
}
