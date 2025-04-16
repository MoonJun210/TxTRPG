using System;
using Newtonsoft.Json;

public class SaveData
{
    public Status PlayerStatus { get; set; }
    public List<Item> InventoryItems { get; set; }
    public List<Item> ShopItems { get; set; }
}

public static class SaveSystem
{
    private static string savePath = "save.json";

    public static void SaveGame(Status status, Inventory inventory, Shop shop)
    {
        var saveData = new SaveData
        {
            PlayerStatus = status,
            InventoryItems = inventory.GetAllItems(),
            ShopItems = shop.GetAllItems()
        };

        string json = JsonConvert.SerializeObject(saveData, Formatting.Indented);
        File.WriteAllText(savePath, json);

        Console.WriteLine("게임 저장 완료!");

    }
    public static bool LoadGame(out Status status, out Inventory inventory, out Shop shop)
    {
        if (!File.Exists(savePath))
        {
            status = null;
            inventory = null;
            shop = null;
            Console.WriteLine("저장된 데이터가 없습니다.");
            return false;
        }

        string json = File.ReadAllText(savePath);
        var saveData = JsonConvert.DeserializeObject<SaveData>(json);

        status = saveData.PlayerStatus;
        inventory = new Inventory(saveData.InventoryItems);
        shop = new Shop(saveData.ShopItems);

        Console.WriteLine("게임 불러오기 완료!");
        return true;
    }
}
