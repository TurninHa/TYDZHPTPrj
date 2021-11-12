import React from "react";
import { Table, Button, Space, message, Modal } from "antd";
import { cdgl } from "../../Api/cdglApi";
import MenuEdit from "./MenuEdit";

class PageListPart extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            dataSource: [],
            pageIndex: 1,
            pageSize: 20,
            total: 0,
            modalVisible: false,
            id: -1,
        };
    }
    columns = [
        {
            title: "序号",
            dataIndex: "XH",
            key: "XH"
        },
        {
            title: "菜单编码",
            dataIndex: "CDBM",
            key: "CDBM"
        },
        {
            title: "菜单名称",
            dataIndex: "CDMC",
            key: "CDMC"
        },
        {
            title: "菜单路径",
            dataIndex: "CDLJ",
            key: "CDLJ"
        },
        {
            title: "排序号",
            dataIndex: "PXH",
            key: "PXH"
        },
        {
            title: "创建时间",
            dataIndex: "CreateTime",
            key: "CreateTime"
        },
        {
            title: "操作",
            dataIndex: "ID",
            key: "ID",
            render: (text, record) => {
                return (
                    <Space>
                        <a data-id={record.ID} onClick={() => { this.editShowForm(record.ID) }}>编辑</a>
                        <a>删除</a>
                    </Space>);
            }
        },
    ];

    rowNo = 1;

    editShowForm(id) {
        this.setState({ modalVisible: true, id })
    }

    componentDidMount() {
        this.loadData();
    }

    loadData() {
        let condition = {};
        if (this.props.condition)
            condition = this.props.condition;

        let pageConditon = {
            data: condition,
            pageIndex: this.state.pageIndex,
            pageSize: this.state.pageSize
        };

        cdgl(pageConditon).then(response => {

            response.data.Data.Data.forEach(element => {
                element.XH = this.rowNo;
                this.rowNo++;
            });

            this.setState({
                dataSource: response.data.Data.Data,
                total: response.data.Data.PageCount
            });
        }).catch(er => {
            // this.setState({
            //     dataSource: [],
            //     total: 0
            // });
            message.error(er.toString());
            console.log(er);
        });
    }

    render() {
        return <>
            <div className="list-grid-container">
                <div className="list-grid-head">
                    <div className="list-grid-head-tool">
                        <div className="list-grid-head-tool-text">菜单管理</div>
                        <div className="list-grid-head-tool-bar">
                            <div>
                                <Button type="primary">新建</Button>
                            </div>
                            <div>
                                <Button>删除</Button>
                            </div>
                        </div>
                    </div>
                </div>
                <div className="list-grid-body">
                    <Table columns={this.columns} dataSource={this.state.dataSource} pagination={{
                        onShowSizeChange: pageIndex => {
                            this.setState({ pageIndex });
                        },
                        defaultCurrent: 1,
                        defaultPageSize: 20,
                        total: this.state.total
                    }}></Table>
                </div>
            </div>
            <Modal visible={this.state.modalVisible} onCancel={() => { this.setState({ modalVisible: false }); }}
                destroyOnClose={true} centered footer={null} maskClosable={false} >
                <MenuEdit id={this.state.id} onSuccess={(success = false) => {
                    this.setState({
                        modalVisible: !success
                    });
                    if (success)
                        this.loadData();
                }} onClose={() => { this.setState({ modalVisible: false }); }}></MenuEdit>
            </Modal>
        </>;
    }
}

export default PageListPart;