using SpyDuh.Models;

namespace SpyDuh.Repositories
{
    public interface ISpyRepository
    {
       public List<EnemySpy> listEnemies(int id);
       public Spy getEnemies(int id);
       public void Add(NewSpy spy);
       public void Delete(int id);
       public void AddEnemy(int spyId, int enemyId);
       //public int HandlerLength();
    }

}