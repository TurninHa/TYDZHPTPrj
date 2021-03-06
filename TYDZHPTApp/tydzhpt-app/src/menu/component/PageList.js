import React from "react";
import { Table, Button, Space, message, Modal, Row, Col, Tree } from "antd";
import { cdgl, menuTree, delOne } from "../../Api/cdglApi";
import MenuEdit from "./MenuEdit";
import { MenuButtons } from "./MenuButtons";

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
            condition: this.props.condition,
            visibleMnBtns: false,
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
                        <a onClick={() => { this.setState({ id: record.ID, visibleMnBtns: true }); }}>操作按钮</a>
                    </Space>);
            }
        },
    ];

    rowNo = 1;

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

    shouldComponentUpdate(nextProps, nextState) {

        if (nextProps.condition && nextProps.condition.search) {
            this.state.condition = {
                cdmc: nextProps.condition.cdmc,
                cdbm: nextProps.condition.cdbm
            };
            this.loadData();
            nextProps.condition.search = false;
        }
        return true;
    }

    componentDidUpdate() {
        console.log("componentDidUpdate 我已经执行了更新");
    }

    loadMenuTree = () => {
        menuTree().then(resp => {
            console.log({ resp });
            this.setState({
                treeData: resp.data.Data
            });
            this.setState({ defaultExpandAll: true });
        }).catch(er => {
            console.error(er);
        });
    }

    loadData() {
        let condition = {};
        if (this.state.condition)
            condition = this.state.condition;

        let pageConditon = {
            data: condition,
            pageIndex: this.state.pageIndex,
            pageSize: this.state.pageSize
        };

        if (this.selectNodeValue && this.selectNodeValue !== "")
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
                            defaultExpandParent={this.state.defaultExpandAll}
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
                            <Table columns={this.columns} dataSource={this.state.dataSource}
                                bordered
                                size="small"
                                pagination={{
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
            <Modal
                onCancel={() => {
                    this.setState({ isShowConfirm: false });
                }}
                onOk={() => {
                    this.deleteModel();
                }}
                okText="删除"
                okType="danger"
                cancelText="取消"
                centered
                visible={this.state.isShowConfirm}
                confirmLoading={this.state.isShowConfirmLoading}
                title="提示"
                width="260px"
            >
                确认删除吗？
            </Modal>
            <Modal
                visible={this.state.modalVisible}
                onCancel={() => { this.setState({ modalVisible: false }); }}
                destroyOnClose={true}
                centered footer={null}
                maskClosable={false}
                title="菜单编辑" >
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
            <Modal visible={this.state.visibleMnBtns}
                onCancel={() => { this.setState({ visibleMnBtns: false }); }}
                destroyOnClose={true}
                centered
                maskClosable={false}
                title="操作功能管理"
                footer={[
                    <div style={{ marginLeft: "auto", marginRight: "auto", textAlign: "center" }}>
                        <Button type="default" onClick={() => { this.setState({ visibleMnBtns: false }); }}>
                            关闭
                        </Button>
                    </div>
                ]}
                width="700px"
                
            >
                <MenuButtons Id={this.state.id}></MenuButtons>
            </Modal>
        </>;
    }
}

export default PageListPart;