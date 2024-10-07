using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LarryLair
{
    internal class Program
    {
        static void Main(string[] args)
        {

            // PLayers Stats
            Entity player = new Entity();
           

            // Create an Enemy 
            Entity larry = new Entity();

            int delayTime = 1000;
            Random ran = new Random();

            Console.WriteLine("Your goal is to defeat Larry! Good luck!");

            // Gets the players name 
            Console.Write("Please enter a name for your animal\nEnter:");
            string animalsName = Console.ReadLine();
            player.name = animalsName;


            while (player.health >= 0 && larry.health >= 0)
            {

                // Players turn

                DisplayPlayerStats(player);

                if (!player.isStunned)
                {
                    Console.WriteLine("What is your action:");
                    string playerInput = Console.ReadLine();
                    Action(player, larry, playerInput);
                }
                else 
                    Console.WriteLine($"{player.name} is stunned!");
                    player.isStunned = false;

                Thread.Sleep(delayTime);
                Console.WriteLine();
                Console.WriteLine("---------------------------------------------");
                Console.WriteLine();

                // Larry's Turn 


                if (!larry.isStunned)
                {

                    if (larry.health <= 30) Action(larry, player, "Heal");

                    int larrysMoves = ran.Next(1, 3);
                    if (larrysMoves == 1) Action(larry, player, "Attack");
                    else Action(larry, player, "Parry");
                }

                larry.isStunned = false;

                DisplayPlayerStats(larry);

                
                Thread.Sleep(delayTime);
                Console.WriteLine();
                Console.WriteLine("---------------------------------------------");
                Console.WriteLine();
            }
        }

        static void Action(Entity attacker, Entity opponent, dynamic input)
        {

            switch (input)
            {

                case "Attack":
                case "1":
                        Console.WriteLine($"{attacker.name} is attacking!");
                        attacker.Attack(opponent);
                        break;

                case "Pass":
                case "2":
                        Console.WriteLine($"{attacker.name} is passing!");
                        break;

                case "Heal":
                case "3":
                        attacker.Heal(attacker);
                        break;

                case "Parry":
                case "4":
                        Console.WriteLine($"{attacker.name} is perrying");
                        attacker.Parry(opponent);
                        break;
            }

            // Applies damage to the opponent
            if (!opponent.isStunned) opponent.health -= attacker.attackDamage;
        }

        static void DisplayPlayerStats(Entity entity) 
        {
            Console.WriteLine();

            Console.WriteLine($"_____{entity.name}_____");
            Console.WriteLine();
            Console.WriteLine($"Health: {String.Concat(Enumerable.Repeat("-", entity.health))}");
            Console.WriteLine($"Stunned: {entity.isStunned}");
        }
    }

    class Entity
    {
        public string name = "Larry";

        public int health = 100;
        public int perryCount = 5;
        public int attackDamage = 10;
        public int incomingDamage = 0;
        public int healCount = 3;

        public bool isStunned = false;
        public bool isPerrying = false;
        public bool isAttacking = false;


        public void Attack(Entity opponent)
        {
            opponent.incomingDamage = attackDamage;
            isAttacking = true;
        }

        public void Parry(Entity opponent)
        {
            // Checks if opponent is attacking
            if (opponent.isAttacking) 
            {
                Console.WriteLine("Perried successful");
                opponent.isStunned = true;
                opponent.isAttacking = false;
                health += opponent.attackDamage;
            }
            else 
            {
                Console.WriteLine("Missed perry");
                isStunned = true;
            }
        }

        public void Heal(Entity user)
        {
            Random ran = new Random();
            int minHeal = 2;
            int maxHeal = 5;

            int healAmount = ran.Next(minHeal, maxHeal) * 10;

            user.health += healAmount;
            if (user.health > 100) user.health = 100;
        }
    }
}
