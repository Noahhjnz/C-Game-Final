using System;
using System.Transactions;

class Program
{
    static void Main(string[] args)
    {
         
        Console.WriteLine("Welcome to ASCII Art Dungeon Crawler!");
        Console.WriteLine("Use WASD keys to move (W: Up, A: Left, S: Down, D: Right)");
        Console.WriteLine("Press Q to quit");

        // player position
        int playerX = 0;
        int playerY = 0;

        //  enemy position
        int enemyX = -1; 
        int enemyY = -1; 

        
        // encounter chance 
        double encounterChance = 1;

        
         // List of active enemies
        List<Enemy> activeEnemies = new List<Enemy>();

        // Array of enemy types
        Enemy[] enemyTypes = {
            new Enemy("Goblin", 20, 7),
            new Enemy("Skeleton", 20, 7),
            new Enemy("Slime", 20, 7)
        };



        // Main game loop
        while (true)
        {
            // Check for enemy encounter
            if (ShouldEncounterEnemy(encounterChance) && enemyX == -1 && enemyY == -1)
            {
                // Spawn enemy at a random position
                (enemyX, enemyY) = GetRandomPosition();
                Enemy enemy = GetRandomEnemy(enemyTypes);
                activeEnemies.Add(enemy);
            }

            // Check if player is on the same position as enemy
            if (playerX == enemyX && playerY == enemyY)
            {
                Enemy enemy = activeEnemies[0]; 
                StartCombat(enemy);
                if (IsEnemyDefeated(enemy))
                {
                    activeEnemies.Remove(enemy);
                    enemyX = -1; // Remove enemy from the grid
                    enemyY = -1; // Remove enemy from the grid
                }
            }
            
            // Display dungeon map
            DrawDungeon(playerX, playerY, enemyX, enemyY);

            // Read player input
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            char keyPressed = char.ToUpper(keyInfo.KeyChar);

            // Update player position based on input
            switch (keyPressed)
            {
                case 'W':
                    playerY = Math.Max(playerY - 1, 0);
                    break;
                case 'A':
                    playerX = Math.Max(playerX - 1, 0);
                    break;
                case 'S':
                    playerY = Math.Min(playerY + 1, 4); //  ajust according to the dungeon size
                    break;
                case 'D':
                    playerX = Math.Min(playerX + 1, 4); // adjust according to the dungeon size
                    break;
                case 'Q':
                    Console.WriteLine("Thanks for playing!");
                    return;
            }//end switch

         }//end while


          

    }//end main

    static void DrawDungeon(int playerX, int playerY, int enemyX, int enemyY)
    {
        
        Console.Clear();
        for (int y = 0; y < 5; y++) // Adjust according to dungeon size
        {
            for (int x = 0; x < 5; x++) // Adjust according to dungeon size

            {
                if (x == playerX && y == playerY)
                {
                    Console.Write("P "); // Player symbol
                }
                 else if (x == enemyX && y == enemyY)
                {
                    Console.Write("E "); // Enemy symbol
                }
                else
                {
                    Console.Write(". "); // Empty grid symbol
                }


                
            }
            Console.WriteLine();
            
            
        }//end for
    }//end DrawDungeon

static bool ShouldEncounterEnemy(double encounterChance)
    {
        Random random = new Random();
        return random.NextDouble() <= encounterChance;
    } //end ShouldEncounterEnemy
    


     static void StartCombat(Enemy enemy)
    {
        Console.WriteLine($"You encountered a {enemy.Name}! Prepare to fight!");
        Console.WriteLine($"Enemy HP: {enemy.Health}, Enemy Damage: {enemy.Damage}");
        {
        int playerHp = 40;
        int enemyHp = 20;

        int playerAttack = 5;
        int enemyAttack = 7;

        int healAmount = 5;
        Random random = new Random();

        
        
        while(playerHp > 0 && enemyHp > 0) 
        {
            //players turn
            Console.WriteLine("--Player Turn--");
            Console.WriteLine("Player Hp " + playerHp + ". Enemy Hp - " + enemyHp);
            Console.WriteLine("Enter A to Attack or H to Heal");

            string choice = Console.ReadLine();
            if (!string.IsNullOrEmpty(choice)) 

            if(choice.ToLower() == "a") 
            {
                enemyHp -= playerAttack;
                Console.WriteLine("player attacks enemy and deals " + playerAttack + " damage");
            }
        
             else if (choice.ToLower() == "h")
            {
                playerHp += healAmount;
                Console.WriteLine("Player restores " + healAmount + " health points");
            }

            else
            {
                Console.WriteLine("Invalid choice. Please enter 'A' to attack or 'H' to heal.");
            }

            else
            {
                Console.WriteLine("Invalid choice. Please enter 'A' to attack or 'H' to heal.");
            }

            
        
        
        //enemy turn    
        if(enemyHp  > 0 )
        {
            Console.WriteLine("-- Enemy Turn --");
            Console.WriteLine("Player Hp " + playerHp + ". Enemy Hp - " + enemyHp);
            int enemyChoice = random.Next(0, 2);

            if(enemyChoice == 0)
            {
                playerHp -= enemyAttack;
                Console.WriteLine("Enemy attacks");
                

                if(enemyChoice == 0)
                {
                    playerHp -= enemyAttack;
                    Console.WriteLine("Enemy Attacks And Deals " + enemyAttack + " damage!");
                }
                else
                {
                    enemyHp += healAmount; 
                    Console.WriteLine("Enemy restores " + healAmount + " heal points!");
                }
            }
        }
        
         
        
        }//end while 
        if(playerHp > 0 )
        {
            Console.WriteLine("You Defeated The Enemy");
        }
        else
        { 
            Console.WriteLine("you have been defeated");
            return;
        }

       
    
        
    }//end identifierMethod
        
    } //end StartCombat
    
    static (int, int) GetRandomPosition()
    {
        Random random = new Random();
        int x = random.Next(5); // Adjust according to dungeon size
        int y = random.Next(5); // Adjust according to dungeon size
        return (x, y);
    } //end GetRandomPosition

    static Enemy GetRandomEnemy(Enemy[] enemies)
    {
        Random random = new Random();
        return enemies[random.Next(enemies.Length)];
    }

     static bool IsEnemyDefeated(Enemy enemy)
    {
        // Implement logic to determine if the enemy is defeated
        return true;
    }
}

class Enemy
{
    public string Name { get; }
    public int Health { get; }
    public int Damage { get; }

    public Enemy(string name, int health, int damage)
    {
        Name = name;
        Health = health;
        Damage = damage;
    }
    


}//end class