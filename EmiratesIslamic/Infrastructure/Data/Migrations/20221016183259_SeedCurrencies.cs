using Microsoft.EntityFrameworkCore.Migrations;

namespace EmiratesIslamic.Infrastructure.Data.Migrations;

public partial class SeedCurrencies : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql("" +
                             "INSERT INTO Currencies (Code, Name, Buy, Sell, IsAvailable) VALUES " +
                             "('AUD', 'AUD - Australian Dollar', 2.30235, 2.50523, 'true'), " +
                             "('BDT', 'BDT - Bangladeshi Taka', 0.03508, 0.03648, 'true'), " +
                             "('BHD', 'BHD - Bahraini Dinar', 9.58368, 9.91087, 'true'), " +
                             "('CAD', 'CAD - Canadian Dollar', 2.63677, 2.76682, 'true'), " +
                             "('CHF', 'CHF - Swiss Franc', 3.65563, 3.86812, 'true'), " +
                             "('DKK', 'DKK - Danish Krone', 0.4727, 0.49166, 'true'), " +
                             "('EGP', 'EGP - Egyptian Pound', 0.18598, 0.19005, 'true'), " +
                             "('EUR', 'EUR - Euro', 3.43682, 3.71965, 'true'), " +
                             "('GBP', 'GBP - British Pound', 3.85943, 4.16828, 'true'), " +
                             "('HKD', 'HKD - Hong Kong Dollar', 0.45979, 0.47872, 'true'), " +
                             "('INR', 'INR - Indian Rupee', 0.04407, 0.04546, 'true'), " +
                             "('IQD', 'IQD - Iraqi Dinar', 0.002485, 0.00254, 'true'), " +
                             "('JOD', 'JOD - Jordanian Dinar', 5.1156, 5.23095, 'true'), " +
                             "('JPY', 'JPY - Japan Yen', 0.02489, 0.02623, 'true'), " +
                             "('KWD', 'KWD - Kuwaiti Dinar', 11.677609, 12.038924, 'true'), " +
                             "('LKR', 'LKR - Srilankan Rupee', 0.00988, 0.01013, 'true'), " +
                             "('MAD', 'MAD - Moroccan Dirham', 0.32568, 0.33959, 'true'), " +
                             "('MYR', 'MYR - Malaysian Ringgit', 0.78636, 0.79907, 'true'), " +
                             "('NOK', 'NOK - Norwegian Krone', 0.33838, 0.35086, 'true'), " +
                             "('NZD', 'NZD - New Zealand Dollar', 2.03019, 2.20771, 'true'), " +
                             "('OMR', 'OMR - Omani Rial', 9.42204, 9.65134, 'true'), " +
                             "('PHP', 'PHP - Philippine Peso', 0.06147, 0.06313, 'true'), " +
                             "('PKR', 'PKR - Pakistan Rupee', 0.01557, 0.01615, 'true'), " +
                             "('QAR', 'QAR - Qatari Rial', 0.96229, 1.03061, 'true'), " +
                             "('SAR', 'SAR - Saudi Riyal', 0.95958, 0.99382, 'true'), " +
                             "('SEK', 'SEK - Swedish Krona', 0.32231, 0.33544, 'true'), " +
                             "('SGD', 'SGD - Singapore Dollar', 2.49787, 2.64107, 'true'), " +
                             "('TRY', 'TRY - Turkish Lira', 0.19537, 0.20011, 'true'), " +
                             "('USD', 'USD - US Dollar', 3.653, 3.685, 'true'), " +
                             "('ZAR', 'ZAR - South African Rand', 0.20352, 0.21123, 'true')" +
                             "");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql("DELETE FROM Currencies");
    }
}