using System.Reflection;
using Ninject;

namespace ExtraAirApi.Utils.Ninject
{
	public static class IoC
	{
		public static T Get<T>()
		{
			IKernel kernal = new StandardKernel();
			kernal.Load(Assembly.GetExecutingAssembly());
			return kernal.Get<T>();
		}
	}
}