

var EmployeeSearchId = new Array();


$(document).on('click', '.panel-heading span.clickable', function (e) {
    var $this = $(this);
    if (!$this.hasClass('panel-collapsed')) {
        $this.parents('.panel').find('.panel-body').slideUp();
        $this.addClass('panel-collapsed');
        $this.find('i').removeClass('glyphicon-chevron-up').addClass('glyphicon-chevron-down');
    } else {
        $this.parents('.panel').find('.panel-body').slideDown();
        $this.removeClass('panel-collapsed');
        $this.find('i').removeClass('glyphicon-chevron-down').addClass('glyphicon-chevron-up');
    }
});

//$(document).ready(function () {
//    //$('.panel-heading span.clickable').click();
//    $('.panel div.clickable').click();
//});



$(document).ready(function () {
    $('.filterable .btn-filter').click(function () {
        var $panel = $(this).parents('.filterable'),
        $filters = $panel.find('.filters input'),
        $tbody = $panel.find('.table tbody');
        if ($filters.prop('disabled') == true) {
            $filters.prop('disabled', false);
            $filters.first().focus();
        } else {
            $filters.val('').prop('disabled', true);
            $tbody.find('.no-result').remove();
            $tbody.find('tr').show();
        }
    });

    $('.filterable .filters input').keyup(function (e) {
        /* Ignore tab key */
        var code = e.keyCode || e.which;
        if (code == '9') return;
        /* Useful DOM data and selectors */
        var $input = $(this),
        inputContent = $input.val().toLowerCase(),
        $panel = $input.parents('.filterable'),
        column = $panel.find('.filters th').index($input.parents('th')),
        $table = $panel.find('.table'),
        $rows = $table.find('tbody tr');
        /* Dirtiest filter function ever ;) */
        var $filteredRows = $rows.filter(function () {
            var value = $(this).find('td').eq(column).text().toLowerCase();
            return value.indexOf(inputContent) === -1;
        });
        /* Clean previous no-result if exist */
        $table.find('tbody .no-result').remove();
        /* Show all rows, hide filtered ones (never do that outside of a demo ! xD) */
        $rows.show();
        $filteredRows.hide();
        /* Prepend no-result row if all rows are filtered */
        if ($filteredRows.length === $rows.length) {
            $table.find('tbody').prepend($('<tr class="no-result text-center"><td colspan="' + $table.find('.filters th').length + '">No result found</td></tr>'));
        }
    });
});





function Messages(sMsg, sType) {
    toastr.options.positionClass = "toast-top-center";
    if (sType == "Success") {
        toastr.success(sMsg);
    }
    else if (sType == "Failure") {
        toastr.error(sMsg);
    }
    else if (sType == "info") {
        toastr.info(sMsg);
    }
    else {
        toastr.warning(sMsg);
    }

}



//$(window).load(function () {
//    $('#status').fadeOut();
//    $('#preloader').delay(350).fadeOut('slow');
//});




$(function () {
    var url = window.location.pathname,
    urlRegExp = new RegExp(url.replace(/\/$/, '') + "$");
    $('a.list-group-item').each(function () {
        if (urlRegExp.test(this.href.replace(/\/$/, ''))) {
            var CollapsName = '';
            if (this.id.substring(0, 7) == 'Setting') {
                CollapsName = '#collapseSetting';
            }
            else if (this.id.substring(0, 6) == 'Report') {
                CollapsName = '#collapseReport';
            }
            else if (this.id.substring(0, 6) == 'Orders') {
                CollapsName = '#collapseOrders';
            }
            else if (this.id.substring(0, 8) == 'Customer') {
                CollapsName = '#collapseCustomer';
            }
            else {
                CollapsName = '#collapseHome';
            }
            $(CollapsName).collapse('show');
            $(this).addClass('active');
        }
    });
});