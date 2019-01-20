using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema; // подключаем распознование [...]
using System.ComponentModel.DataAnnotations; // подключаем распознование [...]
using System.Web.Mvc;

namespace Plan.Models
{
	public class UserContext : DbContext
	{
		// MyConnection1 это соединение с базой данных описанное в файле web.config
		public UserContext()
			: base("MyConnection1")
		{
		}

		public DbSet<User> Users { get; set; }
		public DbSet<Task> Tasks { get; set; }
	}

	public class User
	{
		public int Id { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
	}

	public class Task
	{
		public int Id { get; set; }

		[Column(TypeName = "date")]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		[Required(ErrorMessage = "Пожалуйста, введите Дату (день)")]
		[Display(Name = "Дата (день)")]
		public DateTime? StartDate { get; set; }

		[DataType(DataType.Time)]
		[DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
		[Required(ErrorMessage = "Пожалуйста, введите Время (начало)")]
		[Display(Name = "Время (начало)")]
		public DateTime? StartTime { get; set; }

		[DataType(DataType.Time)]
		[DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
		[Display(Name = "Время (конец)")]
		[Required(ErrorMessage = "Пожалуйста, введите Время (конец)")]
		public DateTime? EndTime { get; set; }

		[Display(Name = "Описание занятия")]
		[Required(ErrorMessage = "Пожалуйста, введите название занятия")]
		public string Name { get; set; }

		[HiddenInput(DisplayValue = false)]
		public int UserId { get; set; } // определяем внешний ключ (int значит НЕ может иметь null значение)
		public User User { get; set; } // это тоже необходимо для внешнего ключа
	}
}