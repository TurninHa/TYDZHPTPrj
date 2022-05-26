import React from "react";
import { Button, Space, Form, Input, Switch, message, Checkbox } from "antd";
import { getModel, saveUser } from '../Api/yhgl'
import { getAllJs } from "../Api/jsgl";

class UserEdit extends React.Component {

    constructor(props) {
        super(props);
    }

    state = {
        roleDataSource: [],
    };

    form = React.createRef();

    saveHandle() {

        this.form.current.validateFields().then(values => {
            console.log({ values });
            if (values.SYZT)
                values.SYZT = 1;
            else
                values.SYZT = 0;

            values.SSYGID = 0;
            values.SFGLY = 0;

            //编辑
            if (this.props.id && this.props.id > 0) {
                values.ID = this.props.id;
            }
            else {
                if (values.MM !== values.QRMM) {
                    message.warn("两次密码输入不一致");
                    return;
                }
            }

            saveUser(values).catch(er => {
                message.error(er);

            }).then(resp => {
                if (resp.data.Code === 0) {
                    message.success("保存成功");
                    this.props.saveEffect(true);
                }
                else
                    message.warn(resp.data.Message);
            });

        }).catch(err => {
            console.log(err);
            console.log("验证没有通过");
            return;
        });

    }

    componentDidMount() {
        this.showRoleCheckList();
        this.initPageData();
    }

    initPageData() {

        console.log(this.props.id);

        if (this.props.id && this.props.id > 0) {

            getModel(this.props.id).then(resp => {
                if (resp.data.Code === 0) {

                    if (resp.data.Data.Roles && resp.data.Data.Roles.length > 0) {
                        resp.data.Data.Roles = resp.data.Data.Roles.map(item => item.ID);
                    }

                    this.form.current.setFieldsValue(resp.data.Data);
                }
                else
                    message.warn(resp.data.Message);
            }).catch(er => {
                message.error(er);
            });
        }
    }

    showRoleCheckList() {
        getAllJs().then(resp => {
            if (resp.data.Code === 0) {
                if (resp.data.Data.length > 0) {
                    let ds = [];
                    resp.data.Data.forEach(item => {
                        ds.push({
                            label: item.JSMC,
                            value: item.ID
                        });
                    });

                    this.setState({ roleDataSource: ds });
                }
            }
            else {
                message.warn(resp.data.Message);
            }
        }).catch(er => {
            console.error(er);
        });
    }

    render() {
        return (
            <>
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
                        {
                            !(this.props.id && this.props.id > 0) ? (
                                <Form.Item label="确认密码" name="QRMM" rules={[{ required: true, max: 16, message: "确认密码不能为空" }]}>
                                    <Input placeholder="确认密码" type="password"></Input>
                                </Form.Item>
                            ) : (<></>)
                        }
                        <Form.Item label="手机号" name="SJH" rules={[{ required: true, max: 11, message: "手机号不能为空,最长11位" }]}>
                            <Input placeholder="手机号码" type="number" max="11" maxLength="11"></Input>
                        </Form.Item>
                        <Form.Item label="使用状态" name="SYZT" initialValue={false} valuePropName="checked">
                            <Switch checkedChildren="开启" unCheckedChildren="停用" ></Switch>
                        </Form.Item>
                        <Form.Item name="Roles" label="所属角色">
                            <Checkbox.Group options={this.state.roleDataSource}>
                            </Checkbox.Group>
                        </Form.Item>
                    </Form>
                </div>
                <div style={{ textAlign: "center" }}>
                    <Space>
                        <Button type="primary" htmlType="button" onClick={() => this.saveHandle()}>保存</Button>
                        <Button htmlType="button" onClick={() => this.props.saveEffect(false)}>取消</Button>
                    </Space>
                </div>
            </>
        );
    }
}

export default UserEdit;