import React from "react";
import { Input, Form, Button, Table } from "antd";

class UserList extends React.Component {

    state = {
        dataSource: [],
        pagination: {
            pageIndex: 1,
            pageSize: 20,
            total: 0
        }
    };

    form = React.createRef();

    render() {
        <div className="list-page-container">
            <div className="search-container">
                <div className="search-tool-container">
                    <Form ref={this.form}>
                        <div className="search-tool-item">
                            <div className="search-tool-item-content">
                                <div className="search-tool-item-content-left">
                                    <label>用户名:</label>
                                </div>
                                <div className="search-tool-item-content-right">
                                    <Form.Item name="YHM">
                                        <Input placeholder="用户名"></Input>
                                    </Form.Item>
                                </div>
                            </div>
                        </div>
                        <div className="search-tool-item">
                            <div className="search-tool-item-content">
                                <div className="search-tool-item-content-left">
                                    <label>姓名:</label>
                                </div>
                                <div className="search-tool-item-content-right">
                                    <Form.Item name="XM">
                                        <Input name="XM" placeholder="姓名"></Input>
                                    </Form.Item>
                                </div>
                            </div>
                        </div>
                        <div className="search-tool-item">
                            <div className="search-tool-item-content">
                                <div className="search-tool-item-content-left">
                                    <label>使用状态:</label>
                                </div>
                                <div className="search-tool-item-content-right">
                                    <Form.Item name="SYZT">
                                        <Select defaultValue="1"
                                            placeholder="请选择"
                                            style={{ width: "120px" }}
                                        >
                                            <Option value="">-请选择-</Option>
                                            <Option value="1">启用</Option>
                                            <Option value="0">禁用</Option>
                                        </Select>
                                    </Form.Item>
                                </div>
                            </div>
                        </div>
                        <div className="search-tool-item">
                            <div className="search-tool-item-content">
                                <div className="search-tool-item-content-left">
                                    <label>所属客户:</label>
                                </div>
                                <div className="search-tool-item-content-right">
                                    <Form.Item name="SSGSID">
                                        <AutoComplete
                                            key="autocompl_sskh"
                                            style={{ width: "120px" }}
                                            options={[{ value: "本公司", id: 0 }]}
                                            placeholder="请选择单位"
                                        >
                                        </AutoComplete>
                                    </Form.Item>
                                </div>
                            </div>
                        </div>
                    </Form>
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
                        <Table columns={columns} dataSource={this.state.dataSource}
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