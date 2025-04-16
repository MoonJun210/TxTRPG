using System;

namespace TextRPG
{
    public static class Handle
    {
        public static void Input(Dictionary<int, Action> actions, Action defaultAction)
        {
            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                if (actions.ContainsKey(choice))
                    actions[choice]?.Invoke();
                else
                {
                    Console.WriteLine("보기에 없는 숫자입니다.");
                    defaultAction?.Invoke();
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("숫자를 입력해주세요.");
                defaultAction?.Invoke();
            }
        }

        public static void InputInventory(Inventory inventory, Action defaultAction, Action finallyAction)
        {
            Console.WriteLine("\n0. 나가기\n");
            Console.Write("원하는 행동을 입력해주세요.\n>> "); 
            string input = Console.ReadLine();

            try
            {
                int choice = int.Parse(input);
                if (choice == 0)
                {
                    defaultAction?.Invoke();
                }
                else if (inventory.GetAllItems().Count + 1 >= choice)
                {
                    inventory.EquipItem(choice-1);
                }
                else
                {
                    Console.WriteLine("보기에 없는 숫자입니다.");
                    defaultAction?.Invoke();
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("숫자를 입력해주세요.");
                defaultAction?.Invoke();
            }
            finally
            {
                finallyAction?.Invoke();
            }
        }

        public static void InputShopPurchase(Shop shop, Inventory inventory, Status playerStatus, Action defaultAction, Action finallyAction)
        {
            Console.WriteLine("\n0. 나가기\n");
            Console.Write("원하는 행동을 입력해주세요.\n>> ");
            string input = Console.ReadLine();

            try
            {
                int choice = int.Parse(input);

                if (choice == 0)
                {
                    defaultAction?.Invoke();
                }
                else if (choice >= 1 && choice <= shop.GetAllItems().Count)
                {
                    Item selectedItem = shop.GetAllItems()[choice - 1];

                    if (selectedItem.IsPurchased)
                    {
                        Console.WriteLine("이미 구매한 아이템입니다.");
                        defaultAction?.Invoke();
                        return;
                    }

                    if (inventory.HasItem(selectedItem.Name))
                    {
                        Console.WriteLine("이미 소유 중인 아이템입니다.");
                        defaultAction?.Invoke();
                        return;
                    }

                    if (playerStatus.Gold < selectedItem.Price)
                    {
                        Console.WriteLine("골드가 부족합니다.");
                        defaultAction?.Invoke();
                        return;
                    }

                    // 구매 처리
                    playerStatus.Gold -= selectedItem.Price;
                    selectedItem.IsPurchased = true;
                    inventory.AddItem(selectedItem);

                    Console.WriteLine($"{selectedItem.Name}을(를) 구매했습니다!");
                }
                else
                {
                    Console.WriteLine("보기에 없는 숫자입니다.");
                    defaultAction?.Invoke();
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("숫자를 입력해주세요.");
                defaultAction?.Invoke();
            }
            finally
            {
                finallyAction?.Invoke();
            }
        }
        public static void InputShopSell(Inventory inventory, Status playerStatus, Action defaultAction, Action finallyAction)
        {
            Console.WriteLine("\n0. 나가기\n");
            Console.Write("원하는 행동을 입력해주세요.\n>> ");
            string input = Console.ReadLine();

            try
            {
                int choice = int.Parse(input);

                if (choice == 0)
                {
                    defaultAction?.Invoke();
                    return;
                }

                var allItems = inventory.GetAllItems();

                if (choice >= 1 && choice <= allItems.Count)
                {
                    Item selectedItem = allItems[choice - 1];

                    // 장착 중이면 해제하고 판매
                    if (selectedItem.IsEquipped)
                    {
                        selectedItem.IsEquipped = false;
                        inventory.UnequipItem(selectedItem);
                        
                    }
                    // 판매 처리
                    int sellPrice = selectedItem.Price;
                    playerStatus.Gold += sellPrice * 85/100;
                    selectedItem.IsPurchased = false;
                    inventory.RemoveItem(selectedItem);

                    Console.WriteLine($"{selectedItem.Name}을(를) {sellPrice}G에 판매했습니다!");
                }
                else
                {
                    Console.WriteLine("보기에 없는 숫자입니다.");
                    defaultAction?.Invoke();
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("숫자를 입력해주세요.");
                defaultAction?.Invoke();
            }
            finally
            {
                finallyAction?.Invoke();
            }
        }
        public static void InputRelax(Status playerStatus, Action defaultAction, Action finallyAction)
        {
          

            string input = Console.ReadLine();

            try
            {
                int choice = int.Parse(input);

                switch (choice)
                {
                    case 1:
                        if (playerStatus.Gold >= 500)
                        {
                            playerStatus.Gold -= 500;
                            playerStatus.CurrentHP = playerStatus.MaxHp;
                            Console.WriteLine("휴식을 취했습니다. 체력이 모두 회복되었습니다!");
                        }
                        else
                        {
                            Console.WriteLine("골드가 부족합니다. (500G 필요)");
                        }
                        break;
                    case 0:
                        defaultAction?.Invoke();
                        return;
                    default:
                        Console.WriteLine("보기에 없는 숫자입니다.");
                        break;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("숫자를 입력해주세요.");
            }
            finally
            {
                finallyAction?.Invoke();
            }
        }

        public static void InputDungeon(Status player, Action defaultAction, Action finallyAction)
        {
            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);

                switch (choice)
                {
                    case 1:
                        Dungeon.EnterDungeon(DungeonDifficulty.Easy, player);
                        break;
                    case 2:
                        Dungeon.EnterDungeon(DungeonDifficulty.Normal, player);
                        break;
                    case 3:
                        Dungeon.EnterDungeon(DungeonDifficulty.Hard, player);
                        break;
                    case 0:
                        defaultAction?.Invoke();
                        return;
                    default:
                        Console.WriteLine("보기에 없는 숫자입니다.");
                        break;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("숫자를 입력해주세요.");
            }
            finally
            {
                finallyAction?.Invoke();
            }
        }

        public static void HandleMenu(Action option1, Action defaultAction)
        {
            var actions = new Dictionary<int, Action>
            {
                { 1, option1 },
                { 0, defaultAction }
            };

            Input(actions, defaultAction);
        }

        public static void HandleMenu(Action option1, Action option2, Action defaultAction)
        {
            var actions = new Dictionary<int, Action>
            {
                { 1, option1 },
                { 2, option2 },
                { 0, defaultAction }
            };

            Input(actions, defaultAction);
        }

        public static void HandleMenu(Action option1, Action option2, Action option3, Action defaultAction)
        {
            var actions = new Dictionary<int, Action>
            {
                { 1, option1 },
                { 2, option2 },
                { 3, option3 },
                { 0, defaultAction }
            };

            Input(actions, defaultAction);
        }

        public static void HandleMenu(Action option1, Action option2, Action option3, Action option4, Action defaultAction)
        {
            var actions = new Dictionary<int, Action>
            {
                { 1, option1 },
                { 2, option2 },
                { 3, option3 },
                { 4, option4 },
                { 0, defaultAction }
            };

            Input(actions, defaultAction);
        }

        public static void HandleMenu(Action option1, Action option2, Action option3, Action option4, Action option5, Action defaultAction)
        {
            var actions = new Dictionary<int, Action>
            {
                { 1, option1 },
                { 2, option2 },
                { 3, option3 },
                { 4, option4 },
                { 5, option5 },
                { 0, defaultAction }
            };

            Input(actions, defaultAction);
        }

        public static void HandleMenu(Action option1, Action option2, Action option3, Action option4, Action option5, Action option6, Action defaultAction)
        {
            var actions = new Dictionary<int, Action>
            {
                { 1, option1 },
                { 2, option2 },
                { 3, option3 },
                { 4, option4 },
                { 5, option5 },
                { 6, option6 },
                { 0, defaultAction }
            };

            Input(actions, defaultAction);
        }

        public static void HandleMenu(Action option1, Action option2, Action option3, Action option4, Action option5, Action option6, Action option7, Action defaultAction)
        {
            var actions = new Dictionary<int, Action>
            {
                { 1, option1 },
                { 2, option2 },
                { 3, option3 },
                { 4, option4 },
                { 5, option5 },
                { 6, option6 },
                { 7, option7 },
                { 0, defaultAction }
            };

            Input(actions, defaultAction);
        }
    }
}

