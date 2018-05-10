$(function () {
    //#region Mortgage Calculator
    $('#btnMortgageCalculator').on('click', function (e) {
        e.preventDefault();
        if (!isFormValid())
            return;
        $.ajax({
            type: "GET",
            url: '/Home/MortgageCalculator',
            data: { Principle: $("#principleAmt").val(), TotalYears: $("#termsInyears").val(), Premium: $("#dpmt").val(), IntrestRate: $("#termsInyears").val() },
            success: function (data, textStatus, jqXHR) {
                $('#MortgageCalculatorContent').html(data);
            }
        });
    });
    var isFormValid = function validateForm() {
        if ($("#principleAmt").val() == "") {
            alert("Invalid principle amount");
            $("#principleAmt").focus();
            return false;
        }
        else if ($("#rateOfIntrest").val() == "") {
            alert("Invalid Interest rate per anum");
            $("#rateOfIntrest").focus();
            return false;
        }
        else if ($("#termsInyears").val() == "") {
            alert("Numbr of years");
            $("#termsInyears").focus();
            return false;
        }
        else if ($("#dpmt").val() == "") {
            alert("Premimum per month");
            $("#dpmt").focus();
            return false;
        }
        return true;

    }
    $("#MortageCalculatorLink").click(function () {
        $("#MortageCalculator").css('display', 'block');
        $("#activeMortgage").css('display', 'none');
    });
    //#endregion Mortgage Calculator


    //#region Active Mortgage
    $('#activeMortgageLink').on('click', function (e) {
        e.preventDefault();
        $("#MortageCalculator").css('display', 'none');
        $("#activeMortgage").css('display', 'block');
        $.ajax({
            type: "GET",
            url: '/Home/ActiveMortgages',
            success: function (data, textStatus, jqXHR) {
                $('#activeMortgageClient').html(data);
                loadActiveMortgage();
            }
        });

    });
    function loadActiveMortgage() {

        var table = $('#activemortgagetable').DataTable({
        });
        $('#activemortgagetable tbody').on('click', 'tr', function () {
            if ($(this).hasClass('selected')) {
                $(this).removeClass('selected');
            }
            else {
                table.$('tr.selected').removeClass('selected');
                $(this).addClass('selected');
            }
            var result = table.row(this).data();
            $("#lblMortgageType").text("Mortgage Type :" + result[0]);
            $("#rateOfIntrest").val(result[1]);
            $("#MortageCalculatorLink").trigger('click');
        });

        $('#button').click(function () {
            table.row('.selected').remove().draw(false);
        });
    }
    $('#doShorting').on('click', function (e) {
        e.preventDefault();
        var data = {
            shortBy: $('#shortby').val(),
            ordershortBy: $('#shortbyOrder').val(),
            thenBy: $('#thenshortbyOrder').val(),
            orderThenBy: $('#thenshortby').val()
        }
        $.ajax({
            type: "GET",
            url: '/Home/ActiveMortgages',
            data: data,
            //dataType: 'json',
            success: function (data, textStatus, jqXHR) {
                alert(data);
                $('#activeMortgageServerbind').html(data);
                $('#activeMortgageServerbind #activemortgagetable').attr('id', 'activemortgagetable2');
                $('#activemortgagetable2').attr('class', 'table');
                $('#activemortgagetable2 thead').attr('class', 'thead-dark');
                $('#activemortgagetable2 tfoot').attr('class', 'thead-dark');
                scroll('activemortgagetable2');
            }
        });
    });
    //#region Active Mortgage

    //#region Public Methods
    function scroll(element) {
        var ele = document.getElementById(element);
        window.scrollTo(ele.offsetLeft, ele.offsetTop);
    }
    //#region Public Methods
});