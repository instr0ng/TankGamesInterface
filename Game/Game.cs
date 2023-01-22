using System;

namespace Game
{

    public interface Player
    {
        public int MAX_HP { get; set; }
        public int HP { get; set; }
        public int Armor { get; set; }
        public int Damage { get; set; }
        public int MAX_Ammo { get; set; }
        public int Cnt_Ammo { get; set; }

        public string Shot(Enemy enemy);

        public void Repair();

        public void Buy_Ammo();

    }

    public interface Enemy
    {
        public int MAX_HP { get; set; }
        public int HP { get; set; }
        public int Armor { get; set; }
        public int Damage { get; set; }
        public int MAX_Ammo { get; set; }
        public int Cnt_Ammo { get; set; }

        public string Shot(Player player);

        public void Repair();

        public void Buy_Ammo();

        public void Turn(Player player);
    }

    public class Tank : Player, Enemy
    {
        public int MAX_HP { get; set; }
        public int HP { get; set; }
        public int Armor { get; set; }
        public int Damage { get; set; }
        public int MAX_Ammo { get; set; }
        public int Cnt_Ammo { get; set; }

        Random rand = new Random();

        private int deaysvie;

        public Tank(int h, int ar, int dm, int ammo)
        {
            MAX_HP = HP = h;
            Armor = ar;
            Damage = dm;
            MAX_Ammo = Cnt_Ammo = ammo;
        }

        public string Shot(Enemy enemy)
        {
            string info;
            int HC = rand.Next(1, 11);
            if (HC < 9)
            {
                if (HC > 1)
                {
                        enemy.HP -= (Damage -   enemy.Armor);
                    Cnt_Ammo--;
                    info = " Нанесено " + (Damage - enemy.Armor) + " урона";
                }
                else
                {
                    enemy.HP -= (int)(Damage * 1.2 - enemy.Armor);
                    Cnt_Ammo--;
                    info = " Критическое попадание! Нанесено " + (int)(Damage * 1.2 - enemy.Armor) + " урона";
                }
            }
            else
            {
                info = " Промах";
                Cnt_Ammo--;
            }
            return info;
        }

        public string Shot(Player player)
        {
            string info;
            int HC = rand.Next(1, 11);
            if (HC < 9)
            {
                if (HC > 1)
                {
                    player.HP -= (Damage - player.Armor);
                    Cnt_Ammo--;
                    info = " Нанесено " + (Damage - player.Armor) + " урона";
                }
                else
                {
                    player.HP -= (int)(Damage * 1.2 - player.Armor);
                    Cnt_Ammo--;
                    info = " Критическое попадание! Нанесено " + (int)(Damage * 1.2 - player.Armor) + " урона";
                }
            }
            else
            {
                info = " Промах";
                Cnt_Ammo--;
            }
            return info;
        }

        public void Repair()
        {
            HP += 50;
            if (HP > MAX_HP)
                HP = MAX_HP;
        }

        public void Buy_Ammo()
        {
            Cnt_Ammo = MAX_Ammo;
        }

        public void Turn(Player player)
        {
            Console.Write("\nХод противника: ");
            if (HP == MAX_HP)
                deaysvie = 1;
            else
                deaysvie = rand.Next(1, 3);
            switch (deaysvie)
            {
                case 1:
                    if (Cnt_Ammo > 0)
                    {
                        Console.WriteLine("1. Выстрел\n");
                        Console.WriteLine(Shot(player));
                    }
                    else
                    {
                        Console.WriteLine("3. Купить боеприпасы\n");
                        Buy_Ammo();
                    }
                    break;

                case 2:
                    Console.WriteLine("2. Починка\n");
                    Repair();
                    break;

            }
        }
    }

   

    class Game
    {
        static void Main()
        {
            Player player = new Tank(500, 20, 100, 8);
            Enemy Enemy = new Tank(300, 20, 50, 5);
            Random Enemy_deystvie = new();
            ConsoleKeyInfo deystvie;

            while (player.HP > 0 && Enemy.HP > 0)
            {
                Console.Clear();
                Console.WriteLine("-----------------------------------------------------------------------------------------------");
                Console.WriteLine($"My Tank HP =  {player.HP} Armor = {player.Armor}  Damage = {player.Damage} Ammo = {player.Cnt_Ammo}\n");
                Console.WriteLine($"Enemy Tank HP =  {Enemy.HP} Armor = {Enemy.Armor} Damage = {Enemy.Damage} Ammo = {Enemy.Cnt_Ammo}\n");
                Console.WriteLine("Выберите действие:\n");
                Console.WriteLine("1. Выстрел\n");
                Console.WriteLine("2. Починка \n");
                Console.WriteLine("3. Купить боеприпасы\n");
                Console.WriteLine("Ваш ход: ");
                deystvie = Console.ReadKey();
                switch (deystvie.Key)
                {
                    case ConsoleKey.D1 or ConsoleKey.NumPad1:

                        if (player.Cnt_Ammo > 0)
                            Console.WriteLine(player.Shot(Enemy));
                        else
                            Console.WriteLine("У вас кончились снаряды");
                        break;

                    case ConsoleKey.D2 or ConsoleKey.NumPad2:
                        player.Repair();
                        break;

                    case ConsoleKey.D3 or ConsoleKey.NumPad3:
                        player.Buy_Ammo();
                        break;

                    default:
                        Console.WriteLine("\nНеверная клавиша\nНажмите любую клавишу чтобы продолжить");
                        Console.ReadKey();
                        continue;

                }

                Enemy.Turn(player);
                
                Console.WriteLine($"My Tank HP =  {player.HP} Armor = {player.Armor}  Damage = {player.Damage} Ammo = {player.Cnt_Ammo}\n");
                Console.WriteLine($"Enemy Tank HP =  {Enemy.HP} Armor = {Enemy.Armor} Damage = {Enemy.Damage} Ammo =  {Enemy.Cnt_Ammo} \n");
                Console.WriteLine("-----------------------------------------------------------------------------------------------");

                Console.Write("Нажмите любую клавишу чтобы продолжить");
                Console.ReadKey();

            }
            Console.Clear();
            if (player.HP > 0)
                Console.WriteLine("Вы выиграли!");
            else
                Console.WriteLine("Вы проиграли!");
        } 
    }
}
