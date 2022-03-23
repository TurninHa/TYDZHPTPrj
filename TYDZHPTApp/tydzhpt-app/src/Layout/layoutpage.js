import React from "react";
import { Layout, Menu } from "antd";
import "antd/dist/antd.css";
import "../Css/login.css";
import { Link } from "react-router-dom";

class LayoutContainer extends React.PureComponent {

    componentDidUpdate() {

    }
    componentDidMount() {
        //验证是否已登录
    }

    render() {
        const { SubMenu } = Menu;
        const { Header, Footer, Sider, Content } = Layout;

        return <>
            <Layout>
                <Header>
                    <div style={{ color: "white", textAlign: "left" }}>
                        统一多租户平台
                    </div>
                </Header>
                <Layout>
                    <Sider style={{ backgroundColor: "white" }}>
                        <Menu mode="inline">
                            <SubMenu key="sub1" title="权限管理">
                                <Menu.Item key="menu1">
                                    用户管理
                                </Menu.Item>
                                <Menu.Item key="menu2">
                                    <Link to="/layout/jsgl" key="link_jsgl">角色管理</Link>
                                </Menu.Item>
                                <Menu.Item key="menu3">
                                    <Link to="/layout/cdgl" key="link_cdgl" replace>菜单管理</Link>
                                </Menu.Item>
                            </SubMenu>
                            <SubMenu key="sub2" title="字典管理">
                                <Menu.Item key="menu5">用户管理</Menu.Item>
                                <Menu.Item key="menu6">角色管理</Menu.Item>
                                <Menu.Item key="menu7">菜单管理</Menu.Item>
                            </SubMenu>
                        </Menu>
                    </Sider>
                    <Content style={{ position: "relative", margin: "24px" }}>
                        {this.props.children}
                    </Content>
                </Layout>
                <Footer style={{ borderTop: "solid 0px #f0f0f0", padding: "0px 50px", backgroundColor: "white", height: "22px", lineHeight: "22px" }}>
                    <div style={{ textAlign: "center" }}>版权所有</div>
                </Footer>
            </Layout>
        </>;
    }
}

export default LayoutContainer;