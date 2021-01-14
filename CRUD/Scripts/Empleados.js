var objID;
$(document).ready(function () {
    ReadData();
}); 
 
function ReadData() {
    $.ajax({
        type: "GET",
        url: '/Personals/ReadListaPersonal',
        success: function (result) {
            var html = '';
            var objID = 0;
            $.each(result, function (key, item) {
                objID = item.ID_personal;
                html += '<tr>';
                html += '<td>' + item.Nombre + '</td>';
                html += '<td>' + item.ApePaterno + '</td>';
                html += '<td>' + item.ApeMaterno + '</td>';
                html += '<td>' + item.Edad + '</td>';
                html += '<td>' + item.IsActive + '</td>';
                html += '<td> <div class="form-group"> <div class="col-md-offset-2 col-md-10"> <input id="btnEdit" onclick= "return getID(' + objID + ');" type="button" value="Editar" class="btn btn-default" /> </div> </div> </td>';
                html += '<td> <div class="form-group"> <div class="col-md-offset-2 col-md-10"> <input id="btnDelete" onclick= "return getID(' + objID + ');" type="button" value="Eliminar" class="btn btn-default" /> </div> </div> </td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
        },
        error: function () {
            console.log("error");
        }
    });
}

function getID(objID) {
    var respuesta;
    console.log("en getID");
    $.ajax({
        url: "/Personals/EditAjax/"+objID,
        type: "GET",
        data: { ID_personal: objID},
        success: function (result) {
            console.log(result);
            $('#PerNombre').val(result.Nombre);
            $('#PerApePa').val(result.ApePaterno);
            $('#PerApeMa').val(result.ApePaterno);
            $('#PerEdad').val(result.Edad);
            $('#PerIsActive').val(result.IsActive);
            $('#myModalLabel').replaceWith("EDITAR PERSONAL");
            $('#myModal').modal('show');
            $('#btn_updateModal').show();
            $('#Btn_Create').hide();
            console.log(result);
        },

        error: function (errormessage) {
            console.log("Error getid")
        }

    });
    return false;
}

function Update() {
    $('#myModal').modal('hide');
    var psl = {
        Nombre: $("#PerNombre").val(),
        ApePaterno: $('#PerApePa').val(),
        ApeMaterno: $('#PerApeMa').val(),
        Edad: $('#PerEdad').val(),
        IsActive: $('#PerIsActive').val()
    };
    $.ajax({
        url: "/Personals/EditAjax",
        data: JSON.stringify(psl),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (data) {
            console.log("ok");
            loadData();
            $('#myModal').modal('hide');
            $('#ID_personal').val("");
            $('#PerNombre').val("");
            $('#PerApePa').val("");
            $('#PerApeMa').val("");
            $('#PerEdad').val("");
            $('#IsActive').val("null");
        },
        error: function (errormessage) {
            console.log("error vista");
        }
    });
}

function Delete(objID) {
    var ans = confirm("Are you sure you want to delete this Record?");
    if (ans) {
        $.ajax({
            url: "/Personals/DeleteAjax/" + objID,
            type: "POST",
            data: { ID_personal: objID },
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                loadData();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }  
}

function CreateData() {
    var formulario = $("formulario");
    $.ajax({
        url: "/Personals/Create",
        data: fomulario.serialize(),
        method: "POST",
        success: function (resultado) {
            if (resultado.result) {
                console.log("OK")
                ReadData();
                $('#myModal').modal('hide');
            }
        }
    });
}

function clearTextBox() {
    $('#ID_personal').val("");
    $('#PerNombre').val("");
    $('#PerApePa').val("");
    $('#PerApeMa').val("");
    $('#PerEdad').val("");
    $('#IsActive').val("null")
    $('#btn_update').hide();
    $('#Btn_Create').show();
}  