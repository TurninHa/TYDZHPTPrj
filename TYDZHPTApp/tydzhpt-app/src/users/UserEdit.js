import React from "react";
import { Button, Space, Form, Input, Switch } from "antd";
import { getModel, saveUser } from '../Api/yhgl'

class UserEdit extends React.Component {

    constructor(props) {
        super(props);
    }
    form = React.createRef();

    saveHandle() {

        this.form.current.validateFields().then(values => {
            console.log(values);
            if (values.SYZT)
                values.SYZT = 1;
            else
                values = 0;

            values.SSYGID= 0;
            values.SFGLY= 0;

        }).catch(err => {
            console.log("验证没有通过");
            return;
        });

    }

    componentDidMount() {
        initPageData();
    }

    initPageData() {

    }

    render() {
        return (
            <div>
                <div>
                    <Form layout="horizontal" labelAlign="right" labelCol={{ span: 5 }} wrapperCol={{ span: 16 }} ref={this.form}>
                        <Form.Item label="用户名" name="YHM" rules={[{ required: true, max: 16, message: "用户名不能为空" }]}>
                            <Input placeholder="用户名"></Input>
                        </Form.Item>
                        <Form.Item label="姓名" name="XM" rules={[{ required: true, max: 30, message: "姓名不能为空" }]}>
                            <Input placeholder="姓名"></Input>
                        </Form.Item>
                        <Form.Item label="密码" name="MM" rules={[{ required: true, max: 16, message: "密码不能为空" }]}>
                            <Input placeholder="密码" type="password" ></Input>
                        </Form.Item>
                        <Form.Item label="确认密码" name="QRMM" rules={[{ required: true, max: 16, message: "确认密码不能为空" }]}>
                            <Input placeholder="确认密码" type="password"></Input>
                        </Form.Item>
                        <Form.Item label="手机号" name="SJH" rules={[{ required: true, max: 11, message: "手机号不能为空" }]}>
                            <Input placeholder="手机号码" type="password"></Input>
                        </Form.Item>
                        <Form.Item label="使用状态" name="SYZT" initialValue={false}>
                            <Switch checkedChildren="开启" unCheckedChildren="停用" ></Switch>
                        </Form.Item>
                    </Form>
                </div>
                <div style={{ textAlign: "center" }}>
                    <Space>
                        <Button type="primary" htmlType="button" onClick={() => this.saveHandle()}>保存</Button>
                        <Button htmlType="button" onClick={() => this.props.saveEffect(false)}>取消</Button>
                    </Space>
                </div>
            </div>
        );
    }
}

export default UserEdit;