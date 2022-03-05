import { useEffect, useState } from "react";
import { Table, Space, message, Form, Input, InputNumber } from "antd";
import { getOperateFuncList } from "../../Api/cdglApi";

const MenuButtons = (props) => {
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
                    <a onClick={ ()=> saveHandle() }>保存</a>
                    <a onClick={()=> setEditKey("") }>取消</a>
                </Space> :
                    <Space>
                        <a onClick={() => { editHandle(record); }}>编辑</a>
                        <a>删除</a>
                    </Space>
            );
        }
    }];

    const saveHandle = async ()=>{

        let submitData = await form.validateFields();
        console.log({submitData});

        
    };

    const newColumns = columns.map(cur => {
        if (!cur.editable)
            return cur;

        return {
            ...cur,
            onCell: record => (
                {
                    record,
                    dataIndex: cur.dataIndex,
                    inputType: cur.inputType,
                    title: cur.title,
                    editing: isEditing(record.key)
                }
            )
        }
    });

    useEffect(() => {
        getOperateFuncList(cdId).then(resp => {
            if (resp.data.Code === 0)
                setDataSource(resp.data.Data);
            else
                message.warn(resp.data.Message);
        }).catch(er => {
            console.error(er);
        });
    }, [cdId]);

    return (
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
    );
}

export default MenuButtons;

const TableCellEdtor = (
    editing,
    record,
    dataIndex,
    inputType,
    title,
    children,
    ...restProps
) => {
    console.log({ restProps }, { children }, record);
    const InputNode = inputType === "text" ? <Input /> : <InputNumber />;
    return (
        <td {...restProps}>
            {editing ? (<Form.Item name={dataIndex} style={{ margin: 0 }} rules={[{required:true,message: `${title}不能为空`}]}>
                {InputNode}
            </Form.Item>) : (children)}
        </td>
    );
}