using WebApplication1.DataModel;
using WebApplication1.RepositryLayer;

namespace WebApplication1.BusinessLogic;

public class UserService
{
    private readonly UserRepository _repository;

    public UserService(UserRepository repository)
    {
        _repository = repository;
    }

    public UserResponse? CreateUser(CreateUserRequest request)
    {
        if (_repository.GetByName(request.Name) is not null)
        {
            return null;
        }

        User user = new User
        {
            Name = request.Name,
            Password = request.Password,
            Role = request.Role
        };

        _repository.Create(user);

        return new UserResponse { Name = user.Name, Role = user.Role };
    }

    public UserResponse? Login(LoginRequest request)
    {
        var user = _repository.GetByNameAndPassword(request.Name, request.Password);

        if (user is null)
        {
            return null;
        }

        return new UserResponse { Name = user.Name, Role = user.Role };
    }
}
