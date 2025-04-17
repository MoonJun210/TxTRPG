using System;

public class Status
{
    private int level;
    private string name;
    private string job;
    private float atkDmg;
    private int defence;
    private int currenthp;
    private int maxHp = 100;
    private int gld;
    private int dgClear = 0;

    public int Level { get => level; set => level = value; }
    public string Name { get => name; set => name = value; }
    public string Job { get => job; set => job = value; }
    public float AtkDmg { get => atkDmg; set => atkDmg = value; }
    public int Defence { get => defence; set => defence = value; }
    public float TotalAtk { get; set; }
    public int TotalDef {  get; set; }
    public int CurrentHP { get => currenthp; set => currenthp = value; }
    public int MaxHp {get => maxHp; set => maxHp = value; }
    public int Gold { get => gld; set => gld = value; }
    public Inventory Inventory { get;  set; }
    public int DgClear { get =>dgClear; set => dgClear = value; }

    public Status(string name, string job, float baseAtk, int baseDef, Inventory inventory)
    {
        this.name = name;
        this.job = job;
        this.level = 1;
        this.currenthp = maxHp;
        this.atkDmg = baseAtk;
        this.defence = baseDef;
        this.gld = 1500;

        Inventory = inventory;
        RecalculateStats();

    }

    public void PrintStatus()
    {
        Console.WriteLine("===== 캐릭터 상태 =====");
        Console.WriteLine($"이름: {name}");
        Console.WriteLine($"직업: {job}");
        Console.WriteLine($"레벨: {level}");
        Console.WriteLine($"체력: {currenthp}");
        Console.WriteLine($"공격력: {TotalAtk} (+{ItemAttack()})");
        Console.WriteLine($"방어력: {TotalDef} (+{ItemDefense()})");
        Console.WriteLine($"소지 골드: {gld} G");
        Console.WriteLine("======================\n");

    }

    

    public int ItemAttack()
    {
        
            int bonus = 0;
            foreach (var item in Inventory.GetAllItems())
            {
                if (item.IsEquipped)
                    bonus += item.Attack;
            }
            return bonus;
    }

    public int ItemDefense()
    {
        
            int bonus = 0;
            foreach (var item in Inventory.GetAllItems())
            {
                if (item.IsEquipped)
                    bonus += item.Defense;
            }
            return  bonus;
        
    }

    public void RecalculateStats()
    {
        TotalAtk = atkDmg + ItemAttack();
        TotalDef = defence + ItemDefense();
    }


    public void AddDungeonClear(bool isEasyToClear)
    {
        if(isEasyToClear)
        {
            Console.WriteLine("레벨이 높아 경험치를 얻을 수 없습니다.\n");
        }
        else
        {
            DgClear++;
            UpdateLevel();
        }
      
    }

    void UpdateLevel()
    {
        Level++;
        AtkDmg += 0.5f;
        Defence += 1;

        RecalculateStats();
    }
}
