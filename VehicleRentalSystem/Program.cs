using VehicleRentalSystem.Models;

Car car = new Car("Mitsubishi", "Mirage", 15000m, 10, 3);

DateTime carDateOfInvoice = new DateTime(2024, 6, 13);
string carCustomerName = "John Doe";
DateTime carReservationStartDate = new DateTime(2024, 6, 3);
DateTime carReservationEndDate = new DateTime(2024, 6, 13);
DateTime carActualReturnDate = new DateTime(2024, 6, 13);

Invoice carInvoice = new Invoice(carDateOfInvoice, carCustomerName, carReservationStartDate, 
	carReservationEndDate, carActualReturnDate, car);

Motorcycle motorcycle = new Motorcycle("Triumph", "Tiger Sport 660", 10000m, 10, 20);

DateTime motorcycleDateOfInvoice = new DateTime(2024, 6, 13);
string motorcycleCustomerName = "Marry Johnson";
DateTime motorcycleReservationStartDate = new DateTime(2024, 6, 3);
DateTime motorcycleReservationEndDate = new DateTime(2024, 6, 13);
DateTime motorcycleActualReturnDate = new DateTime(2024, 6, 13);

Invoice motorcycleInvoice = new Invoice(motorcycleDateOfInvoice, motorcycleCustomerName, motorcycleReservationStartDate,
	motorcycleReservationEndDate, motorcycleActualReturnDate, motorcycle);

CargoVan van = new CargoVan("Citroen", "Jumper", 20000m, 15, 8);

DateTime cargoVanDateOfInvoice = new DateTime(2024, 6, 13);
string cargoVanCustomerName = "John Markson";
DateTime cargoVanReservationStartDate = new DateTime(2024, 6, 3);
DateTime cargoVanReservationEndDate = new DateTime(2024, 6, 18);
DateTime cargoVanActualReturnDate = new DateTime(2024, 6, 13);

Invoice cargoVanInvoice = new Invoice(cargoVanDateOfInvoice, cargoVanCustomerName, cargoVanReservationStartDate,
	cargoVanReservationEndDate, cargoVanActualReturnDate, van);

Console.WriteLine(carInvoice.ToString());
Console.WriteLine(motorcycleInvoice.ToString());
Console.WriteLine(cargoVanInvoice.ToString());