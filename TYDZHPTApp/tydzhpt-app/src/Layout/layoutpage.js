import React from "react";
import { Layout, Menu } from "antd";

class LayoutContainer extends React.Component {

    render() {
        const { SubMenu } = Menu;
        const { Header, Footer, Sider, Content } = Layout;

        return <>
            <Layout>
                <Header>

                </Header>
                <Layout>
                    <Sider>

                    </Sider>
                    <Content></Content>
                </Layout>
                <Footer></Footer>
            </Layout>
        </>;
    }
}

export default LayoutContainer;