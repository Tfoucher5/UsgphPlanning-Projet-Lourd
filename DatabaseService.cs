using Microsoft.EntityFrameworkCore;
namespace UsgphPlanning
{
    public class DatabaseService
    {
        private readonly UsgphPlanningDbContext _context;
        public DatabaseService(UsgphPlanningDbContext context)
        {
            _context = context;
        }

        // Read - Récupérer tous les lieux
        public async Task<List<Lieu>> GetLieux()
        {
            return await _context.Lieux.ToListAsync();
        }

        // Read - Récupérer un lieu spécifique par son ID
        public async Task<Lieu> GetLieuById(int id)
        {
            return await _context.Lieux.FindAsync(id);
        }

        // Create - Ajouter un nouveau lieu
        public async Task<Lieu> CreateLieu(Lieu lieu)
        {
            _context.Lieux.Add(lieu);
            await _context.SaveChangesAsync();
            return lieu;
        }

        // Update - Mettre à jour un lieu existant
        public async Task<bool> UpdateLieu(Lieu lieu)
        {
            _context.Entry(lieu).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LieuExists((int)lieu.Id))
                {
                    return false;
                }
                throw;
            }
        }

        // Delete - Supprimer un lieu
        public async Task<bool> DeleteLieu(int id)
        {
            var lieu = await _context.Lieux.FindAsync(id);
            if (lieu == null)
            {
                return false;
            }

            _context.Lieux.Remove(lieu);
            await _context.SaveChangesAsync();
            return true;
        }

        // Vérifier si un lieu existe
        private bool LieuExists(int id)
        {
            return _context.Lieux.Any(e => e.Id == id);
        }
    }
}