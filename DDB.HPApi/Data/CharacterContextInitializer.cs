﻿using DDB.HPApi.Models;
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

            JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions(new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                UnmappedMemberHandling = JsonUnmappedMemberHandling.Skip,
                PropertyNameCaseInsensitive = true,
            });

            // Seed the database
            Character Briv = JsonSerializer.Deserialize<Character>(Properties.Resources.briv, jsonSerializerOptions) ??
              throw new InvalidOperationException("Failed to deserialize JSON file to object of type 'Character'");
            context.Characters.Add(Briv);

            context.SaveChanges();
        }
    }
}
