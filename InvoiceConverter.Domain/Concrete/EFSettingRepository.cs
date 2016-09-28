using InvoiceConverter.Domain.Abstract;
using InvoiceConverter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceConverter.Domain.Concrete
{
    public class EFSettingRepository : ISettingRepository
    {
        private EFDbContext context = new EFDbContext();

        public IQueryable<Setting> Settings { get { return context.Settings; } }

        public void SaveSetting(Setting setting)
        {
            if (setting.ID == string.Empty)
            {
                context.Settings.Add(setting);
            }
            else
            {
                Setting dbEntry = context.Settings.Find(setting.ID);
                if (dbEntry != null)
                {
                    dbEntry.Value = setting.Value;
                }
            }

            context.SaveChanges();
        }

        public Setting DeleteSetting(string settingID)
        {
            Setting dbEntry = context.Settings.Find(settingID);
            if (dbEntry != null)
            {
                context.Settings.Remove(dbEntry);
                context.SaveChanges();
            }

            return dbEntry;
        }
    }
}
