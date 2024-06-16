namespace VehicleRentalSystem.Models
{
	public class InsuranceRate
	{
		public decimal InitialRate { get; set; }

		public decimal TotalRate { get; set; }

		public decimal RateAdjustment { get; set; }

		public InsuranceRate(decimal initialRate, decimal totalRate, decimal rateAdjustment)
		{
			InitialRate = initialRate;
			TotalRate = totalRate;
			RateAdjustment = rateAdjustment;
		}
	}
}
