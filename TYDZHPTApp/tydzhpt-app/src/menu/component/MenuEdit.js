import React from "react";
import { Form, TreeSelect, Input, Button, Space, message } from 'antd';
import { getModel, save, menuTree } from "../../Api/cdglApi";

class MenuEdit extends React.Component {

    state = { id: this.props.id, cdbmReadonly: true, treeValue: "", treeData: [] };
    formRef = React.createRef();

    formSubmit = data => {
        console.log(data);
        if (!data.CDBM || data.CDBM === "") {
            message.warn("请填写菜单编码");
        }
        if (!data.CDMC || data.CDMC === "") {
            message.warn("请填写菜单名称");
        }

        if (!data.FCDID || data.FCDID == "" || data.FCDID == undefined)
            data.FCDID = "0";

        save(data).then(resp => {
            if (resp.data.Code === 0)
                this.props.onSuccess(true);
            else
                message.warn(resp.data.Message);
        }).catch(er => {
            console.error(er);
        });
    }

    initMenuTreeSelect() {
        menuTree().then(response => {
            this.setState({ treeData: response.data.Data });
        }).catch(er => {
            console.error(er);
        });
    }
    treeChangeHandle(data) {
        console.log("选中的菜单ID " + data);
        this.setState({ treeValue: data });
    }

    componentDidMount() {
        this.initMenuTreeSelect();

        if (!this.state.id || this.state.id === -1) {
            this.setState({ cdbmReadonly: false });
            return;
        }
        getModel(this.state.id).then(resp => {
            console.log({ resp });
            this.formRef.current.setFieldsValue(resp.data.Data);
        }).catch(er => {
            console.log(er);
        });
    }

    render() {
        return <div style={{ margin: "20px 0" }}>
            <Form layout="horizontal" colon={false} onFinish={this.formSubmit} labelCol={{ span: 5 }} ref={this.formRef}>
                <Form.Item label="菜单编码" name="CDBM" required>
                    <Input placeholder="请输入菜单编码" readOnly={this.state.cdbmReadonly}></Input>
                </Form.Item>
                <Form.Item label="菜单名称" name="CDMC" required>
                    <Input placeholder="请输入菜单名称"></Input>
                </Form.Item>
                <Form.Item label="所属父菜单" name="FCDID">
                    <TreeSelect showSearch placeholder="请选择父菜单" allowClear value={this.state.treeValue} onChange={value => { this.treeChangeHandle(value) }} treeData={this.state.treeData}></TreeSelect>
                </Form.Item>
                <Form.Item label="菜单路径" name="CDLJ">
                    <Input placeholder="请输入菜单路径"></Input>
                </Form.Item>
                <Form.Item label="排序号" name="PXH">
                    <Input placeholder="请输入排序号" type="number"></Input>
                </Form.Item>
                <Form.Item style={{ textAlign: "center" }}>
                    <Space>
                        <Button type="primary" htmlType="submit">保存</Button>
                        <Button type="default" htmlType="button" onClick={() => { this.props.onClose() }}>关闭</Button>
                    </Space>
                </Form.Item>
            </Form>
        </div >;
    }
}

export default MenuEdit;