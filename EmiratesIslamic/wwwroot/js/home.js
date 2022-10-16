$(document).ready(function () {

    $(".currency_dd").on("change", function () {
        const currencySelected = $(this).val();
        const aedConversion = $("#currency_" + currencySelected).attr("data-buy");
        const aedConversionRound = Math.round(aedConversion * 1000) / 1000;

        const getAedToOtherConversion = $("#currency_" + currencySelected).attr("data-sell");
        const aedToOtherConversion = 1 / getAedToOtherConversion;
        const aedToOtherConversionRound = Math.round(aedToOtherConversion * 1000) / 1000;
        $("#AED_Conversion").text(aedConversionRound);
        $("#AED_to_OtherConversion").text(aedToOtherConversionRound);
        $("#currency_selected,#currency_choosen").text(currencySelected);
        $(".flag").attr("src", "/images/flags/" + currencySelected + ".png");
    });

    $('.toggle_products').click(function () {
        $('html, body').animate({ scrollTop: $("#product_content").position().top }, "slow");
        $("#arrow_products").toggleClass("fa-angle-double-down fa-angle-double-up");
    });

    $('#curr_1 option[value=USD]').attr("selected", "selected");

    $(".currency_dd").select2({
        templateResult: formatState,
        templateSelection: formatState
    });

    //this script is to load the currency on load before changing dropdown values
    const getUsdConversionRate = $("#currency_USD").attr("data-buy");
    const getUsdConversionRateSell = $("#currency_USD").attr("data-sell");
    const usDconversionRound = Math.round(getUsdConversionRate * 1000) / 1000;
    var aeDconversionRound = 1 / getUsdConversionRateSell;
    aeDconversionRound = Math.round(aeDconversionRound * 1000) / 1000;
    $("#AED_Conversion").text(usDconversionRound);
    $("#AED_to_OtherConversion").text(aeDconversionRound);
    $("#currency_selected,#currency_choosen").text("USD");

    $(".flag").attr("src", "/images/flags/USD.png");

});