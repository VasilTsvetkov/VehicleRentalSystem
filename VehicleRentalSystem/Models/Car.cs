namespace VehicleRentalSystem.Models
{
	public class Car : Vehicle
	{
		public int SafetyRating { get; set; }

		public Car(string brand, string model, decimal value,int rentalPeriod, int safetyRating)
			: base(brand, model, value, rentalPeriod)
		{
			SafetyRating = safetyRating;
		}
	}
}
