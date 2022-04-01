import { Form, Input, Button } from "antd";

const RoleEdit = (props) => {
    const form = Form.useForm();
    return (
        <>
            <Form form={form}>
                <Form.Item label="角色编码" name="JSBM" required rules={[{ len: 10 }]}>
                    <Input></Input>
                </Form.Item>
                <Form.Item label="角色名称" name="JSMC" required rules={[{ len: 50 }]}>
                    <Input></Input>
                </Form.Item>
                <Form.Item label="使用状态" name="SYZT">
                    
                </Form.Item>
            </Form>
        </>
    );
}

export default RoleEdit;