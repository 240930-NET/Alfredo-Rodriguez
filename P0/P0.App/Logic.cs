namespace todolist;
using System.Text.Json;
using System;
using System.IO;

public class Logic { 
    private Dictionary<int, Task> taskList;
    public Dictionary<int, Task>Tasks
    {
        get 
        {
            return taskList;
        }
    }
    private int taskCount; //ends up acting more like a taskID that constantly goes up after adding a task
    //options: 
        //1. add a task
        //      should be able to choose if they want to add a deadline, notes, or both
        //2. edit a task
        //      can complete the task, edit the notes, or change the deadline
        //3. delete a task
        //      should probably have a task id of some sort to do this
        //4. save and exit
        //9. exit without saving

        //should the method to parse through the text file with all the tasks be here too?
        /**
        * Constructor for the logic class. Should initialize a new task list
        */
        public Logic()
        {
            taskList = new Dictionary<int, Task>();
            //load all tasks
            taskCount = 0;
            this.loadTasks();
  
        }

        /**
        * Method to add a new task
        */
        public void addTask()
        {
            string taskName = this.enterName();
            string notes = this.enterNotes();
            DateTime? deadline = this.enterDeadline();
            Console.WriteLine(taskCount);
            Task newTask = new Task(taskCount, taskName, notes, deadline); //id and key should be the same
            taskList.Add(taskCount, newTask);
            taskCount++;
            Console.WriteLine("The following task has been added succesfully!");
            Console.WriteLine(newTask.ToString());
            Console.WriteLine();
        }

