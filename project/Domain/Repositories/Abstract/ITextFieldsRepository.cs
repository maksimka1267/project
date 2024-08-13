using project.Domain.Entities;

namespace project.Domain.Repositories.Abstract
{
    public interface ITextFieldsRepository
    {
		IEnumerable<TextField> GetTextFields();
        TextField GetTextFieldById(Guid id);
        TextField GetTextByCodeWord(string codeWord);
		TextField GetTextByTitle(string title);
		List<string> GetCodeWordsList();
        List<string> GetTitleList();
        void SaveTextField(TextField entity);
        void DeleteTextField(Guid id);
    }
}
