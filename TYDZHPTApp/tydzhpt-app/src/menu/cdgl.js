import React from "react";
import { Table, Space } from "antd";

class MenuMng extends React.Component {

    render() {
        return (
            <div style={{ width: "100%" }}>
                <div style={{ width: "100%" }}>
                    <div style={{ marginBottom: "16px", padding: "24px 24px 0px", background: "#fff" }}>
                        <div style={{ marginLeft: "-12px", marginRight: "-12px", display: "flex" }}>
                            <div style={{ flexBasis: "25%", flexGrow: "0", flexShrink: "0", maxWidth: "25%", paddingLeft: "12px", paddingRight: "12px", background: "gray" }}>
                                <div style={{margin:"0 0 24px"}}>
                                    <div style={{flex:"0 0 120px",overflow:"hidden"}}>菜单名称</div>
                                    <div></div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div style={{ background: "#fff", borderRadius: "2px" }}>
                        <div style={{ margin: "0 24px", background: "white" }}>
                            1<br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br /><br />
                            <br />
                            2
                            <br />
                            <br />
                            <br />4
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}

export default MenuMng;