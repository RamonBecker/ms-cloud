using RestWithASPNETVS.Model;

namespace RestWithASPNETVS.Services.Implementations
{
    public class PersonServiceImpletation : IPersonService
    {
        private volatile int count;

        public Person Create(Person person)
        {
            return person;
        }

        public void Delete(long id)
        {
        }

        public List<Person> FindAll()
        {

            var persons = new List<Person>();


            for (int i = 0; i < 8; i++)
            {
                var person = MockPerson(i);

                persons.Add(new Person { Id = i });
            }


            return persons;
        }

        private Person MockPerson(int i)
        {
            return new Person 
                            { 
                                Id = IncrementAndGet(), 
                                FirstName = "Person Name" + i, 
                                LastName = "Person Last Name"+ i, 
                                Address = "Some Address"+ i, 
                                Gender = "Male" 
                            };
        }

        private long IncrementAndGet()
        {
            return Interlocked.Increment(ref count);
        }

        public Person FindById(long id)
        {
            return new Person
            {
                Id = IncrementAndGet(),
                FirstName = "Marcos",
                LastName = "Silva",
                Address = "Canoinhas - Santa Catarina",
                Gender = "Male"
            };
        }

        public Person Update(Person person)
        {
            return person;
        }
    }
}
