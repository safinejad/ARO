using Contracts.DataModel;
using Microsoft.EntityFrameworkCore;

namespace Data;

public static class DbInitializer
{
    public static int TestAdultSearch = 2;
    public static int TestChildSearch = 1;
    public static DateTime TestCheckIn = DateTime.Today.AddDays(15).AddHours(14);
    public static DateTime TestCheckOut = DateTime.Today.AddDays(15 + 8).AddHours(12);
    public static void Initialize(BookingContext context)
    {
        context.Database.EnsureCreated();

        if (context.Currencies.Any())
        {
            return;
        }
        SetIdentityInsert(typeof(Currency), context, () =>
        {
            var currencies = new Currency[]
            {
                new Currency() { ExchangeRateFromUSDollar = 1, Name = "USD", Sign = "$", Id = 1 },
                new Currency() { ExchangeRateFromUSDollar = 0.8M, Name = "EUR", Sign = "€", Id = 2 },
                new Currency() { ExchangeRateFromUSDollar = 1.5M, Name = "GBP", Sign = "£", Id = 3 }
            };
            context.Currencies.AddRange(currencies);
        });

        SetIdentityInsert(typeof(GeographicBoundary), context, () =>
        {
            var geos = new GeographicBoundary[]
            {
            new GeographicBoundary(){ BoundaryType = GeographicBoundaryTypeEnum.Country, Name = "United States", Id = 1, ParentId = null, CenterLatitude = 42.2152141M, CenterLongitude = -111.0898047M,MapLink = "https://www.google.com/maps/place/United+States/@42.2152141,-111.0898047,4z/data=!4m5!3m4!1s0x54eab584e432360b:0x1c3bb99243deb742!8m2!3d37.09024!4d-95.712891"},
            new GeographicBoundary(){ BoundaryType = GeographicBoundaryTypeEnum.State, Name = "New York State", ParentId = 1, Id = 2, CenterLatitude = 42.7287756M, CenterLongitude = -78.0132688M,MapLink = "https://www.google.com/maps/place/New+York,+USA/@42.7287756,-78.0132688,7z/data=!3m1!4b1!4m5!3m4!1s0x4ccc4bf0f123a5a9:0xddcfc6c1de189567!8m2!3d43.2994285!4d-74.2179326"},
            new GeographicBoundary(){ BoundaryType = GeographicBoundaryTypeEnum.City, Name = "New York City", ParentId = 2, Id =3 ,CenterLatitude = 40.6976701M, CenterLongitude = -74.2598679M, MapLink = "https://www.google.com/maps/place/New+York,+NY,+USA/@40.6976701,-74.2598679,10z/data=!3m1!4b1!4m13!1m7!3m6!1s0x4ccc4bf0f123a5a9:0xddcfc6c1de189567!2sNew+York,+USA!3b1!8m2!3d43.2994285!4d-74.2179326!3m4!1s0x89c24fa5d33f083b:0xc80b8f06e177fe62!8m2!3d40.7139558!4d-74.0039063"},
            new GeographicBoundary(){ BoundaryType = GeographicBoundaryTypeEnum.District, Name = "Brooklyn", ParentId = 3, Id=4, CenterLatitude = 40.6406192M, CenterLongitude = -74.015691M, MapLink = "https://www.google.com/maps/place/Brooklyn,+NY,+USA/@40.6406192,-74.015691,12.3z/data=!4m13!1m7!3m6!1s0x4ccc4bf0f123a5a9:0xddcfc6c1de189567!2sNew+York,+USA!3b1!8m2!3d43.2994285!4d-74.2179326!3m4!1s0x89c24416947c2109:0x82765c7404007886!8m2!3d40.6781646!4d-73.9441681"},
            new GeographicBoundary(){ BoundaryType = GeographicBoundaryTypeEnum.State, Name = "Washington State", ParentId = 1, Id = 5, CenterLatitude = 47.2548363M, CenterLongitude = -123.1252223M, MapLink = "https://www.google.com/maps/place/Washington,+USA/@47.2548363,-123.1252223,7z/data=!3m1!4b1!4m13!1m7!3m6!1s0x4ccc4bf0f123a5a9:0xddcfc6c1de189567!2sNew+York,+USA!3b1!8m2!3d43.2994285!4d-74.2179326!3m4!1s0x5485e5ffe7c3b0f9:0x944278686c5ff3ba!8m2!3d46.9989876!4d-120.602417"},
            new GeographicBoundary(){ BoundaryType = GeographicBoundaryTypeEnum.City, Name = "Seattle", ParentId = 5, Id = 6, CenterLatitude = 47.5976203M, CenterLongitude = -122.6273127M, MapLink = "https://www.google.com/maps/place/Seattle,+WA,+USA/@47.5976203,-122.6273127,9.18z/data=!4m13!1m7!3m6!1s0x4ccc4bf0f123a5a9:0xddcfc6c1de189567!2sNew+York,+USA!3b1!8m2!3d43.2994285!4d-74.2179326!3m4!1s0x5490102c93e83355:0x102565466944d59a!8m2!3d47.606163!4d-122.332077"},
            new GeographicBoundary(){ BoundaryType = GeographicBoundaryTypeEnum.District, Name = "Delridge", ParentId = 6, Id = 7, CenterLatitude = 47.5498556M, CenterLongitude = -122.3871459M, MapLink = "https://www.google.com/maps/place/Delridge,+Seattle,+WA,+USA/@47.5498556,-122.3871459,13z/data=!3m1!4b1!4m5!3m4!1s0x549041a435d7a96d:0xa6d4695b2fcf828e!8m2!3d47.5355297!4d-122.3550582"},
            new GeographicBoundary(){ BoundaryType = GeographicBoundaryTypeEnum.City, Name = "Rochester", ParentId = 2, Id = 8, CenterLatitude = 43.1862853M, CenterLongitude = -77.686439M, MapLink = "https://www.google.com/maps/place/Rochester,+NY,+USA/@43.1862853,-77.686439,12z/data=!3m1!4b1!4m5!3m4!1s0x89d6b3059614b353:0x5a001ffc4125e61e!8m2!3d43.1566079!4d-77.6087952"},
            new GeographicBoundary(){ BoundaryType = GeographicBoundaryTypeEnum.District, Name = "19th Ward", ParentId = 8, Id = 9, CenterLatitude = 43.1862853M, CenterLongitude = -77.686439M, MapLink = "https://www.google.com/maps/place/Rochester,+NY,+USA/@43.1862853,-77.686439,12z/data=!3m1!4b1!4m5!3m4!1s0x89d6b3059614b353:0x5a001ffc4125e61e!8m2!3d43.1566079!4d-77.6087952"},
            new GeographicBoundary(){ BoundaryType = GeographicBoundaryTypeEnum.District, Name = "Queens", ParentId = 3, Id=10, CenterLatitude = 40.6511939M, CenterLongitude = -74.0112766M, MapLink = "https://www.google.com/maps/place/Queens,+NY,+USA/@40.6511939,-74.0112766,11z/data=!3m1!4b1!4m5!3m4!1s0x89c24369470a592b:0x4109d18b6c5c7b05!8m2!3d40.7282019!4d-73.7948227"},
            };
            context.GeographicBoundaries.AddRange(geos);
        });
        var neighbours = new Neighbourhood[]
        {
            new Neighbourhood() { Name = "JFK Airport", PropertyType = PropertyTypeEnum.Airport, CenterLatitude = 40.6622095M , CenterLongitude = -73.8513445M, GeographicBoundaryId = 10},
            new Neighbourhood() { Name = "King Manor Museum", PropertyType = PropertyTypeEnum.Museum ,CenterLatitude =40.7044103M , CenterLongitude = -73.8073599M, GeographicBoundaryId = 10},
            new Neighbourhood() { Name = "Tranquility Garden", PropertyType = PropertyTypeEnum.Nature ,CenterLatitude = 40.6521959M, CenterLongitude = -74.0014904M, GeographicBoundaryId = 4},
            new Neighbourhood() { Name = "Floyd Bennett Field", PropertyType = PropertyTypeEnum.Nature,CenterLatitude = 40.6052886M, CenterLongitude = -73.882861M, GeographicBoundaryId = 4},
        };
        context.Neighbourhoods.AddRange(neighbours);
        var facilities = new Facility[]
            {
            new Facility(){Id=   1, FacilityType = FacilityTypeEnum.Dining, Highlighted = true, Name = "Bed And Breakfast"},
            new Facility(){Id=   2, FacilityType = FacilityTypeEnum.Dining, Highlighted = true, Name = "Full Board"},
            new Facility(){Id=   3, FacilityType = FacilityTypeEnum.Dining, Highlighted = true, Name = "All Inclusive"},
            new Facility(){Id=   4, FacilityType = FacilityTypeEnum.Dining, Highlighted = true, Name = "Ultimate All Inclusive"},
            new Facility(){Id=   5, FacilityType = FacilityTypeEnum.Accommodation, Highlighted = false, Name = "Electric Kettle"},
            new Facility(){Id=   6, FacilityType = FacilityTypeEnum.Accommodation, Highlighted = false, Name = "Free Toiletries"},
            new Facility(){Id=   7, FacilityType = FacilityTypeEnum.Accommodation, Highlighted = false, Name = "Shower"},
            new Facility(){Id=   8, FacilityType = FacilityTypeEnum.Accommodation, Highlighted = false, Name = "Toilet"},
            new Facility(){Id=   9, FacilityType = FacilityTypeEnum.Accommodation, Highlighted = false, Name = "Towels"},
            new Facility(){Id=  10, FacilityType = FacilityTypeEnum.Accommodation, Highlighted = false, Name = "Kitchen"},
            new Facility(){Id=  11, FacilityType = FacilityTypeEnum.Accommodation, Highlighted = false, Name = "Bidet"},
            new Facility(){Id=  12, FacilityType = FacilityTypeEnum.Accommodation, Highlighted = false, Name = "Iron"},
            new Facility(){Id=  13, FacilityType = FacilityTypeEnum.Internet, Highlighted = false, Name = "Free Wifi on lobby"},
            new Facility(){Id=  14, FacilityType = FacilityTypeEnum.CleaningServices, Highlighted = false, Name = "Ironing Facilities"},
            new Facility(){Id=  15, FacilityType = FacilityTypeEnum.CleaningServices, Highlighted = false, Name = "Laundry"},
            new Facility(){Id=  16, FacilityType = FacilityTypeEnum.CleaningServices, Highlighted = false, Name = "Dry Cleaning"},
            new Facility(){Id=  17, FacilityType = FacilityTypeEnum.CleaningServices, Highlighted = false, Name = "Daily house kipping"},
            new Facility(){Id=  18, FacilityType = FacilityTypeEnum.Accommodation, Highlighted = false, Name = "Fan"},
            new Facility(){Id=  19, FacilityType = FacilityTypeEnum.Comfort, Highlighted = false, Name = "Extra Long Beds"},
            new Facility(){Id=  20, FacilityType = FacilityTypeEnum.Comfort, Highlighted = false, Name = "Blackout Shade"},
            new Facility(){Id=  95, FacilityType = FacilityTypeEnum.Comfort, Highlighted = false, Name = "Extra Bed"},
            new Facility(){Id=  21, FacilityType = FacilityTypeEnum.Accommodation, Highlighted = false, Name = "Cable Channel"},
            new Facility(){Id=  22, FacilityType = FacilityTypeEnum.Accommodation, Highlighted = false, Name = "Sat Channel"},
            new Facility(){Id=  23, FacilityType = FacilityTypeEnum.Accommodation, Highlighted = false, Name = "Hairdryer"},
            new Facility(){Id=  24, FacilityType = FacilityTypeEnum.Accommodation, Highlighted = false, Name = "Paid Wifi"},
            new Facility(){Id=  25, FacilityType = FacilityTypeEnum.Accommodation, Highlighted = false, Name = "Hand Sanitizer"},
            new Facility(){Id=  26, FacilityType = FacilityTypeEnum.Accommodation, Highlighted = false, Name = "Wake-up Service"},
            new Facility(){Id=  27, FacilityType = FacilityTypeEnum.Accommodation, Highlighted = false, Name = "Wardrobe or Closet"},
            new Facility(){Id=  28, FacilityType = FacilityTypeEnum.Accommodation, Highlighted = true, Name = "Private Kitchen"},
            new Facility(){Id=  29, FacilityType = FacilityTypeEnum.Accommodation, Highlighted = true, Name = "Flat screen TV"},
            new Facility(){Id=  30, FacilityType = FacilityTypeEnum.Accommodation, Highlighted = true, Name = "Coffee machine"},
            new Facility(){Id=  31, FacilityType = FacilityTypeEnum.Accommodation, Highlighted = true, Name = "Air Conditioner"},
            new Facility(){Id=  32, FacilityType = FacilityTypeEnum.Parking, Highlighted = true, Name = "Parking"},
            new Facility(){Id=  33, FacilityType = FacilityTypeEnum.Outdoors, Highlighted = true, Name = "Inner courtyard view"},
            new Facility(){Id=  34, FacilityType = FacilityTypeEnum.Outdoors, Highlighted = true, Name = "Garden view"},
            new Facility(){Id=  35, FacilityType = FacilityTypeEnum.Outdoors, Highlighted = true, Name = "Sea view"},
            new Facility(){Id=  36, FacilityType = FacilityTypeEnum.Outdoors, Highlighted = true, Name = "Jungle view"},
            new Facility(){Id=  37, FacilityType = FacilityTypeEnum.Outdoors, Highlighted = true, Name = "Waterfall view"},
            new Facility(){Id=  38, FacilityType = FacilityTypeEnum.Accommodation, Highlighted = true, Name = "Entire villa"},
            new Facility(){Id=  39, FacilityType = FacilityTypeEnum.Accommodation, Highlighted = true, Name = "Ensuite bathroom"},
            new Facility(){Id=  40, FacilityType = FacilityTypeEnum.Accommodation, Highlighted = true, Name = "Mini bar"},
            new Facility(){Id=  41, FacilityType = FacilityTypeEnum.Comfort, Highlighted = true, Name = "Soundproofing"},
            new Facility(){Id=  42, FacilityType = FacilityTypeEnum.Comfort, Highlighted = true, Name = "NoSmoking Rooms"},
            new Facility(){Id=  43, FacilityType = FacilityTypeEnum.Internet, Highlighted = true, Name = "Free Wifi"},
            new Facility(){Id=  44, FacilityType = FacilityTypeEnum.Internet, Highlighted = true, Name = "Unlimited Wifi"},
            new Facility(){Id=  45, FacilityType = FacilityTypeEnum.Outdoors, Highlighted = true, Name = "Terrace"},
            new Facility(){Id=  46, FacilityType = FacilityTypeEnum.Outdoors, Highlighted = true, Name = "Private Courtyard"},
            new Facility(){Id=  47, FacilityType = FacilityTypeEnum.Outdoors, Highlighted = true, Name = "Private Beach"},
            new Facility(){Id=  48, FacilityType = FacilityTypeEnum.Outdoors, Highlighted = true, Name = "Courtyard"},
            new Facility(){Id=  49, FacilityType = FacilityTypeEnum.Floor, Highlighted = true, Name = "High floor"},
            new Facility(){Id=  50, FacilityType = FacilityTypeEnum.Floor, Highlighted = true, Name = "Mid floor"},
            new Facility(){Id=  51, FacilityType = FacilityTypeEnum.Floor, Highlighted = true, Name = "Sky scratch floor"},
            new Facility(){Id=  52, FacilityType = FacilityTypeEnum.Location, Highlighted = true, Name = "Tramway Access"},
            new Facility(){Id=  53, FacilityType = FacilityTypeEnum.Location, Highlighted = true, Name = "Subway Access"},
            new Facility(){Id=  54, FacilityType = FacilityTypeEnum.Location, Highlighted = true, Name = "Ferry Access"},
            new Facility(){Id=  55, FacilityType = FacilityTypeEnum.Location, Highlighted = true, Name = "Beach Front"},
            new Facility(){Id=  56, FacilityType = FacilityTypeEnum.Luxury, Highlighted = true, Name = "On-Door-Ski"},
            new Facility(){Id=  57, FacilityType = FacilityTypeEnum.Luxury, Highlighted = true, Name = "Massage"},
            new Facility(){Id=  58, FacilityType = FacilityTypeEnum.Entertainment, Highlighted = false, Name = "Disco"},
            new Facility(){Id=  59, FacilityType = FacilityTypeEnum.Entertainment, Highlighted = false, Name = "Bar"},
            new Facility(){Id=  60, FacilityType = FacilityTypeEnum.Entertainment, Highlighted = false, Name = "Night Club"},
            new Facility(){Id=  61, FacilityType = FacilityTypeEnum.Entertainment, Highlighted = false, Name = "Art gallery"},
            new Facility(){Id=  62, FacilityType = FacilityTypeEnum.Fitness, Highlighted = true, Name = "Spa"},
            new Facility(){Id=  63, FacilityType = FacilityTypeEnum.Fitness, Highlighted = true, Name = "Pool"},
            new Facility(){Id=  64, FacilityType = FacilityTypeEnum.Fitness, Highlighted = true, Name = "Indoor Pool"},
            new Facility(){Id=  65, FacilityType = FacilityTypeEnum.Fitness, Highlighted = false, Name = "Gym"},
            new Facility(){Id=  66, FacilityType = FacilityTypeEnum.Luxury, Highlighted = false, Name = "Conference Room"},
            new Facility(){Id=  67, FacilityType = FacilityTypeEnum.Entertainment, Highlighted = false, Name = "Cinema"},
            new Facility(){Id=  68, FacilityType = FacilityTypeEnum.Entertainment, Highlighted = false, Name = "Theater"},
            new Facility(){Id=  69, FacilityType = FacilityTypeEnum.Transfer, Highlighted = false, Name = "Airport Shuttle"},
            new Facility(){Id=  70, FacilityType = FacilityTypeEnum.Transfer, Highlighted = false, Name = "Pickup Service"},
            new Facility(){Id=  71, FacilityType = FacilityTypeEnum.Transfer, Highlighted = false, Name = "City Transfer"},
            new Facility(){Id=  72, FacilityType = FacilityTypeEnum.Reception, Highlighted = false, Name = "Travel Guide"},
            new Facility(){Id=  73, FacilityType = FacilityTypeEnum.Pricing, Highlighted = true, Name = "Limited-Time Deal"},
            new Facility(){Id=  74, FacilityType = FacilityTypeEnum.Pricing, Highlighted = true, Name = "Opening Festival Deal"},
            new Facility(){Id=  75, FacilityType = FacilityTypeEnum.Pricing, Highlighted = true, Name = "Event price break down"},
            new Facility(){Id=  76, FacilityType = FacilityTypeEnum.Pricing, Highlighted = true, Name = "Late check-out deal"},
            new Facility(){Id=  77, FacilityType = FacilityTypeEnum.Pricing, Highlighted = true, Name = "Early check-in deal"},
            new Facility(){Id=  78, FacilityType = FacilityTypeEnum.Languages, Highlighted = true, Name = "English"},
            new Facility(){Id=  79, FacilityType = FacilityTypeEnum.Languages, Highlighted = true, Name = "Spanish"},
            new Facility(){Id=  80, FacilityType = FacilityTypeEnum.Languages, Highlighted = true, Name = "Turkish"},
            new Facility(){Id=  81, FacilityType = FacilityTypeEnum.Languages, Highlighted = true, Name = "German"},
            new Facility(){Id=  82, FacilityType = FacilityTypeEnum.Languages, Highlighted = true, Name = "Swedish"},
            new Facility(){Id=  83, FacilityType = FacilityTypeEnum.Reception, Highlighted = true, Name = "24Hr Desk"},
            new Facility(){Id=  84, FacilityType = FacilityTypeEnum.Reception, Highlighted = true, Name = "ATM/cash Machine"},
            new Facility(){Id=  85, FacilityType = FacilityTypeEnum.Reception, Highlighted = true, Name = "Currency Exchange"},
            new Facility(){Id=  86, FacilityType = FacilityTypeEnum.Reception, Highlighted = true, Name = "Tour Desk"},
            new Facility(){Id=  87, FacilityType = FacilityTypeEnum.Recommend, Highlighted = true, Name = "Great for two travelers"},
            new Facility(){Id=  88, FacilityType = FacilityTypeEnum.Recommend, Highlighted = true, Name = "Great for couples"},
            new Facility(){Id=  89, FacilityType = FacilityTypeEnum.Recommend, Highlighted = true, Name = "Great for elders"},
            new Facility(){Id=  90, FacilityType = FacilityTypeEnum.Recommend, Highlighted = true, Name = "Great for two travelers with a kid"},
            new Facility(){Id=  91, FacilityType = FacilityTypeEnum.Recommend, Highlighted = true, Name = "Great for two travelers with two kids"},
            new Facility(){Id=  92, FacilityType = FacilityTypeEnum.Recommend, Highlighted = true, Name = "Great for two travelers with three kids"},
            new Facility(){Id=  93, FacilityType = FacilityTypeEnum.Recommend, Highlighted = true, Name = "Great for traveling solo"},
            new Facility(){Id=  94, FacilityType = FacilityTypeEnum.Recommend, Highlighted = true, Name = "Great for business"},

            };
        SetIdentityInsert(typeof(Facility), context, () =>
        {
            context.Facilities.AddRange(facilities);
        });
        SetIdentityInsert(typeof(Hotel), context, () =>
        {
            var hotels = new Hotel[]
            {
            new Hotel() { Id = 1, Name = "Holiday Inn Express Nyc Brooklyn", AddressBaseLine = "833 39th St, Brooklyn, NY 11232, United States", Latitude = 40.6457724M, Longitude = -74.0009023M, CleanlinessRate = 8.9M, ComfortRate = 9.2M, LocationRate = 7.1M, FacilityRate = 9.8M, MaxRoomsInReservation = 0, OverallScore = 9.3M, StaffRate = 7.4M, ValueRate = 9M, NumberOfReviews = 3381, PaymentCurrencyId =1, GeographicBoundaryId = 4, SustainableBadge  = true, Promoted = true, Stars = 2, Description = @"<div><p>Situated within 5.9 km of Barclays Center and 8.5 km of National September 11 Memorial &amp; Museum, Holiday Inn Express - NYC Brooklyn - Sunset Park, an IHG Hotel features rooms in Brooklyn. With free WiFi, this 3-star hotel offers a 24-hour front desk and a business centre. The property is set 10 km from Brooklyn Bridge and 10 km from Bloomingdales.</p><p>At the hotel, the rooms include a desk. The private bathroom is fitted with a hairdryer. All rooms will provide guests with air conditioning, a safety deposit box and a flat-screen TV.</p><p>Guests at Holiday Inn Express - NYC Brooklyn - Sunset Park, an IHG Hotel can enjoy a continental or a buffet breakfast.</p><p>One World Trade Center is 8.7 km from the accommodation, while Coney Island is 10 km from the property. The nearest airport is John F. Kennedy International Airport, 23 km from Holiday Inn Express - NYC Brooklyn - Sunset Park, an IHG Hotel. </p></div>" },
            new Hotel() { Id = 2, Name = "Ramada by Wyndham Rockville Centre", AddressBaseLine = "1000 Sunrise Highway, Rockville Centre, NY 11570, United States", Latitude = 40.6574934M, Longitude = -73.6235592M, CleanlinessRate = 9.9M, ComfortRate = 8.2M, LocationRate = 7.6M, FacilityRate = 7.6M, MaxRoomsInReservation = 0, OverallScore = 9.1M, StaffRate = 8.3M, ValueRate = 8M, NumberOfReviews = 1121, PaymentCurrencyId =2, GeographicBoundaryId = 10, SustainableBadge  = true, Promoted = false, Stars = 4, Description = @"<div><p>This Rockville Centre hotel offers free Wi-Fi, a seasonal outdoor pool, and rooms that feature a flat-screen TV and a refrigerator. Long Beach is 9.7 km away.</p><p>Rooms feature dark wood furnishing and a seating area. Tea and coffee making facilities are also provided at Ramada by Wyndham Rockville Centre.</p><p>Continental breakfast is served each morning and it features hot coffee or tea along with pastries.</p><p>Guests can workout in the fitness centre or use the business centre that offers fax and photocopying services. Vending machines that offer drinks and snacks are also available.</p><p>Ramada by Wyndham Rockville Centre is 12 minutes’ drive from Barnum Island and Jones Beach is 16.1 km away. </p></div>" },
            new Hotel() { Id = 3, Name = "JFK INN- Hotel JFK Airport New York", AddressBaseLine = "154 10 South Conduit Avenue, Queens, NY 11434, United States", Latitude = 40.6622728M, Longitude = -73.7904521M, CleanlinessRate = 9.6M, ComfortRate = 8.9M, LocationRate = 8.6M, FacilityRate = 8.5M, MaxRoomsInReservation = 0, OverallScore = 8.8M, StaffRate = 9.3M, ValueRate = 9.1M, NumberOfReviews = 566, PaymentCurrencyId =1, GeographicBoundaryId = 10, SustainableBadge  = true, Promoted = false, Stars = 2, Description = @"<div><p>Located 1.6 km from John F. Kennedy International Airport, The JFK Inn in Jamaica offers guests complimentary WiFi and free airport transfer to and from the JFK Airport Federal Circle.</p><p>Every room at the JFK Inn comes with a flat-screen TV, a desk  and an en-suite bathroom. Coffee makers are also provided in the rooms.</p><p>A complimentary buffet breakfast is served every morning at this hotel. Guests at the JFK Inn can enjoy the convenience of having vending machines and an ATM onsite. A business centre with fax and photocopying services is also available.</p><p>LaGuardia Airport and Citi Field are 16 km away from the JFK Inn. The Queens Botanical Garden is 12.7 km away while Rockaway Beach is 15 km away. </p></div>" },
            };
            context.Hotels.AddRange(hotels);
        });
        var hotelFacilities = new List<HotelFacility>();
        var rnd = new Random();
        foreach (var facility in facilities.Where(x =>
                     x.FacilityType != FacilityTypeEnum.Accommodation && x.FacilityType != FacilityTypeEnum.Recommend &&
                     x.FacilityType != FacilityTypeEnum.Outdoors && x.FacilityType != FacilityTypeEnum.Pricing &&
                     x.FacilityType != FacilityTypeEnum.Floor && x.FacilityType != FacilityTypeEnum.Dining))
        {
            if (hotelFacilities.All(x => x.FacilityId != facility.Id && x.HotelId == 1))
            {
                hotelFacilities.Add(new HotelFacility()
                    { HotelId = 1, FacilityId = facility.Id, ExtraChargeRequired = rnd.Next(0, 10) % 4 == 0 });
            }

            if (hotelFacilities.All(x => x.FacilityId != facility.Id && x.HotelId == 2))
            {
                if (rnd.Next(0, 10) < 8)
                {
                    hotelFacilities.Add(new HotelFacility()
                        { HotelId = 2, FacilityId = facility.Id, ExtraChargeRequired = rnd.Next(0, 10) % 4 == 0 });
                }
            }

            if (hotelFacilities.All(x => x.FacilityId != facility.Id && x.HotelId == 3))
            {
                if (rnd.Next(0, 10) < 4)
                {
                    hotelFacilities.Add(new HotelFacility()
                        { HotelId = 3, FacilityId = facility.Id, ExtraChargeRequired = rnd.Next(0, 10) % 4 == 0 });
                }
            }
        }

        var defaults = new HotelFacility[]
        {
            new HotelFacility() { HotelId = 1, FacilityId = 87, SearchMatchAdult = 2, SearchMatchChild = 0 },
            new HotelFacility() { HotelId = 1, FacilityId = 88, SearchMatchAdult = 2, SearchMatchChild = 0 },
            new HotelFacility() { HotelId = 1, FacilityId = 93, SearchMatchAdult = 1, SearchMatchChild = 0 },
            new HotelFacility() { HotelId = 1, FacilityId = 94, SearchMatchAdult = 1, SearchMatchChild = 0 },
            new HotelFacility() { HotelId = 3, FacilityId = 93, SearchMatchAdult = 1, SearchMatchChild = 0 },
            new HotelFacility() { HotelId = 3, FacilityId = 94, SearchMatchAdult = 1, SearchMatchChild = 0 },
            new HotelFacility() { HotelId = 2, FacilityId = 89, SearchMatchAdult = 1, SearchMatchChild = 0 },
            new HotelFacility() { HotelId = 2, FacilityId = 89, SearchMatchAdult = 2, SearchMatchChild = 0 },
            new HotelFacility() { HotelId = 2, FacilityId = 90, SearchMatchAdult = 2, SearchMatchChild = 1 },
            new HotelFacility() { HotelId = 2, FacilityId = 90, SearchMatchAdult = 2, SearchMatchChild = 2 },
            new HotelFacility() { HotelId = 2, FacilityId = 90, SearchMatchAdult = 2, SearchMatchChild = 3 }

        };
        hotelFacilities.AddRange(defaults.Where(x =>
            !hotelFacilities.Any(y => y.FacilityId == x.FacilityId && y.HotelId == x.HotelId)));

        context.HotelFacilities.AddRange(hotelFacilities);
        var rooms = new HotelRoomPrice[]
        {
            new HotelRoomPrice(){ Id=1, HotelId = 1, LeftCount = 4, TotalCount = 5, PreviousPrice = 6800, Price = 6604, CheckIn = TestCheckIn, CheckOut = TestCheckOut, RoomName  = "Grand Deluxe Room", SqMeter = 25, PayBackCredit = 500},
            new HotelRoomPrice(){ Id=2, HotelId = 1, LeftCount = 1, TotalCount = 3, PreviousPrice = 7900, Price = 7501, CheckIn = TestCheckIn, CheckOut = TestCheckOut, RoomName  = "Executive Grand Deluxe Room - Lounge Access including Afternoon Tea, Evening Cocktail Hours, Soft Refreshments & Canapes", SqMeter = 35, PayBackCredit = 800},
            new HotelRoomPrice(){ Id=3, HotelId = 1, LeftCount = 3, TotalCount = 3, PreviousPrice = 8200, Price = 8000, CheckIn = TestCheckIn, CheckOut = TestCheckOut, RoomName  = "Executive UAlls Grand Deluxe Room - UAlls , Soft Refreshments & Canapes", SqMeter = 35, PayBackCredit = 1060},
            new HotelRoomPrice(){ Id=4, HotelId = 2, LeftCount = 1, TotalCount = 3, PreviousPrice = 4500, Price = 4100, CheckIn = TestCheckIn, CheckOut = TestCheckOut, RoomName  = "Family Room", SqMeter = 18},
            new HotelRoomPrice(){ Id=5, HotelId = 2, LeftCount = 5, TotalCount = 6, PreviousPrice = 5200, Price = 4810, CheckIn = TestCheckIn, CheckOut = TestCheckOut, RoomName  = "Special Family Room", SqMeter = 28},
            new HotelRoomPrice(){ Id=6, HotelId = 3, LeftCount = 4, TotalCount = 4, PreviousPrice = 3999, Price = 3500, PriceTax = 93, CheckIn = TestCheckIn, CheckOut = TestCheckOut, RoomName  = "Couple with tiny one Room", SqMeter = 23},
            new HotelRoomPrice(){ Id=7, HotelId = 3, LeftCount = 2, TotalCount = 4, PreviousPrice = 4999, Price = 4000, PriceTax = 100, CheckIn = TestCheckIn, CheckOut = TestCheckOut, RoomName  = "King, Queen and Princess Room", SqMeter = 36}
        };

        SetIdentityInsert(typeof(HotelRoomPrice), context, () =>
        {
            context.HotelRoomPrices.AddRange(rooms);

        });
        SetIdentityInsert(typeof(HotelRoomPrice), context, () =>
        {
            var discounts = new DiscountOnCount[]
            {
                new DiscountOnCount() { Count = 2, DiscountPercent = 5, RoomId  = 1 },
                new DiscountOnCount() { Count = 3, DiscountPercent = 10, RoomId  = 1 },
                new DiscountOnCount() { Count = 2, DiscountPercent = 15, RoomId  = 3 }
            };
            context.Discounts.AddRange(discounts);
        });


        var sleeps = new Sleep[]
        {
            new Sleep(){ Count = 1, Type = SleepTypeEnum.SingleBed, RoomId = 1},
            new Sleep(){ Count = 1, Type = SleepTypeEnum.DoubleBed, RoomId = 1},
            new Sleep(){ Count = 1, Type = SleepTypeEnum.KingDouble, RoomId = 2},
            new Sleep(){ Count = 1, Type = SleepTypeEnum.DoubleBed, RoomId = 2},
            new Sleep(){ Count = 1, Type = SleepTypeEnum.KingDouble, RoomId = 3},
            new Sleep(){ Count = 1, Type = SleepTypeEnum.DoubleBed, RoomId = 3},
            new Sleep(){ Count = 1, Type = SleepTypeEnum.SingleBed, RoomId = 4},
            new Sleep(){ Count = 1, Type = SleepTypeEnum.DoubleBed, RoomId = 4},
            new Sleep(){ Count = 1, Type = SleepTypeEnum.SingleBed, RoomId = 5},
            new Sleep(){ Count = 1, Type = SleepTypeEnum.QueenDouble, RoomId = 5},
            new Sleep(){ Count = 1, Type = SleepTypeEnum.SingleBed, RoomId = 6},
            new Sleep(){ Count = 1, Type = SleepTypeEnum.DoubleBed, RoomId = 6},
            new Sleep(){ Count = 1, Type = SleepTypeEnum.DoubleBed, RoomId = 7},
            new Sleep(){ Count = 1, Type = SleepTypeEnum.KingDouble, RoomId = 7}
        };
        context.Sleeps.AddRange(sleeps);

        var cancellations = new CancellationPolicy[]
        {
            new CancellationPolicy(){ RoomId = 1, DayBeforeCheckOut = 7, TimeBeforeCheckOut = TimeSpan.FromHours(12)},
            new CancellationPolicy(){ RoomId = 1, DayBeforeCheckOut = 2, TimeBeforeCheckOut = TimeSpan.FromHours(12), CashCharge = 200},
            new CancellationPolicy(){ RoomId = 1, DayBeforeCheckOut = 0, TimeBeforeCheckOut = TimeSpan.FromHours(12), PercentCharge = 60},
            new CancellationPolicy(){ RoomId = 2, DayBeforeCheckOut = 7, TimeBeforeCheckOut = TimeSpan.FromHours(12), CashCharge = 50},
            new CancellationPolicy(){ RoomId = 2, DayBeforeCheckOut = 2, TimeBeforeCheckOut = TimeSpan.FromHours(12), PercentCharge = 30},
            new CancellationPolicy(){ RoomId = 2, DayBeforeCheckOut = 0, TimeBeforeCheckOut = TimeSpan.FromHours(12), PercentCharge = 60},
            new CancellationPolicy(){ RoomId = 3, DayBeforeCheckOut = 7, TimeBeforeCheckOut = TimeSpan.FromHours(12), PercentCharge = 60},
            new CancellationPolicy(){ RoomId = 4, DayBeforeCheckOut = 5, TimeBeforeCheckOut = TimeSpan.FromHours(12), CashCharge = 200},
            new CancellationPolicy(){ RoomId = 4, DayBeforeCheckOut = 0, TimeBeforeCheckOut = TimeSpan.FromHours(12), PercentCharge = 50},
            new CancellationPolicy(){ RoomId = 5, DayBeforeCheckOut = 5, TimeBeforeCheckOut = TimeSpan.FromHours(12), CashCharge = 100},
            new CancellationPolicy(){ RoomId = 5, DayBeforeCheckOut = 0, TimeBeforeCheckOut = TimeSpan.FromHours(12), PercentCharge = 40},
            new CancellationPolicy(){ RoomId = 6, DayBeforeCheckOut = 5, TimeBeforeCheckOut = TimeSpan.FromHours(12), CashCharge = 100},
            new CancellationPolicy(){ RoomId = 6, DayBeforeCheckOut = 0, TimeBeforeCheckOut = TimeSpan.FromHours(12), PercentCharge = 40},
        };
        context.CancellationPolicies.AddRange(cancellations);

        var roomFacilities = new List<RoomFacility>()
        {
            new RoomFacility(){ RoomId = 1, FacilityId = 1},
            new RoomFacility(){ RoomId = 1, FacilityId = 95, ExtraCharge = 10},
            new RoomFacility(){ RoomId = 1, FacilityId = 49},
            new RoomFacility(){ RoomId = 1, FacilityId = 73, FromDate = DateTime.Today.AddDays(-1), ToDate = DateTime.Today.AddDays(2)},
            new RoomFacility(){ RoomId = 2, FacilityId = 3},
            new RoomFacility(){ RoomId = 2, FacilityId = 49},
            new RoomFacility(){ RoomId = 2, FacilityId = 73, FromDate = DateTime.Today.AddDays(-3), ToDate = DateTime.Today.AddDays(1)},
            new RoomFacility(){ RoomId = 3, FacilityId = 4},
            new RoomFacility(){ RoomId = 3, FacilityId = 49},
            new RoomFacility(){ RoomId = 3, FacilityId = 73, FromDate = DateTime.Today.AddDays(-1), ToDate = DateTime.Today.AddDays(2)},
            new RoomFacility(){ RoomId = 4, FacilityId = 76, FromDate = DateTime.Today.AddDays(-1), ToDate = DateTime.Today.AddDays(2)},
            new RoomFacility(){ RoomId = 5, FacilityId = 45},
            new RoomFacility(){ RoomId = 5, FacilityId = 51},
            new RoomFacility(){ RoomId = 5, FacilityId = 76, FromDate = DateTime.Today.AddDays(-1), ToDate = DateTime.Today.AddDays(2)},
            
            new RoomFacility(){ RoomId = 6, FacilityId = 2},
            new RoomFacility(){ RoomId = 6, FacilityId = 35},

            new RoomFacility(){ RoomId = 7, FacilityId = 2},
            new RoomFacility(){ RoomId = 7, FacilityId = 35},
        };
        foreach (var facility in facilities.Where(x =>
                     x.FacilityType == FacilityTypeEnum.Accommodation || x.FacilityType == FacilityTypeEnum.Comfort ||
                     x.FacilityType == FacilityTypeEnum.Outdoors))
        {
            foreach (var room in rooms)
            {

                if (!roomFacilities.Any(x => x.RoomId == room.Id && facility.Id == x.FacilityId))
                {
                    if (rnd.Next(0, 10) < 4)
                    {
                        roomFacilities.Add(new RoomFacility()
                            { RoomId = room.Id, FacilityId = facility.Id });
                    }
                }
            }
        }

        context.RoomFacilities.AddRange(roomFacilities);
        context.SaveChanges();

            
    }

    private static void SetIdentityInsert(Type type, BookingContext context, Action act)
    {
        context.SaveChanges();
        using (var transaction = context.Database.BeginTransaction())
        {
            var entityType = context.Model.FindEntityType(type);
            var schema = entityType.GetSchema();
            var raw =
                $"SET IDENTITY_INSERT {schema}{(string.IsNullOrWhiteSpace(schema) ? "" : ".")}{entityType.GetTableName()} ";
            context.Database.ExecuteSqlRaw(raw + "ON");
            act();
            context.SaveChanges();
            context.Database.ExecuteSqlRaw(raw + "OFF");
            transaction.Commit();
        }
    }
}