namespace DDB.HPApi.Exceptions
{
    public class CharacterWithIdNotFound : Exception
    {
        public CharacterWithIdNotFound(Guid Id) : base($"Character with id '{Id}' was not found")
        {

        }
    }

}
