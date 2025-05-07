using Article.Domain.Abstractions;
using ZudBron.Application.IService.IAuthServices;
using ZudBron.Application.IService.IEmailServices;
using ZudBron.Application.IService.ITokenServices;
using ZudBron.Domain.Abstractions;
using ZudBron.Domain.DTOs.TokenDTOs;
using ZudBron.Domain.DTOs.UserDTOs;
using ZudBron.Domain.Models.UserModel;
using ZudBron.Infrastructure.Repositories.AuthRepositories;

namespace ZudBron.Infrastructure.Services.AuthServices
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthRepository _authRepository;
        private readonly IEmailService _emailService;
        private readonly ITokenService _tokenService;
        public AuthService(IUnitOfWork unitOfWork, IAuthRepository authRepository, IEmailService emailService, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _authRepository = authRepository;
            _emailService = emailService;
            _tokenService = tokenService;
        }

        public async Task<Result<string>> SignUpService(UserRegisterByEmailDto model)
        {
            string email = model.Email.Trim().ToLower();

            var existingUser = await _authRepository.GetUserByEmailAsync(email);
            if (existingUser is not null)
                return Result<string>.Failure(UserError.EmailAlreadyRegistered);

            var code = _authRepository.GenerateCode();

            var existingTempUser = await _authRepository.GetTempUserByEmailAsync(email);

            var tempUser = new TempUser
            {
                FullName = model.FullName,
                Email = email,
                HashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password),
                VerificationCode = code,
            };

            try
            {
                if (existingTempUser is not null)
                    await _authRepository.SaveUpdateVerificationCode(existingTempUser, tempUser);
                else
                    await _authRepository.SaveAddVerificationCode(tempUser);

                string body = @"
                    <h2>Hurmatli foydalanuvchi,</h2>
                    <p>Ro'yhatdan o'tish uchun tasdiqlash kodingiz:</p>
                    <h3><span class='highlight'>{{CODE}}</span></h3>
                    <p>Ushbu kodni kiriting va hisobingizni tasdiqlang.</p>";

                body = body.Replace("{{CODE}}", code.ToString());

                await _emailService.SendMessageEmail(email, "Ro'yhatdan o'tish kodi", body);

                await _unitOfWork.SaveChangesAsync(); // SaveChanges emaildan keyin!

                return Result<string>.Success($"{email} ga tasdiqlash kodi yuborildi");
            }
            catch (Exception ex)
            {
                return Result<string>.Failure(new Error("User.SignUp.SendEmailFailed", ex.Message));
            }
        }

        public async Task<Result<string>> VerifyRegisterCodeService(RegisterVerificationCodeDto registerVerificationCode)
        {
            try
            {
                string email = registerVerificationCode.Email.Trim().ToLower(); // Emailni tozalash

                var tempUser = await _authRepository.GetTempUserByEmailAsync(email);

                if (tempUser is null)
                    return Result<string>.Failure(UserError.VerificationEmailNotFound); // ❌ Email topilmadi

                if (tempUser.VerificationCode != registerVerificationCode.Code)
                    return Result<string>.Failure(UserError.IncorrectVerificationCode); // ❌ Tasdiqlash kodi noto‘g‘ri

                if (DateTime.UtcNow > tempUser.ExpirationTime)
                    return Result<string>.Failure(UserError.VerificationCodeExpired); // ❌ Tasdiqlash kodi muddati o‘tgan

                User user = new User
                {
                    FullName = tempUser.FullName,
                    Email = email,
                    PasswordHash = tempUser.HashedPassword
                };

                await _authRepository.AddUserAsync(user);

                _authRepository.TempUserDelete(tempUser);

                await _unitOfWork.SaveChangesAsync();

                string body = @"
                    <h2>Hurmatli {{FULLNAME}},</h2>
                    <p class='user-info'>Muvaffaqqiyatli ro'yhatdan o'tdingiz.</p>";

                body = body.Replace("{{FULLNAME}}", $"{tempUser.FullName}");

                try
                {
                    await _emailService.SendMessageEmail(email, "Xush kelibsiz!", body);
                }
                catch (Exception ex)
                {
                    return Result<string>.Failure(new Error("User.SignUp.SendEmailFailed", ex.Message));
                }

                return Result<string>.Success($"{email} muvaffaqiyatli ro‘yxatdan o‘tdi");
            }
            catch (Exception ex)
            {
                return Result<string>.Failure(new Error("User.SignUp.ServerError", ex.Message));
            }
        }

        public async Task<Result<TokenResponse>> SignInService(SignInByEmailDto signInByEmail)
        {
            try
            {
                string email = signInByEmail.Email.Trim().ToLower();

                var user = await _authRepository.GetUserByEmailAsync(email);

                if (user is null)
                    return Result<TokenResponse>.Failure(UserError.EmailNotRegistered);

                if (!BCrypt.Net.BCrypt.Verify(signInByEmail.Password, user.PasswordHash))
                    return Result<TokenResponse>.Failure(UserError.IncorrectPassword);

                string accessToken = _tokenService.GenerateAccessToken(user.Id, user.Role);
                string refreshToken = await _tokenService.GenerateRefreshTokenAsync(user.Id);

                await _unitOfWork.SaveChangesAsync(); // RefreshToken saqlangan bo‘lsa

                return Result<TokenResponse>.Success(new TokenResponse
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                });
            }
            catch (Exception ex)
            {
                return Result<TokenResponse>.Failure(new Error("User.SignIn.Failed", ex.Message));
            }
        }

        public async Task<Result<string>> ForgotPasswordService(ForgotPasswordDto model)
        {
            string email = model.Email.Trim().ToLower();

            var existingUser = await _authRepository.GetUserByEmailAsync(email);
            if (existingUser is null)
                return Result<string>.Failure(UserError.VerificationEmailNotFound); // ❌ Email bazada yo'q

            var code = _authRepository.GenerateCode();

            var existingForgotPassword = await _authRepository.GetForgotPasswordByEmailAsync(email);

            var forgotPassword = new ForgotPassword
            {
                Email = email,
                VerificationCode = code
            };

            if (existingForgotPassword is not null)
                await _authRepository.UpdateForgotPassword(existingForgotPassword, forgotPassword);
            else
                await _authRepository.AddForgotPasswordAsync(forgotPassword);

            try
            {
                string body = @"
                    <h2>Hurmatli foydalanuvchi,</h2>
                    <p>Parolni tiklash uchun tasdiqlash kodingiz:</p>
                    <h3><span class='highlight'>{{CODE}}</span></h3>
                    <p>Ushbu kodni kiriting va hisobingizni tasdiqlang.</p>";

                body = body.Replace("{{CODE}}", code.ToString());

                await _emailService.SendMessageEmail(email, "Parolni tiklash kodi", body);

                await _unitOfWork.SaveChangesAsync(); // Emaildan keyin SaveChanges chaqiramiz!

                return Result<string>.Success($"{email} ga parolni tiklash kodi yuborildi");
            }
            catch (Exception ex)
            {
                return Result<string>.Failure(new Error("User.ForgotPassword.SendEmailFailed", ex.Message));
            }
        }

        public async Task<Result<string>> VerifyForgotPasswordCodeService(ForgotPasswordVerificationCodeDto forgotPasswordVerificationCode)
        {
            try
            {
                string email = forgotPasswordVerificationCode.Email.Trim().ToLower(); // Emailni tozalash

                var forgotPassword = await _authRepository.GetForgotPasswordByEmailAsync(email);

                if (forgotPassword is null)
                    return Result<string>.Failure(UserError.VerificationEmailNotFound); // ❌ Email topilmadi

                if (forgotPassword.VerificationCode != forgotPasswordVerificationCode.Code)
                    return Result<string>.Failure(UserError.IncorrectVerificationCode); // ❌ Tasdiqlash kodi noto‘g‘ri

                if (DateTime.UtcNow > forgotPassword.ExpirationTime)
                    return Result<string>.Failure(UserError.VerificationCodeExpired); // ❌ Tasdiqlash kodi muddati o‘tgan

                forgotPassword.IsUsed = true;

                await _unitOfWork.SaveChangesAsync();

                return Result<string>.Success("Kod to'g'ri! Endi yangi parol o'rnatishingiz mumkin.");
            }
            catch (Exception ex)
            {
                return Result<string>.Failure(new Error("User.ForgotPassword.ServerError", ex.Message));
            }
        }

        public async Task<Result<string>> ResetPasswordService(ResetPasswordRequestDto resetPasswordRequest)
        {
            try
            {
                string email = resetPasswordRequest.Email.Trim().ToLower();

                var forgotPassword = await _authRepository.GetForgotPasswordByEmailAsync(email);

                if (forgotPassword is null)
                    return Result<string>.Failure(UserError.VerificationEmailNotFound); // ❌ Email topilmadi

                if (!forgotPassword.IsUsed)
                    return Result<string>.Failure(UserError.IncorrectVerificationCode); // ❌ Kod hali tasdiqlanmagan

                var user = await _authRepository.GetUserByEmailAsync(email);
                if (user is null)
                    return Result<string>.Failure(UserError.VerificationEmailNotFound); // ❌ User topilmadi

                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(resetPasswordRequest.NewPassword);
                user.UpdateDate = DateTime.UtcNow;

                _authRepository.ForgotPasswordDelete(forgotPassword); // Parolni tiklashdan keyin eski kodni o'chirish

                await _unitOfWork.SaveChangesAsync();

                string body = @"
                    <h2>Hurmatli foydalanuvchi,</h2>
                    <p>Parolingiz muvaffaqiyatli yangilandi</p>
                    <p>Endi yangilangan parol bilan tizimga kira olasiz.</p>";

                await _emailService.SendMessageEmail(email, "Parol yangilandi", body);

                return Result<string>.Success("Parolingiz muvaffaqiyatli o'zgartirildi.");
            }
            catch (Exception ex)
            {
                return Result<string>.Failure(new Error("User.ResetPassword.ServerError", ex.Message));
            }
        }

        public async Task<Result<string>> SignUpByPhoneService(UserRegisterByPhoneDto model)
        {
            var existingUser = await _authRepository.GetUserByPhoneAsync(model.PhoneNumber);
            if (existingUser is not null)
                return Result<string>.Failure(UserError.PhoneNumberAlreadyRegistered);

            var code = _authRepository.GenerateCode();
            var existingTempUser = await _authRepository.GetTempUserByPhoneAsync(model.PhoneNumber);

            var tempUser = new TempUser
            {
                FullName = model.FullName,
                PhoneNumber = model.PhoneNumber,
                HashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password),
                VerificationCode = code,
            };

            try
            {
                if (existingTempUser is not null)
                    await _authRepository.SaveUpdateVerificationCode(existingTempUser, tempUser);
                else
                    await _authRepository.SaveAddVerificationCode(tempUser);

                string body = @"
            <h2>Hurmatli foydalanuvchi,</h2>
            <p>Ro'yhatdan o'tish uchun tasdiqlash kodingiz:</p>
            <h3><span class='highlight'>{{CODE}}</span></h3>
            <p>Ushbu kodni kiriting va hisobingizni tasdiqlang.</p>";

                body = body.Replace("{{CODE}}", code.ToString());

                // Telefonga kod jo'natish metodi

                await _unitOfWork.SaveChangesAsync(); // SaveChanges emaildan keyin!

                return Result<string>.Success($"{model.PhoneNumber} ga tasdiqlash kodi yuborildi");
            }
            catch (Exception ex)
            {
                return Result<string>.Failure(new Error("User.SignUp.SendEmailFailed", ex.Message));
            }
        }

        public async Task<Result<string>> VerifyRegisterCodeByPhoneService(RegisterVerificationCodeByPhoneDto registerVerificationCodeByPhone)
        {
            try
            {
                var tempUser = await _authRepository.GetTempUserByPhoneAsync(registerVerificationCodeByPhone.PhoneNumber);

                if (tempUser is null)
                    return Result<string>.Failure(UserError.VerificationPhoneNotFound);

                if (tempUser.VerificationCode != registerVerificationCodeByPhone.Code)
                {
                    return Result<string>.Failure(UserError.IncorrectVerificationCode);
                }

                if (DateTime.UtcNow > tempUser.ExpirationTime)
                {
                    return Result<string>.Failure(UserError.VerificationCodeExpired);
                }

                User user = new User
                {
                    FullName = tempUser.FullName,
                    PhoneNumber = registerVerificationCodeByPhone.PhoneNumber,
                    PasswordHash = tempUser.HashedPassword
                };

                await _authRepository.AddUserAsync(user);

                _authRepository.TempUserDelete(tempUser);

                await _unitOfWork.SaveChangesAsync();

                // Telefonga xush kelibsiz xabari jo'natish metodi

                return Result<string>.Success($"{registerVerificationCodeByPhone.PhoneNumber} muvaffaqiyatli ro‘yxatdan o‘tdi");
            }
            catch (Exception ex)
            {
                return Result<string>.Failure(new Error("User.SignUp.ServerError", ex.Message));
            }
        }

        public async Task<Result<TokenResponse>> SignInByPhoneService(SignInByPhoneDto signInByPhone)
        {
            try
            {
                var user = await _authRepository.GetUserByPhoneAsync(signInByPhone.PhoneNumber);

                if (user is null)
                    return Result<TokenResponse>.Failure(UserError.PhoneNotRegistered);

                if (!BCrypt.Net.BCrypt.Verify(signInByPhone.Password, user.PasswordHash))
                    return Result<TokenResponse>.Failure(UserError.IncorrectPassword);

                string accessToken = _tokenService.GenerateAccessToken(user.Id, user.Role);
                string refreshToken = await _tokenService.GenerateRefreshTokenAsync(user.Id);

                await _unitOfWork.SaveChangesAsync(); // RefreshToken saqlangan bo‘lsa

                return Result<TokenResponse>.Success(new TokenResponse
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                });
            }
            catch (Exception ex)
            {
                return Result<TokenResponse>.Failure(new Error("User.SignIn.Failed", ex.Message));
            }
        }

        public async Task<Result<string>> ForgotPasswordByPhoneService(ForgotPasswordByPhoneDto model)
        {
            var existingUser = await _authRepository.GetUserByPhoneAsync(model.PhoneNumber);
            if (existingUser is null)
                return Result<string>.Failure(UserError.VerificationPhoneNotFound);

            var code = _authRepository.GenerateCode();

            var existingForgotPassword = await _authRepository.GetForgotPasswordByPhoneAsync(model.PhoneNumber);

            var forgotPassword = new ForgotPassword
            {
                PhoneNumber = model.PhoneNumber,
                VerificationCode = code
            };

            if (existingForgotPassword is not null)
                await _authRepository.UpdateForgotPasswordByPhone(existingForgotPassword, forgotPassword);
            else
                await _authRepository.AddForgotPasswordAsync(forgotPassword);

            try
            {
                // Telefonga parolni tiklash kodi jo'natish metodi

                await _unitOfWork.SaveChangesAsync();

                return Result<string>.Success($"{model.PhoneNumber} ga parolni tiklash kodi yuborildi");
            }
            catch (Exception ex)
            {
                return Result<string>.Failure(new Error("User.ForgotPassword.SendEmailFailed", ex.Message));
            }
        }

        public async Task<Result<string>> VerifyForgotPasswordCodeByPhoneService(ForgotPasswordVerificationCodeByPhoneDto forgotPasswordVerificationCodeByPhone)
        {
            try
            {
                var forgotPassword = await _authRepository.GetForgotPasswordByPhoneAsync(forgotPasswordVerificationCodeByPhone.PhoneNumber);

                if (forgotPassword is null)
                    return Result<string>.Failure(UserError.VerificationPhoneNotFound);

                if (forgotPassword.VerificationCode != forgotPasswordVerificationCodeByPhone.Code)
                    return Result<string>.Failure(UserError.IncorrectVerificationCode);

                if (DateTime.UtcNow > forgotPassword.ExpirationTime)
                    return Result<string>.Failure(UserError.VerificationCodeExpired);

                forgotPassword.IsUsed = true;

                await _unitOfWork.SaveChangesAsync();

                return Result<string>.Success("Kod to'g'ri! Endi yangi parol o'rnatishingiz mumkin.");
            }
            catch (Exception ex)
            {
                return Result<string>.Failure(new Error("User.ForgotPassword.ServerError", ex.Message));
            }
        }

        public async Task<Result<string>> ResetPasswordByPhoneService(ResetPasswordRequestByPhoneDto resetPasswordRequestByPhone)
        {
            try
            {
                var forgotPassword = await _authRepository.GetForgotPasswordByPhoneAsync(resetPasswordRequestByPhone.PhoneNumber);

                if (forgotPassword is null)
                    return Result<string>.Failure(UserError.VerificationPhoneNotFound);

                if (!forgotPassword.IsUsed)
                    return Result<string>.Failure(UserError.IncorrectVerificationCode); // ❌ Kod hali tasdiqlanmagan

                var user = await _authRepository.GetUserByPhoneAsync(resetPasswordRequestByPhone.PhoneNumber);
                if (user is null)
                    return Result<string>.Failure(UserError.VerificationPhoneNotFound); // ❌ User topilmadi

                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(resetPasswordRequestByPhone.NewPassword);
                user.UpdateDate = DateTime.UtcNow;

                _authRepository.ForgotPasswordDelete(forgotPassword); // Parolni tiklashdan keyin eski kodni o'chirish

                await _unitOfWork.SaveChangesAsync();

                // Telefonga parol yangilandi xabari jo'natish metodi

                return Result<string>.Success("Parolingiz muvaffaqiyatli o'zgartirildi.");
            }
            catch (Exception ex)
            {
                return Result<string>.Failure(new Error("User.ResetPassword.ServerError", ex.Message));
            }
        }

        public async Task<Result<TokenResponse>> RefreshTokenService(RefreshTokenDto refreshTokenDto)
        {
            try
            {
                var refreshTokenEntity = await _tokenService.GetToken(refreshTokenDto.RefreshToken);

                if (refreshTokenEntity == null)
                {
                    return Result<TokenResponse>.Failure(UserError.RefreshTokenInvalid);
                }

                if (refreshTokenEntity.ExpiryDate < DateTime.UtcNow)
                {
                    return Result<TokenResponse>.Failure(UserError.RefreshTokenExpired);
                }

                var user = await _authRepository.GetByIdAsync(refreshTokenEntity.UserId);
                if (user == null)
                {
                    return Result<TokenResponse>.Failure(UserError.UserNotFoundByRefreshToken);
                }

                var newAccessToken = _tokenService.GenerateAccessToken(user.Id, user.Role);
                var newRefreshToken = await _tokenService.GenerateRefreshTokenAsync(user.Id);

                await _unitOfWork.SaveChangesAsync();

                return Result<TokenResponse>.Success(new TokenResponse
                {
                    AccessToken = newAccessToken,
                    RefreshToken = newRefreshToken
                });
            }
            catch (Exception ex)
            {
                return Result<TokenResponse>.Failure(new Error("User.RefreshToken.ServerError", ex.Message));
            }
        }
    }
}
