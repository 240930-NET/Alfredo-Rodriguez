namespace P1.API.Model;

public class User
{
    public int UserId{get;set;}
    public required string UserName{get;set;}
    //make password secret using DTO
    public required string Password{get;set;}
}