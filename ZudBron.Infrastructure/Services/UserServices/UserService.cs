using Article.Domain.Abstractions;
using ZudBron.Application.IService.IEmailServices;
using ZudBron.Application.IService.IUserServices;
using ZudBron.Domain.Abstractions;
using ZudBron.Domain.DTOs.UserDTOs;
using ZudBron.Domain.Models.UserModel;
using ZudBron.Infrastructure.Repositories.AuthRepositories;
using ZudBron.Infrastructure.Repositories.UserRepositories;

namespace ZudBron.Infrastructure.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthRepository _authRepository;
        private readonly IEmailService _emailService;
        private readonly IUserRepository _userRepository;

        public UserService(IUnitOfWork unitOfWork, IAuthRepository authRepository, IEmailService emailService, IUserRepository userRepository)
        {
            _authRepository = authRepository;
            _emailService = emailService;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task<Result<string>> ChangeUserFullNameService(ChangeUserFullNameDto changeUserFullNameDto, string userId)
        {
            try
            {
                var user = await _authRepository.GetByIdAsync(Guid.Parse(userId));
                if (user == null)
                {
                    return Result<string>.Failure(UserError.UserNotFoundById);
                }

                user.FullName = changeUserFullNameDto.FullName;
                user.UpdateDate = DateTime.UtcNow;

                await _unitOfWork.SaveChangesAsync();

                return Result<string>.Success("FullName muvaffaqqiyatli yangilandi");
            }
            catch (Exception ex)
            {
                return Result<string>.Failure(new Error("User.ChangeUserFullName.ServerError", ex.Message));
            }
        }

        public async Task<Result<string>> ChangeUserEmailService(ChangeUserEmailDto changeUserEmailDto, string Id)
        {
            try
            {
                var userId = Guid.Parse(Id);

                var userById = await _authRepository.GetByIdAsync(userId);
                if (userById == null)
                    return Result<string>.Failure(UserError.UserNotFoundById);

                string email = changeUserEmailDto.Email.Trim().ToLower();

                var userByEmail = await _authRepository.GetUserByEmailAsync(email);
                if (userByEmail != null)
                    return Result<string>.Failure(UserError.ChangeEmailAlreadyRegistered);

                var code = _authRepository.GenerateCode();

                var existingChangeEmailOrPhoneNumber = await _userRepository.GetChangeUserEmailOrPhoneNumberById(userId);

                var changeEmailOrPhoneNumber = new ChangeUserEmailOrPhoneNumber
                {
                    UserId = userId,
                    Email = email,
                    VerificationCode = code
                };

                if (existingChangeEmailOrPhoneNumber is not null)
                    await _userRepository.UpdateChangeUserEmailOrPhoneNumber(existingChangeEmailOrPhoneNumber, changeEmailOrPhoneNumber);
                else
                    await _userRepository.AddChangeUserEmailOrPhoneNumber(changeEmailOrPhoneNumber);
                string body = @"
                    <h2>Hurmatli foydalanuvchi,</h2>
                    <p>Ro'yhatdan o'tish uchun tasdiqlash kodingiz:</p>
                    <h3><span class='highlight'>{{CODE}}</span></h3>
                    <p>Ushbu kodni kiriting va hisobingizni tasdiqlang.</p>";

                body = body.Replace("{{CODE}}", code.ToString());

                await _emailService.SendMessageEmail(email, "Ro'yhatdan o'tish kodi", body);

                await _unitOfWork.SaveChangesAsync(); // Emaildan keyin SaveChanges chaqiramiz!

                return Result<string>.Success($"{email} ga tasdiqlash kodi yuborildi");
            }
            catch (Exception ex)
            {
                return Result<string>.Failure(new Error("User.ChangeUserFullName.ServerError", ex.Message));
            }
        }

        public async Task<Result<string>> VerifyChangeUserEmailCodeService(ChangeUserEmailVerificationCode changeUserEmailVerificationCode, string Id)
        {
            try
            {
                var userId = Guid.Parse(Id);

                var changeUserEmail = await _userRepository.GetChangeUserEmailOrPhoneNumberById(userId);

                if (changeUserEmail is null)
                    return Result<string>.Failure(UserError.VerificationUserIdNotFound);

                if (changeUserEmail.VerificationCode != changeUserEmailVerificationCode.Code)
                    return Result<string>.Failure(UserError.IncorrectVerificationCode); // ❌ Tasdiqlash kodi noto‘g‘ri

                if (DateTime.UtcNow > changeUserEmail.ExpirationTime)
                    return Result<string>.Failure(UserError.VerificationCodeExpired); // ❌ Tasdiqlash kodi muddati o‘tgan

                var user = await _authRepository.GetByIdAsync(userId);
                if (user == null)
                    return Result<string>.Failure(UserError.UserNotFoundById);

                user.Email = changeUserEmail.Email;
                user.UpdateDate = DateTime.UtcNow;

                _userRepository.ChangeUserEmailDelete(changeUserEmail);  

                await _unitOfWork.SaveChangesAsync();

                string body = @"
                    <h2>Hurmatli {{FULLNAME}},</h2>
                    <p class='user-info'>Muvaffaqqiyatli ro'yhatdan o'tdingiz.</p>";

                body = body.Replace("{{FULLNAME}}", $"{user.FullName}");

                try
                {
                    if(changeUserEmail.Email != null)
                        await _emailService.SendMessageEmail(changeUserEmail.Email, "Xush kelibsiz!", body);
                }
                catch (Exception ex)
                {
                    return Result<string>.Failure(new Error("User.ChangeUserEmail.SendEmailFailed", ex.Message));
                }

                return Result<string>.Success($"{changeUserEmail.Email} muvaffaqiyatli ro‘yxatdan o‘tdi");
            }
            catch (Exception ex)
            {
                return Result<string>.Failure(new Error("User.ChangeUserEmail.ServerError", ex.Message));
            }
        }
    }
}
