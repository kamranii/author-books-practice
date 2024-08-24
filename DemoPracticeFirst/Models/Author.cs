using System;
namespace DemoPracticeFirst.Models
{
	public class Author
	{
		public int? Id { get; set; }
		public string Name { get; set; }
		public string Surnmae { get; set; }
		public int Age { get; set; }
		public List<Books> books = new List<Books>();
		public Author()
		{
		}
	}
}

