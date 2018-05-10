using MortgageData.DataSources;
using System;
using System.Linq;
namespace MortgageData
{
	public class MortgageDataContext : IDisposable
	{
		public IQueryable<Mortgage> Mortgages
		{
			get
			{
				return MortgageDataHelper.GetMortgagesFromApi().AsQueryable<Mortgage>();
			}
		}
		private void ReleaseUnmanagedResources()
		{
		}
		public void Dispose()
		{
			this.ReleaseUnmanagedResources();
			GC.SuppressFinalize(this);
		}
		~MortgageDataContext()
		{
			this.ReleaseUnmanagedResources();
		}
	}
}
