import { Input, Button, Table, Select, AutoComplete, Modal, Space, message } from "antd";
import React, { useState, useEffect, useRef } from "react";
import { getRoleList, deleteRole, disEnRole } from "../Api/jsgl";
import '../index.css';
import "../Css/glb.css";
import { RoleEdit } from "./RoleEdit"

const RoleList = (props) => {

    const [dataSource, setDataSource] = useState([]);
    const [pagination, setPagination] = useState({
        pageIndex: 1,
        pageSize: 20,
        total: 0
    });

    const [roleModalVisb, setRoleModalVisb] = useState(false);
    const [roleId, setRoleId] = useState(0);

    const { Option } = Select;

    const jsbm = useRef("");
    const jsmc = useRef("");
    let syzt = 1;
    let ssgId = -1;

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
                return "本公司角色";
            else
                return "-";
        }
    },
    {
        title: "操作",
        dataIndex: "ID",
        key: "col__role_ID_CZ",
        render: (text, rec) => {
            if (rec.SSGSID !== 0) {
                return (
                    <Space>
                        <a onClick={() => {
                            disableOrEnabledRole({ Id: rec.ID, RoleStatue: 0 });
                        }}>禁用</a>
                        <a onClick={() => {
                            disableOrEnabledRole({ Id: rec.ID, RoleStatue: 1 });
                        }}>启用</a>
                    </Space>
                );
            }
            else {
                return (
                    <Space>
                        <a onClick={() => {
                            setRoleModalVisb(true);
                            setRoleId(rec.ID);
                        }}>编辑</a>
                        <a onClick={() => {
                            deleteData(rec.ID);
                        }}>删除</a>
                    </Space>
                );
            }
        }
    }];

    useEffect(() => {
        loadData();
    }, []);

    const loadData = () => {

        console.log({ jsbm });

        let reqData = {
            pageIndex: pagination.pageIndex,
            pageSize: pagination.pageSize,
            JSBM: jsbm.current.input.value,
            JSMC: jsmc.current.input.value,
            SYZT: syzt,
            SSGSID: ssgId
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

    const { confirm } = Modal;

    const deleteData = (id = 0) => {

        if (id <= 0) {
            message.error("参数错误");
            return;
        }

        confirm({
            title: "提示",
            okText: "确认",
            cancelText: "取消",
            centered: true,
            onOk: () => {
                deleteRole(id).then(resp => {
                    if (resp && resp.data.Code === 0) {
                        message.success("删除成功");
                        loadData();
                    }
                    else {
                        message.warn(resp.data.Message);
                    }
                }).catch(er => {
                    console.error(er);
                    message.error(er);
                });
            },
            content: "确认删除吗？"
        });
    };

    const disableOrEnabledRole = (data = {}) => {
        confirm({
            title: "提示",
            okText: "确认",
            cancelText: "取消",
            centered: true,
            onOk: () => {

                disEnRole(data).then(resp => {
                    if (resp.data.Code === 0) {
                        loadData();
                    }
                    else {
                        message.warn(resp.data.Message);
                    }
                }).catch(er => {
                    console.error(er);
                });
            },
            content: "确认" + (data.RoleStatue === 0 ? "禁用" : "启用") + "吗？"
        });

    };

    const syztHandle = (value, options) => {
        console.log(value, { options });
        if (value == "" || typeof value == "undefined")
            syzt = -1;
        syzt = value;
    };

    const sskhHandle = (value, option) => {
        ssgId = option.id;
    };

    const searchReset = () => {
        
        jsbm.current.input.value = "";
        jsbm.current.state.value = "";
        jsmc.current.input.value = "";
        jsmc.current.state.value = "";
        message.info("重置功能计划屏蔽");
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
                                    <Input name="JSMC" ref={jsmc} placeholder="角色名称"></Input>
                                </div>
                            </div>
                        </div>
                        <div className="search-tool-item">
                            <div className="search-tool-item-content">
                                <div className="search-tool-item-content-left">
                                    <label>角色编码:</label>
                                </div>
                                <div className="search-tool-item-content-right">
                                    <Input name="JSBM" ref={jsbm} placeholder="角色编码"></Input>
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
                                        onChange={syztHandle}
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
                                        onSelect={sskhHandle}
                                    >
                                    </AutoComplete>
                                </div>
                            </div>
                        </div>
                        <div className="search-tool-item-action">
                            <div className="search-tool-item-action-content">
                                <div>
                                    <Button onClick={() => { searchReset(); }}>重置</Button>
                                </div>
                                <div>
                                    <Button type="primary" onClick={() => { loadData(); }}>查询</Button>
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
                                        <Button type="primary" onClick={() => { setRoleId(0); setRoleModalVisb(true); }}>添加角色</Button>
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
            >
                <RoleEdit roleId={roleId} onClose={(isRefList = false) => {
                    if (isRefList)
                        loadData();
                    setRoleModalVisb(false);
                }}></RoleEdit>
            </Modal>
        </>
    );
}

export default RoleList;