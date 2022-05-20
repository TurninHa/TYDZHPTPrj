import React from "react";
import { Button, Space, Form, Input,Switch } from "antd";

class UserEdit extends React.Component {
    render() {
        return (
            <div>
                <div>
                    <Form layout="horizontal" labelAlign="right" labelCol={{ span:5 }} wrapperCol={{ span:16}}>
                        <Form.Item label="用户名">
                            <Input placeholder="用户名"></Input>
                        </Form.Item>
                        <Form.Item label="姓名">
                            <Input placeholder="姓名"></Input>
                        </Form.Item>
                        <Form.Item label="密码">
                            <Input placeholder="密码" type="password" ></Input>
                        </Form.Item>
                        <Form.Item label="确认密码">
                            <Input placeholder="确认密码" type="password"></Input>
                        </Form.Item>
                        <Form.Item label="手机号">
                            <Input placeholder="手机号码" type="password"></Input>
                        </Form.Item>
                        <Form.Item label="使用状态">
                            <Switch checkedChildren="开启" unCheckedChildren="停用" ></Switch>
                        </Form.Item>
                    </Form>
                </div>
                <div style={{ textAlign: "center" }}>
                    <Space>
                        <Button type="primary" htmlType="button">保存</Button>
                        <Button htmlType="button">取消</Button>
                    </Space>
                </div>
            </div>
        );
    }
}

export default UserEdit;