using Forum.DAL.Interfaces;
using Forum.DAL.Repositories;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Forum.BLL.Infrastructure
{
    public class ServiceModule : NinjectModule
    {
        private string connectionString;

        //Connection string with DB.
        public ServiceModule(string connection)
        {
            connectionString = connection;
        }

        //Injection IUnitOfWork to EFUnitOfWork.
        public override void Load()
        {
            Bind<IUnitOfWork>().To<EFUnitOfWork>().WithConstructorArgument(connectionString);
        }
    }
}
