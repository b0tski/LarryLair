using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LarryLair
{
    internal class Program
    {
        static void Main(string[] args)
        {

            // PLayers Stats
            Entity player = new Entity();
            player.name = "Botski";

            // Create an Enemy 
            Entity larry = new Entity();
            larry.health = 50;
            int enemyCount = 0;

            Console.WriteLine("Your goal is to Larry! Good luck!");


            while (player.health >= 0 && larry.health >= 0)
            {

                // larry's turn
                DisplayPlayerStats(larry);

                Action(larry, player, "2");

                DisplayPlayerStats(larry);


                // Players turn

                DisplayPlayerStats(player);

                Console.WriteLine("What is your action:");
                string playerInput = Console.ReadLine();
                Action(player, larry, playerInput);

                DisplayPlayerStats(player);

            }


        }

        static void Action(Entity attacker, Entity opponent, dynamic input)
        {

            switch (input)
            {
                case "Attack":
                case "1":
                    // Checks if attacker is stunned

                    if (attacker.isStunned)
                    {
                        Console.WriteLine($"{attacker.name} is stunned!");
                        attacker.isAttacking = false;
                        attacker.isStunned = false;
                    }
                    else
                    {
                        attacker.health -= attacker.incomingDamage;
                        attacker.incomingDamage = 0;

                        Console.WriteLine($"{attacker.name} is attacking!");
                        attacker.Attack(opponent);
                    }

                    break;

                case "Perry":
                case "2":
                    if (attacker.isStunned)
                    {
                        attacker.health -= attacker.incomingDamage;
                        attacker.incomingDamage = 0;

                        Console.WriteLine($"{attacker.name} is stunned!");
                        attacker.isStunned = false;
                    }
                    else
                    {
                        Console.WriteLine($"{attacker.name} is perrying!");
                        attacker.Perry(opponent);
                        Console.WriteLine(opponent.isStunned);
                    }

                    break;
            }
        }

        static void DisplayPlayerStats(Entity entity) 
        {
            Console.WriteLine();
            Console.WriteLine("_____STATS_____");
            Console.WriteLine($"{entity.name}'s health: {entity.health} ");
            Console.WriteLine($"Stunned: {entity.isStunned}");
            Console.WriteLine($"Attacking: {entity.isAttacking}");
            Console.WriteLine($"Perrying: {entity.isPerrying}");
            Console.WriteLine();
        }

    }

    class Entity
    {
        public string name = "Larry";

        public int health = 100;
        public int perryCount = 5;
        public int attackCount = 15;
        public int attackDamage = 10;
        public int incomingDamage = 0;
        public int healCount = 3;

        public bool isStunned = false;
        public bool isPerrying = false;
        public bool isAttacking = false;


        public void Attack(Entity opponent)
        {
            opponent.incomingDamage = attackDamage;

            attackCount -= 1;
            isAttacking = true;
        }

        public void Perry(Entity opponent)
        {
            // Checks if opponent is attacking
            if (opponent.isAttacking) 
            {
                Console.WriteLine("Perried successful");
                opponent.isStunned = true;
            }
            else 
            {
                Console.WriteLine("Missed perry");
                isStunned = true;
                
            }
        }
    }
}
