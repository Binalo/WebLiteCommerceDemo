$(document).ready(function () {

    $('#subChange').click(function (e) {
        e.preventDefault();
        $('#resultcurrentpassword').html("");
        $('#resultconfirmpassword').html("");
        $('#resultnewpassword').html("");
        if ($('#currentpassword').val() != "" && $('#newpassword').val() != "" && $('#confirmpassword').val() != "") {
            var data = {
                "id": $("#userID").val(),
                "newPWd": $("#newpassword").val(),
                "currentPWd": $("#currentpassword").val()
            };
            if ($('#newpassword').val() == $('#confirmpassword').val()) {
                $.ajax({
                    type: "POST",
                    url: "/Account/ChangePassword",
                    contentType: "application/json",
                    dataType: "json",
                    data: JSON.stringify(data),
                    success: function (response) {
                        console.log(response);
                        if (response.status == true) {
                            $('#newpassword').val("");
                            $('#currentpassword').val("");
                            $('#confirmpassword').val("");
                            alert("Update password success!");
                        }
                        else {
                            $('#resultcurrentpassword').html('<i class="fa fa-exclamation-circle"></i> current password not right');
                        }
                    }
                });
            }
            else {
                $('#resultconfirmpassword').html('<i class="fa fa-exclamation-circle"></i> Confirm password not right!');
            }
        } else {
            $('#resultcurrentpassword').html('<i class="fa fa-exclamation-circle"></i> Please fill out the field');
            $('#resultconfirmpassword').html('<i class="fa fa-exclamation-circle"></i> Please fill out the field');
            $('#resultnewpassword').html('<i class="fa fa-exclamation-circle"></i> Please fill out the field');
        }
    });
    $('#OrderStatistic').change(function () {
        var year = $("#OrderStatistic").val();
        $.ajax({
            type: "GET",
            url: "/Dashboard/Input", // Tên servlet
            data: "year="+year,
            contentType: "application/json",
            dataType: "json",
            success: function (result) {
                console.log(result);
                if (result != null) {
                    $('td').remove();
                    $.each(result, function (index, dashboard) {
                        $('#TTotal').text(dashboard.January + dashboard.February + dashboard.March + dashboard.April + dashboard.May + dashboard.June + dashboard.July + dashboard.August +
                            dashboard.September + dashboard.October + dashboard.November + dashboard.December + '$');
                        $('#titleThongKe').text('Statistics of monthly revenue in ' + year)
                        $('#OrderStatistics').append("<td>" + dashboard.January+"$" + "</td>");
                        $('#OrderStatistics').append("<td>" + dashboard.February + "$" + "</td>");
                        $('#OrderStatistics').append("<td>" + dashboard.March + "$" + "</td>");
                        $('#OrderStatistics').append("<td>" + dashboard.April + "$" + "</td>");
                        $('#OrderStatistics').append("<td>" + dashboard.May + "$" + "</td>");
                        $('#OrderStatistics').append("<td>" + dashboard.June + "$" + "</td>");
                        $('#OrderStatistics').append("<td>" + dashboard.July + "$" + "</td>");
                        $('#OrderStatistics').append("<td>" + dashboard.August + "$" + "</td>");
                        $('#OrderStatistics').append("<td>" + dashboard.September + "$" + "</td>");
                        $('#OrderStatistics').append("<td>" + dashboard.October + "$" + "</td>");
                        $('#OrderStatistics').append("<td>" + dashboard.November + "$" + "</td>");
                        $('#OrderStatistics').append("<td>" + dashboard.December + "$" + "</td>");
                    });
                }
                else {
                    alert("Non data!");
                }
            }
        });
    });
});