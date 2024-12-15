using DDB.HPApi.Models;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace DDB.HPApi.Data
{
    public static class CharacterContextInitializer
    {
        public static void Initialize(CharacterContext context)
        {
            context.Database.EnsureCreated();

            if (context.Characters.Any())
            {
                return; // We have already seeded the database.
            }

            /* 
             * Hardcoded JSON for initialization
             * Ideally we would instead create characters by extending our API to do so or use other apps to do so
             */
            const string BRIV_JSON = "{\"name\":\"Briv\",\"level\":5,\"hitPoints\":25,\"classes\":[{\"name\":\"fighter\",\"hitDiceValue\":10,\"classLevel\":5}],\"stats\":{\"strength\":15,\"dexterity\":12,\"constitution\":14,\"intelligence\":13,\"wisdom\":10,\"charisma\":8},\"items\":[{\"name\":\"IounStoneofFortitude\",\"modifier\":{\"affectedObject\":\"stats\",\"affectedValue\":\"constitution\",\"value\":2}}],\"defenses\":[{\"type\":\"fire\",\"defense\":\"immunity\"},{\"type\":\"slashing\",\"defense\":\"resistance\"}]}";

            JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions(new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                UnmappedMemberHandling = JsonUnmappedMemberHandling.Skip,
                PropertyNameCaseInsensitive = true,
            });

            // Seed the database
            Character Briv = JsonSerializer.Deserialize<Character>(BRIV_JSON, jsonSerializerOptions) ??
              throw new InvalidOperationException("Failed to deserialize JSON file to object of type 'Character'");
            context.Characters.Add(Briv);

            context.SaveChanges();
        }
    }
}
