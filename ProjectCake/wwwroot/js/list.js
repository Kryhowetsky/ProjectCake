

$(function () {
    debugger;
    var $list = $('#list');
    var listUrl = $list.data('url');
    var listPage = 0;

    getList(listPage);

    $('#list-button-previous').click(function () {
        if (listPage < 0)
            return;

        listPage--;
        getList(listPage);
    });


    $('#list-button-next').click(function () {
        listPage++;
        getList(listPage);
    });

    function getList(page) {
        $.get(listUrl, { page })
            .done(function (data) {
                $list.html(data);
            });
    }
});