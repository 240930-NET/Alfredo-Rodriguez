using P1.API.Model;

namespace P1.API.Service;

public interface IUserService
{
    IEnumerable<User> GetAllUsers();
    public User? GetUserById(int id);
    public User NewUser(User user);
    public User? GetUserByUsername(string username);
    public void DeleteUser(User deleteUser);
    public void EditUser(User user);
}
public interface IGameService
{
    IEnumerable<Game> GetAllGames();
    public Game? GetGameById(int id);

    public IEnumerable<Game> GetGamesByName(string name);
    public void DeleteGame(Game deleteGame);
    public Game NewGame(Game game);
}

public interface IBacklogService{
    public Backlog? GetBacklogEntry(int id, int gameId);
    public IEnumerable<Backlog> GetBacklogByUserId(int id);
    public void DeleteGameFromUserBacklog(Backlog log);
    public Backlog? AddGameToBacklog(Backlog log);

}