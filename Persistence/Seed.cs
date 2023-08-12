using Domain;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context)
        {
            if (context.Countries.Any()) return;

            var countries = new List<Country>
            {
                new Country
                {
                    CountryName = "Abu Dhabi",
                },
                new Country
                {
                    CountryName = "Pakistan",
                },
                new Country {
                    CountryName = "Dubai",
                },
                new Country
                {
                    CountryName = "Sharjah",
                },
                new Country
                {
                    CountryName = "Ajmaan",
                },
                new Country
                {
                    CountryName = "Saudi Arabia",
                },

            };

            await context.Countries.AddRangeAsync(countries);
            await context.SaveChangesAsync();
        }
    }
}