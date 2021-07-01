import React from 'react';
import { Input, Button,Checkbox } from 'antd';
import { UserOutlined, LockOutlined } from "@ant-design/icons"
import "./Css/login.css"

class Login extends React.Component {
    constructor(props) {
        super(props);
        this.txtUserName = React.createRef();
        this.txtPassword = React.createRef();
    }
    render() {
        return (
            <>
                <header>统一多租户平台</header>
                <div className="container-middle">
                    <div className="content-container">
                        <div className="content-container-left">
                            <div className="content-content">
                                <div className="content-content-text">
                                GitLab 中文社区版<br/>
用于代码协作的开源软件
细粒度访问控制管理 git 仓库以保证代码安全。 使用合并请求进行代码审查并加强团体合作。 每个项目均有自己的问题跟踪和维基页面。
                                </div>
                            </div>
                        </div>
                        <div className="content-container-right">
                            <div className="content-login">
                                <div className="content-login-header">登录</div>
                                <div className="content-login-Input">
                                    用户名
                                    <Input ref={this.txtUserName} size="large" prefix={<UserOutlined />}></Input>
                                </div>
                                <div className="content-login-Input">
                                    密码
                                    <Input ref={this.txtPassword} size="large" prefix={<LockOutlined />}></Input>
                                </div>
                                <div className="content-login-Input">
                                    <div className="content-login-Input-remb">
                                        <Checkbox>记住我</Checkbox>
                                    </div>
                                    <div className="content-login-Input-fpwd">
                                        忘记密码
                                    </div>
                                </div>
                                <div className="content-login-Input-button">
                                    <Button type="primary" size="large" block>登录</Button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <footer>版权所有</footer>
            </>);
    }
}

export default Login;