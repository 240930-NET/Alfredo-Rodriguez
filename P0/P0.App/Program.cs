
namespace todolist;
class Program {

    public static void Main(string[] args)
    {
        //make a todolist for now just to keep it simple
        //keep a text file with all the tasks their status, any deadlineas and any notes
        //"infinite" loop until the user exits and/or saves
        //deadline could be also edited so that it displays a symbol or a warning if you have any late tasks (can also change the txt color)
        //options: 
        //1. add a task
        //      should be able to choose if they want to add a deadline, notes, or both
        //2. edit a task
        //      can complete the task, edit the notes, or change the deadline
        //3. delete a task
        //      should probably have a task id of some sort to do this
        //4. save and exit
        //9. exit without saving
        Logic logic = new Logic();
        
        start(logic);
        // Console.WriteLine("ID\t\tStatus\t\tTask Name\t\tDeadline\t\tNotes");
        // Task newTask = new Task(0, "Test", "This is a test", DateTime.Now);
        // Console.WriteLine(newTask.ToString());
    }

    public static void start(Logic logic)
    {
        Console.WriteLine("Welcome to your task list!");
        // Console.WriteLine(logic.ToString());
        // Console.WriteLine("What would you like to do? (input a number)");
        bool isRunning = true;
        while (isRunning)
        {
            try {
                // Console.WriteLine(logic.ToString());
                logic.printString();
                Console.WriteLine("What would you like to do? (input a number)");
                Console.WriteLine("1. add a task");
                Console.WriteLine("2. edit a task");
                Console.WriteLine("3. delete a task");
                Console.WriteLine("4. save and exit"); // could display in green
                Console.WriteLine("9. exit without saving"); // could display in red and then give a warning
                string? choice = Console.ReadLine();
                int choiceInt = -1;
                if (choice != null)
                {
                    choiceInt = Int32.Parse(choice); //is it a format exception if it's not a number? Read up on that!
                }
                
            
                switch(choiceInt)
                {
                    case 1:
                        logic.addTask();
                        break;
                    case 2:
                        logic.editTask();
                        break;
                    case 3:
                        logic.deleteTask();
                        break;
                    case 4:
                        logic.saveAndExit();
                        isRunning = false;
                        break;
                    case 9:
                        isRunning = false;
                        break;
                    default:
                        //if not a proper number
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nImproper input! Please write a number or a proper option!\n");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                }
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nImproper input! Please write a number or a proper option!\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
            
            
        }
    }




}