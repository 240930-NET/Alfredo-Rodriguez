namespace P0.Test;
using todolist;
using Xunit;
public class UnitTest1
{
    //tests needed:
    //1. Adding a task
    //  test for no deadline, no notes, improper name, improper deadline
    //2. edit a task
    //  test for editing name, deadline and notes, along with improper name or deadline (also trying to edit non existing id)
    //  Also test for completion
    //3. test for deleting a task and for trying to delete non existent task
    //4. test for exit (both with save and no save)
    private Logic logic;


    /**
    * Constructor/setUp() for all unit tests
    * Adds a base task that is used for all methods
    */
    public UnitTest1()
    {
        logic = new Logic();
        var input = new StringReader("Test task 1\nNotes for test task\n01/01/2001\n");
        Console.SetIn(input);
        logic.addTask();

    }

    /**
    * Tests for the adding functionality both when all fields are filled
    * and when deadline or notes are empty 
    */
    [Fact]
    public void Add_Success()
    {
        //add after filling all fields
        Assert.Equal("Test task 1", logic.Tasks[0].Name);
        Assert.Equal("Notes for test task", logic.Tasks[0].Notes);
        Assert.Equal("01/01/2001", logic.Tasks[0].Deadline.Value.ToString("MM/dd/yyyy"));
        //add after no deadline or notes
        var input = new StringReader("Test task 2\n\n\n");
        Console.SetIn(input);
        logic.addTask();
        Assert.Equal("Test task 2", logic.Tasks[1].Name);
        Assert.Equal("", logic.Tasks[1].Notes);
        Assert.Null(logic.Tasks[1].Deadline);
    }

    /**
    * Tests for the adding functionality when no name is input
    * by the user or when a deadline is input with improper formatting
    */
    [Fact]
    public void Add_Fail()
    {
        //improper name
        var input = new StringReader("\nName to avoid infinite loop\n\n\n");
        var output = new StringWriter();
        Console.SetIn(input);
        Console.SetOut(output);
        logic.addTask();
        var terminalMessage = output.ToString();
        Assert.Contains("Please input a name for your task!", terminalMessage);
        //improper deadline
        input = new StringReader("Test Task\nNotes\nThis is not a date\n\n");
        output = new StringWriter();
        Console.SetIn(input);
        Console.SetOut(output);
        logic.addTask();
        terminalMessage = output.ToString().Trim();
        Assert.Contains("Please input the date in the correct format!", terminalMessage);
    }

    /**
    * Tests for the edit functionality for editing a name, notes, deadline
    * or completing a task
    */
    [Fact]
    public void Edit_Success()
    {
        //edit name
        var input = new StringReader("0\n1\nEdited name\n");
        Console.SetIn(input);
        logic.editTask();
        Assert.Equal("Edited name", logic.Tasks[0].Name);
        //edit notes
        input = new StringReader("0\n2\nEdited notes\n");
        Console.SetIn(input);
        logic.editTask();
        Assert.Equal("Edited notes", logic.Tasks[0].Notes);
        //edit deadline
        input = new StringReader("0\n3\n10/10/2020\n");
        Console.SetIn(input);
        logic.editTask();
        Assert.Equal("10/10/2020", logic.Tasks[0].Deadline.Value.ToString("MM/dd/yyyy"));
        //toggle completed status
        input = new StringReader("0\n4\n10/10/2020\n");
        Console.SetIn(input);
        logic.editTask();
        Assert.True(logic.Tasks[0].Completed);
    }

    /**
    * Tests for editing functionality when the name input is empty or the new deadline has
    * an improper format
    */
    [Fact]
    public void Edit_Fail()
    {
        //improper name
        var input = new StringReader("0\n1\n\nname to avoid infinite loop");
        var output = new StringWriter();
        Console.SetIn(input);
        Console.SetOut(output);
        logic.editTask();
        var terminalMessage = output.ToString();
        Assert.Contains("Please input a name for your task!", terminalMessage);
        //improper deadline
        input = new StringReader("0\n3\nThis is not a date\n\n");
        output = new StringWriter();
        Console.SetIn(input);
        Console.SetOut(output);
        logic.editTask();
        terminalMessage = output.ToString().Trim();
        Assert.Contains("Please input the date in the correct format!", terminalMessage);
        //non existing id
        input = new StringReader("76\n0\n2\n\n");
        output = new StringWriter();
        Console.SetIn(input);
        Console.SetOut(output);
        logic.editTask();
        terminalMessage = output.ToString().Trim();
        Assert.Contains("Improper input! Please write an existing ID!", terminalMessage);
    }


    /**
    * Tests for delete functionality when a existing task is properly 
    * deleted
    */
    [Fact]
    public void Delete_Success()
    {
        var input = new StringReader("0\n");
        Console.SetIn(input);
        logic.deleteTask();
        Assert.Equal(logic.Tasks.Count, 0);
        Assert.False(logic.Tasks.ContainsKey(0));
    }


    /**
    * Tests for delete functionality when the user tries to delete
    * a task with an id that doesn't exist or when the id input
    * is not a number
    */
    [Fact]
    public void Delete_Fail()
    {
        //non-existent id
        var input = new StringReader("76\n1\n");
        var output = new StringWriter();
        Console.SetIn(input);
        Console.SetOut(output);
        
        
        Assert.False(logic.deleteTask());

        //improper input
        input = new StringReader("a\n1\n");
        output = new StringWriter();
        Console.SetIn(input);
        Console.SetOut(output);
        logic.deleteTask();
        string terminalMessage = output.ToString().Trim();
        Assert.Contains("Improper input! Please write an existing ID!", terminalMessage);
    }
    // [Fact]














    // public void Exit_Save()
    // {
    //     //NOTE: This test screwed over a couple of other tests because it kept adding more tasks and saving them
    //     //cannot find where the output file thaT IT'S LOADING FROM IS AT
    //     // logic.saveAndExit();
    //     // string cwd = Path.Combine(Directory.GetCurrentDirectory(), "output.txt");
    //     // using(StreamReader reader = new StreamReader(cwd))
    //     // {
    //     //     string line = reader.ReadLine();
    //     //     Assert.Equal("{\"TaskId\":1,\"Name\":\"Test task 1\",\"Notes\":\"Notes for test task\",\"Deadline\":\"2001-01-01T00:00:00\",\"Completed\":false}", line);
    //     // }
    //     //read from output after saving
    //     //compare to hand typed json
    // }
    // [Fact]
    // public void Exit_NoSave()
    // {
    // }
}