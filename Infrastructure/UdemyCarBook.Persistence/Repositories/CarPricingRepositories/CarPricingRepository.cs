﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Interfaces.CarPricingInterfaces;
using UdemyCarBook.Application.ViewModels;
using UdemyCarBook.Domain.Entities;
using UdemyCarBook.Persistence.Context;

namespace UdemyCarBook.Persistence.Repositories.CarPricingRepositories
{
    public class CarPricingRepository : ICarPricingRepository
    {
        // CarBookContext tipinde bir _context değişkeni tanımlanıyor, veritabanı işlemleri burada yapılır.
        private readonly CarBookContext _context;

        // Constructor: Repository sınıfı örneği oluşturulurken context dışarıdan verilir (dependency injection).
        public CarPricingRepository(CarBookContext context)
        {
            _context = context;
        }

        // Bu metot sadece PricingID'si 2 olan fiyatları, ilişkili araç ve marka bilgileri ile birlikte getirir.
        public List<CarPricing> GetCarPricingWithCars()
        {
            var values = _context.CarPricings
                .Include(x => x.Car) // Car tablosunu dahil eder
                    .ThenInclude(y => y.Brand) // Car üzerinden Brand (marka) ilişkisini dahil eder
                .Include(x => x.Pricing) // Pricing bilgisini de dahil eder
                .Where(z => z.PricingID == 2) // Sadece PricingID'si 2 olanları alır
                .ToList();
            return values;
        }

        // Bu metot henüz yazılmamış (NotImplementedException fırlatır).
        public List<CarPricing> GetCarPricingWithTimePeriod()
        {
            throw new NotImplementedException();
        }

        // Bu metot, farklı fiyatlandırma periyotlarına göre (örneğin: günlük, haftalık, aylık) araç fiyatlarını pivot tablo şeklinde getirir.
        public List<CarPricingViewModel> GetCarPricingWithTimePeriod1()
        {
            List<CarPricingViewModel> values = new List<CarPricingViewModel>();

            // SQL komutu tanımlanıyor, pivot tablo oluşturulacak.
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = @"
            Select * From 
            (
                Select Model, Name, CoverImageUrl, PricingID, Amount 
                From CarPricings 
                Inner Join Cars On Cars.CarID = CarPricings.CarId 
                Inner Join Brands On Brands.BrandID = Cars.BrandID
            ) As SourceTable 
            Pivot 
            (
                Sum(Amount) For PricingID In ([2],[3],[4])
            ) as PivotTable;
        ";
                command.CommandType = System.Data.CommandType.Text;

                // Veritabanı bağlantısı açılır
                _context.Database.OpenConnection();

                // Komut çalıştırılır ve sonuçlar okunur
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Her satır için ViewModel oluşturulur ve fiyatlar listeye eklenir
                        CarPricingViewModel carPricingViewModel = new CarPricingViewModel()
                        {
                            Brand = reader["Name"].ToString(), // Marka adı
                            Model = reader["Model"].ToString(), // Araç modeli
                            CoverImageUrl = reader["CoverImageUrl"].ToString(), // Araç görseli
                            Amounts = new List<decimal>
                    {
                        Convert.ToDecimal(reader["2"]), // Günlük fiyat
                        Convert.ToDecimal(reader["3"]), // Haftalık fiyat
                        Convert.ToDecimal(reader["4"])  // Aylık fiyat
                    }
                        };
                        values.Add(carPricingViewModel);
                    }
                }

                // Veritabanı bağlantısı kapatılır
                _context.Database.CloseConnection();

                // Sonuçlar döndürülür
                return values;
            }
        }



    }
}



















/*
 var values = from x in _context.CarPricings
						 group x by x.PricingID into g
						 select new
						 {
							 CarId = g.Key,
							 DailyPrice = g.Where(y => y.CarPricingID == 2).Sum(z => z.Amount),
							 WeeklyPrice = g.Where(y => y.CarPricingID == 3).Sum(z => z.Amount),
							 MonthlyPrice = g.Where(y => y.CarPricingID == 4).Sum(z => z.Amount)
						 };
			return 0;
 */
/*
 public List<CarPricing> GetCarPricingWithTimePeriod()
		{
			//List<CarPricing> values = new List<CarPricing>();
			//using (var command = _context.Database.GetDbConnection().CreateCommand())
			//{
			//	command.CommandText = "Select * From (Select Model,PricingID,Amount From CarPricings Inner Join Cars On Cars.CarID=CarPricings.CarId Inner Join Brands On Brands.BrandID=Cars.BrandID) As SourceTable Pivot (Sum(Amount) For PricingID In ([2],[3],[4])) as PivotTable;";
			//	command.CommandType = System.Data.CommandType.Text;
			//	_context.Database.OpenConnection();
			//	using(var reader=command.ExecuteReader())
			//	{
			//		while (reader.Read())
			//		{
			//			CarPricing carPricing = new CarPricing();
			//			Enumerable.Range(1, 3).ToList().ForEach(x =>
			//			{
			//				if (DBNull.Value.Equals(reader[x]))
			//				{
			//					carPricing.
			//				}
			//				else
			//				{
			//					carPricing.Amount
			//				}
			//			});
			//			values.Add(carPricing);
			//		}
			//	}
			//	_context.Database.CloseConnection();
			//	return values;	
			}
		}
 */