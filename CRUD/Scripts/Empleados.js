var objID;
var Nombre;
var apeP;
 
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
        url: "/Personals/Edit/"+objID,
        type: "GET",
        data: { ID_personal: objID},
        success: function (result) {
            $("#PerNombre").val(result.Nombre);
            $('#PerApePa').val(result.ApePaterno);
            $('#PerApeMa').val(result.ApePaterno);
            $('#PerEdad').val(result.Edad);
            $('#PerIsActive').val(result.IsActive);
            console.log(result);
        },

        error: function (errormessage) {
            console.log("Error getid")
        }

    });
    return false;
}

//function CreateData() {
//    //console.log("entrando");
//    //$('#Btn_Create').click(function () {
//        console.log("en el boton");
//            var psl = {
//                Nombre: $("#PerNombre").val(),
//                ApePaterno: $('#PerApePa').val(),
//                ApeMaterno: $('#PerApeMa').val(),
//                Edad: $('#PerEdad').val(),
//                IsActive: $('#PerIsActive').val()
//            };
//            $.ajax({
//                type: "POST",
//                url: '/Create',
//                data: psl,
//                dataType: "JSON",
//                success: function () {
//                    console.log("ok");
//                },
//                error: function () {
//                    console.log("error");
//                    //alert(res);
//                }
//            });
//    //});
//}

function Update() {
    var respuesta;
    var psl = {
        Nombre: $("#PerNombre").val(),
        ApePaterno: $('#PerApePa').val(),
        ApeMaterno: $('#PerApeMa').val(),
        Edad: $('#PerEdad').val(),
        IsActive: $('#PerIsActive').val()
    };
    $.ajax({
        url: "/Personals/Edit",
        data: psl,
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (data) {
            console.log("ok");
            respuesta = data;
        },
        error: function (errormessage) {
            console.log("error vista");
        }
    });
    return respuesta;
}

function DeleteConfirmar() {
    $(document).ready(function () {
        $('#btn_delete').click(function () {
            var res = confirm("¿Desea eliminar al personal?");
            if (res) {
                $.ajax({
                    url: "/Personals/DeleteConfirmed/",
                    type: "POST",
                    contentType: "application/json;charset=UTF-8",
                    dataType: "json",
                    success: function (result) {
                        console.log("ok");
                    },
                    error: function () {
                        console.log("error");
                    }
                });
            }
        });
    });
}

function Delete(objID) {
    $(document).ready(function () {
        $('#btnEdit').click(function () {
            $.ajax({
                url: "/Personals/Delete/" + objID,
                type: "POST",
                contentType: "application/json;charset=UTF-8",
                dataType: "json",
                success: function (result) {
                    console.log("Delete ok")
                },
                error: function (errormessage) {
                    alert(errormessage.responseText);
                }
            });
        });
    });

}