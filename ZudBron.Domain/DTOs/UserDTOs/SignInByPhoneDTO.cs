using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZudBron.Domain.DTOs.UserDTOs
{
    public class SignInByPhoneDTO
    {
        [Required(ErrorMessage = "Phone majburiy!")]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Parol majburiy!")]
        [MinLength(6, ErrorMessage = "Parol kamida 6 ta belgidan iborat bo‘lishi kerak!")]
        public string Password { get; set; }
    }
}
