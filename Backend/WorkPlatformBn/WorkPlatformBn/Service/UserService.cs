using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WorkPlatformBn.Common;
using WorkPlatformBn.Interface;
using WorkPlatformBn.Model;
using WorkPlatformBn.Model.Request;
using WorkPlatformBn.Model.Response;
using WorkPlatformBn.Utility;

namespace WorkPlatformBn.Service;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly JwtTokenGenerator _jwtGenerator;

    public UserService(ApplicationDbContext context,IMapper mapper, JwtTokenGenerator jwtToken)
    {
        _context = context;
        _mapper = mapper;
        _jwtGenerator = jwtToken;
    }

    public async Task<BaseResponseModel<LoginResponseModel>> Login(LoginRequestModel model)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == model.Email);
        if (user == null)
            return new BaseResponseModel<LoginResponseModel>() { Message = StatusMessage.Error, StatusCode = StatusCodes.Status404NotFound, Data = new() };

        if (PasswordHasher.VerifyPassword(model.Password, user.Password))
        {
            return new BaseResponseModel<LoginResponseModel>()
            {
                Message = StatusMessage.Success,
                StatusCode = StatusCodes.Status200OK,
                Data = new LoginResponseModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    Token= _jwtGenerator.GenerateJwtToken(user.Name,user.Email)
                }
            };
        }
        else
        {
            return new BaseResponseModel<LoginResponseModel>()
            {
                Message = StatusMessage.Error,
                StatusCode = StatusCodes.Status404NotFound,
                Data = new()
            };
        }
    }

    public async Task<BaseResponseModel<UserResponseModel>> RegisterUser(UserRequestModel model)
    {
        try
        {

            var mapped = _mapper.Map<User>(model);
            mapped.Password = PasswordHasher.HashPassword(model.Password);

            await _context.Users.AddAsync(mapped);
            await _context.SaveChangesAsync();

            foreach (var item in model.Categories)
            {
                await _context.WorkerCategories.AddAsync(new WorkerCategory { CategoryId = item.Id, UserId = mapped.Id });
            }

            await _context.SaveChangesAsync();  

            return new BaseResponseModel<UserResponseModel>() { Message = StatusMessage.Success, StatusCode = StatusCodes.Status200OK, Data = new() };
        }
        catch(Exception ex) 
        {
            return new BaseResponseModel<UserResponseModel>() { Message=ex.Message,StatusCode=StatusCodes.Status500InternalServerError,Data=new()};
        }
    }
}
