import { useEffect, useState } from "react";
import { Table, Space, message, Form, Input, InputNumber, Button, Row, Col } from "antd";
import { getOperateFuncList, saveCzGn, deleteCzgn } from "../../Api/cdglApi";

export const MenuButtons = (props) => {
    const cdId = props.Id;
    const [dataSource, setDataSource] = useState([]);
    const [form] = Form.useForm();

    const [editKey, setEditKey] = useState("");

    const editHandle = record => {
        form.setFieldsValue({
            GNBM: "",
            GNMC: "",
            PXH: "",
            ...record
        });
        setEditKey(record.key);
    };

    const isEditing = key => editKey === key;

    const columns = [{
        title: "序号",
        dataIndex: "No",
        key: "cNo",
    }, {
        title: "功能编码",
        dataIndex: "GNBM",
        key: "cGNBM",
        editable: true,
        inputType: "text"
    }, {
        title: "功能名称",
        dataIndex: "GNMC",
        key: "cGNMC",
        editable: true,
        inputType: "text"
    }, {
        title: "序号",
        dataIndex: "PXH",
        key: "cPXH",
        editable: true,
        inputType: "number"
    }, {
        title: "操作",
        dataIndex: "ID",
        key: "cID",
        render: (text, record) => {
            console.log(text, record);
            let editing = isEditing(record.key);
            return (
                editing ? <Space>
                    <a onClick={() => saveHandle()}>保存</a>
                    <a onClick={() => setEditKey("")}>取消</a>
                </Space> :
                    <Space>
                        <a onClick={() => { editHandle(record); }}>编辑</a>
                        <a onClick={() => { deleteHandle(record.ID) }}>删除</a>
                    </Space>
            );
        }
    }];

    const saveHandle = async () => {

        let submitData = await form.validateFields();
        console.log({ submitData });

        let isExit = dataSource.findIndex(f => f.GNBM == submitData.GNBM);

        if (isExit >= 0)
            submitData.ID = dataSource[isExit].ID;

        submitData.CDID = cdId;

        saveCzGn(submitData).then(resp => {
            message.success("保存成功");
            setEditKey("");
        }).then(er => {
            console.error(er);
            message.error("保存失败");
        });
    };

    const deleteHandle = (id = 0) => {
        if (id <= 0) {
            message.warn("请选择要删除的数据");
            return;
        }

        deleteCzgn(id).then(resp => {
            if (resp.data.Code == 0) {
                message.success("删除成功", () => {
                    loadDataList();
                });
            }
        }).catch(er => {
            message.error("请求出现错误");
            console.error(er);
        });

    };

    const newColumns = columns.map(cur => {
        if (!cur.editable)
            return cur;

        return {
            ...cur,
            onCell: record => (
                {
                    editing: isEditing(record.key),
                    record,
                    dataIndex: cur.dataIndex,
                    inputType: cur.inputType,
                    title: cur.title,
                }
            )
        }
    });

    useEffect(() => {
        loadDataList();
    }, [cdId]);

    const loadDataList = () => {
        console.log({ cdId });
        getOperateFuncList(cdId).then(resp => {
            if (resp.data.Code === 0)
                setDataSource(resp.data.Data);
            else
                message.warn(resp.data.Message);
        }).catch(er => {
            console.error(er);
        });
    }

    return (
        <>
            <div style={{ marginBottom: "10px", textAlign: "right", paddingRight: "10px" }}>
                <Space>
                    <Button type="primary">新增</Button>
                </Space>
            </div>
            <div>
                <Form form={form}>
                    <Table
                        columns={newColumns}
                        dataSource={dataSource}
                        components={
                            {
                                body: {
                                    cell: TableCellEdtor
                                }
                            }
                        }
                        size="small"
                        pagination={false}
                    ></Table>
                </Form>
            </div>
        </>
    );
}

const TableCellEdtor = ({
    editing,
    record,
    dataIndex,
    inputType,
    title,
    children,
    ...restProps
}) => {

    //console.log({ restProps }, { children }, record);
    console.log(editing);
    const InputNode = inputType === "text" ? <Input /> : <InputNumber />;
    return (
        <td {...restProps}>
            {editing ? (<Form.Item name={dataIndex} style={{ margin: 0 }} rules={[{ required: true, message: `${title}不能为空` }]}>
                {InputNode}
            </Form.Item>) : (children)}
        </td>
    );
}