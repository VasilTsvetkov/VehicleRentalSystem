using System.Text;

namespace VehicleRentalSystem.Models
{
	public class Invoice
	{
		public DateTime Date { get; set; }

		public string CustomerName { get; set; }

		public DateTime ReservationStartDate { get; set; }

		public DateTime ReservationEndDate { get; set; }

		public DateTime ActualReturnDate { get; set; }

		public Vehicle Vehicle { get; set; }

		public Invoice(DateTime date, string customerName,
					   DateTime reservationStartDate, DateTime reservationEndDate,
					   DateTime actualReturnDate, Vehicle vehicle)
		{
			this.Date = date;
			this.CustomerName = customerName;
			this.ReservationStartDate = reservationStartDate;
			this.ReservationEndDate = reservationEndDate;
			this.ActualReturnDate = actualReturnDate;
			this.Vehicle = vehicle;
		}

		private decimal CalculateRentalCostPerDay(Vehicle vehicle)
		{
			decimal dailyRentalCost = vehicle is Car ? 20m : vehicle is Motorcycle ? 15m : 50m;

			if ((ReservationEndDate - ReservationStartDate).Days > 7)
			{
				dailyRentalCost = vehicle is Car ? 15m : vehicle is Motorcycle ? 10m : 40m;
			}

			return dailyRentalCost;
		}

		private InsuranceRate CalculateInsurancePerDay(Vehicle vehicle)
		{
			decimal initialRate = 0;
			decimal totalRate = 0;

			if (vehicle is Car)
			{
				Car car = (Car)vehicle;
				initialRate = car.VehicleValue * 0.0001m;
				totalRate = initialRate;

				if (car.SafetyRating >= 4)
				{
					totalRate = 0.9m * initialRate;
				}
			}
			else if (vehicle is Motorcycle)
			{
				Motorcycle motorcycle = (Motorcycle)vehicle;
				initialRate = motorcycle.VehicleValue * 0.0002m;
				totalRate = initialRate;

				if (motorcycle.RiderAge < 25)
				{
					totalRate = 1.20m * initialRate;
				}
			}
			else if (vehicle is CargoVan)
			{
				CargoVan van = (CargoVan)vehicle;
				initialRate = van.VehicleValue * 0.0003m;
				totalRate = initialRate;

				if (van.DriverExperience > 5)
				{
					totalRate = 0.85m * initialRate;
				}
			}

			decimal rateAdhustment = totalRate > initialRate ? totalRate - initialRate : initialRate - totalRate;

			return new InsuranceRate(initialRate, totalRate, rateAdhustment);
		}

		private decimal AdjustRentalCost(DateTime reservationStartDate, DateTime reservationEndDate,
					   DateTime actualReturnDate, Vehicle vehicle)
		{
			return this.CalculateRentalCostPerDay(vehicle) * (ActualReturnDate - ReservationStartDate).Days + (this.CalculateRentalCostPerDay(vehicle) * (ReservationEndDate - ActualReturnDate).Days * 0.5m);
		}

		private decimal CalculateInsuranceDiscount(DateTime reservationStartDate, DateTime reservationEndDate,
					   DateTime actualReturnDate, Vehicle vehicle)
		{
			return (ReservationEndDate - ActualReturnDate).Days * CalculateInsurancePerDay(vehicle).TotalRate;
		}

		public override string ToString()
		{
			decimal rentalCostPerDay = CalculateRentalCostPerDay(Vehicle);
			InsuranceRate insuranceRate = CalculateInsurancePerDay(Vehicle);
			int rentalDays = (ActualReturnDate - ReservationStartDate).Days;
			decimal earlyReturnTotalRent = AdjustRentalCost(ReservationStartDate, ReservationEndDate, ActualReturnDate, Vehicle);
			decimal earlyReturnInsuranceDiscount = CalculateInsuranceDiscount(ReservationStartDate, ReservationEndDate, ActualReturnDate, Vehicle);

			StringBuilder sb = new StringBuilder();
			sb.AppendLine("XXXXXXXXXX");
			sb.AppendLine($"Date: {Date.ToString("yyyy-MM-dd")}");
			sb.AppendLine($"Customer Name: {CustomerName}");
			sb.AppendLine($"Rented Vehicle: {Vehicle.VehicleBrand} {Vehicle.VehicleModel}");
			sb.AppendLine();
			sb.AppendLine($"Reservation start date: {ReservationStartDate.ToString("yyyy-MM-dd")}");
			sb.AppendLine($"Reservation end date: {ReservationEndDate.ToString("yyyy-MM-dd")}");
			sb.AppendLine($"Reserved rental days: {Vehicle.RentalPeriod} days");
			sb.AppendLine();
			sb.AppendLine($"Actual return date: {ActualReturnDate.ToString("yyyy-MM-dd")}");
			sb.AppendLine($"Actual rental days: {rentalDays} days");
			sb.AppendLine();
			sb.AppendLine($"Rental cost per day: ${rentalCostPerDay.ToString("F2")}");
			if (insuranceRate.RateAdjustment != 0)
			{
				sb.AppendLine($"Initial insurance per day: ${insuranceRate.InitialRate.ToString("F2")}");
				if (insuranceRate.TotalRate > insuranceRate.InitialRate)
				{
					sb.AppendLine($"Insurance addition per day: ${insuranceRate.RateAdjustment.ToString("F2")}");
				}
				else
				{
					sb.AppendLine($"Insurance discount per day: ${insuranceRate.RateAdjustment.ToString("F2")}");
				}
			}
			sb.AppendLine($"Insurance per day: ${insuranceRate.TotalRate.ToString("F2")}");
			sb.AppendLine();
			if (ReservationEndDate != ActualReturnDate)
			{
				sb.AppendLine($"Early return discount for rent: ${(earlyReturnTotalRent - rentalCostPerDay * rentalDays).ToString("F2")}");
				sb.AppendLine($"Early return discount for insurance: ${earlyReturnInsuranceDiscount.ToString("F2")}");
				sb.AppendLine();
				sb.AppendLine($"Total rent: ${AdjustRentalCost(ReservationStartDate, ReservationEndDate, ActualReturnDate, Vehicle).ToString("F2")}");
			}
			else
			{
				sb.AppendLine($"Total rent: ${(rentalCostPerDay * rentalDays).ToString("F2")}");
			}
			sb.AppendLine($"Total Insurance: ${(insuranceRate.TotalRate * rentalDays).ToString("F2")}");
			if (ReservationEndDate != ActualReturnDate)
			{
				sb.AppendLine($"Total: ${(AdjustRentalCost(ReservationStartDate, ReservationEndDate, ActualReturnDate, Vehicle)+ (insuranceRate.TotalRate * rentalDays)).ToString("F2")}");
			}
			else
			{
				sb.AppendLine($"Total: ${(rentalCostPerDay * rentalDays + insuranceRate.TotalRate * rentalDays).ToString("F2")}");
			}
			sb.AppendLine("XXXXXXXXXX");

			return sb.ToString();
		}

