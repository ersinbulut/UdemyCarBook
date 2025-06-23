using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyCarBook.Application.Interfaces.StatisticsInterfaces
{
    public interface IStatisticsRepository
    {
        /// <summary>
        /// Toplam araç sayısını döndürür.
        /// </summary>
        int GetCarCount();

        /// <summary>
        /// Toplam lokasyon sayısını döndürür.
        /// </summary>
        int GetLocationCount();

        /// <summary>
        /// Toplam yazar sayısını döndürür.
        /// </summary>
        int GetAuthorCount();

        /// <summary>
        /// Toplam blog sayısını döndürür.
        /// </summary>
        int GetBlogCount();

        /// <summary>
        /// Toplam marka sayısını döndürür.
        /// </summary>
        int GetBrandCount();

        /// <summary>
        /// Araçların günlük ortalama kira fiyatını döndürür.
        /// </summary>
        decimal GetAvgRentPriceForDaily();

        /// <summary>
        /// Araçların haftalık ortalama kira fiyatını döndürür.
        /// </summary>
        decimal GetAvgRentPriceForWeekly();

        /// <summary>
        /// Araçların aylık ortalama kira fiyatını döndürür.
        /// </summary>
        decimal GetAvgRentPriceForMonthly();

        /// <summary>
        /// Otomatik vitesli araçların sayısını döndürür.
        /// </summary>
        int GetCarCountByTranmissionIsAuto();

        /// <summary>
        /// En fazla araca sahip markanın adını döndürür.
        /// </summary>
        string GetBrandNameByMaxCar();

        /// <summary>
        /// En fazla yoruma sahip blog başlığını döndürür.
        /// </summary>
        string GetBlogTitleByMaxBlogComment();

        /// <summary>
        /// 1000 kilometreden daha az yol yapmış araçların sayısını döndürür.
        /// </summary>
        int GetCarCountByKmSmallerThen1000();

        /// <summary>
        /// Benzinli veya dizel yakıtlı araçların sayısını döndürür.
        /// </summary>
        int GetCarCountByFuelGasolineOrDiesel();

        /// <summary>
        /// Elektrikli araçların sayısını döndürür.
        /// </summary>
        int GetCarCountByFuelElectric();

        /// <summary>
        /// Günlük kira fiyatı en yüksek olan aracın marka ve modelini döndürür.
        /// </summary>
        string GetCarBrandAndModelByRentPriceDailyMax();

        /// <summary>
        /// Günlük kira fiyatı en düşük olan aracın marka ve modelini döndürür.
        /// </summary>
        string GetCarBrandAndModelByRentPriceDailyMin();

    }
}
