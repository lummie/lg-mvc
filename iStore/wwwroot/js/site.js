

function BindSubmitAjax(formId, success, failure) {
    var frm = $(formId);
    frm.submit(function (ev) {
        ev.preventDefault(); // stop submit button default action    

        // convert array of key values to json map object
        var data = {};
        formArray = frm.serializeArray()
        $(formArray).each(function (index, obj) {
            data[obj.name] = obj.value;
        });

        $.ajax({
            type: "POST",
            url: frm.attr('action'),
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: success,
            error: failure
        });
    });
}









