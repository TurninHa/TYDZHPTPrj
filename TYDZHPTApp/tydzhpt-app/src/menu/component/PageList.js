import React from "react";
import { Table, Button, Space } from "antd";

class PageListPart extends React.Component {
    constructor(props) {
        super(props);
        this.state = { dataSource: [] };
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
                return
                <Space>
                    <a> 编辑</a>
                    <a>删除</a>
                </Space>
            }
        },
    ];

    componentDidMount() {
        let condition = this.props.condition;

        this.setState({ dataSource: [] });
    }

    render() {
        return (
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
                    <Table columns={this.columns} dataSource={this.state.dataSource}></Table>
                </div>
            </div>
        );
    }
}

export default PageListPart;