using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();

            builder.RegisterType<UserDal>().As<IUserDal>();
            builder.RegisterType<UserManager>().As<IUserService>();

            builder.RegisterType<CustomerDal>().As<ICustomerDal>();
            builder.RegisterType<CustomerManager>().As<ICustomerService>();

            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>();

            builder.RegisterType<UserOperationClaimDal>().As<IUserOperationClaimDal>();
            builder.RegisterType<UserOperationClaimManager>().As<IUserOperationClaimService>();

            builder.RegisterType<CurrencyDal>().As<ICurrencyDal>();
            builder.RegisterType<CurrencyManager>().As<ICurrencyService>();

            builder.RegisterType<ProductDal>().As<IProductDal>();
            builder.RegisterType<ProductManager>().As<IProductService>();

            builder.RegisterType<InvoiceDal>().As<IInvoiceDal>();
            builder.RegisterType<InvoiceManager>().As<IInvoiceService>();

            builder.RegisterType<InvoiceProductDal>().As<IInvoiceProductDal>();
            builder.RegisterType<InvoiceProductManager>().As<IInvoiceProductService>();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}
