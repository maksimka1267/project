using project.Domain.Entities;

namespace project.Domain.Repositories.Abstract
{
    public interface ITextModelRepository
    {
        Task<TextModel> GetTextModelByIdAsync(Guid id);
        Task<string?> GetTextTextModelByIdAsync(Guid id);
        Task SaveTextModelAsync(TextModel entity);
        Task DeleteTextModelAsync(Guid id);
    }
}
