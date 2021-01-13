

function ReadData() {
    $(document).ready(function () {
        $('#btn_Read').click(function () {
            $.ajax({
                type: "GET",
                url: '/Personals/ReadListaPersonal',
                success: function (result) {
                    var html = '';
                    $.each(result, function (key, item) {
                        html += '<tr>';
                        //html += '<td>' + item.ID_personal + '</td>';
                        html += '<td>' + item.Nombre + '</td>';
                        html += '<td>' + item.ApePaterno + '</td>';
                        html += '<td>' + item.ApeMaterno + '</td>';
                        html += '<td>' + item.Edad + '</td>';
                        html += '<td>' + item.IsActive + '</td>';
                        html += '<td><a href="Views/Personals/Edit.cshtml" }}" onclick="return getbyID(' + item.ID_personal + ')">Editar</a> | <a href="Views/Personals/Edit.cshtml" onclick="Delele(' + item.ID_personal + ')">Eliminar</a></td>';  
                        html += '</tr>';
                    });
                    $('.tbody').html(html);
                },
                error: function () {
                    console.log("error");
                }
            });
        });
    });
}

function getbyID(ID_personal) {
    //$('#PerNombre').css('border-color', 'lightgrey');
    //$('#PerApePa').css('border-color', 'lightgrey');
    //$('#PerApeMa').css('border-color', 'lightgrey');
    //$('#PerEdad').css('border-color', 'lightgrey');
    //$('#PerIsActive').css('border-color', 'lightgrey');
    $.ajax({
        url: "/Personals/getbyID/" + ID_personal,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#PerNombre').val(result.Nombre);
            $('#PerApePa').val(result.ApePaterno);
            $('#PerApeMa').val(result.ApeMaterno);
            $('#PerEdad').val(result.Edad);
            $('#PerIsActive').val(result.IsActive);
            console.log("Se obtuvo id")
        },
        error: function (errormessage) {
            console.log("Error")
        }
    });
    return false;
}  



