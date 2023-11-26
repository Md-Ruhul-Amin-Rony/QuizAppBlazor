using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizAppBlazor.Shared
{
	public class Answer
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string? UserId { get; set; }
		public string? Text { get; set; }
		public bool IsCorrectAnswer { get; set; }
		public int QuestionId { get; set; } // Foreign key for Question

		public Question? Question { get; set; } // Navigation property to Question
	}
}
