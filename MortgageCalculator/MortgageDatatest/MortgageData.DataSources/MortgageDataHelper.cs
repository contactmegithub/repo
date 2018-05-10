using System;
using System.Collections.Generic;
using System.Threading;
namespace MortgageData.DataSources
{
	internal class MortgageDataHelper
	{
		internal static Mortgage[] GetMortgagesFromApi()
		{
			List<Mortgage> list = new List<Mortgage>();
			list.Add(new Mortgage
			{
				MortgageId = 1,
				Name = "Fixed Home Loan (Interest Only)",
				InterestRepayment = InterestRepayment.InterestOnly,
				EffectiveStartDate = DateTime.Now.AddDays(-1.0),
				MortgageType = MortgageType.Fixed,
				CancellationFee = 259.99m,
				EstablishmentFee = 199.99m,
				EffectiveEndDate = DateTime.Now.AddYears(1),
				InterestRate = 4.99m,
				SchemaIdentifier = Guid.NewGuid(),
                TermsInMonths = 12
			});
            list.Add(new Mortgage
            {
                MortgageId = 2,
                Name = "Fixed Home Loan (Principal and Interest)",
                InterestRepayment = InterestRepayment.PrincipalAndInterest,
                EffectiveStartDate = DateTime.Now.AddDays(-1.0),
                MortgageType = MortgageType.Fixed,
                CancellationFee = 259.99m,
                EstablishmentFee = 199.99m,
                EffectiveEndDate = DateTime.Now.AddYears(1),
                InterestRate = 4.81m,
                SchemaIdentifier = Guid.NewGuid(),
                TermsInMonths = 12
            });
            list.Add(new Mortgage
            {
                MortgageId = 3,
                Name = "Variable Home Loan (Interest Only)",
                InterestRepayment = InterestRepayment.InterestOnly,
                EffectiveStartDate = DateTime.Now.AddDays(-1.0),
                MortgageType = MortgageType.Variable,
                CancellationFee = 259.99m,
                EstablishmentFee = 199.99m,
                EffectiveEndDate = DateTime.Now.AddYears(1),
                InterestRate = 5.24m,
                SchemaIdentifier = Guid.NewGuid(),
                TermsInMonths = 12
            });
            list.Add(new Mortgage
            {
                MortgageId = 4,
                Name = "Variable Home Loan (Principal and Interest)",
                InterestRepayment = InterestRepayment.PrincipalAndInterest,
                EffectiveStartDate = DateTime.Now.AddDays(-1.0),
                MortgageType = MortgageType.Variable,
                CancellationFee = 259.99m,
                EstablishmentFee = 199.99m,
                EffectiveEndDate = DateTime.Now.AddYears(1),
                InterestRate = 5.12m,
                SchemaIdentifier = Guid.NewGuid(),
                TermsInMonths = 12
            });
            list.Add(new Mortgage
            {
                MortgageId = 5,
                Name = "Variable Investment Loan (Principal and Interest)",
                InterestRepayment = InterestRepayment.PrincipalAndInterest,
                MortgageType = MortgageType.Variable,
                EffectiveStartDate = DateTime.Now.AddDays(-1.0),
                CancellationFee = 259.99m,
                EstablishmentFee = 199.99m,
                EffectiveEndDate = DateTime.Now.AddYears(1),
                InterestRate = 5.99m,
                SchemaIdentifier = Guid.NewGuid(),
                TermsInMonths = 12
            });
            list.Add(new Mortgage
            {
                MortgageId = 6,
                Name = "Variable Investment Loan (Interest Only)",
                InterestRepayment = InterestRepayment.InterestOnly,
                MortgageType = MortgageType.Variable,
                EffectiveStartDate = DateTime.Now.AddDays(-1.0),
                CancellationFee = 259.99m,
                EstablishmentFee = 199.99m,
                EffectiveEndDate = DateTime.Now.AddYears(1),
                InterestRate = 5.39m,
                SchemaIdentifier = Guid.NewGuid(),
                TermsInMonths = 12
            });
            list.Add(new Mortgage
            {
                MortgageId = 7,
                Name = "Fixed Investment Loan (Principal and Interest)",
                InterestRepayment = InterestRepayment.PrincipalAndInterest,
                MortgageType = MortgageType.Fixed,
                EffectiveStartDate = DateTime.Now.AddDays(-1.0),
                CancellationFee = 259.99m,
                EstablishmentFee = 199.99m,
                EffectiveEndDate = DateTime.Now.AddYears(1),
                InterestRate = 5.89m,
                SchemaIdentifier = Guid.NewGuid(),
                TermsInMonths = 12
            });
            list.Add(new Mortgage
            {
                MortgageId = 8,
                Name = "Fixed Investment Loan (Interest Only)",
                InterestRepayment = InterestRepayment.InterestOnly,
                MortgageType = MortgageType.Fixed,
                EffectiveStartDate = DateTime.Now.AddDays(-1.0),
                CancellationFee = 259.99m,
                EstablishmentFee = 199.99m,
                EffectiveEndDate = DateTime.Now.AddYears(1),
                InterestRate = 5.19m,
                SchemaIdentifier = Guid.NewGuid(),
                TermsInMonths = 12
            });
            //Thread.Sleep(TimeSpan.FromSeconds(3.0));
			return list.ToArray();
		}
	}
}
