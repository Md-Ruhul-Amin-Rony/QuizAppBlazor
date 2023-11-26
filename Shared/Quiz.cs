using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizAppBlazor.Shared
{
	public class Quiz
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string? UserId { get; set; }
		public string Title { get; set; }
		public int TimeLimit { get; set; }  //minutes, timespan doees not make sense here
		public bool IsPublished { get; set; }
		public List<Question>? Questions { get; set; }
	}
}
