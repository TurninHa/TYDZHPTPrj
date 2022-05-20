import React from "react";
import { Input, Form, Button, Table, Select, AutoComplete, Modal, message, Space } from "antd";
import '../index.css';
import "../Css/glb.css";
import { getUserList, getModel, disEnUser, deleteUser } from '../Api/yhgl'
import UserEdit from "./UserEdit";


const { Option } = Select;


class UserList extends React.Component {

    constructor(props) {
        super(props);

        this.state = {
            dataSource: [],
            pagination: {
                pageIndex: 1,
                pageSize: 20,
                total: 0
            },
            userFormVis: false
        }
    }

    form = React.createRef();
    SltSSGID = -1;

    columns = [{
        title: "序号",
        dataIndex: "XH",
        key: "XH"
    }, {
        title: "用户名",
        dataIndex: "YHM",
        key: "YHM"
    }, {
        title: "所属角色",
        dataIndex: "RoleNames",
        key: "RoleNames"
    }, {
        title: "姓名",
        dataIndex: "XM",
        key: "XM"
    }, {
        title: "手机号",
        dataIndex: "SJH",
        key: "SJH"
    }, {
        title: "所属员工",
        dataIndex: "SSYGID",
        key: "SSYGID"
    }, {
        title: "所属公司",
        dataIndex: "SSGSName",
        key: "SSGSName"
    }, {
        title: "是否管理员",
        dataIndex: "SFGLY",
        key: "SFGLY",
        render: (text, rowData) => {
            if (text === 1)
                return "是";
            return "否";
        }
    }, {
        title: "是否启用",
        dataIndex: "SYZTName",
        key: "SYZTName",
        render: (text, record) => {
            if (record.SYZT == 1)
                return <span style={{ color: "green" }}>{text}</span>;

            return <span style={{ color: "red" }}>{text}</span>
        }
    }, {
        title: "操作",
        dataIndex: "ID",
        key: "ID",
        render: (text, record) => {
            return "编辑";
        }
    }];

    componentDidMount() {
        this.loadUserDataList();
    }

    loadUserDataList() {
        let schData = this.form.current.getFieldsValue(true);

        if (!schData || typeof schData == "undefined")
            schData = {};

        schData.pageIndex = this.state.pagination.pageIndex;
        schData.pageSize = this.state.pagination.pageSize;
        schData.SSGSID = this.SltSSGID;

        getUserList(schData).then(resp => {
            let data = resp.data;

            if (data.Code === 0) {

                data.Data.Data.forEach((element, i) => {
                    element.XH = i + 1;
                });

                this.setState({
                    dataSource: data.Data.Data,
                    pagination: {
                        pageIndex: data.Data.PageIndex,
                        pageSize: data.Data.PageSize,
                        total: data.Data.Count,
                    }
                });
            }
            else {
                message.error(data.Message);
            }
        }).catch(er => {
            console.error(er);
            message.error("请求错误");
        });
    }

    searchHandle() {

        this.loadUserDataList();
    }

    autoCompleteSelectHandle(option) {
        console.log(option);
        this.SltSSGID = option.id;
    }
    autoCompleteChangeHandle(v) {

        if (!v || typeof v == "undefined")
            this.SltSSGID = -1;
    }

    resetHandle() {
        this.form.current.setFieldsValue({
            YHM: "",
            XM: "",
            SJH: "",
            SYZT: "1",
            SSKH: ""
        });
    }

    render() {
        return (<>
            <div className="list-page-container">
                <div className="search-container">
                    <div className="search-tool-container">
                        <div className="search-tool-antd-form">
                            <Form layout="inline" ref={this.form} >
                                <Form.Item name="YHM" label="用户名">
                                    <Input placeholder="用户名"></Input>
                                </Form.Item>
                                <Form.Item name="XM" label="姓名">
                                    <Input placeholder="姓名"></Input>
                                </Form.Item>
                                <Form.Item name="SJH" label="手机号">
                                    <Input placeholder="手机号"></Input>
                                </Form.Item>
                                <Form.Item name="SYZT" label="使用状态" initialValue="1">
                                    <Select
                                        placeholder="请选择"
                                        style={{ width: "120px" }}
                                    >
                                        <Option value="">-请选择-</Option>
                                        <Option value="1">启用</Option>
                                        <Option value="0">禁用</Option>
                                    </Select>
                                </Form.Item>
                                <Form.Item name="SSKH" label="所属客户">
                                    <AutoComplete
                                        key="autocompl_sskh"
                                        style={{ width: "120px" }}
                                        options={[{ value: "本公司", id: 0 }]}
                                        placeholder="请选择单位"
                                        onSelect={(v, opt) => { this.autoCompleteSelectHandle(opt); }}
                                        onChange={v => this.autoCompleteChangeHandle(v)}
                                    >
                                    </AutoComplete>
                                </Form.Item>
                                <Form.Item>
                                    <Space>
                                        <Button type="primary" onClick={() => this.searchHandle()}>查询</Button>
                                        <Button onClick={() => this.resetHandle()}>重置</Button>
                                    </Space>
                                </Form.Item>
                            </Form>
                        </div>
                    </div>
                </div>
                <div className="list-container">
                    <div className="list-grid-container">
                        <div className="list-grid-head">
                            <div className="list-grid-head-tool">
                                <div className="list-grid-head-tool-text">用户管理</div>
                                <div className="list-grid-head-tool-bar">
                                    <div>
                                        <Button type="primary" onClick={() => this.setState({ userFormVis: true })}>添加用户</Button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div className="list-grid-body">
                            <Table columns={this.columns} dataSource={this.state.dataSource}
                                bordered
                                size="small"
                                pagination={{
                                    onShowSizeChange: (pageIndex, pageSize) => {
                                        const pagnt = { ...this.state.pagination };

                                        pagnt.pageIndex = pageIndex;
                                        pagnt.pageSize = pageSize;

                                        this.setState({ pagination: pagnt });
                                    },
                                    defaultCurrent: 1,
                                    current: this.state.pagination.pageIndex,
                                    defaultPageSize: this.state.pagination.pageSize,
                                    total: this.state.pagination.total
                                }}></Table>
                        </div>
                    </div>
                </div>
            </div>
            <Modal
                destroyOnClose={true}
                title="用户编辑"
                footer={null}
                visible={this.state.userFormVis}
                onCancel={() => this.setState({ userFormVis: false })}
                maskClosable={false}
                centered
            >
                <UserEdit></UserEdit>
            </Modal>
        </>
        )
    }
}

export default UserList;