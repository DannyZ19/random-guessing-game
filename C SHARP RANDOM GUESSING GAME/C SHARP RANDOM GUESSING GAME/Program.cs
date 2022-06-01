using System.Linq;

using System.Collections.Generic;

using System.Text.Json;


/// /////////////////


Console.WriteLine("Daniel Zahiroddini's Guessing Game");

List<int> CountAvg = new List<int>();
List<int> SaveScore;

Random Number = new Random();

int correctNumber = Number.Next(1, 31);
bool win = false;
int count = 0;
int avg = 1;
int store = 0;

// LOAD SCORE FROM FILE
string scoreStr = File.ReadAllText(@"C:\Users\d.zahiroddini\Desktop\SaveScore\score1.txt");

if (scoreStr != "")
{
    var options = new JsonSerializerOptions();
    SaveScore = JsonSerializer.Deserialize<List<int>>(scoreStr, options);
} else
{
    SaveScore = new List<int>();
}



BeginningGame(); // asks if user wants to load save file, or start from scratch // 






do
{
    GuessingGame();  // main code of game
    if (win == true)
    {
        Console.WriteLine("\nWould you like to play again?");
        Console.WriteLine("\n Press Y to play again \n Press N to exit \n Press S to save"); // p // 
        string response = Console.ReadLine();
        if (response == "N")
        {
            Console.WriteLine("\nHave a great day!");

            break;
            

        }
        else if (response == "Y")
        {
            RestartGame();

            continue;
        } 
        else if (response == "S")
        {
            Console.WriteLine("Saving...");

            var options = new JsonSerializerOptions();
            string jsonString = JsonSerializer.Serialize(SaveScore, options);
            // Write String Data to File
            // File will be created if it doesn't exist
            // File.WriteAllText(path_to_file, string_to_write);

            File.WriteAllText(@"C:\Users\d.zahiroddini\Desktop\SaveScore\score1.txt", jsonString);

            Console.WriteLine("Saved!"); // wip // 
            BeginningGame();

            
            
        }
    } 
    



} while (true);




void GuessingGame()
{

    string input = Console.ReadLine(); // get user input
    int num = -1;
   
    
    if (!int.TryParse(input, out num)) // check if user input is an integer, otherwise return as "not an integer"
    {
        Console.WriteLine("\nNot an integer");
    } else if (num > 30)
    {
        Console.WriteLine("\nPlease enter a number inside the range");


    }
    else if (num > correctNumber)

    {

        Console.WriteLine("Smaller");
        count++;
        Console.WriteLine($"\n{count} total attempts");

    }
    else if (num < correctNumber)

    {
        Console.WriteLine("\nHigher");
        count++;
        Console.WriteLine($"\n{count} total attempts");

    }
    else if (num == correctNumber)
    {
        Console.WriteLine($"\nCorrect! The digit was {correctNumber}");
        count++;
        
        SaveScore.Add(count);
        SaveScore.Add(avg);
       

        Console.WriteLine($"\nIt took {count} total attempts to get the number right");

        AverageAttempts();  // # of attempts each game stored in a list // 


        win = true;

    }
   
}
    void RestartGame()
  {
    avg++;
    count = 0;
    win = false;
    correctNumber = Number.Next(1, 31);
    Console.WriteLine("\nI am thinking of a number between 1-30. Can you guess the number?");

  }

 /*void GuessingGameLvl2()
{
    string a = Console.ReadLine();
    int b = int.Parse(a);
    win = false;
    correctNumber = Number.Next(1, 51);
    Console.WriteLine("\nI am thinking of a number between 1-50. Can you guess the number?");
    
    if (b > 50)
    {
        Console.WriteLine("\nPlease enter a number inside the range");
    
    } else if (b > correctNumber && correctNumber < 50)
    
    {
        Console.WriteLine("\nSmaller");
        count++;
        Console.WriteLine($"\n{count} total attempts");
    
    } else if (b < correctNumber)
    {
        Console.WriteLine("Higher");
        count++;
        Console.WriteLine($"{count} total attempts");
    } else if (b == correctNumber)
    {
        Console.WriteLine($"\nCorrect! The digit was {correctNumber}");
        count++;
    }
}
 */

 void AverageAttempts()
{
    
    CountAvg.Add(count);
     store = CountAvg.Sum(x => Convert.ToInt32(x));
    double ttlavg = (double)store / avg;
    Console.WriteLine($"\nAverage attempts: {ttlavg}");
    Console.WriteLine($"\nTotal attempts: {store} ");
}

void BeginningGame()
{

    win = false;
    
    Console.WriteLine("Welcome!\n \nWould you like to play from a previous load, or start from scratch?");
    string responsePrev = Console.ReadLine();
    if (responsePrev == "A")
    {
        Console.WriteLine("Was this your data?");


        // Load JSON string from file
        string jsonStringFromFile = File.ReadAllText(@"C:\Users\d.zahiroddini\Desktop\SaveScore\score1.txt");
        Console.WriteLine(jsonStringFromFile);

        

        string answer = Console.ReadLine();
        if (answer == "Y")
        {
            RestartGame();
            avg--;
            
            if (win == true)
            {
                SaveScore.Add(count);
                SaveScore.Add(avg); // figure out why it does not add score when boot up
                
            }
        }
    }
    else if (responsePrev == "B")
    {
        Console.WriteLine("\nI am thinking of a number between 1-30. Can you guess the number?");
        correctNumber = Number.Next(1, 31);

        Clear();
        
        GuessingGame();
        avg = 0;
        avg++;
        
        
    }
}


void Clear()
{
    SaveScore.Clear();
    CountAvg.Clear();
}