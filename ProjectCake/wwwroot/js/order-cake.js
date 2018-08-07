$('#order-cake-submit').click(function (event) {
    var $form = $("#order-cake-form");

    var name = $form.find("#Name").val();
    if (!name || name.length < 3) {
        $form.find("#name_error").show();
        return;
    } else
        $form.find("#name_error").hide();

    var surname = $form.find("#Surname").val();
    if (!surname || surname.length < 3) {
        $form.find("#surname_error").show();
        return;
    } else
        $form.find("#surname_error").hide();

    var phone = $form.find("#phone").val();
    var pattern_phone = /^((\+3|8|0)+([0-9]){9})$/.test(phone);
    if (!pattern_phone) {
        $form.find("#phone_error").show();
        return;
    } else
        $form.find("#phone_error").hide();

    var email = $form.find("#email").val();
    var pattern_email = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/.test(email);
    if (!pattern_email) {
        $form.find("#email_error").show();
        return;
    } else
        $form.find("#email_error").hide();

    var data = new FormData($form[0]);
    var url = $form.attr('action');
    //debugger;
    $.ajax({
        url: url,
        type: 'POST',
        data: data,
        async: false,
        cache: false,
        contentType: false,
        processData: false,
        success: function () {
            $('#order-button-modal').modal();
        },
    });
});