


$(function () {
    $("[id$=txtSearchRemove]").autocomplete({

        source: function (request, response) {
            //debugger
            $.ajax({
                type: "POST",
                url: "items.aspx/GetItemsNo",
                data: "{ 'prefix': '" + request.term + "'}",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    response($.map(data.d, function (item) {
                        return {
                            label: item,
                            val: item
                        }
                    }))
                },
                error: function (response) {
                    alert(response.responseText);
                },
                failure: function (response) {
                    alert(response.responseText);
                }
            });
        },
        select: function (e, i) {
            $("[id$=txtSearchRemove]").val(i.item.val);
        },
        minLength: 1
    }).each(function() {
        $(this).autocomplete("widget").insertAfter($("#txtSearchRemove").parent());});
});

$(function () {
    $("[id$=txtSearchKeep]").autocomplete({

        source: function (request, response) {
            //debugger
            $.ajax({
                type: "POST",
                url: "items.aspx/GetItemsNo",
                data: "{ 'prefix': '" + request.term + "'}",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    response($.map(data.d, function (item) {
                        return {
                            label: item,
                            val: item
                        }
                    }))
                },
                error: function (response) {
                    alert(response.responseText);
                },
                failure: function (response) {
                    alert(response.responseText);
                }
            });
        },
        select: function (e, i) {
            $("[id$=txtSearchKeep]").val(i.item.val);
        },
        minLength: 1
    }).each(function () {
        $(this).autocomplete("widget").insertAfter($("#txtSearchKeep").parent());
    });
});

$(function () {
    $("[id$=TextBox1]").autocomplete({

        source: function (request, response) {
            //debugger
            $.ajax({
                type: "POST",
                url: "items.aspx/GetItemsNo",
                data: "{ 'prefix': '" + request.term + "'}",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    response($.map(data.d, function (item) {
                        return {
                            label: item,
                            val: item
                        }
                    }))
                },
                error: function (response) {
                    alert(response.responseText);
                },
                failure: function (response) {
                    alert(response.responseText);
                }
            });
        },
        select: function (e, i) {
            $("[id$=TextBox1]").val(i.item.val);
        },
        minLength: 1
    }).each(function () {
        $(this).autocomplete("widget").insertAfter($("#TextBox1").parent());
    });
});