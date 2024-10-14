namespace todolist;
public class Task {

    private int taskId;
    private string name;
    private string notes;
    private DateTime? deadline;
    private bool completed;
    public int TaskId
    {
        get 
        {
            return taskId;
        }
        set 
        {
            taskId = value;
        }
    }
    public string Name 
    {
        get 
        {
            return name;
        }
        set 
        {
            name = value;
        }
    }
    public string Notes
    {
        get 
        {
            return notes;
        }
        set 
        {
            notes = value;
        }
    }
    public DateTime? Deadline 
    { 
        get 
        {
            return deadline;
        }
        set 
        {
            deadline = value;
        }
    }
    public bool Completed { 
        get 
        { 
            return completed;
        } 
        set 
        {
            completed = value;
        }
    }
    // /**
    // * Constructor where only the name of the task is provided
    // */
    // public Task(int taskId, string name)
    // {
    //     this.name = name;
    //     this.completed = false;
    //     this.taskId = taskId;
    // }
    /**
    // * Constructor where a deadline and a name are provided
    // */
    // public Task(int taskId, string name, DateTime deadline)
    // {
    //     this.name = name;
    //     this.completed = false;
    //     this.taskId = taskId;
    //     this.deadline = deadline;
    // }
    /**
    // * Constructor where a name and notes are provided
    // */
    // public Task(int taskId, string name, string notes)
    // {
    //     this.name = name;
    //     this.completed = false;
    //     this.taskId = taskId;
    //     this.notes = notes;
    // }
    /**
    * Constructor where a name, notes and a deadline are all provided
    */
    public Task(int taskId, string name, string notes, DateTime? deadline)
    {
        this.name = name;
        this.completed = false;
        this.taskId = taskId;
        this.notes = notes;
        this.deadline = deadline;
    }

    /** 
    * ToString method for the Task class which prints a task in the proper format to be displayed
    * on the todolist with its id, status, name, deadline and notes.
    */
    public override string ToString()
    {
        char status = this.completed ? 'X' : ' ';
        string date = this.deadline.HasValue ? this.deadline.Value.ToString("MM/dd/yyyy") : "\t";
        return $"{this.taskId}\t\t[{status}]\t\t{this.name}\t\t{date}\t\t{this.notes}";
        //format date in mm/dd/yyyy
    }

    /**
    * Checks if the task is past the deadline
    */
    public bool isLate()
    {
        if (deadline.HasValue && this.deadline < DateTime.Now)
        {
            return true;
        }
        return false;
    }
}