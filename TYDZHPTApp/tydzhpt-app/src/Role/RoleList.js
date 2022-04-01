import { Input, Button, Table, Select, AutoComplete, Modal } from "antd";
import { useState, useEffect } from "react";
import { getRoleList, getModel, deleteRole } from "../Api/jsgl";
import '../index.css';
import "../Css/glb.css";

const RoleList = (props) => {

    const [dataSource, setDataSource] = useState([]);
    const [pagination, setPagination] = useState({
        pageIndex: 1,
        pageSize: 20,
        total: 0
    });

    const [roleModalVisb, setRoleModalVisb] = useState(false);

    const { Option } = Select;

    const columns = [{
        title: "序号",
        dataIndex: "XH",
        key: "col_xh"
    }, {
        title: "角色编码",
        dataIndex: "JSBM",
        key: "col_jsbm"
    }, {
        title: "角色名称",
        dataIndex: "JSMC",
        key: "col_jsmc"
    }, {
        title: "使用状态",
        dataIndex: "StatusName",
        key: "col_syztname",
        render: (text, r) => {
            if (r.SYZT === 1)
                return (<span style={{ color: "green" }}>{text}</span>)
            else
                return text
        }
    }, {
        title: "所属客户",
        dataIndex: "SSGSID",
        key: "col_ssgsid",
        render: (text, r) => {
            if (text === 0)
                return "本公司人员";
            else
                return "-";
        }
    },
    {
        title: "操作",
        dataIndex: "ID",
        key: "col__role_ID_CZ",
    }];

    useEffect(() => {
        loadData();
    }, []);

    const loadData = () => {

        let reqData = {
            pageIndex: pagination.pageIndex,
            pageSize: pagination.pageSize
        };

        getRoleList(reqData).then(resp => {
            let pgnt = { ...pagination };

            pgnt.total = resp.data.Data.PageCount;
            setPagination(pgnt);

            resp.data.Data.Data.forEach((element, index) => {
                element.XH = (index + 1);
            });

            setDataSource(resp.data.Data.Data);
        }).catch(er => {
            console.error(er);
        });
    };

    return (
        <>
            <div className="list-page-container">
                <div className="search-container">
                    <div className="search-tool-container">
                        <div className="search-tool-item">
                            <div className="search-tool-item-content">
                                <div className="search-tool-item-content-left">
                                    <label>角色名称:</label>
                                </div>
                                <div className="search-tool-item-content-right">
                                    <Input name="JSMC"></Input>
                                </div>
                            </div>
                        </div>
                        <div className="search-tool-item">
                            <div className="search-tool-item-content">
                                <div className="search-tool-item-content-left">
                                    <label>角色编码:</label>
                                </div>
                                <div className="search-tool-item-content-right">
                                    <Input name="JSBM"></Input>
                                </div>
                            </div>
                        </div>
                        <div className="search-tool-item">
                            <div className="search-tool-item-content">
                                <div className="search-tool-item-content-left">
                                    <label>使用状态:</label>
                                </div>
                                <div className="search-tool-item-content-right">
                                    <Select defaultValue="1" placeholder="请选择" allowClear>
                                        <Option value="">-请选择-</Option>
                                        <Option value="1">启用</Option>
                                        <Option value="2">禁用</Option>
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
                                    <AutoComplete></AutoComplete>
                                </div>
                            </div>
                        </div>
                        <div className="search-tool-item-action">
                            <div className="search-tool-item-action-content">
                                <div>
                                    <Button >重置</Button>
                                </div>
                                <div>
                                    <Button type="primary" >查询</Button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div className="list-container">
                    <div className="list-grid-container">
                        <div className="list-grid-head">
                            <div className="list-grid-head-tool">
                                <div className="list-grid-head-tool-text">角色管理</div>
                                <div className="list-grid-head-tool-bar">
                                    <div>
                                        <Button type="primary" onClick={() => { setRoleModalVisb(true); }}>添加角色</Button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div className="list-grid-body">
                            <Table columns={columns} dataSource={dataSource}
                                bordered
                                size="small"
                                pagination={{
                                    onShowSizeChange: (pageIndex, pageSize) => {
                                        const pagnt = { ...pagination };

                                        pagnt.pageIndex = pageIndex;
                                        pagnt.pageSize = pageSize;

                                        setPagination(pagnt);
                                    },
                                    defaultCurrent: 1,
                                    defaultPageSize: pagination.pageSize,
                                    total: pagination.total
                                }}></Table>
                        </div>

                    </div>
                </div>
            </div>
            <Modal
                title="角色编辑"
                visible={roleModalVisb}
                footer={null}
                onCancel={() => { setRoleModalVisb(false); }}
                destroyOnClose={true}
                maskClosable={false}
                centered
            ></Modal>
        </>
    );
}

export default RoleList;