        /**
        * Helper method that gets user input for the task name
        */
        private string enterName()
        {
            while(true)
            {
                Console.WriteLine("Enter a name for your task: ");
                string? taskName = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(taskName))
                {
                    return taskName;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nPlease input a name for your task!\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        /**
        * Helper method that gets user input for the task notes
        */
        private string enterNotes()
        {
            Console.WriteLine("Enter some notes for your task (or leave empty for no notes): ");
            string? notes = Console.ReadLine();
            if(notes != null)
            {
                return notes;
            }
            return "";
            
        }
        /**
        * Helper method that gets user input for the task's deadline
        */
        private DateTime? enterDeadline()
        {
            while(true)
            {
                try {
                    Console.WriteLine("Enter a deadline for your task (or leave empty for no deadline). Please use the following format 'mm/dd/yyyy' ");
                    string? date = Console.ReadLine();
                    if(string.IsNullOrWhiteSpace(date))
                    {
                        return null;
                    }
                    else {
                        string[] monthDayYear = date.Split('/', 3);
                        DateTime? deadline = new DateTime(Int32.Parse(monthDayYear[2]), Int32.Parse(monthDayYear[0]), Int32.Parse(monthDayYear[1]));
                        return deadline;
                    }
                    
                }
                catch (Exception)
                {
                    // Console.WriteLine(e);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nPlease input the date in the correct format!\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            

        }
        /**
        * Method to edit a task
        */
        public void editTask()
        {
            //separate stuff in here into multiple methods
            bool editing = true;
            while (editing)
            {
                try {
                    Console.WriteLine("Enter the id of the task you would like to edit: ");
                    string? choice = Console.ReadLine();
                    // int choiceAsInt = Int32.Parse(choice);
                    // choiceAsInt = Int32.Parse(choice);
                    int choiceAsInt = -1;
                    if (choice != null)
                    {
                        choiceAsInt = Int32.Parse(choice); 
                    }
                
                    //might need my own implementation of a list to search with ID
                    //
                    Task taskToEdit = taskList[choiceAsInt];
                    Console.WriteLine("Enter the corresponding number of the task would you like to edit: ");
                    Console.WriteLine("1. Edit Task name");
                    Console.WriteLine("2. Edit notes");
                    Console.WriteLine("3. Edit deadlines");
                    Console.WriteLine("4. Change status completion");
                    Console.WriteLine("9. exit");
                    choice = Console.ReadLine();
                    // choiceAsInt = Int32.Parse(choice);
                    choiceAsInt = -1;
                    if (choice != null)
                    {
                        choiceAsInt = Int32.Parse(choice); 
                    }
                    switch(choiceAsInt)
                    {
                        case 1:
                            taskToEdit.Name = this.enterName();
                            break;
                        case 2:
                            taskToEdit.Notes = this.enterNotes();
                            break;
                        case 3:
                            taskToEdit.Deadline = this.enterDeadline();
                            break;
                        case 4:
                            taskToEdit.Completed = !taskToEdit.Completed;
                            break;
                        case 9:
                            editing = false;
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nImproper input! Please write a number or a proper option!\n");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                    }
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Task has been successfully edited!");
                    Console.ForegroundColor = ConsoleColor.White;
                    editing = false;
                    //after deletion
                    // this.taskCount--;
                }
                catch(Exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nImproper input! Please write an existing ID!\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            
            

        }
        /**
        * Method to delete a task
        */
        public bool deleteTask()
        {
            // bool deleting = true;
            while (true)
            {
                try {
                    Console.WriteLine("Enter the id of the task you would like to delete: ");
                    string? choice = Console.ReadLine();
                    // int choiceAsInt = Int32.Parse(choice);
                    int choiceAsInt = -1;
                    if (choice != null)
                    {
                        choiceAsInt = Int32.Parse(choice); 
                    }
                    //might need my own implementation of a list to search with ID
                    //
                    // Task taskToDelete = taskList[choiceAsInt];
                    return taskList.Remove(choiceAsInt);
                    // deleting = false;
                }
                catch(Exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nImproper input! Please write an existing ID!\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            
        }


        /**
        * Method to save task list and then exit
        */
        public void saveAndExit()
        {
            string outputFile = Path.Combine(Directory.GetCurrentDirectory(), "outputTest.json");
            //write all tasks to a log file in JSON format
            using(StreamWriter writer = new StreamWriter(outputFile))
            {
                for(int i = 0; i < taskCount; i++)
                {
                    if(taskList.ContainsKey(i))
                    {
                        //for each task: (if it exists)
                        //1. Serialize
                        //2. write to file
                        
                        string jsonString = JsonSerializer.Serialize(taskList[i]);
                        
                        writer.WriteLine(jsonString);

                        //stringreader write to file
                    }
                }
                //not saving id = 0??
                
            }
            
        }

        /**
        * Method to load currently saved task list
        */
        public void loadTasks()
        {
            //deserialize all tasks from file (from json to Task)
            //if file doesn't exist create one
            string outputFile = Path.Combine(Directory.GetCurrentDirectory(), "outputTest.json");
            if(File.Exists(outputFile))
            {
                //read all lines
                using (StreamReader reader = new StreamReader(outputFile))
                {
                    string? line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        //deserialize
                        Task? readTask = JsonSerializer.Deserialize<Task>(line);
                        //make a new task and add to list
                        if(readTask != null)
                        {
                            taskList.Add(readTask.TaskId, readTask);
                            taskCount = readTask.TaskId + 1;
                        }
                        
                        // taskCount++;
                        
                    }
                }
                



            }
            else{
                File.Create(outputFile).Dispose();
            }           
        }


        // /**
        // * Overriden ToString to display the entire task list
        // */
        // public override string ToString()
        // {


        /**
        * Writes a list of all the tasks to the console
        */
        public void printString()
        {
            // string printString = "This is your current task list:\n";
            Console.WriteLine("This is your current task list:\n");
            if(taskList.Count() == 0)
            {
                Console.WriteLine("\n\t\t\t Your task list is empty!\n");
            }
            else 
            {
                // printString += "ID\t\tStatus\t\tTask Name\t\tDeadline\t\tNotes\n";
                Console.WriteLine("ID\t\tStatus\t\tTask Name\t\tDeadline\t\tNotes");
                for(int i = 0; i < taskCount; i++)
                {
                    if(taskList.ContainsKey(i))
                    {
                        Task currTask = taskList[i];
                        if (currTask.Completed)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                        else if(currTask.isLate())
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        // printString += currTask.ToString() + "\n";
                        Console.WriteLine(currTask.ToString() + "\n");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                // return printString;
            }
            
        }
}