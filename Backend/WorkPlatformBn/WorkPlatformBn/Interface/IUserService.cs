using WorkPlatformBn.Common;
using WorkPlatformBn.Model.Request;
using WorkPlatformBn.Model.Response;

namespace WorkPlatformBn.Interface;

public interface IUserService
{
    Task<BaseResponseModel<UserResponseModel>> RegisterUser(UserRequestModel model);

    Task<BaseResponseModel<LoginResponseModel>> Login(LoginRequestModel model);
}
