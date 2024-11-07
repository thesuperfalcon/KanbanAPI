using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KanbanAPI.Data;
using KanbanAPI.Controller;
using KanbanAPI.Models;
using KanbanAPI.Dto;
using Task = System.Threading.Tasks.Task;

public class UserControllerTests
{
    private UserController CreateController(string dbName)
    {
        var options = new DbContextOptionsBuilder<KanbanContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;

        var context = new KanbanContext(options);
        return new UserController(context);
    }

    [Fact]
    public async Task GetUsers_ReturnsAllUsers()
    {
        // Arrange
        var controller = CreateController("TestDb1");
        var users = new List<User>
        {
            new User { Id = "1", Username = "user1", Password = "pass1" },
            new User { Id = "2", Username = "user2", Password = "pass2" }
        };

        await controller.PostUser(new UserDto { UserName = "user1", Password = "pass1" });
        await controller.PostUser(new UserDto { UserName = "user2", Password = "pass2" });

        // Act
        var result = await controller.GetUsers();

        // Assert
        var okResult = Assert.IsType<ActionResult<IEnumerable<User>>>(result);
        var returnedUsers = Assert.IsAssignableFrom<IEnumerable<User>>(okResult.Value);
        Assert.Equal(2, returnedUsers.Count());
    }

    [Fact]
    public async Task PostUser_CreatesUser()
    {
        // Arrange
        var controller = CreateController("TestDb2");
        var userDto = new UserDto { UserName = "newUser", Password = "newPass" };

        // Act
        var result = await controller.PostUser(userDto);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var user = Assert.IsType<User>(createdResult.Value);
        Assert.Equal("newUser", user.Username);

        var users = await controller.GetUsers(); 
        Assert.Single(users.Value); 
    }

    [Fact]
    public async Task GetUser_ReturnsUser_WhenUserExists()
    {
        // Arrange
        var controller = CreateController("TestDb3");
        var userDto = new UserDto { UserName = "user1", Password = "pass1" };
        var postResult = await controller.PostUser(userDto);

        var createdResult = Assert.IsType<CreatedAtActionResult>(postResult.Result);
        var createdUser = Assert.IsType<User>(createdResult.Value);
        var userId = createdUser.Id; 

        // Act
        var result = await controller.GetUser(userId.ToString()); 

        // Assert
        var okResult = Assert.IsType<ActionResult<User>>(result);
        var returnedUser = Assert.IsType<User>(okResult.Value);
        Assert.Equal("user1", returnedUser.Username);
    }

    [Fact]
    public async Task GetUser_ReturnsNotFound_WhenUserDoesNotExist()
    {
        // Arrange
        var controller = CreateController("TestDb4");

        // Act
        var result = await controller.GetUser("nonexistent");

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }
}