		//public override string ToString()
		//{
		//	StringBuilder sb = new StringBuilder();
		//	sb.AppendLine("XXXXXXXXXX");
		//	sb.AppendLine($"Date: {Date.ToString("yyyy-MM-dd")}");
		//	sb.AppendLine($"Customer Name: {CustomerName}");
		//	sb.AppendLine($"Rented Vehicle: {Vehicle.VehicleBrand} {Vehicle.VehicleModel}");
		//	sb.AppendLine();
		//	sb.AppendLine($"Reservation start date: {ReservationStartDate.ToString("yyyy-MM-dd")}");
		//	sb.AppendLine($"Reservation end date: {ReservationEndDate.ToString("yyyy-MM-dd")}");
		//	sb.AppendLine($"Reserved rental days: {Vehicle.RentalPeriod} days");
		//	sb.AppendLine();
		//	sb.AppendLine($"Actual return date: {ActualReturnDate.ToString("yyyy-MM-dd")}");
		//	sb.AppendLine($"Actual rental days: {(ActualReturnDate - ReservationStartDate).Days} days");
		//	sb.AppendLine();
		//	sb.AppendLine($"Rental cost per day: ${CalculateRentalCostPerDay(Vehicle).ToString("F2")}");
		//	if (CalculateInsurancePerDay(Vehicle).RateAdjustment != 0)
		//	{
		//		sb.AppendLine($"Initial insurance per day: ${CalculateInsurancePerDay(Vehicle).InitialRate.ToString("F2")}");
		//	}
		//	if (CalculateInsurancePerDay(Vehicle).RateAdjustment != 0)
		//	{
		//		if (CalculateInsurancePerDay(Vehicle).TotalRate > CalculateInsurancePerDay(Vehicle).InitialRate)
		//		{
		//			sb.AppendLine($"Insurance addition per day: ${CalculateInsurancePerDay(Vehicle).RateAdjustment.ToString("F2")}");
		//		}
		//		else
		//		{
		//			sb.AppendLine($"Insurance discount per day: ${CalculateInsurancePerDay(Vehicle).RateAdjustment.ToString("F2")}");
		//		}
		//	}
		//	sb.AppendLine($"Insurance per day: ${CalculateInsurancePerDay(Vehicle).TotalRate.ToString("F2")}");
		//	sb.AppendLine();
		//	if (ReservationEndDate != ActualReturnDate)
		//	{
		//		sb.AppendLine($"Early return discount for rent: ${(Vehicle.RentalPeriod * CalculateRentalCostPerDay(Vehicle) - AdjustRentalCost(ReservationStartDate, ReservationEndDate, ActualReturnDate, Vehicle)).ToString("F2")}");
		//		sb.AppendLine($"Early return discount for insurance: ${CalculateInsuranceDiscount(ReservationStartDate, ReservationEndDate, ActualReturnDate, Vehicle).ToString("F2")}");
		//		sb.AppendLine();
		//	}
		//	if (ReservationEndDate == ActualReturnDate)
		//	{
		//		sb.AppendLine($"Total rent: ${((ActualReturnDate - ReservationStartDate).Days * CalculateRentalCostPerDay(Vehicle)).ToString("F2")}");
		//	}
		//	else
		//	{
		//		sb.AppendLine($"Total rent: ${AdjustRentalCost(ReservationStartDate, ReservationEndDate, ActualReturnDate, Vehicle).ToString("F2")}");
		//	}
		//	sb.AppendLine($"Total Insurance: ${((ActualReturnDate - ReservationStartDate).Days * CalculateInsurancePerDay(Vehicle).TotalRate).ToString("F2")}");
		//	if (ReservationEndDate == ActualReturnDate)
		//	{
		//		sb.AppendLine($"Total: ${((ActualReturnDate - ReservationStartDate).Days * (CalculateInsurancePerDay(Vehicle).TotalRate + CalculateRentalCostPerDay(Vehicle))).ToString("F2")}");
		//	}
		//	else
		//	{
		//		sb.AppendLine($"Total: ${(AdjustRentalCost(ReservationStartDate, ReservationEndDate, ActualReturnDate, Vehicle) + (ActualReturnDate - ReservationStartDate).Days * CalculateInsurancePerDay(Vehicle).TotalRate).ToString("F2")}");
		//	}
		//	sb.AppendLine("XXXXXXXXXX");

		//	return sb.ToString();
		//}
	}
}
