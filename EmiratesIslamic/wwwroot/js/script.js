$('#search_icon').click(function (event) {
    $('#frm_search').submit();
});

$('#frm_search').submit(function (event) {
    event.preventDefault();
    $('#search_content').html('');
    $('.freq_search').hide();
    $('#loader_gif').show();
    var getSearchValue = $('#txt_search').val();

    $.ajax({
        url: "/eng/cfc/search_web.cfc/?method=fun_getResults",
        type: 'post',
        data: { searchQuery: getSearchValue },
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        ////cache: false,
        //processData:false,
        success: function (data) {
            $('.freq_search').hide();
            $('#loader_gif').hide();
            $('#search_content').html('');
            $('#search_content').html(data);

            //offers_like();
            //alert(data);
        }
    });
});

function openSearchNav() {
    if (document.getElementById("SearchPanel").style.width == "" || document.getElementById("SearchPanel").style.width == "0px") {
        document.getElementById("SearchPanel").style.width = "100%";
        document.getElementById("SearchPanel").style.height = "100%";
        $('#search_content').html('');
        $('.freq_search').show();
        $('#txt_search').val('');
        $('html').css('overflow', 'hidden');
        document.getElementById("myLogin").style.display = "none";
    } else {
        document.getElementById("SearchPanel").style.width = "0";
        $('html').css('overflow', 'scroll');
    }
}

function closeSearchNav() {
    document.getElementById("SearchPanel").style.width = "0";
    document.getElementById("SearchPanel").style.height = "0";
    $('html').css('overflow', 'scroll');
}

function loginClick() {

    var x = document.getElementById("myLogin");

    document.getElementById("mySidepanel").style.width = "0";
    document.getElementById("mySidepanel").style.height = "0";

    if (x.style.display === "none" || x.style.display === "")
        x.style.display = "block";
    else
        x.style.display = "none";
}

function openNav() {
    if (document.getElementById("mySidepanel").style.width == "" || document.getElementById("mySidepanel").style.width == "0px") {
        document.getElementById("mySidepanel").style.width = "100%";
        document.getElementById("mySidepanel").style.height = "100%";
        document.getElementById("myLogin").style.display = "none";
    } else
        document.getElementById("mySidepanel").style.width = "0";
}

function closeNav() {
    document.getElementById("mySidepanel").style.width = "0";
    document.getElementById("mySidepanel").style.height = "0";
}

function formatState(state) {
    if (!state.id) { return state.text; }
    var $state = $('<span> ' + state.text + '</span>');
    return $state;
};