using RestWithASPNETVS.Model;

namespace RestWithASPNETVS.Services
{
    public interface IPersonService
    {
        Person Create(Person person);
        Person Update(Person person);
        Person FindById(long id);
        void Delete(long id);
        List<Person> FindAll();
    }
}
