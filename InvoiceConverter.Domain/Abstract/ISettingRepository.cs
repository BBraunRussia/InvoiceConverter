using InvoiceConverter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceConverter.Domain.Abstract
{
    public interface ISettingRepository
    {
        IQueryable<Setting> Settings { get; }
        void SaveSetting(Setting setting);
        Setting DeleteSetting(string settingID);
    }
}
