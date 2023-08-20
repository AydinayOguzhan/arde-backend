using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager: IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;
        private IUserOperationClaimService _userOperationClaimService;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper, IUserOperationClaimService userOperationClaimService)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _userOperationClaimService = userOperationClaimService;
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims.Data);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.Successful);
        }

        public IDataResult<User> Login(UserForLoginDto loginDto)
        {
            var userToCheck = _userService.GetByMail(loginDto.Email);
            if (userToCheck.Data == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(loginDto.Password, userToCheck.Data.passwordHash, userToCheck.Data.passwordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }

            return new SuccessDataResult<User>(userToCheck.Data, Messages.SuccessfulLogin);
        }

        public IDataResult<User> Register(UserForRegisterDto registerDto)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(registerDto.Password, out passwordHash, out passwordSalt);
            User newUser = new User
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                passwordHash = passwordHash,
                passwordSalt = passwordSalt
            };
            _userService.Add(newUser);

            var user = _userService.GetByMail(registerDto.Email);
            //yetki no 2 => user
            UserOperationClaim userOperationClaim = new UserOperationClaim { OperationClaimId = 2, UserId = user.Data.Id };
            _userOperationClaimService.Add(userOperationClaim);

            return new SuccessDataResult<User>(newUser, Messages.UserRegistered);
        }

        public IResult UserExists(string email)
        {
            IDataResult<User> result = _userService.GetByMail(email);
            if (result.Data == null) return new SuccessResult();
            else return new ErrorResult(Messages.UserAlreadyExists);
        }
    }
}
