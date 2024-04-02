using ElectronicQueue.Database.Models;
using ElectronicQueue.Database.Models.Enums;
using ElectronicQueue.Models;
using System;

namespace ElectronicQueue.Database
{
    public static class Database
    {
        public static List<Status> statusList = new List<Status>()
        {
             new Status() { Number = (short)QueueElementStatus.None, Name = "В очереди" },
             new Status() { Number = (short)QueueElementStatus.Called, Name = "Подойдите к окну" },
             new Status() { Number = (short)QueueElementStatus.Processing, Name = "В процессе" },
             new Status() { Number = (short)QueueElementStatus.Processed, Name = "Завершен" },
        };
        public static List<Theme> themes = new List<Theme>()
        {
            new Theme() { Id =Guid.NewGuid().ToString("D"),ThemeName = "Получить пенсию", Description = "Test" },
            new Theme() { Id =Guid.NewGuid().ToString("D"),ThemeName = "Перевести деньги", Description = "Test" }
        };

        public static List<QueueItem> queueItems = new List<QueueItem>()
        {
             new QueueItem() { Id =Guid.NewGuid().ToString("D"), ThemeId = themes[0].Id,Theme =themes[0], Number = "П1" ,Status = statusList[0] },
             new QueueItem() { Id =Guid.NewGuid().ToString("D"),ThemeId = themes[1].Id,Theme =themes[1], Number = "П2" , Status = statusList[0]}
        };


    }
}
