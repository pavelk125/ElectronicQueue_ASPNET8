using ElectronicQueue.Database.Models;
using MvcApp.Models;

namespace ElectronicQueue.Database.Initializers
{
    public static class TestThemeInitializer
    {
        public static async Task InitializeAsync(DatabaseContext dbContext)
        {
            if (dbContext.Themes.Any()) return;

            var theme1 = new Theme()
            {
                ThemeName = "Тестовая тема",
                Description = "Описание тестовой темы"
            };

            var theme2 = new Theme()
            {
                ThemeName = "Тестовая тема 2",
                Description = "Описание тестовой темы 2"
            };

            var theme3 = new Theme()
            {
                ThemeName = "Тестовая тема 2"
            };

            var theme4 = new Theme()
            {
                ThemeName = "Тестовая тема 4",
                Description = "- Пункт 1" + Environment.NewLine
                                          + "- Пункт 1" + Environment.NewLine
                                          + "- Пункт 1" + Environment.NewLine
                                          + "- Пункт 1" + Environment.NewLine
                                          + "- Пункт 1" + Environment.NewLine
            };

            dbContext.Themes.Add(theme1);
            dbContext.Themes.Add(theme2);
            dbContext.Themes.Add(theme3);
            dbContext.Themes.Add(theme4);

            await dbContext.SaveChangesAsync();
        }
    }
}
