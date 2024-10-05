using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleaMarketV2Client
{
    public class ServiceIoC
    {
        private static readonly Lazy<ServiceIoC> serviceIoc = new Lazy<ServiceIoC>(() => new ServiceIoC(), true);
        private ConcurrentDictionary<Type, object> viewModels = new ConcurrentDictionary<Type, object>();

        public static ServiceIoC Container
        {
            get { return serviceIoc.Value; }
        }

        public void Dispose()
        {
            this.viewModels.Clear();
        }

        public void Register<TClass>() where TClass : new()
        {
            Type type = typeof(TClass);
            if (this.viewModels.ContainsKey(type) == false)
                this.viewModels.TryAdd(type, new TClass());
        }

        public void Register<TClass>(SimpleInjector.Lifestyles.SingletonLifestyle singleton) where TClass : new()
        {
            Type type = typeof(TClass);
            if (this.viewModels.ContainsKey(type) == false)
                this.viewModels.TryAdd(type, new TClass());
        }

        public void Unregister<TClass>()
        {
            Type type = typeof(TClass);
            this.viewModels.TryRemove(type, out object viewModel);
        }

        public TClass GetViewModel<TClass>()
        {
            Type type = typeof(TClass);
            this.viewModels.TryGetValue(type, out object viewModel);

            return (TClass)viewModel;
        }

        public void SetViewModel<TClass>(TClass @class)
        {
            Type type = typeof(TClass);
            bool isGet = this.viewModels.TryGetValue(type, out object viewModel);
            if (isGet == true)
                viewModel = @class;
        }
    }
}
