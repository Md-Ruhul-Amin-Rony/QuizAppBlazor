using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizAppBlazor.Shared
{
	public class Question
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string? Text { get; set; }
		//public string? ImageOrVideoUrl { get; set; }

		public string? FileName { get; set; }
		public string? Content { get; set; }

		public string? ImageOrVideoUrl { get; set; }
		public AnswerType AnswerType { get; set; }
		public string? Option1 { get; set; }
		public string? Option2 { get; set; }
		public string? Option3 { get; set; }
		public string? Option4 { get; set; }

		public string? CorrectAnswer { get; set; }

		public List<Answer>? Answers { get; set; }
		public int QuizId { get; set; } // Foreign key for Quiz
		public Quiz? Quiz { get; set; } // Navigation property to Quiz

	}
}
