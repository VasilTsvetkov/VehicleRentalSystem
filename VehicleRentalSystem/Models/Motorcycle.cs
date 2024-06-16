namespace VehicleRentalSystem.Models
{
	public class Motorcycle : Vehicle
	{
		public int RiderAge { get; set; }

		public Motorcycle(string brand, string model, decimal value, int rentalPeriod, int riderAge)
			: base(brand, model, value, rentalPeriod)
		{
			RiderAge = riderAge;
		}
	}
}
