using TextEditor.Models;

namespace TextEditor.Data.Services
{
    public interface IDocsService
    {
        Task<IEnumerable<Doc>> GetAll();
        Task Add(Doc doc);
        Task<Doc> GetById(int? id);
        Task<Doc> Update(Doc newDoc);
        Task Delete(Doc doc);
    }
}
