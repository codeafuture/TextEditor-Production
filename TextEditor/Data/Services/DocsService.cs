using Microsoft.EntityFrameworkCore;
using TextEditor.Models;

namespace TextEditor.Data.Services
{
    public class DocsService : IDocsService
    {
        private readonly ApplicationDbContext _context;
        public DocsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(Doc doc)
        {
            _context.Add(doc);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Doc doc)
        {
            _context.Remove(doc);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Doc>> GetAll()
        {
            return await _context.Docs.Include(d => d.User).ToListAsync();
        }
        public async Task<Doc> GetById(int? id)
        {
            return await _context.Docs.FindAsync(id);
        }

        public async Task<Doc> Update(Doc newDoc)
        {
            _context.Update(newDoc);
            await _context.SaveChangesAsync();
            return newDoc;
        }
    }
}
