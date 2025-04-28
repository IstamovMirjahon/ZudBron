using Article.Domain.Abstractions;

namespace ZudBron.Domain.Models.UserModel
{
    public static class UserError
    {
        public static readonly Error EmailAlreadyRegistered = new(
            "User.Register.EmailAlreadyExists",
            "Bu email oldin ro'yhatdan o'tgan.");

        public static readonly Error EmailNotRegistered = new(
            "User.SignIn.EmailNotFound",
            "Hali ro'yhatdan o'tmagansiz, iltimos Register qiling.");

        public static readonly Error IncorrectPassword = new(
            "User.SignIn.IncorrectPassword",
            "Parol xato!");

        public static readonly Error VerificationEmailNotFound = new(
            "User.Verification.EmailNotFound",
            "Email topilmadi!");

        public static readonly Error IncorrectVerificationCode = new(
            "User.Verification.IncorrectCode",
            "Tasdiqlash kodi noto‘g‘ri.");

        public static readonly Error VerificationCodeExpired = new(
            "User.Verification.CodeExpired",
            "Tasdiqlash kodi muddati o‘tgan.");

        public static readonly Error RefreshTokenInvalid = new(
            "User.RefreshToken.Invalid",
            "Berilgan refresh token bazada topilmadi.");

        public static readonly Error RefreshTokenExpired = new(
            "User.RefreshToken.Expired",
            "Yangi login qilish talab etiladi.");

        public static readonly Error UserNotFoundByRefreshToken = new(
            "User.RefreshToken.UserNotFound",
            "Berilgan refresh tokenga mos foydalanuvchi yo‘q.");
    }
}
