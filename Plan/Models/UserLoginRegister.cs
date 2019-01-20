using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Plan.Models
{
	public class UserLogin
	{
		[Required(ErrorMessage = "Пожалуйста, введите Login")]
		[Display(Name = "Login")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Пожалуйста, введите Пароль")]
		[Display(Name = "Пароль")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}

	public class UserRegister
	{
		[Required(ErrorMessage = "Пожалуйста, введите Login")]
		[Display(Name = "Login")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Пожалуйста, введите Пароль")]
		[Display(Name = "Пароль")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required(ErrorMessage = "Пожалуйста, введите Пароль")]
		[Display(Name = "Подтвердить Пароль")]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Пароли не совпадают")]
		public string ConfirmPassword { get; set; }
	}
}