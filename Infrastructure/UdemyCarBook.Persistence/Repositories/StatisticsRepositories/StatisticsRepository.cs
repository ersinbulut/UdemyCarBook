using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Interfaces.StatisticsInterfaces;
using UdemyCarBook.Persistence.Context;

namespace UdemyCarBook.Persistence.Repositories.StatisticsRepositories
{
    public class StatisticsRepository : IStatisticsRepository
    {
        private readonly CarBookContext _context;
        public StatisticsRepository(CarBookContext context)
        {
            _context = context;
        }
        /// <summary>
        /// En çok yoruma sahip blogun başlığını döndürür.
        /// </summary>
        /// SQL: SELECT TOP(1) BlogID, COUNT(*) AS 'Sayi' FROM Comments GROUP BY BlogID ORDER BY Sayi DESC
        public string GetBlogTitleByMaxBlogComment()
        {
            var values = _context.Comments.GroupBy(x => x.BlogID)
                           .Select(y => new { BlogID = y.Key, Count = y.Count() })
                           .OrderByDescending(z => z.Count).Take(1).FirstOrDefault();

            string blogName = _context.Blogs.Where(x => x.BlogID == values.BlogID)
                                  .Select(y => y.Title).FirstOrDefault();
            return blogName;
        }

        /// <summary>
        /// En fazla araca sahip markanın ismini döndürür.
        /// </summary>
        /// SQL: SELECT TOP(1) BrandID, COUNT(*) AS 'ToplamArac' FROM Cars GROUP BY BrandID ORDER BY ToplamArac DESC
        public string GetBrandNameByMaxCar()
        {
            var values = _context.Cars.GroupBy(x => x.BrandID)
                           .Select(y => new { BrandID = y.Key, Count = y.Count() })
                           .OrderByDescending(z => z.Count).Take(1).FirstOrDefault();

            string brandName = _context.Brands.Where(x => x.BrandID == values.BrandID)
                                    .Select(y => y.Name).FirstOrDefault();
            return brandName;
        }

        /// <summary>
        /// Toplam yazar sayısını döndürür.
        /// </summary>
        /// SQL: SELECT COUNT(*) FROM Authors
        public int GetAuthorCount()
        {
            return _context.Authors.Count();
        }

        /// <summary>
        /// Araçların günlük ortalama kira fiyatını döndürür.
        /// </summary>
        /// SQL: SELECT AVG(Amount) FROM CarPricings WHERE PricingID = (SELECT PricingID FROM Pricings WHERE Name = 'Günlük')
        public decimal GetAvgRentPriceForDaily()
        {
            int id = _context.Pricings.Where(y => y.Name == "Günlük").Select(z => z.PricingID).FirstOrDefault();
            return _context.CarPricings.Where(w => w.PricingID == id).Average(x => x.Amount);
        }

        /// <summary>
        /// Araçların aylık ortalama kira fiyatını döndürür.
        /// </summary>
        /// SQL: SELECT AVG(Amount) FROM CarPricings WHERE PricingID = (SELECT PricingID FROM Pricings WHERE Name = 'Aylık')
        public decimal GetAvgRentPriceForMonthly()
        {
            int id = _context.Pricings.Where(y => y.Name == "Aylık").Select(z => z.PricingID).FirstOrDefault();
            return _context.CarPricings.Where(w => w.PricingID == id).Average(x => x.Amount);
        }

        /// <summary>
        /// Araçların haftalık ortalama kira fiyatını döndürür.
        /// </summary>
        /// SQL: SELECT AVG(Amount) FROM CarPricings WHERE PricingID = (SELECT PricingID FROM Pricings WHERE Name = 'Haftalık')
        public decimal GetAvgRentPriceForWeekly()
        {
            int id = _context.Pricings.Where(y => y.Name == "Haftalık").Select(z => z.PricingID).FirstOrDefault();
            return _context.CarPricings.Where(w => w.PricingID == id).Average(x => x.Amount);
        }

        /// <summary>
        /// Toplam blog sayısını döndürür.
        /// </summary>
        /// SQL: SELECT COUNT(*) FROM Blogs
        public int GetBlogCount()
        {
            return _context.Blogs.Count();
        }

