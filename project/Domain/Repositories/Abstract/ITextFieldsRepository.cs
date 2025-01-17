using project.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace project.Domain.Repositories.Abstract
{
    public interface ITextFieldsRepository
    {
        Task<IQueryable<TextField>> GetTextFieldsAsync();
        Task<TextField> GetTextFieldByIdAsync(Guid id);
        Task<TextField> GetTextByCodeWordAsync(string codeWord);
        Task<TextField> GetTextByTitleAsync(string title);
        Task<List<TextField>> GetTextByFatherAsync(string father);
        Task<List<string>> GetDistinctCodeWordsAsync();
        Task<List<string>> GetDistinctTitlesAsync();
        Task<List<TextFieldDto>> GetAllTitlesAndIdsAsync();

        //Task<List<string>> GetAllPageModelForIndex();
        Task SaveTextFieldAsync(TextField entity);
        Task DeleteTextFieldAsync(Guid id);
    }
}