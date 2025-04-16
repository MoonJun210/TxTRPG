using System;

namespace TextRPG
{
    internal class Program
    {

        static Inventory inventory = new Inventory();
        static Status player ;
        static Shop shop = new Shop();


        static void Main(string[] args)
        {
            CharacterCreation();
        }

        static void CharacterCreation()
        {
            Console.Clear();
            Console.WriteLine("====== 캐릭터 생성 ======\n");

            Console.Write("당신의 이름을 입력하세요: ");
            string name = Console.ReadLine();

            Console.WriteLine("\n직업을 선택하세요:");
            Console.WriteLine("1. 전사 (공격력 +1 / 방어력 +1)");
            Console.WriteLine("2. 마법사 (공격력 +3 / 방어력 0)");
            Console.Write(">> ");

            string job = "";
            int atk = 0;
            int def = 0;

            while (true)
            {
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        job = "전사";
                        atk = 1;
                        def = 1;
                        break;
                    case "2":
                        job = "마법사";
                        atk = 3;
                        def = 0;
                        break;
                    default:
                        Console.Write("올바른 번호를 선택해주세요 (1~3): ");
                        continue;
                }
                break;
            }

            player = new Status(name, job, atk, def, inventory);
            Console.WriteLine($"\n{name} 님의 {job} 캐릭터가 생성되었습니다!\n");
            Console.WriteLine("아무 키나 눌러 게임을 시작하세요...");
            Console.ReadKey();

            StartMessage();
        }

        static void StartMessage()
        {
            Console.WriteLine("================================");
            Console.WriteLine("\n텍스트 RPG를 가동합니다. 무엇을 하시겠습니까?");
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");
            Console.WriteLine("4. 던전 입장하기");
            Console.WriteLine("5. 휴식하기");
            Console.WriteLine("6. 저장하기");
            Console.WriteLine("7. 불러오기");

            Console.WriteLine("\n");
            Console.Write("원하는 행동을 입력해주세요.\n>> ");


            MenuChoice();
        }

        // 이후 사용할 함수들
        static void ShowStatus()
        {
            Console.WriteLine("\n플레이어의 상태를 표시합니다.\n");

            player.PrintStatus();

            ExitCurrent();
        }

        static void ShowInventory()
        {
            Console.WriteLine("\n인벤토리를 엽니다.\n");
            Console.WriteLine("================================");

            inventory.ShowInventory();

            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("0. 나가기\n");
            Console.Write("원하는 행동을 입력해주세요.\n>> ");


            Handle.HandleMenu(EquiptManage, StartMessage);
        }

        static void EnterShop()
        {
            Console.WriteLine("\n상점에 입장합니다.\n");
            Console.WriteLine("================================");

            shop.ShowShop(player);

            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("2. 아이템 판매");
            Console.WriteLine("0. 나가기\n");
            Console.Write("원하는 행동을 입력해주세요.\n>> ");


            Handle.HandleMenu(PurchaseMenu, SellingMenu, StartMessage);
        }

        static void MenuChoice()
        {
            Handle.HandleMenu(ShowStatus, ShowInventory, EnterShop, EnterDungeon, TakeRelax, SaveGame, LoadGame, StartMessage);
        }

        static void ExitCurrent()
        {
            Console.WriteLine("0. 나가기\n");
            Console.Write("원하는 행동을 입력해주세요.\n>> ");

            Handle.HandleMenu(StartMessage, StartMessage);
        }

        static void EquiptManage()
        {
            inventory.ShowEquipManageInventory();
            Handle.InputInventory(inventory, StartMessage, ShowInventory);
        }

        static void PurchaseMenu()
        {
            shop.ShowPurchaseShop(player);
            Handle.InputShopPurchase(shop, inventory, player, StartMessage, EnterShop);
        }

        static void SellingMenu()
        {
            inventory.ShowSellingShop(player);
            Handle.InputShopSell(inventory, player, StartMessage, EnterShop);
        }

        static void EnterDungeon()
        {
            Console.WriteLine("\n던전에 입장합니다.\n");
            Console.WriteLine("================================");
            Console.WriteLine("\n**던전**");
            Console.WriteLine($"이곳에서 던전으로 들어갈 수 있습니다.\n");
            Console.WriteLine("1. 쉬운 던전\t| 방어력 5 이상 권장");
            Console.WriteLine("2. 일반 던전\t| 방어력 11 이상 권장");
            Console.WriteLine("3. 어려운 던전\t| 방어력 17 이상 권장");
            Console.WriteLine("0. 나가기\n");
            Console.Write("원하는 행동을 입력해주세요.\n>> ");

            Handle.InputDungeon(player, StartMessage, ExitDungeon);

        }

        static void ExitDungeon()
        {
            Dungeon.DungeonResult();
            ExitCurrent();
        }

        static void TakeRelax()
        {
            Console.WriteLine("\n휴식을 준비합니다.\n");
            Console.WriteLine($"500 G 를 내면 체력을 회복할 수 있습니다. (보유골드 : {player.Gold} G)");
            Console.WriteLine("\n1. 휴식하기");
            Console.WriteLine("0. 나가기\n");
            Console.Write("원하는 행동을 입력해주세요.\n>> ");

            Handle.InputRelax(player, StartMessage, StartMessage);
            
        }

        static void SaveGame()
        {
            SaveSystem.SaveGame(player, inventory, shop);
            StartMessage();
        }

        static void LoadGame()
        {
            SaveSystem.LoadGame(out player, out inventory, out shop);
            player.Inventory = inventory;
            StartMessage();
        }

    }

}