        /// <summary>
        /// Toplam marka sayısını döndürür.
        /// </summary>
        /// SQL: SELECT COUNT(*) FROM Brands
        public int GetBrandCount()
        {
            return _context.Brands.Count();
        }

        /// <summary>
        /// Günlük kira fiyatı en yüksek olan aracın marka ve modelini döndürür.
        /// </summary>
        /// SQL: SELECT * FROM CarPricings WHERE Amount = (SELECT MAX(Amount) FROM CarPricings WHERE PricingID = 3)
        public string GetCarBrandAndModelByRentPriceDailyMax()
        {
            int pricingID = _context.Pricings.Where(x => x.Name == "Günlük").Select(y => y.PricingID).FirstOrDefault();
            decimal amount = _context.CarPricings.Where(y => y.PricingID == pricingID).Max(x => x.Amount);
            int carId = _context.CarPricings.Where(x => x.Amount == amount).Select(y => y.CarID).FirstOrDefault();
            string brandModel = _context.Cars.Where(x => x.CarID == carId).Include(y => y.Brand).Select(z => z.Brand.Name + " " + z.Model).FirstOrDefault();
            return brandModel;
        }

        /// <summary>
        /// Günlük kira fiyatı en düşük olan aracın marka ve modelini döndürür.
        /// </summary>
        /// SQL: SELECT * FROM CarPricings WHERE Amount = (SELECT MIN(Amount) FROM CarPricings WHERE PricingID = 3)
        public string GetCarBrandAndModelByRentPriceDailyMin()
        {
            int pricingID = _context.Pricings.Where(x => x.Name == "Günlük").Select(y => y.PricingID).FirstOrDefault();
            decimal amount = _context.CarPricings.Where(y => y.PricingID == pricingID).Min(x => x.Amount);
            int carId = _context.CarPricings.Where(x => x.Amount == amount).Select(y => y.CarID).FirstOrDefault();
            string brandModel = _context.Cars.Where(x => x.CarID == carId).Include(y => y.Brand).Select(z => z.Brand.Name + " " + z.Model).FirstOrDefault();
            return brandModel;
        }

        /// <summary>
        /// Toplam araç sayısını döndürür.
        /// </summary>
        /// SQL: SELECT COUNT(*) FROM Cars
        public int GetCarCount()
        {
            return _context.Cars.Count();
        }

        /// <summary>
        /// Elektrikli yakıt tipine sahip araçların sayısını döndürür.
        /// </summary>
        /// SQL: SELECT COUNT(*) FROM Cars WHERE Fuel = 'Elektrik'
        public int GetCarCountByFuelElectric()
        {
            return _context.Cars.Where(x => x.Fuel == "Elektrik").Count();
        }

        /// <summary>
        /// Benzinli veya dizel araçların sayısını döndürür.
        /// </summary>
        /// SQL: SELECT COUNT(*) FROM Cars WHERE Fuel = 'Benzin' OR Fuel = 'Dizel'
        public int GetCarCountByFuelGasolineOrDiesel()
        {
            return _context.Cars.Where(x => x.Fuel == "Benzin" || x.Fuel == "Dizel").Count();
        }

        /// <summary>
        /// 1000 km'den az yol yapmış araçların sayısını döndürür.
        /// </summary>
        /// SQL: SELECT COUNT(*) FROM Cars WHERE Km <= 1000
        public int GetCarCountByKmSmallerThen1000()
        {
            return _context.Cars.Where(x => x.Km <= 1000).Count();
        }

        /// <summary>
        /// Otomatik vitese sahip araçların sayısını döndürür.
        /// </summary>
        /// SQL: SELECT COUNT(*) FROM Cars WHERE Transmission = 'Otomatik'
        public int GetCarCountByTranmissionIsAuto()
        {
            return _context.Cars.Where(x => x.Transmission == "Otomatik").Count();
        }

        /// <summary>
        /// Toplam lokasyon sayısını döndürür.
        /// </summary>
        /// SQL: SELECT COUNT(*) FROM Locations
        public int GetLocationCount()
        {
            return _context.Locations.Count();
        }

    }
}
