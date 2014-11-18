using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dependencies;
using Ninject;
using taurus.Core.Interfaces;
using taurus.Core.Factories;

namespace ninject.WebUI
{
    /// <summary>
    /// Resolves Dependencies Using Ninject
    /// </summary>
    public class NinjectHttpResolver : IDependencyResolver, IDependencyScope
    {
        public IKernel Kernel { get; private set; }
        public NinjectHttpResolver(params NinjectModule[] modules)
        {
            Kernel = new StandardKernel(modules);
        }

        public NinjectHttpResolver(Assembly assembly)
        {
            Kernel = new StandardKernel();
            Kernel.Load(assembly);
        }

        public object GetService(Type serviceType)
        {
            return Kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return Kernel.GetAll(serviceType);
        }

        public void Dispose()
        {
            //Do Nothing
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }
    }



    //Main Module For Application
    public class LocalModule : NinjectModule
    {
        public override void Load()
        {
            //TODO: Bind to Concrete Types Here
            this.Bind<ICastleProvider>().To<CastleProviderFactory>();
            this.Bind<IUser>().To<UserFactory>();
            this.Bind<IProveedor>().To<ProveedorFactory>();
            this.Bind<IArticulo>().To<ArticuloFactory>();
            this.Bind<ISistema>().To<SistemaFactory>();
            this.Bind<IStock>().To<StockFactory>();
            this.Bind<IRolFactory>().To<RolFactory>();
            this.Bind<IDepartamento>().To<DepartmentFactory>();
            this.Bind<IUso>().To<UsoFactory>();
            this.Bind<IEmpleado>().To<EmpleadoFactory>();
            this.Bind<IConsumo>().To<ConsumoFactory>();
            this.Bind<IMaquina>().To<MaquinaFactory>();
            this.Bind<IBitacora>().To<BitacoraFactory>();
            this.Bind<IArea>().To<AreaFactory>();
            this.Bind<ICategoria>().To<CategoriaFactory>();
            this.Bind<IUnidad>().To<UnidadFactory>();
            this.Bind<ICuenta>().To<CuentaFactory>();
            this.Bind<ICompac>().To<CompacFactory>();
        }
    }

    /// <summary>
    /// Its job is to Register Ninject Modules and Resolve Dependencies
    /// </summary>
    public class NinjectHttpContainer
    {
        private static NinjectHttpResolver _resolver;

        //Register Ninject Modules
        public static void RegisterModules(NinjectModule[] modules)
        {
            _resolver = new NinjectHttpResolver(modules);
            GlobalConfiguration.Configuration.DependencyResolver = _resolver;
        }

        public static void RegisterAssembly()
        {
            //Assembly assambly = Assembly.Load("ninject.WebUI");
            //_resolver = new NinjectHttpResolver(assambly);
            _resolver = new NinjectHttpResolver(Assembly.GetExecutingAssembly());
            //This is where the actual hookup to the Web API Pipeline is done.
            GlobalConfiguration.Configuration.DependencyResolver = _resolver;
        }

        //Manually Resolve Dependencies
        public static T Resolve<T>()
        {
            return _resolver.Kernel.Get<T>();
        }
    }
}

/*[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(taurus.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(taurus.App_Start.NinjectWebCommon), "Stop")]

namespace taurus.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using taurus.Core.Interfaces;
    using taurus.Core.Factories;
    using System.Web.Http;
    using Ninject.Web.Mvc;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IUser>().To<UserFactory>();
        }        
    }


}
*/