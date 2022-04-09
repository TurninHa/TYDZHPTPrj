import { useEffect, useState } from "react"
import { Form, Input, Button, Switch, message, Space } from "antd";
import { getModel, saveRole } from "../Api/jsgl"

export const RoleEdit = (props) => {
    const [form] = Form.useForm();
    const [isOpenSwtich, SetSwitchOpen] = useState(false);

    const roleId = props.roleId;
    const onCloseForm = props.onClose;

    useEffect(() => {
        showPageDataForEdit();
    }, []);

    const showPageDataForEdit = () => {

        if (!roleId)
            return;

        getModel(roleId).then(resp => {
            form.setFieldsValue(resp.data.Data);

            if (resp.data.Data && resp.data.Data.SYZT === 1)
                SetSwitchOpen(true);
            else
                SetSwitchOpen(false);

        }).catch(er => {
            message.error(er);
            console.error(er);
        });
    };

    const saveRoleData = () => {
        let errData = form.getFieldsError();
        let errs = errData.filter(f => f.errors.length > 0);

        console.log({ errData });

        if (errs && errs.length > 0) {
            message.warn(errs[0]);
            return;
        }

        let data = form.getFieldsValue();

        console.log({ data });

        if (roleId && roleId > 0)
            data.ID = roleId;

        if (data.SYZT === true)
            data.SYZT = 1;
        else if (data.SYZT === false)
            data.SYZT = 0;

        if (typeof (data.SYZT) == "undefined")
            data.SYZT = 0;

        saveRole(data).then(resp => {
            if (resp.data.Code == 0) {
                message.success("保存成功");
                if (typeof (onCloseForm) == "function")
                    onCloseForm(true);
            }
            else {
                message.warn(resp.data.Message);
            }
        }).catch(er => {
            message.error(er);
            console.error(er);
        });
    };

    const SwitchChangeHandle = chk => {

        SetSwitchOpen(chk);
    }

    return (

        <Form form={form} name="rolesaveform">
            <Form.Item label="角色编码" name="JSBM" required rules={[{ max: 15 }]}>
                <Input disabled={roleId ? true : false}></Input>
            </Form.Item>
            <Form.Item label="角色名称" name="JSMC" required rules={[{ max: 50 }]}>
                <Input></Input>
            </Form.Item>
            <Form.Item label="使用状态" name="SYZT">
                <Switch checkedChildren="开启" unCheckedChildren="禁用" onChange={SwitchChangeHandle} checked={isOpenSwtich} ></Switch>
            </Form.Item>
            <Form.Item>
                <div style={{ marginLeft: "auto", marginRight: "auto", textAlign: "center" }}>
                    <Space>
                        <Button htmlType="button" onClick={saveRoleData} type="primary">保存</Button>
                        <Button onClick={() => {
                            if (typeof (onCloseForm) == "function")
                                onCloseForm(false);
                        }}>取消</Button>
                    </Space>
                </div>
            </Form.Item>
        </Form>

    );
}