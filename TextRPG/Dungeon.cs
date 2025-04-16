using System;

public enum DungeonDifficulty
{
    Easy = 1,
    Normal,
    Hard
}
namespace TextRPG
{
    public class DungeonReport
    {
        public DungeonDifficulty Difficulty { get; set; }
        public int HPBefore { get; set; }
        public int HPAfter { get; set; }
        public int GoldBefore { get; set; }
        public int GoldAfter { get; set; }
        public bool IsCleared { get; set; }
    }

    public static class Dungeon
    {
        private static Random random = new Random();
        public static DungeonReport LastReport { get; private set; }  // 마지막 결과 저장용

        public static void EnterDungeon(DungeonDifficulty difficulty, Status player)
        {
            int recommendedDef = 0;
            int baseReward = 0;
            switch (difficulty)
            {
                case DungeonDifficulty.Easy:
                    recommendedDef = 5;
                    baseReward = 1000;
                    break;
                case DungeonDifficulty.Normal:
                    recommendedDef = 11;
                    baseReward = 1700;
                    break;
                case DungeonDifficulty.Hard:
                    recommendedDef = 17;
                    baseReward = 2500;
                    break;
            }

            int hpBefore = player.CurrentHP;
            int goldBefore = player.Gold;
            bool isCleared = true;
            int rewardGold = 0;

            Console.WriteLine($"\n{difficulty} 던전에 입장합니다! (권장 방어력: {recommendedDef})");

            if (player.Defence - recommendedDef < 0)
            {
                bool fail = true;
                if(player.Defence - recommendedDef < -5)
                {
                    Console.WriteLine($"\n{player.Name}님의 방어력이 너무 낮습니다! 힘든 싸움이 예상됩니다..!");
                    fail = random.Next(0, 100) < 80;
                }
                else
                {
                    Console.WriteLine($"\n{player.Name}님의 방어력이 낮습니다. 하지만 부딪혀봐야 아는 법입니다.");
                    fail = random.Next(0, 100) < 40;
                }

                if (fail)
                {
                    int hpLoss_inFail = 40;
                    player.CurrentHP -= hpLoss_inFail;
                    Console.WriteLine("\n던전 실패! 체력이 (40) 감소합니다.");
                    if (player.CurrentHP <= 0)
                    {
                        Console.WriteLine("체력이 0이 되어 쓰러졌습니다. 게임 종료.");
                        Environment.Exit(0); // 프로그램 종료
                    }

                    isCleared = false;
                }
                else
                {
                }
            }
            else
            {
            }

            if (isCleared)
            {
                int defDiff = recommendedDef - player.Defence;
                int hpLoss = random.Next(20 + defDiff, 36 + defDiff);
                player.CurrentHP -= hpLoss;

                if (player.CurrentHP <= 0)
                {
                    Console.WriteLine("던전을 클리어했지만 체력이 0이 되어 쓰러졌습니다. 게임 종료.");
                    Environment.Exit(0);
                }

                // 보상 페널티 적용
                int adjustedBaseReward = baseReward;
                bool isEasyToClear = false;
                if (difficulty == DungeonDifficulty.Easy && player.Level >= 3)
                {
                    isEasyToClear = true;
                    adjustedBaseReward = (int)(baseReward * 0.2);  // 80% 감소
                    Console.WriteLine("레벨이 높아 쉬운 던전의 보상이 감소되었습니다. (-30%)");
                }
                else if (difficulty == DungeonDifficulty.Normal && player.Level >= 5)
                {
                    isEasyToClear = true;
                    adjustedBaseReward = (int)(baseReward * 0.2);  // 80% 감소
                    Console.WriteLine("레벨이 높아 일반 던전의 보상이 감소되었습니다. (-15%)");
                }

                int atkBonusPercent = random.Next((int)player.AtkDmg, (int)player.AtkDmg * 2 + 1);
                int bonusGold = adjustedBaseReward * atkBonusPercent / 100;
                rewardGold = adjustedBaseReward + bonusGold;

                player.Gold += rewardGold;
                player.AddDungeonClear(isEasyToClear);
            }


            // 결과 저장
            LastReport = new DungeonReport
            {
                Difficulty = difficulty,
                HPBefore = hpBefore,
                HPAfter = player.CurrentHP,
                GoldBefore = goldBefore,
                GoldAfter = player.Gold,
                IsCleared = isCleared
            };
        }

        public static void DungeonResult()
        {
            if (LastReport == null)
            {
                Console.WriteLine("던전 탐험 기록이 없습니다.");
                return;
            }

            Console.WriteLine("\n====== 던전 탐색 결과 ======");
            Console.WriteLine($"던전 난이도: {LastReport.Difficulty}");
            Console.WriteLine($"던전 결과: {(LastReport.IsCleared ? "클리어 성공" : "실패")}");
            Console.WriteLine($"체력 변화: {LastReport.HPBefore} → {LastReport.HPAfter}");
            Console.WriteLine($"골드 변화: {LastReport.GoldBefore} → {LastReport.GoldAfter}");
            Console.WriteLine("=============================\n");
        }
    }


}
