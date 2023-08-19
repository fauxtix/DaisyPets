/* eslint-disable no-extra-bind */
import axios from 'axios';
import { L10n } from '@syncfusion/ej2-base';
import React, { useState, useEffect } from 'react';
import { DataManager, UrlAdaptor } from '@syncfusion/ej2-data';
import { ColumnDirective, ColumnsDirective, GridComponent, CommandColumn } from '@syncfusion/ej2-react-grids';
import { ToastComponent } from '@syncfusion/ej2-react-notifications';
import { ButtonComponent } from '@syncfusion/ej2-react-buttons';
import { isNullOrUndefined } from '@syncfusion/ej2-base';
import { Edit, Page, Sort, ForeignKey, Toolbar, Inject } from '@syncfusion/ej2-react-grids';


const onLoad = () => {
    let gridElement = document.getElementById('grid');
    if (gridElement && gridElement.ej2_instances[0]) {
        let gridInstance = gridElement.ej2_instances[0];
        /** height of the each row */
        const rowHeight = gridInstance.getRowHeight();
        /** Grid height */
        const gridHeight = gridInstance.height;
        /** initial page size */
        const pageSize = gridInstance.pageSettings.pageSize;
        /** new page size is obtained here */
        const pageResize = (gridHeight - (pageSize * rowHeight)) / rowHeight;
        gridInstance.pageSettings.pageSize = pageSize + Math.round(pageResize);
    }
};

L10n.load({
    'en-US': {
        grid: {
            'SaveButton': 'Submit',
            'CancelButton': 'Discard'
        }
    },
    'pt-PT': {
        grid: {
            'SaveButton': 'Submit',
            'CancelButton': 'Discard'
        }
    }
});

let toastObj;
let position = { X: 'Right' };
let toastBtnShow;
let toastBtnHide;

const todosEndpoint = 'https://localhost:7161/api/ToDos';

const commands = [{ type: 'Edit', buttonOption: { iconCss: ' e-icons e-edit', cssClass: 'e-flat' } },
{ type: 'Delete', buttonOption: { iconCss: 'e-icons e-delete', cssClass: 'e-flat' } },
{ type: 'Save', buttonOption: { iconCss: 'e-icons e-update', cssClass: 'e-flat' } },
{ type: 'Cancel', buttonOption: { iconCss: 'e-icons e-cancel-icon', cssClass: 'e-flat' } }];
const SyncfusionToDoLists = () => {
    const [todos, setTodos] = useState([]);
    const [todo, setTodo] = useState();

    useEffect(() => {
        axios.get(todosEndpoint).then((response) => {
            setTodos(response.data);
        });
    }, [todos]);


    const position = { X: "Right", Y: "Bottom" };
    const buttons = [
        { model: { content: "Ignore" } },
        { model: { content: "reply" } }
    ];
    const pageOptions = {
        pageSize: 12, pageSizes: true
    };
    const editOptions = { allowEditing: true, allowAdding: true, allowDeleting: true, allowFiltering: true, mode: 'Dialog' };
    const toolbarOptions = ['Add', 'Edit', 'Delete', 'Update', 'Cancel', 'Search'];
    const descriptionRules = { required: true, minLength: 3 };
    const settings = { checkboxMode: 'ResetOnRowClick' };

    function create() {
        setTimeout(function () {
            toastObj.show({
                title: 'Adaptive Tiles Meeting', content: 'Conference Room 01 / Building 135 10:00 AM',
                icon: 'e-meeting',
            });
            // eslint-disable-next-line no-extra-bind
        }.bind(this), 200);
    }
    function hideBtnClick() {
        toastObj.hide('All');
    }
    function showBtnClick() {
        toastObj.show();
    }
    function onclose(e) {
        if (e.toastContainer.childElementCount === 0) {
            toastBtnHide.element.style.display = 'none';
        }
    }
    function onbeforeOpen() {
        toastBtnHide.element.style.display = 'inline-block';
    }


    const actionComplete = (args) => {
        if ((args.requestType === 'beginEdit' || args.requestType === 'add')) {
            const dialog = args.dialog;
            dialog.showCloseIcon = false;
            dialog.height = 400;
            dialog.width = 600;
            // change the header of the dialog
            dialog.header = args.requestType === 'beginEdit' ? 'Editar registo ' + args.rowData['id'] : 'Novo registo';
        }
    };

    return (
        <div>
            <div className='control-panel'>
                <div className='control-section col-lg-12 toast-default-section'>
                    <div className="e-sample-resize-container">
                        <ToastComponent ref={(toast) => { toastObj = toast; }} id='toast_default' position={position} created={create.bind(this)} close={onclose.bind(this)} beforeOpen={onbeforeOpen.bind(this)}></ToastComponent>
                        <div id="toastBtnDefault" style={{ margin: '12px', textAlign: 'center' }}>
                            <ButtonComponent id='toastBtnShow' ref={(btn) => { toastBtnShow = btn; }} className='e-btn' onClick={showBtnClick.bind(this)}>Show Toasts</ButtonComponent>
                            <ButtonComponent id='toastBtnHide' ref={(btn) => { toastBtnHide = btn; }} className='e-btn' onClick={hideBtnClick.bind(this)}>Hide All</ButtonComponent>
                        </div>
                    </div>
                </div>
            </div>
            <GridComponent dataSource={todos}
                allowPaging={true}
                allowFiltering={true}
                actionComplete={actionComplete}
                onLoad={onLoad}
                pageSettings={pageOptions}
                toolbar={toolbarOptions}
                selectionSettings={settings}
                editSettings={editOptions}>
                <ColumnsDirective>
                    <ColumnDirective field='Id' headerText='Id' width='80' isPrimaryKey={true} visible={false} />
                    <ColumnDirective field='Description' headerText='Descrição' validationRules={descriptionRules} width='400' />
                    <ColumnDirective field='CategoryDescription' headerText='Categoria' allowEditing={false} width='200' />
                    <ColumnDirective field='TodoStartDate' headerText='Data' width='100' textAlign="Center" editType='datepickeredit' />
                    <ColumnDirective field='Status' headerText='Status' width='70' textAlign="Center" />
                    <ColumnDirective headerText='Manage Records' width='160' commands={commands}></ColumnDirective>
                </ColumnsDirective>
                <Inject services={[Page, Edit, Sort, Toolbar, CommandColumn]} />
            </GridComponent>
        </div>);

}

export default SyncfusionToDoLists;