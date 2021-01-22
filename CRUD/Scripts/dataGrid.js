$("#gridContainer").dxDataGrid({
    dataSource: "/DEpersonal/ReadListaPersonal",
    allowColumnReordering: true,
    allowColumnResizing: true,
    columnAutoWidth: true,
    showBorders: true,
    columnChooser: {
        enabled: true
    },
    columnFixing: {
        enabled: true
    },
    columns: [
        //{
        //    caption: "Nombre Personal",
        //    width: 230,
        //    fixed: true,
        //    calculateCellValue: function (data) {
        //        return [data.Nombre,
        //        data.ApePaterno, data.ApeMaterno]
        //            .join(" ");
        //    }
        //}
        {
            dataField: "Nombre",
            dataType: "string",
            alignment: "center",
        },
        {
            dataField: "ApePaterno",
            dataType: "string",
            alignment: "center",
        },
        {
            dataField: "ApeMaterno",
            dataType: "string",
            alignment: "center",
        }
        , {
            dataField: "Edad",
            dataType: "number",
            alignment: "center",
        }, {
            dataField: "IsActive",
            dataType: "string",
            alignment: "center",
        }]
});

//FUNCION GRID CON BOTONES
            $(function () {
                $(function () {
                    function addRequestVerificationToken(data) {
                        data.__RequestVerificationToken = $('input[name=__RequestVerificationToken]').val();
                        return data;
                    };
                    var selectedRowIndex = -1;
                    var dataSource = new DevExpress.data.DataSource({
                        key: "ID_personal",
                        load: function () {
                            var d = $.Deferred();
                            return $.ajax({
                                url: '/DEpersonal/ReadListaPersonal'
                            }).done(function (result) {
                                d.resolve(result);
                            }).fail(function () {
                                throw "Data loading error";
                            });
                        },
                        insert: function (values) {
                            return $.ajax({
                                type: "POST",
                                url: "/DEpersonal/Create",
                                data: addRequestVerificationToken({ personal: values }),
                            })
                        },
                        update: function (key, values) {
                            return $.ajax({
                                type: "POST",
                                url: "/DEpersonal/Edit",
                                data: JSON.stringify({ ID_personal: key, personal: values }),
                            })
                        },
                        remove: function (key) {
                            return $.ajax({
                                url: "/DEpersonal/Delete/"+encodeURIComponent(key),
                                data: addRequestVerificationToken({ ID: key })
                            })
                        }
                    });
                    var IsActive = [{
                        "ID": true,
                        "Name": "Activo"
                    }, {
                        "ID": false,
                        "Name": "No Activo"
                    }];
                    $("#action-add").dxSpeedDialAction({
                        label: "Add row",
                        icon: "add",
                        index: 1,
                        onClick: function () {
                            grid.addRow();
                            grid.deselectAll();
                        }
                    }).dxSpeedDialAction("instance");

                    var deleteSDA = $("#action-remove").dxSpeedDialAction({
                        icon: "trash",
                        label: "Delete row",
                        index: 2,
                        visible: false,
                        onClick: function () {
                            grid.deleteRow(selectedRowIndex);
                            grid.deselectAll();
                        }
                    }).dxSpeedDialAction("instance");

                    var editSDA = $("#action-edit").dxSpeedDialAction({
                        label: "Edit row",
                        icon: "edit",
                        index: 3,
                        visible: false,
                        onClick: function () {
                            grid.editRow(selectedRowIndex);
                            grid.deselectAll();
                        }
                    }).dxSpeedDialAction("instance");

                    var grid = $("#grid").dxDataGrid({
                        dataSource: dataSource,
                        showBorders: true,
                        selection: {
                            mode: "single"
                        },
                        paging: {
                            enabled: false
                        },
                        editing: {
                            mode: "popup",
                            texts: {
                                confirmDeleteMessage: ""
                            }
                        },
                        onSelectionChanged: function (e) {
                            selectedRowIndex = e.component.getRowIndexByKey(e.selectedRowKeys[0]);

                            deleteSDA.option("visible", selectedRowIndex !== -1);
                            editSDA.option("visible", selectedRowIndex !== -1);
                        },
                        columns: [
                            {
                            dataField: "Nombre",
                            dataType: "string",
                            alignment: "center",
                        },
                        {
                            caption: "Apellido Paterno",
                            dataField: "ApePaterno",
                            dataType: "string",
                            alignment: "center",
                        },
                        {
                            caption: "Apellido Materno",
                            dataField: "ApeMaterno",
                            dataType: "string",
                            alignment: "center",
                        }
                            , {
                            dataField: "Edad",
                            dataType: "number",
                            alignment: "center",
                        },
                        {
                            dataField: "IsActive",
                            caption: "Is Active",
                            width: 125,
                            lookup: {
                                dataSource: IsActive,
                                displayExpr: "Name",
                                valueExpr: "ID"
                            }
                        }]
                    }).dxDataGrid("instance");


                });
            });