using InvoiceConverter.Domain.Abstract;
using InvoiceConverter.Domain.Concrete;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceConverter.Domain.Infractructure
{
    public static class CompositionRoot
    {
        private static IKernel kernel;

        static CompositionRoot()
        {
            kernel = new StandardKernel();
            AddBindings();
        }

        private static void AddBindings()
        {
            kernel.Bind<ICustomerRepository>().To<EFCustomerRepository>();
            kernel.Bind<ISettingRepository>().To<EFSettingRepository>();
            kernel.Bind<IMailRepository>().To<EFMailRepository>();
        }

        public static T Resolve<T>()
        {
            return kernel.Get<T>();
        }
    }
}
