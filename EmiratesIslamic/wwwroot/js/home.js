$(document).ready(function () {

    $('.currency_dd').on('change', function () {
        var currency_selected = $(this).val();
        var aed_conversion = $('#currency_' + currency_selected).attr("data-buy");
        var aed_conversion_round = Math.round(aed_conversion * 1000) / 1000;

        var get_aed_to_OtherConversion = $('#currency_' + currency_selected).attr("data-sell");
        var aed_to_OtherConversion = 1 / get_aed_to_OtherConversion;
        var aed_to_OtherConversion_round = Math.round(aed_to_OtherConversion * 1000) / 1000;
        $('#AED_Conversion').text(aed_conversion_round);
        $('#AED_to_OtherConversion').text(aed_to_OtherConversion_round);
        $('#currency_selected,#currency_choosen').text(currency_selected);
        $('.flag').attr('src', '/images/flags/' + currency_selected + '.png');
    });

    $('.toggle_products').click(function () {
        $('html, body').animate({ scrollTop: $('#product_content').position().top }, 'slow');
        $('#arrow_products').toggleClass('fa-angle-double-down fa-angle-double-up');
    });

    document.querySelectorAll('#carousel-inner .carousel-item')[0].classList.add("active");

    class CurrencyRate {
        constructor(value, buy, sell, text) {
            this.value = value;
            this.buy = buy;
            this.sell = sell;
            this.text = text;
        }
    }

    const currenciesRate = [
        new CurrencyRate("AUD", 2.30235, 2.50523, "AUD - Australian Dollar"),
        new CurrencyRate("BDT", 0.03508, 0.03648, "BDT - Bangladeshi Taka"),
        new CurrencyRate("BHD", 9.58368, 9.91087, "BHD - Bahraini Dinar"),
        new CurrencyRate("CAD", 2.63677, 2.76682, "CAD - Canadian Dollar"),
        new CurrencyRate("CHF", 3.65563, 3.86812, "CHF - Swiss Franc"),
        new CurrencyRate("DKK", 0.4727, 0.49166, "DKK - Danish Krone"),
        new CurrencyRate("EGP", 0.18598, 0.19005, "EGP - Egyptian Pound"),
        new CurrencyRate("EUR", 3.43682, 3.71965, "EUR - Euro"),
        new CurrencyRate("GBP", 3.85943, 4.16828, "GBP - British Pound"),
        new CurrencyRate("HKD", 0.45979, 0.47872, "HKD - Hong Kong Dollar"),
        new CurrencyRate("INR", 0.04407, 0.04546, "INR - Indian Rupee"),
        new CurrencyRate("IQD", 0.002485, 0.00254, "IQD - Iraqi Dinar"),
        new CurrencyRate("JOD", 5.1156, 5.23095, "JOD - Jordanian Dinar"),
        new CurrencyRate("JPY", 0.02489, 0.02623, "JPY - Japan Yen"),
        new CurrencyRate("KWD", 11.677609, 12.038924, "KWD - Kuwaiti Dinar"),
        new CurrencyRate("LKR", 0.00988, 0.01013, "LKR - Srilankan Rupee"),
        new CurrencyRate("MAD", 0.32568, 0.33959, "MAD - Moroccan Dirham"),
        new CurrencyRate("MYR", 0.78636, 0.79907, "MYR - Malaysian Ringgit"),
        new CurrencyRate("NOK", 0.33838, 0.35086, "NOK - Norwegian Krone"),
        new CurrencyRate("NZD", 2.03019, 2.20771, "NZD - New Zealand Dollar"),
        new CurrencyRate("OMR", 9.42204, 9.65134, "OMR - Omani Rial"),
        new CurrencyRate("PHP", 0.06147, 0.06313, "PHP - Philippine Peso"),
        new CurrencyRate("PKR", 0.01557, 0.01615, "PKR - Pakistan Rupee"),
        new CurrencyRate("QAR", 0.96229, 1.03061, "QAR - Qatari Rial"),
        new CurrencyRate("SAR", 0.95958, 0.99382, "SAR - Saudi Riyal"),
        new CurrencyRate("SEK", 0.32231, 0.33544, "SEK - Swedish Krona"),
        new CurrencyRate("SGD", 2.49787, 2.64107, "SGD - Singapore Dollar"),
        new CurrencyRate("TRY", 0.19537, 0.20011, "TRY - Turkish Lira"),
        new CurrencyRate("USD", 3.653, 3.685, "USD - US Dollar"),
        new CurrencyRate("ZAR", 0.20352, 0.21123, "ZAR - South African Rand")
    ];

    currenciesRate.forEach((item) => {
        $("#curr_1").append(`
        <option value="${item.value}" data-buy="${item.buy}" 
            data-sell="${item.sell}" id="currency_${item.value}"
            flag="" curr_desc="">
            ${item.text}
        </option>
        `);
    });

    $('#curr_1 option[value=USD]').attr('selected', 'selected');

    $(".currency_dd").select2({
        templateResult: formatState,
        templateSelection: formatState
    });

    //this script is to load the currency on load before changing dropdown values
    var getUsdConversionRate = $('#currency_USD').attr("data-buy");
    var getUsdConversionRateSell = $('#currency_USD').attr("data-sell");
    var USDconversion_round = Math.round(getUsdConversionRate * 1000) / 1000;
    var AEDconversion_round = 1 / getUsdConversionRateSell;
    var AEDconversion_round = Math.round(AEDconversion_round * 1000) / 1000;
    $('#AED_Conversion').text(USDconversion_round);
    $('#AED_to_OtherConversion').text(AEDconversion_round);
    $('#currency_selected,#currency_choosen').text('USD');

    $('.flag').attr('src', '/images/flags/USD.png');

});