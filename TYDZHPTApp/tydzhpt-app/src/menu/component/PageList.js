import React from "react";
import { Table, Button, Space, message, Modal, Row, Col, Tree } from "antd";
import { cdgl, menuTree, delOne } from "../../Api/cdglApi";
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
            treeData: [],
            isShowConfirm: false,
            isShowConfirmLoading: false,
            defaultExpandAll: false,
            
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
                        <a data-id={record.ID} onClick={() => { this.editShowForm(record.ID) }} >编辑</a>
                        <a onClick={() => { this.deleteHandle(record.ID) }} >删除</a>
                    </Space>);
            }
        },
    ];

    rowNo = 1;
    selectNodeValue = this.props.nodeValue;

    editShowForm(id) {
        this.setState({ modalVisible: true, id })
    }

    deleteHandle(id = 0) {
        if (!id || id <= 0)
            return;

        this.setState({ isShowConfirm: true, id: id });
    }

    deleteModel() {
        this.setState({ isShowConfirmLoading: true });
        delOne(this.state.id).then(resp => {

            if (resp.data.Code === 0) {
                this.setState({ isShowConfirmLoading: false, isShowConfirm: false });
                this.loadMenuTree();
                this.loadData();
            }
            else {
                this.setState({ isShowConfirmLoading: false });
                message.error(resp.data.Message);
            }
        }).catch(er => {
            this.setState({ isShowConfirmLoading: false });
            message.error(er);
        });
    }

    componentDidMount() {
        this.loadMenuTree();
        this.loadData();
    }

    static getDerivedStateFromProps(prop, state) {
        console.log({ prop });
        console.log({ state });

        
        return null;
    }

    // shouldComponentUpdate(nextProps, nextState) {

    //     console.log({ nextProps });
    //     console.log({ nextState });
        
    //     return false;
    // }

    loadMenuTree = () => {
        menuTree().then(resp => {
            console.log({ resp });
            this.setState({
                treeData: resp.data.Data,
                defaultExpandAll: true
            });
        }).catch(er => {
            console.error(er);
        });
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

        if (this.selectNodeValue && this.selectNodeValue != "")
            pageConditon.data.FCDID = this.selectNodeValue;

        this.rowNo = 1;

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
            message.error(er.toString());
            console.log(er);
        });
    }

    createMenuHandle() {
        this.setState({
            modalVisible: true,
            id: -1
        });
    }

    clicckTreeNodeHandle(node, e) {
        this.selectNodeValue = e.node.value;
        this.loadData();
    }

    render() {
        return <>
            <Row>
                <Col flex="250px">
                    <div style={{ marginTop: "15px", borderRight: "1px solid #DCDCDC", height: "96%", marginRight: "10px" }}>
                        <Tree showLine={true}
                            treeData={this.state.treeData}
                            defaultExpandAll={this.state.defaultExpandAll}
                            onSelect={(node, e) => { this.clicckTreeNodeHandle(node, e) }}>
                        </Tree>
                    </div>
                </Col>
                <Col flex="auto">
                    <div className="list-grid-container">
                        <div className="list-grid-head">
                            <div className="list-grid-head-tool">
                                <div className="list-grid-head-tool-text">菜单管理</div>
                                <div className="list-grid-head-tool-bar">
                                    <div>
                                        <Button type="primary" onClick={() => { this.createMenuHandle(); }}>新建</Button>
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
                </Col>
            </Row>
            <Modal onCancel={() => {
                this.setState({ isShowConfirm: false });
            }} onOk={() => {
                this.deleteModel();
            }} centered visible={this.state.isShowConfirm} confirmLoading={this.state.isShowConfirmLoading} title="提示">
                确认删除吗？
            </Modal>
            <Modal visible={this.state.modalVisible} onCancel={() => { this.setState({ modalVisible: false }); }}
                destroyOnClose={true} centered footer={null} maskClosable={false} >
                <MenuEdit id={this.state.id}
                    onSuccess={(success = false) => {
                        this.setState({
                            modalVisible: !success
                        });
                        if (success) {
                            this.loadData();
                            this.loadMenuTree();
                        }
                    }}
                    onClose={() => { this.setState({ modalVisible: false }); }}></MenuEdit>
            </Modal>
        </>;
    }
}

export default PageListPart;