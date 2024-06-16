namespace VehicleRentalSystem.Models
{
	public class CargoVan : Vehicle
	{
		public int DriverExperience { get; set; }

		public CargoVan(string brand, string model, decimal value, int rentalPeriod, int driverExperience)
			: base(brand, model, value, rentalPeriod)
		{
			DriverExperience = driverExperience;
		}
	}
}
