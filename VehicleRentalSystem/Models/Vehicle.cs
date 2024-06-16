namespace VehicleRentalSystem.Models
{
	public abstract class Vehicle
	{
        public string VehicleBrand { get; set; }

        public string VehicleModel { get; set; }

		public decimal VehicleValue { get; set; }

		public int RentalPeriod { get; set; }

		protected Vehicle(string brand, string model, decimal value, int rentalPeriod)
        {
            this.VehicleBrand = brand;
            this.VehicleModel = model;
            this.VehicleValue = value;
            this.RentalPeriod = rentalPeriod;
        }
    }
}
