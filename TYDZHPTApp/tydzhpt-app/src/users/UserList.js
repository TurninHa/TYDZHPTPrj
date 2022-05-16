import React from "react";
import { Input, Form, Button, Table, Select, AutoComplete, Modal, message } from "antd";
import '../index.css';
import "../Css/glb.css";
import { getUserList, getModel, saveUser, disEnUser, deleteUser } from '../Api/yhgl'


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
            }
        }
    }

    //form =  React.createRef();

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
    },{
        title:"操作",
        dataIndex:"ID",
        key:"ID",
        render:(text,record)=>{
            return "编辑";
        }
    }];

    componentDidMount() {
        this.loadUserDataList();
    }

    loadUserDataList() {
        getUserList({
            pageIndex: this.state.pagination.pageIndex,
            pageSize: this.state.pagination.pageSize
        }).then(resp => {
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

    render() {
        return <div className="list-page-container">
            <div className="search-container">
                <div className="search-tool-container">

                    <div className="search-tool-item">
                        <div className="search-tool-item-content">
                            <div className="search-tool-item-content-left">
                                <label>用户名:</label>
                            </div>
                            <div className="search-tool-item-content-right">

                                <Input placeholder="用户名"></Input>

                            </div>
                        </div>
                    </div>
                    <div className="search-tool-item">
                        <div className="search-tool-item-content">
                            <div className="search-tool-item-content-left">
                                <label>姓名:</label>
                            </div>
                            <div className="search-tool-item-content-right">

                                <Input name="XM" placeholder="姓名"></Input>

                            </div>
                        </div>
                    </div>
                    <div className="search-tool-item">
                        <div className="search-tool-item-content">
                            <div className="search-tool-item-content-left">
                                <label>使用状态:</label>
                            </div>
                            <div className="search-tool-item-content-right">

                                <Select defaultValue="1"
                                    placeholder="请选择"
                                    style={{ width: "120px" }}
                                >
                                    <Option value="">-请选择-</Option>
                                    <Option value="1">启用</Option>
                                    <Option value="0">禁用</Option>
                                </Select>

                            </div>
                        </div>
                    </div>
                    <div className="search-tool-item">
                        <div className="search-tool-item-content">
                            <div className="search-tool-item-content-left">
                                <label>所属客户:</label>
                            </div>
                            <div className="search-tool-item-content-right">

                                <AutoComplete
                                    key="autocompl_sskh"
                                    style={{ width: "120px" }}
                                    options={[{ value: "本公司", id: 0 }]}
                                    placeholder="请选择单位"
                                >
                                </AutoComplete>

                            </div>
                        </div>
                    </div>

                    <div className="search-tool-item-action">
                        <div className="search-tool-item-action-content">
                            <div>
                                <Button onClick={() => { }}>重置</Button>
                            </div>
                            <div>
                                <Button type="primary" onClick={() => { }}>查询</Button>
                            </div>
                        </div>
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
                                    <Button type="primary" onClick={() => { }}>添加用户</Button>
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
    }
}

export default UserList;