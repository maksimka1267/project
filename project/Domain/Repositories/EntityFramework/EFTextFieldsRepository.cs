using Microsoft.EntityFrameworkCore;
using project.Domain.Entities;
using project.Domain.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace project.Domain.Repositories.EntityFramework
{
	public class EFTextFieldsRepository : ITextFieldsRepository
	{
		private readonly AppDbContext context;
		public EFTextFieldsRepository(AppDbContext context)
		{
			this.context = context;
		}

		public void DeleteTextField(Guid id)
		{
			context.TextFields.Remove(new TextField() { Id = id });
			context.SaveChanges();
		}

		public TextField GetTextByCodeWord(string codeWord)
		{
			return context.TextFields.FirstOrDefault(x => x.CodeWord == codeWord);
		}
		public TextField GetTextByTitle(string title)
		{
			return context.TextFields.FirstOrDefault(x => x.Title == title);
		}
		public List<string> GetCodeWordsList()
		{
			return context.TextFields.Select(x => x.CodeWord).Distinct().ToList();
		}

		public List<string> GetTitleList()
		{
			return context.TextFields.Select(x => x.Title).Distinct().ToList();
		}

		public TextField GetTextFieldById(Guid id)
		{
			return context.TextFields.FirstOrDefault(x => x.Id == id);
		}

		public IEnumerable<TextField> GetTextFields()
		{
			return context.TextFields;
		}

		public void SaveTextField(TextField entity)
		{
			if (entity.Id == default)
			{
				context.Entry(entity).State = EntityState.Added;
			}
			else
			{
				context.Entry(entity).State = EntityState.Modified;
			}
			context.SaveChanges();
		}
	}
}
